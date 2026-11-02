using CaroProtocol;
using System.Collections.Concurrent;

namespace CaroServer
{
    public class RoomManager
    {
        private readonly ConcurrentDictionary<string, Room> _rooms = new ConcurrentDictionary<string, Room>();
        private int _roomCounter = 1000;
        public Func<Task>? OnRoomStateChanged { get; set; }

        public async Task<Room> CreateRoomAsync(ClientSession creator, GameMode mode, AIDifficulty difficulty, int boardSize = 15, bool blockBothEndsRule = true, int timePerTurn = 30)
        {
            string roomId = $"R{Interlocked.Increment(ref _roomCounter)}";
            Room room = new Room(roomId, mode, difficulty, creator, boardSize, blockBothEndsRule, timePerTurn);
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

            if (OnRoomStateChanged != null)
            {
                await OnRoomStateChanged();
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

                if (OnRoomStateChanged != null)
                {
                    await OnRoomStateChanged();
                }
                return true;
            }

            return false;
        }

        public List<RoomInfoData> GetActiveRooms()
        {
            var list = new List<RoomInfoData>();
            foreach (var room in _rooms.Values)
            {
                list.Add(new RoomInfoData
                {
                    RoomId = room.RoomId,
                    Mode = room.Mode,
                    Player1Name = room.Player1.PlayerName,
                    Player2Name = (room.Mode == GameMode.PvAI) ? "Máy (AI)" : (room.Player2?.PlayerName ?? string.Empty),
                    SpectatorCount = room.SpectatorCount,
                    Status = room.Engine.Status,
                    IsFull = room.IsFull,
                    CanJoinAsPlayer = (room.Mode == GameMode.PvP && !room.IsFull),
                    BlockBothEndsRule = room.BlockBothEndsRule,
                    TimePerTurn = room.TimePerTurn
                });
            }
            return list;
        }

        public async Task<bool> SpectateRoomAsync(ClientSession spectator, string roomId)
        {
            if (!_rooms.TryGetValue(roomId, out Room? room))
            {
                var errResp = Packet.Create(PacketType.SpectateRoomResp, new SpectateRoomRespData
                {
                    Success = false,
                    Message = "Không tìm thấy phòng chơi này."
                });
                await spectator.SendPacketAsync(errResp);
                return false;
            }

            if (spectator.CurrentRoom != null)
            {
                var errResp = Packet.Create(PacketType.SpectateRoomResp, new SpectateRoomRespData
                {
                    Success = false,
                    Message = "Bạn đang ở trong phòng khác rồi!"
                });
                await spectator.SendPacketAsync(errResp);
                return false;
            }

            room.AddSpectator(spectator);

            var resp = Packet.Create(PacketType.SpectateRoomResp, new SpectateRoomRespData
            {
                Success = true,
                RoomId = roomId,
                Player1Name = room.Player1.PlayerName,
                Player2Name = (room.Mode == GameMode.PvAI) ? "Máy (AI)" : (room.Player2?.PlayerName ?? "Chưa có"),
                Mode = room.Mode,
                BlockBothEndsRule = room.BlockBothEndsRule,
                TimePerTurn = room.TimePerTurn,
                CurrentState = room.GetCurrentGameStateData(),
                Message = $"Đang xem trực tiếp phòng {roomId}"
            });
            await spectator.SendPacketAsync(resp);

            // Notify room via chat
            var chatPacket = Packet.Create(PacketType.ChatMessage, new ChatMessageData
            {
                SenderName = "Hệ thống",
                Message = $"{spectator.PlayerName} đã vào xem trận đấu."
            });
            await room.BroadcastPacketAsync(chatPacket);

            if (OnRoomStateChanged != null)
            {
                await OnRoomStateChanged();
            }

            return true;
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

            // Xử lý người xem rời phòng (không ảnh hưởng ván đấu của 2 người chơi)
            if (session.IsSpectator)
            {
                room.RemoveSpectator(session.PlayerId);
                session.CurrentRoom = null;
                session.IsSpectator = false;

                var okResp = Packet.Create(PacketType.LeaveRoomResp, new LeaveRoomRespData
                {
                    Success = true,
                    Message = "Đã rời chế độ xem phòng."
                });
                await session.SendPacketAsync(okResp);

                var chatPacket = Packet.Create(PacketType.ChatMessage, new ChatMessageData
                {
                    SenderName = "Hệ thống",
                    Message = $"{session.PlayerName} đã rời phòng xem."
                });
                await room.BroadcastPacketAsync(chatPacket);

                if (OnRoomStateChanged != null)
                {
                    await OnRoomStateChanged();
                }
                return;
            }

            // Can only block leave when game is actively being played between 2 human players (PvP)
            bool isGameActivelyPlaying = (room.Mode == GameMode.PvP) && room.IsFull && (room.Engine.Status == GameStateStatus.Playing);
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

            // Remove room & stop timer
            room.StopTurnTimeoutTimer();
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
            var okRespRoom = Packet.Create(PacketType.LeaveRoomResp, new LeaveRoomRespData
            {
                Success = true,
                Message = "Đã thoát phòng thành công."
            });
            await session.SendPacketAsync(okRespRoom);

            Console.WriteLine($"[ROOM] {session.PlayerName} đã rời phòng {room.RoomId}");

            if (OnRoomStateChanged != null)
            {
                await OnRoomStateChanged();
            }
        }

