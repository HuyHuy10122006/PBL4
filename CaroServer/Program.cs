using CaroProtocol;
using CaroServer.Database;
using System.Net;
using System.Net.Sockets;

namespace CaroServer
{
    internal class Program
    {
        private static readonly RoomManager _roomManager = new RoomManager();
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, ClientSession> _sessions = new System.Collections.Concurrent.ConcurrentDictionary<string, ClientSession>();
        private static readonly int Port = 8888;

        static async Task Main(string[] args)
        {
            Console.Title = "Caro Server - Trọng tài & Quản lý Trận đấu TCP";
            Console.WriteLine("=================================================");
            Console.WriteLine("    CARO GAME SERVER - SYSTEM STARTING...       ");
            Console.WriteLine("=================================================");

            // Khởi tạo kết nối SQL Server và tự động tạo Database/Bảng
            await DatabaseManager.InitializeAsync();

            _roomManager.OnRoomStateChanged = async () =>
            {
                await BroadcastPlayerListAsync();
                await BroadcastRoomListAsync();
            };

            TcpListener listener = new TcpListener(IPAddress.Any, Port);
            try
            {
                listener.Start();
                Console.WriteLine($"[INFO] Server đang lắng nghe cổng: {Port}...");
                Console.WriteLine("[INFO] Sẵn sàng nhận kết nối từ các Client...\n");

                while (true)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    ClientSession session = new ClientSession(tcpClient);
                    _sessions[session.PlayerId] = session;
                    IPEndPoint? remoteEp = tcpClient.Client.RemoteEndPoint as IPEndPoint;
                    Console.WriteLine($"[CONNECT] Client mới kết nối từ {remoteEp?.Address}:{remoteEp?.Port} (ID: {session.PlayerId})");

                    _ = session.StartListenLoopAsync(HandlePacketAsync, OnClientDisconnected);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CRITICAL] Server gặp lỗi nghiêm trọng: {ex.Message}");
            }
            finally
            {
                listener.Stop();
            }
        }

