using CaroProtocol;
using System.Collections.Concurrent;

namespace CaroServer
{
    public class RoomManager
    {
        private readonly ConcurrentDictionary<string, Room> _rooms = new ConcurrentDictionary<string, Room>();
        private int _roomCounter = 1000;

        public async Task<Room> CreateRoomAsync(ClientSession creator, GameMode mode, AIDifficulty difficulty, int boardSize = 15)
        {
            string roomId = $"R{Interlocked.Increment(ref _roomCounter)}";
            Room room = new Room(roomId, mode, difficulty, creator, boardSize);
            _rooms[roomId] = room;

            var resp = Packet.Create(PacketType.CreateRoomResp, new CreateRoomRespData
            {
                Success = true,
                RoomId = roomId,
                PlayerSymbol = room.Player1Symbol,
                IsYourTurn = true,
                Message = (mode == GameMode.PvAI) ? "Đã tạo phòng đấu với Máy (AI)." : $"Đã tạo phòng {roomId}. Đang chờ đối thủ..."
            });

            await creator.SendPacketAsync(resp);

            if (mode == GameMode.PvAI)
            {
                await room.StartGameAsync();
            }

            return room;
        }

        public async Task<bool> JoinRoomAsync(ClientSession joiner, string roomId)
        {
            if (!_rooms.TryGetValue(roomId, out Room? room))
            {
                var errResp = Packet.Create(PacketType.JoinRoomResp, new JoinRoomRespData
                {
                    Success = false,
                    Message = "Không tìm thấy phòng chơi này."
                });
                await joiner.SendPacketAsync(errResp);
                return false;
            }

            // Prevent joining own room
            if (room.Player1.PlayerId == joiner.PlayerId)
            {
                var errResp = Packet.Create(PacketType.JoinRoomResp, new JoinRoomRespData
                {
                    Success = false,
                    Message = "Bạn không thể vào phòng do chính mình tạo!"
                });
                await joiner.SendPacketAsync(errResp);
                return false;
            }

            // Prevent joining if already in a room
            if (joiner.CurrentRoom != null)
            {
                var errResp = Packet.Create(PacketType.JoinRoomResp, new JoinRoomRespData
                {
                    Success = false,
                    Message = "Bạn đang ở trong phòng khác rồi!"
                });
                await joiner.SendPacketAsync(errResp);
                return false;
            }

            if (room.IsFull)
            {
                var errResp = Packet.Create(PacketType.JoinRoomResp, new JoinRoomRespData
                {
                    Success = false,
                    Message = "Phòng chơi đã đầy."
                });
                await joiner.SendPacketAsync(errResp);
                return false;
            }

            if (room.JoinPlayer2(joiner))
            {
                // Send response to Joiner
                var joinerResp = Packet.Create(PacketType.JoinRoomResp, new JoinRoomRespData
                {
                    Success = true,
                    RoomId = roomId,
                    PlayerSymbol = room.Player2Symbol,
                    IsYourTurn = false, // X (Player1) plays first
                    OpponentName = room.Player1.PlayerName,
                    Message = $"Đã tham gia phòng {roomId} thành công!"
                });
                await joiner.SendPacketAsync(joinerResp);

                // Notify Player1
                var chatPacket = Packet.Create(PacketType.ChatMessage, new ChatMessageData
                {
                    SenderName = "Hệ thống",
                    Message = $"{joiner.PlayerName} đã tham gia phòng!"
                });
                await room.Player1.SendPacketAsync(chatPacket);

                // Start game
                await room.StartGameAsync();
                return true;
            }

            return false;
        }

        public async Task LeaveRoomAsync(ClientSession session)
        {
            if (session.CurrentRoom == null)
            {
                var resp = Packet.Create(PacketType.LeaveRoomResp, new LeaveRoomRespData
                {
                    Success = false,
                    Message = "Bạn chưa ở trong phòng nào."
                });
                await session.SendPacketAsync(resp);
                return;
            }

            Room room = session.CurrentRoom;

            // Can only block leave when game is actively being played (both players present)
            bool isGameActivelyPlaying = room.IsFull && room.Engine.Status == GameStateStatus.Playing;
            if (isGameActivelyPlaying)
            {
                var resp = Packet.Create(PacketType.LeaveRoomResp, new LeaveRoomRespData
                {
                    Success = false,
                    Message = "Không thể thoát phòng khi trận đấu đang diễn ra! Hãy đầu hàng hoặc hoàn thành trận đấu."
                });
                await session.SendPacketAsync(resp);
                return;
            }

            // Remove room
            _rooms.TryRemove(room.RoomId, out _);
            session.CurrentRoom = null;

            // Notify opponent if exists
            ClientSession? opponent = (session.PlayerId == room.Player1.PlayerId) ? room.Player2 : room.Player1;
            if (opponent != null && opponent.Client.Connected)
            {
                opponent.CurrentRoom = null;
                var dcPacket = Packet.Create(PacketType.OpponentDisconnected, new ChatMessageData
                {
                    SenderName = "Hệ thống",
                    Message = $"{session.PlayerName} đã rời phòng."
                });
                await opponent.SendPacketAsync(dcPacket);
            }

            // Confirm leave to the player
            var okResp = Packet.Create(PacketType.LeaveRoomResp, new LeaveRoomRespData
            {
                Success = true,
                Message = "Đã thoát phòng thành công."
            });
            await session.SendPacketAsync(okResp);

            Console.WriteLine($"[ROOM] {session.PlayerName} đã rời phòng {room.RoomId}");
        }

        public async Task HandleClientDisconnectedAsync(ClientSession session)
        {
            if (session.CurrentRoom != null)
            {
                Room room = session.CurrentRoom;
                _rooms.TryRemove(room.RoomId, out _);

                ClientSession? opponent = (session.PlayerId == room.Player1.PlayerId) ? room.Player2 : room.Player1;
                if (opponent != null && opponent.Client.Connected)
                {
                    opponent.CurrentRoom = null;
                    var dcPacket = Packet.Create(PacketType.OpponentDisconnected, new ChatMessageData
                    {
                        SenderName = "Hệ thống",
                        Message = $"{session.PlayerName} đã ngắt kết nối. Trận đấu kết thúc!"
                    });
                    await opponent.SendPacketAsync(dcPacket);
                }
            }
        }
    }
}
