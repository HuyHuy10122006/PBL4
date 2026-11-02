using CaroProtocol;

namespace CaroServer
{
    public class Room
    {
        public string RoomId { get; }
        public GameMode Mode { get; }
        public AIDifficulty Difficulty { get; }
        public ClientSession Player1 { get; private set; }
        public ClientSession? Player2 { get; private set; }
        public GameEngine Engine { get; }
        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, ClientSession> _spectators = new System.Collections.Concurrent.ConcurrentDictionary<string, ClientSession>();

        public int TimePerTurn { get; }
        public bool BlockBothEndsRule { get; }
        private CancellationTokenSource? _turnTimeoutCts;
        private readonly object _timerLock = new object();
        private bool _isEloRecorded = false;

        public bool IsWaitingForReconnect { get; private set; } = false;
        public string? DisconnectedPlayerId { get; private set; }
        public string? DisconnectedPlayerName { get; private set; }
        private CancellationTokenSource? _reconnectTimeoutCts;
        private readonly object _reconnectLock = new object();

        public char Player1Symbol => 'X';
        public char Player2Symbol => 'O';

        public bool IsFull => Mode == GameMode.PvAI || Player2 != null;
        public int SpectatorCount => _spectators.Count;

        public Room(string roomId, GameMode mode, AIDifficulty difficulty, ClientSession player1, int boardSize = 15, bool blockBothEndsRule = true, int timePerTurn = 30)
        {
            RoomId = roomId;
            Mode = mode;
            Difficulty = difficulty;
            Player1 = player1;
            Player1.CurrentRoom = this;
            BlockBothEndsRule = blockBothEndsRule;
            TimePerTurn = timePerTurn;

            Engine = new GameEngine(boardSize, blockBothEndsRule: blockBothEndsRule);
        }

        public bool JoinPlayer2(ClientSession player2)
        {
            if (IsFull) return false;
            Player2 = player2;
            Player2.CurrentRoom = this;
            return true;
        }

        public bool AddSpectator(ClientSession spectator)
        {
            spectator.CurrentRoom = this;
            spectator.IsSpectator = true;
            _spectators[spectator.PlayerId] = spectator;
            return true;
        }

        public bool RemoveSpectator(string playerId)
        {
            return _spectators.TryRemove(playerId, out _);
        }

        public GameStateUpdateData GetCurrentGameStateData() => CreateGameStateData();

        public async Task StartGameAsync()
        {
            Engine.Reset();
            await BroadcastGameStateAsync();
            StartTurnTimeoutTimer();
        }

        public async Task ProcessMoveAsync(ClientSession player, int row, int col)
        {
            if (player.IsSpectator) return;
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

            if (Engine.Status == GameStateStatus.Playing)
            {
                StartTurnTimeoutTimer();
            }
            else
            {
                StopTurnTimeoutTimer();
                await CheckAndRecordMatchResultAsync();
            }

            // Check if game continues and it's AI turn
            if (Mode == GameMode.PvAI && Engine.Status == GameStateStatus.Playing && Engine.CurrentTurnSymbol == Player2Symbol)
            {
                _ = Task.Run(async () =>
                {
                    await Task.Delay(300); // Brief delay for realistic feel
                    Point aiMove = MinimaxAI.FindBestMove(Engine, Player2Symbol, Difficulty);
                    Engine.MakeMove(aiMove.Row, aiMove.Col, Player2Symbol, out _);
                    await BroadcastGameStateAsync();

                    if (Engine.Status == GameStateStatus.Playing)
                    {
                        StartTurnTimeoutTimer();
                    }
                    else
                    {
                        StopTurnTimeoutTimer();
                        await CheckAndRecordMatchResultAsync();
                    }
                });
            }
        }

        public async Task RestartAsync(ClientSession requester)
        {
            if (requester.IsSpectator) return;

            // Trận đấu chưa kết thúc thì không được phép chơi lại!
            if (Engine.Status != GameStateStatus.GameOver)
            {
                var errorPacket = Packet.Create(PacketType.ChatMessage, new ChatMessageData
                {
                    SenderName = "Hệ thống",
                    Message = "Trận đấu đang diễn ra, chưa thể tái đấu!"
                });
                await requester.SendPacketAsync(errorPacket);
                return;
            }

            _isEloRecorded = false;
            StopReconnectTimer();
            Engine.Reset();
            await BroadcastGameStateAsync();
            StartTurnTimeoutTimer();
        }