        private static async Task HandlePacketAsync(ClientSession session, Packet packet)
        {
            switch (packet.Type)
            {
                case PacketType.LoginReq:
                    var loginReq = packet.DeserializePayload<LoginReqData>();
                    if (loginReq != null)
                    {
                        var (success, msg, user) = await DatabaseManager.LoginAsync(loginReq.Username, loginReq.Password);
                        if (success && user != null)
                        {
                            session.PlayerName = user.Username;
                            session.Elo = user.Elo;
                            session.Wins = user.Wins;
                            session.Losses = user.Losses;
                            session.Draws = user.Draws;
                            session.IsAuthenticated = true;
                            _sessions[session.PlayerId] = session;

                            Console.WriteLine($"[AUTH-LOGIN] '{session.PlayerName}' đăng nhập thành công. ELO: {session.Elo} (W:{session.Wins} L:{session.Losses} D:{session.Draws})");

                            var loginResp = Packet.Create(PacketType.LoginResp, new LoginRespData
                            {
                                Success = true,
                                Message = msg,
                                PlayerId = session.PlayerId,
                                Username = user.Username,
                                Elo = user.Elo,
                                Wins = user.Wins,
                                Losses = user.Losses,
                                Draws = user.Draws
                            });
                            await session.SendPacketAsync(loginResp);

                            // Kiểm tra xem người chơi này có ván đấu nào đang chờ kết nối lại (30s grace period) không
                            var pendingRoom = _roomManager.FindRoomWaitingForPlayer(user.Username);
                            if (pendingRoom != null)
                            {
                                Console.WriteLine($"[RECONNECT] Người chơi '{user.Username}' đang kết nối lại ván đấu tại phòng {pendingRoom.RoomId}...");
                                await pendingRoom.ReconnectPlayerAsync(session);
                            }

                            _ = BroadcastPlayerListAsync();
                            _ = BroadcastRoomListAsync();
                        }
                        else
                        {
                            Console.WriteLine($"[AUTH-LOGIN-FAIL] '{loginReq.Username}' thất bại: {msg}");
                            var loginResp = Packet.Create(PacketType.LoginResp, new LoginRespData
                            {
                                Success = false,
                                Message = msg
                            });
                            await session.SendPacketAsync(loginResp);
                        }
                    }
                    break;

                case PacketType.RegisterReq:
                    var regReq = packet.DeserializePayload<RegisterReqData>();
                    if (regReq != null)
                    {
                        var (success, msg) = await DatabaseManager.RegisterAsync(regReq.Username, regReq.Password);
                        Console.WriteLine($"[AUTH-REGISTER] '{regReq.Username}': {(success ? "Thành công" : "Thất bại - " + msg)}");
                        var regResp = Packet.Create(PacketType.RegisterResp, new RegisterRespData
                        {
                            Success = success,
                            Message = msg
                        });
                        await session.SendPacketAsync(regResp);
                    }
                    break;

                case PacketType.GetHistoryReq:
                    var historyList = await DatabaseManager.GetUserMatchHistoryAsync(session.PlayerName);
                    var historyPacket = Packet.Create(PacketType.GetHistoryResp, new MatchHistoryRespData { History = historyList });
                    await session.SendPacketAsync(historyPacket);
                    break;

                case PacketType.GetLeaderboardReq:
                    var topList = await DatabaseManager.GetLeaderboardAsync(25);
                    var lbPacket = Packet.Create(PacketType.GetLeaderboardResp, new LeaderboardRespData { TopPlayers = topList });
                    await session.SendPacketAsync(lbPacket);
                    break;

                case PacketType.ConnectReq:
                    var connReq = packet.DeserializePayload<ConnectReqData>();
                    if (connReq != null)
                    {
                        session.PlayerName = string.IsNullOrWhiteSpace(connReq.PlayerName) ? $"Player_{session.PlayerId}" : connReq.PlayerName;
                        _sessions[session.PlayerId] = session;
                        Console.WriteLine($"[LEGACY-CONNECT] Client {session.PlayerId} đặt tên là: '{session.PlayerName}'");

                        var resp = Packet.Create(PacketType.ConnectResp, new ConnectRespData
                        {
                            Success = true,
                            PlayerId = session.PlayerId,
                            ServerHostName = Environment.MachineName,
                            Message = $"Chào mừng {session.PlayerName} kết nối đến Server Caro!",
                            Elo = session.Elo,
                            Wins = session.Wins,
                            Losses = session.Losses,
                            Draws = session.Draws
                        });
                        await session.SendPacketAsync(resp);

                        var pendingRoom = _roomManager.FindRoomWaitingForPlayer(session.PlayerName);
                        if (pendingRoom != null)
                        {
                            Console.WriteLine($"[RECONNECT] Người chơi '{session.PlayerName}' đang kết nối lại ván đấu tại phòng {pendingRoom.RoomId}...");
                            await pendingRoom.ReconnectPlayerAsync(session);
                        }

                        _ = BroadcastPlayerListAsync();
                        _ = BroadcastRoomListAsync();
                    }
                    break;

                case PacketType.CreateRoomReq:
                    var createReq = packet.DeserializePayload<CreateRoomReqData>();
                    if (createReq != null)
                    {
                        Console.WriteLine($"[ROOM] {session.PlayerName} yêu cầu tạo phòng. Chế độ: {createReq.Mode}, Luật: {(createReq.BlockBothEndsRule ? "Chặn 2 đầu" : "Tự do")}, Thời gian: {createReq.TimePerTurn}s");
                        await _roomManager.CreateRoomAsync(session, createReq.Mode, createReq.AIDifficulty, createReq.BoardSize, createReq.BlockBothEndsRule, createReq.TimePerTurn);
                    }
                    break;

                case PacketType.JoinRoomReq:
                    var joinReq = packet.DeserializePayload<JoinRoomReqData>();
                    if (joinReq != null)
                    {
                        Console.WriteLine($"[ROOM] {session.PlayerName} yêu cầu vào phòng '{joinReq.RoomId}'");
                        await _roomManager.JoinRoomAsync(session, joinReq.RoomId);
                    }
                    break;

                case PacketType.MoveReq:
                    var moveReq = packet.DeserializePayload<MoveReqData>();
                    if (moveReq != null && session.CurrentRoom != null)
                    {
                        Console.WriteLine($"[MOVE] {session.PlayerName} đánh ô ({moveReq.Row}, {moveReq.Col}) trong phòng {session.CurrentRoom.RoomId}");
                        await session.CurrentRoom.ProcessMoveAsync(session, moveReq.Row, moveReq.Col);
                    }
                    break;

                case PacketType.SurrenderReq:
                    if (session.CurrentRoom != null)
                    {
                        Console.WriteLine($"[SURRENDER] {session.PlayerName} đầu hàng trong phòng {session.CurrentRoom.RoomId}");
                        await session.CurrentRoom.HandleSurrenderAsync(session);
                    }
                    break;

                case PacketType.RestartReq:
                    if (session.CurrentRoom != null)
                    {
                        Console.WriteLine($"[RESTART] {session.PlayerName} yêu cầu chơi lại trong phòng {session.CurrentRoom.RoomId}");
                        await session.CurrentRoom.RestartAsync(session);
                    }
                    break;

                case PacketType.ChatMessage:
                    var chatReq = packet.DeserializePayload<ChatMessageData>();
                    if (chatReq != null && session.CurrentRoom != null)
                    {
                        await session.CurrentRoom.BroadcastChatMessageAsync(session, chatReq.Message);
                    }
                    break;

                case PacketType.LeaveRoomReq:
                    Console.WriteLine($"[ROOM] {session.PlayerName} yêu cầu thoát phòng");
                    await _roomManager.LeaveRoomAsync(session);
                    break;

                case PacketType.GetRoomsReq:
                    var roomList = _roomManager.GetActiveRooms();
                    var listPacket = Packet.Create(PacketType.RoomList, new RoomListData { Rooms = roomList });
                    await session.SendPacketAsync(listPacket);
                    break;

                case PacketType.SpectateRoomReq:
                    var specReq = packet.DeserializePayload<SpectateRoomReqData>();
                    if (specReq != null)
                    {
                        Console.WriteLine($"[SPECTATE] {session.PlayerName} yêu cầu xem phòng '{specReq.RoomId}'");
                        await _roomManager.SpectateRoomAsync(session, specReq.RoomId);
                    }
                    break;
            }
        }

