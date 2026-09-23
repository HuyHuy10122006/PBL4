using CaroProtocol;

namespace CaroServer
{
    public class Room
    {
        public string RoomId { get; }
        public GameMode Mode { get; }
        public AIDifficulty Difficulty { get; }
        public ClientSession Player1 { get; }
        public ClientSession? Player2 { get; private set; }
        public GameEngine Engine { get; }

        public char Player1Symbol => 'X';
        public char Player2Symbol => 'O';

        public bool IsFull => Mode == GameMode.PvAI || Player2 != null;

        public Room(string roomId, GameMode mode, AIDifficulty difficulty, ClientSession player1, int boardSize = 15)
        {
            RoomId = roomId;
            Mode = mode;
            Difficulty = difficulty;
            Player1 = player1;
            Player1.CurrentRoom = this;

            Engine = new GameEngine(boardSize, blockBothEndsRule: true);
        }

        public bool JoinPlayer2(ClientSession player2)
        {
            if (IsFull) return false;
            Player2 = player2;
            Player2.CurrentRoom = this;
            return true;
        }

        public async Task StartGameAsync()
        {
            Engine.Reset();
            await BroadcastGameStateAsync();
        }

        public async Task ProcessMoveAsync(ClientSession player, int row, int col)
        {
            char symbol = (player.PlayerId == Player1.PlayerId) ? Player1Symbol : Player2Symbol;

            if (!Engine.MakeMove(row, col, symbol, out string errorMsg))
            {
                // Send move error response
                var errResp = Packet.Create(PacketType.MoveResp, new MoveRespData
                {
                    Success = false,
                    ErrorMessage = errorMsg
                });
                await player.SendPacketAsync(errResp);
                return;
            }

            // Move success
            var okResp = Packet.Create(PacketType.MoveResp, new MoveRespData { Success = true });
            await player.SendPacketAsync(okResp);

            await BroadcastGameStateAsync();

            // Check if game continues and it's AI turn
            if (Mode == GameMode.PvAI && Engine.Status == GameStateStatus.Playing && Engine.CurrentTurnSymbol == Player2Symbol)
            {
                _ = Task.Run(async () =>
                {
                    await Task.Delay(300); // Brief delay for realistic feel
                    Point aiMove = MinimaxAI.FindBestMove(Engine, Player2Symbol, Difficulty);
                    Engine.MakeMove(aiMove.Row, aiMove.Col, Player2Symbol, out _);
                    await BroadcastGameStateAsync();
                });
            }
        }

        public async Task RestartAsync(ClientSession requester)
        {
            Engine.Reset();
            await BroadcastGameStateAsync();
        }

        public async Task HandleSurrenderAsync(ClientSession surrenderPlayer)
        {
            char winner = (surrenderPlayer.PlayerId == Player1.PlayerId) ? Player2Symbol : Player1Symbol;
            string winnerName = (winner == 'X') ? Player1.PlayerName : (Player2?.PlayerName ?? "Máy (AI)");

            var stateData = CreateGameStateData();
            stateData.Status = GameStateStatus.GameOver;
            stateData.WinnerSymbol = winner;
            stateData.StatusMessage = $"{surrenderPlayer.PlayerName} đã đầu hàng! {winnerName} giành chiến thắng!";

            var packet = Packet.Create(PacketType.GameStateUpdate, stateData);
            await BroadcastPacketAsync(packet);
        }

        public async Task BroadcastChatMessageAsync(ClientSession sender, string message)
        {
            var packet = Packet.Create(PacketType.ChatMessage, new ChatMessageData
            {
                SenderName = sender.PlayerName,
                Message = message
            });
            await BroadcastPacketAsync(packet);
        }

        public async Task BroadcastGameStateAsync()
        {
            var stateData = CreateGameStateData();
            var packet = Packet.Create(PacketType.GameStateUpdate, stateData);
            await BroadcastPacketAsync(packet);
        }

        private GameStateUpdateData CreateGameStateData()
        {
            string currentTurnPlayerName = (Engine.CurrentTurnSymbol == 'X') ? Player1.PlayerName : (Player2?.PlayerName ?? "Máy (AI)");

            string statusMsg = Engine.Status switch
            {
                GameStateStatus.WaitingForOpponent => "Đang chờ đối thủ tham gia...",
                GameStateStatus.Playing => $"Lượt đi: {currentTurnPlayerName} ({Engine.CurrentTurnSymbol})",
                GameStateStatus.GameOver => Engine.WinnerSymbol switch
                {
                    'D' => "Trận đấu HÒA!",
                    'X' => $"{Player1.PlayerName} (X) THẮNG!",
                    'O' => $"{(Player2?.PlayerName ?? "Máy AI")} (O) THẮNG!",
                    _ => "Trận đấu kết thúc."
                },
                _ => ""
            };

            return new GameStateUpdateData
            {
                BoardSize = Engine.BoardSize,
                Board = Engine.GetBoardMatrix(),
                CurrentTurnSymbol = Engine.CurrentTurnSymbol,
                CurrentTurnPlayerName = currentTurnPlayerName,
                LastMoveRow = Engine.LastMove?.Row ?? -1,
                LastMoveCol = Engine.LastMove?.Col ?? -1,
                Status = Engine.Status,
                WinnerSymbol = Engine.WinnerSymbol,
                WinLine = Engine.WinLine,
                StatusMessage = statusMsg
            };
        }

        public async Task BroadcastPacketAsync(Packet packet)
        {
            await Player1.SendPacketAsync(packet);
            if (Player2 != null)
            {
                await Player2.SendPacketAsync(packet);
            }
        }
    }
}