        public async Task HandleSurrenderAsync(ClientSession surrenderPlayer)
        {
            if (surrenderPlayer.IsSpectator) return;
            if (Engine.Status != GameStateStatus.Playing) return;

            StopTurnTimeoutTimer();
            StopReconnectTimer();

            char winner = (surrenderPlayer.PlayerId == Player1.PlayerId) ? Player2Symbol : Player1Symbol;
            string winnerName = (winner == 'X') ? Player1.PlayerName : (Player2?.PlayerName ?? "Máy (AI)");

            Engine.Surrender(winner); // Cập nhật trạng thái ván đấu thành GameOver trên Engine

            var stateData = CreateGameStateData();
            stateData.Status = GameStateStatus.GameOver;
            stateData.WinnerSymbol = winner;
            stateData.StatusMessage = $"{surrenderPlayer.PlayerName} đã đầu hàng! {winnerName} giành chiến thắng!";

            var packet = Packet.Create(PacketType.GameStateUpdate, stateData);
            await BroadcastPacketAsync(packet);
            await CheckAndRecordMatchResultAsync();
        }

        public async Task HandleTurnTimeoutAsync()
        {
            if (Engine.Status != GameStateStatus.Playing) return;

            StopTurnTimeoutTimer();
            StopReconnectTimer();

            char timedOutSymbol = Engine.CurrentTurnSymbol;
            char winner = (timedOutSymbol == 'X') ? Player2Symbol : Player1Symbol;
            string timedOutName = (timedOutSymbol == 'X') ? Player1.PlayerName : (Player2?.PlayerName ?? "Máy (AI)");
            string winnerName = (winner == 'X') ? Player1.PlayerName : (Player2?.PlayerName ?? "Máy (AI)");

            Engine.Surrender(winner);

            var stateData = CreateGameStateData();
            stateData.Status = GameStateStatus.GameOver;
            stateData.WinnerSymbol = winner;
            stateData.StatusMessage = $"Hết giờ! {timedOutName} ({timedOutSymbol}) đã bị xử thua do hết thời gian lượt đi. {winnerName} chiến thắng!";

            var packet = Packet.Create(PacketType.GameStateUpdate, stateData);
            await BroadcastPacketAsync(packet);
            await CheckAndRecordMatchResultAsync();
        }

        public async Task StartReconnectGracePeriodAsync(ClientSession disconnectedPlayer, Func<Room, Task>? onTimeoutCallback = null)
        {
            lock (_reconnectLock)
            {
                _reconnectTimeoutCts?.Cancel();
                _reconnectTimeoutCts?.Dispose();
                _reconnectTimeoutCts = new CancellationTokenSource();

                IsWaitingForReconnect = true;
                DisconnectedPlayerId = disconnectedPlayer.PlayerId;
                DisconnectedPlayerName = disconnectedPlayer.PlayerName;

                var token = _reconnectTimeoutCts.Token;

                _ = Task.Run(async () =>
                {
                    try
                    {
                        // Thời gian chờ kết nối lại: 30 giây (trong thời gian này, turn timer vẫn chạy như thường)
                        await Task.Delay(30000, token);
                        if (!token.IsCancellationRequested && IsWaitingForReconnect && Engine.Status == GameStateStatus.Playing)
                        {
                            await HandleReconnectTimeoutAsync(onTimeoutCallback);
                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ReconnectTimer] Lỗi: {ex.Message}");
                    }
                });
            }
            await Task.CompletedTask;
        }

        public async Task HandleReconnectTimeoutAsync(Func<Room, Task>? onTimeoutCallback = null)
        {
            lock (_reconnectLock)
            {
                if (!IsWaitingForReconnect || Engine.Status != GameStateStatus.Playing) return;
                IsWaitingForReconnect = false;
                _reconnectTimeoutCts?.Cancel();
                _reconnectTimeoutCts = null;
            }

            StopTurnTimeoutTimer();

            // Người mất kết nối quá 30s bị xử thua
            char winner = (DisconnectedPlayerId == Player1.PlayerId) ? Player2Symbol : Player1Symbol;
            string winnerName = (winner == 'X') ? Player1.PlayerName : (Player2?.PlayerName ?? "Máy (AI)");

            Engine.Surrender(winner);

            var stateData = CreateGameStateData();
            stateData.Status = GameStateStatus.GameOver;
            stateData.WinnerSymbol = winner;
            stateData.StatusMessage = $"{DisconnectedPlayerName} đã mất kết nối quá 30 giây! {winnerName} giành chiến thắng!";

            var packet = Packet.Create(PacketType.GameStateUpdate, stateData);
            await BroadcastPacketAsync(packet);
            await CheckAndRecordMatchResultAsync();

            if (onTimeoutCallback != null)
            {
                try { await onTimeoutCallback(this); } catch { }
            }
        }