        public Room? FindRoomWaitingForPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName)) return null;

            return _rooms.Values.FirstOrDefault(r =>
                r.IsWaitingForReconnect &&
                r.Engine.Status == GameStateStatus.Playing &&
                string.Equals(r.DisconnectedPlayerName, playerName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task HandleClientDisconnectedAsync(ClientSession session)
        {
            if (session.CurrentRoom != null)
            {
                Room room = session.CurrentRoom;

                if (session.IsSpectator)
                {
                    room.RemoveSpectator(session.PlayerId);
                    session.CurrentRoom = null;
                    session.IsSpectator = false;
                    if (OnRoomStateChanged != null)
                    {
                        await OnRoomStateChanged();
                    }
                    return;
                }

                // Nếu là trận PvP đang đấu dở dang (Playing), kích hoạt 30s thời gian chờ kết nối lại!
                // Đồng thời thời gian chơi của ván (turn timer) vẫn tiếp tục chạy
                if (room.Mode == GameMode.PvP && room.IsFull && room.Engine.Status == GameStateStatus.Playing)
                {
                    await room.StartReconnectGracePeriodAsync(session, async (r) =>
                    {
                        // Callback khi hết 30s mà không vào lại: dọn dẹp phòng
                        _rooms.TryRemove(r.RoomId, out _);
                        if (OnRoomStateChanged != null)
                        {
                            await OnRoomStateChanged();
                        }
                    });

                    // Gửi thông báo cho đối thủ và khán giả biết đang trong thời gian ân hạn 30s
                    ClientSession? opponent = (session.PlayerId == room.Player1.PlayerId) ? room.Player2 : room.Player1;
                    if (opponent != null && opponent.Client != null && opponent.Client.Connected)
                    {
                        var reconnectingPacket = Packet.Create(PacketType.OpponentReconnecting, new OpponentReconnectingData
                        {
                            DisconnectedPlayerName = session.PlayerName,
                            GracePeriodSeconds = 30,
                            Message = $"{session.PlayerName} đã mất kết nối. Đang chờ kết nối lại (30s)... Thời gian ván đấu vẫn đang tiếp tục tính."
                        });
                        await opponent.SendPacketAsync(reconnectingPacket);
                    }

                    if (OnRoomStateChanged != null)
                    {
                        await OnRoomStateChanged();
                    }
                    return;
                }

                // Các trường hợp khác: chưa bắt đầu, hoặc đấu AI, hoặc đã GameOver
                room.StopTurnTimeoutTimer();
                room.StopReconnectTimer();
                _rooms.TryRemove(room.RoomId, out _);

                ClientSession? other = (session.PlayerId == room.Player1.PlayerId) ? room.Player2 : room.Player1;
                if (other != null && other.Client != null && other.Client.Connected)
                {
                    other.CurrentRoom = null;
                    var dcPacket = Packet.Create(PacketType.OpponentDisconnected, new ChatMessageData
                    {
                        SenderName = "Hệ thống",
                        Message = $"{session.PlayerName} đã rời phòng."
                    });
                    await other.SendPacketAsync(dcPacket);
                }

                if (OnRoomStateChanged != null)
                {
                    await OnRoomStateChanged();
                }
            }
        }
    }
}
