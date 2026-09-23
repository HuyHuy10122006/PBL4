using CaroProtocol;
using System.Net;
using System.Net.Sockets;

namespace CaroServer
{
    internal class Program
    {
        private static readonly RoomManager _roomManager = new RoomManager();
        private static readonly int Port = 8888;

        static async Task Main(string[] args)
        {
            Console.Title = "Caro Server - Trọng tài & Quản lý Trận đấu TCP";
            Console.WriteLine("=================================================");
            Console.WriteLine("    CARO GAME SERVER - SYSTEM STARTING...       ");
            Console.WriteLine("=================================================");

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
                        Console.WriteLine($"[LOGIN] Client {session.PlayerId} đặt tên là: '{session.PlayerName}'");

                        var resp = Packet.Create(PacketType.ConnectResp, new ConnectRespData
                        {
                            Success = true,
                            PlayerId = session.PlayerId,
                            Message = $"Chào mừng {session.PlayerName} kết nối đến Server Caro!"
                        });
                        await session.SendPacketAsync(resp);
                    }
                    break;

                case PacketType.CreateRoomReq:
                    var createReq = packet.DeserializePayload<CreateRoomReqData>();
                    if (createReq != null)
                    {
                        Console.WriteLine($"[ROOM] {session.PlayerName} yêu cầu tạo phòng. Chế độ: {createReq.Mode}, Độ khó: {createReq.AIDifficulty}");
                        await _roomManager.CreateRoomAsync(session, createReq.Mode, createReq.AIDifficulty, createReq.BoardSize);
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
            }
        }

        private static void OnClientDisconnected(ClientSession session)
        {
            Console.WriteLine($"[DISCONNECT] Client '{session.PlayerName}' (ID: {session.PlayerId}) đã ngắt kết nối.");
            _ = _roomManager.HandleClientDisconnectedAsync(session);
        }
    }
}