        public async Task<bool> ReconnectPlayerAsync(ClientSession newSession)
        {
            if (!IsWaitingForReconnect) return false;
            if (newSession.PlayerName != DisconnectedPlayerName && newSession.PlayerId != DisconnectedPlayerId)
                return false;

            lock (_reconnectLock)
            {
                _reconnectTimeoutCts?.Cancel();
                _reconnectTimeoutCts?.Dispose();
                _reconnectTimeoutCts = null;
                IsWaitingForReconnect = false;
            }

            char mySymbol;
            string opponentName;
            if (Player1.PlayerName == DisconnectedPlayerName || Player1.PlayerId == DisconnectedPlayerId)
            {
                Player1 = newSession;
                mySymbol = Player1Symbol;
                opponentName = Player2?.PlayerName ?? "Máy (AI)";
            }
            else
            {
                Player2 = newSession;
                mySymbol = Player2Symbol;
                opponentName = Player1.PlayerName;
            }

            newSession.CurrentRoom = this;

            // Phản hồi dữ liệu ván cờ hiện tại cho người vừa kết nối lại
            var respData = new ReconnectRespData
            {
                Success = true,
                RoomId = RoomId,
                PlayerSymbol = mySymbol,
                IsYourTurn = (Engine.CurrentTurnSymbol == mySymbol),
                OpponentName = opponentName,
                Mode = Mode,
                BlockBothEndsRule = BlockBothEndsRule,
                TimePerTurn = TimePerTurn,
                CurrentState = GetCurrentGameStateData(),
                Message = $"Đã kết nối lại phòng {RoomId} thành công!"
            };
            await newSession.SendPacketAsync(Packet.Create(PacketType.ReconnectResp, respData));

            // Báo cho đối thủ và khán giả biết người chơi đã kết nối lại
            var reconnectedPacket = Packet.Create(PacketType.OpponentReconnected, new OpponentReconnectedData
            {
                ReconnectedPlayerName = newSession.PlayerName,
                Message = $"{newSession.PlayerName} đã kết nối lại ván đấu!"
            });
            await BroadcastPacketAsync(reconnectedPacket);

            return true;
        }

        public void StopReconnectTimer()
        {
            lock (_reconnectLock)
            {
                try
                {
                    _reconnectTimeoutCts?.Cancel();
                    _reconnectTimeoutCts?.Dispose();
                }
                catch { }
                finally
                {
                    _reconnectTimeoutCts = null;
                    IsWaitingForReconnect = false;
                }
            }
        }

        private async Task CheckAndRecordMatchResultAsync()
        {
            if (_isEloRecorded || Mode != GameMode.PvP || Player2 == null) return;
            if (Engine.Status != GameStateStatus.GameOver) return;

            _isEloRecorded = true;
            double score1 = 0.5;
            string? winnerUsername = null;
            if (Engine.WinnerSymbol == 'X')
            {
                score1 = 1.0;
                winnerUsername = Player1.PlayerName;
            }
            else if (Engine.WinnerSymbol == 'O')
            {
                score1 = 0.0;
                winnerUsername = Player2.PlayerName;
            }

            await Program.RecordMatchResultAsync(Player1, Player2, winnerUsername, score1, Engine.TotalMovesCount);
        }

        private void StartTurnTimeoutTimer()
        {
            lock (_timerLock)
            {
                StopTurnTimeoutTimer();

                if (TimePerTurn <= 0 || Engine.Status != GameStateStatus.Playing)
                    return;

                var cts = new CancellationTokenSource();
                _turnTimeoutCts = cts;

                _ = Task.Run(async () =>
                {
                    try
                    {
                        // Thêm 1.2s độ trễ mạng để client xử lý trước
                        await Task.Delay((TimePerTurn + 1) * 1000 + 200, cts.Token);
                        if (!cts.Token.IsCancellationRequested && Engine.Status == GameStateStatus.Playing)
                        {
                            await HandleTurnTimeoutAsync();
                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Timer] Lỗi timer: {ex.Message}");
                    }
                });
            }
        }

        public void StopTurnTimeoutTimer()
        {
            lock (_timerLock)
            {
                try
                {
                    _turnTimeoutCts?.Cancel();
                    _turnTimeoutCts?.Dispose();
                }
                catch { }
                finally
                {
                    _turnTimeoutCts = null;
                }
            }
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
                StatusMessage = statusMsg,
                BlockBothEndsRule = BlockBothEndsRule,
                TimePerTurn = TimePerTurn
            };
        }

        public async Task BroadcastPacketAsync(Packet packet)
        {
            await Player1.SendPacketAsync(packet);
            if (Player2 != null)
            {
                await Player2.SendPacketAsync(packet);
            }
            foreach (var spec in _spectators.Values)
            {
                if (spec.Client != null && spec.Client.Connected)
                {
                    try { await spec.SendPacketAsync(packet); } catch { }
                }
            }
        }
    }
}