        public static async Task RecordMatchResultAsync(ClientSession p1, ClientSession p2, string? winnerUsername, double score1, int totalMoves)
        {
            var (newElo1, newElo2, change1, change2) = await DatabaseManager.RecordMatchAsync(
                p1.PlayerName, p2.PlayerName, winnerUsername, score1, totalMoves);

            p1.Elo = newElo1;
            p2.Elo = newElo2;
            if (score1 == 1.0) { p1.Wins++; p2.Losses++; }
            else if (score1 == 0.0) { p1.Losses++; p2.Wins++; }
            else { p1.Draws++; p2.Draws++; }

            Console.WriteLine($"[ELO-UPDATE] {p1.PlayerName}: {p1.Elo} ({(change1 >= 0 ? "+" : "")}{change1}) | {p2.PlayerName}: {p2.Elo} ({(change2 >= 0 ? "+" : "")}{change2})");

            var p1Update = Packet.Create(PacketType.EloUpdate, new EloUpdateData
            {
                NewElo = newElo1,
                EloChange = change1,
                Wins = p1.Wins,
                Losses = p1.Losses,
                Draws = p1.Draws
            });
            await p1.SendPacketAsync(p1Update);

            var p2Update = Packet.Create(PacketType.EloUpdate, new EloUpdateData
            {
                NewElo = newElo2,
                EloChange = change2,
                Wins = p2.Wins,
                Losses = p2.Losses,
                Draws = p2.Draws
            });
            await p2.SendPacketAsync(p2Update);

            _ = BroadcastPlayerListAsync();
        }

        private static void OnClientDisconnected(ClientSession session)
        {
            Console.WriteLine($"[DISCONNECT] Client '{session.PlayerName}' (ID: {session.PlayerId}) đã ngắt kết nối.");
            _sessions.TryRemove(session.PlayerId, out _);
            _ = _roomManager.HandleClientDisconnectedAsync(session);
            _ = BroadcastPlayerListAsync();
            _ = BroadcastRoomListAsync();
        }

        public static async Task BroadcastPlayerListAsync()
        {
            try
            {
                var players = _sessions.Values
                    .Where(s => s.Client != null && s.Client.Connected && !string.IsNullOrEmpty(s.PlayerName))
                    .Select(s => new PlayerInfoData
                    {
                        PlayerId = s.PlayerId,
                        PlayerName = s.PlayerName,
                        Status = (s.CurrentRoom != null) ? "Trong phòng" : "Đang chờ",
                        Elo = s.Elo,
                        Wins = s.Wins,
                        Losses = s.Losses
                    })
                    .ToList();

                var packet = Packet.Create(PacketType.PlayerList, new PlayerListData { Players = players });

                foreach (var session in _sessions.Values)
                {
                    if (session.Client != null && session.Client.Connected)
                    {
                        try
                        {
                            await session.SendPacketAsync(packet);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] BroadcastPlayerListAsync: {ex.Message}");
            }
        }

        public static async Task BroadcastRoomListAsync()
        {
            try
            {
                var rooms = _roomManager.GetActiveRooms();
                var packet = Packet.Create(PacketType.RoomList, new RoomListData { Rooms = rooms });

                foreach (var session in _sessions.Values)
                {
                    if (session.Client != null && session.Client.Connected)
                    {
                        try
                        {
                            await session.SendPacketAsync(packet);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] BroadcastRoomListAsync: {ex.Message}");
            }
        }
    }
}
