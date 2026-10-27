using CaroProtocol;
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
                case PacketType.ConnectReq:
                    var connReq = packet.DeserializePayload<ConnectReqData>();
                    if (connReq != null)
                    {
                        session.PlayerName = string.IsNullOrWhiteSpace(connReq.PlayerName) ? $"Player_{session.PlayerId}" : connReq.PlayerName;
                        _sessions[session.PlayerId] = session;
                        Console.WriteLine($"[LOGIN] Client {session.PlayerId} đặt tên là: '{session.PlayerName}'");

                        var resp = Packet.Create(PacketType.ConnectResp, new ConnectRespData
                        {
                            Success = true,
                            PlayerId = session.PlayerId,
                            ServerHostName = Environment.MachineName,
                            Message = $"Chào mừng {session.PlayerName} kết nối đến Server Caro!"
                        });
                        await session.SendPacketAsync(resp);
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
                        Status = (s.CurrentRoom != null) ? "Trong phòng" : "Đang chờ"
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
