using CaroProtocol;
using System.Net.Sockets;

namespace CaroClient
{
    public class NetworkClient
    {
        private TcpClient? _tcpClient;
        private NetworkStream? _stream;
        private CancellationTokenSource? _cts;
        private SynchronizationContext? _syncContext;

        public bool IsConnected => _tcpClient != null && _tcpClient.Connected;
        public string PlayerId { get; private set; } = string.Empty;
        public string PlayerName { get; private set; } = "Player";
        public string ServerHostName { get; private set; } = string.Empty;
        public string ServerIp { get; private set; } = string.Empty;
        public int ServerPort { get; private set; } = 8888;
        public bool IsSpectator { get; set; } = false;

        public int Elo { get; set; } = 1000;
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int Draws { get; set; } = 0;

        // --- UI Events ---
        public event Action<bool, string>? OnConnectionCompleted;
        public event Action<LoginRespData>? OnLoginCompleted;
        public event Action<RegisterRespData>? OnRegisterCompleted;
        public event Action<CreateRoomRespData>? OnCreateRoomResponse;
        public event Action<JoinRoomRespData>? OnJoinRoomResponse;
        public event Action<LeaveRoomRespData>? OnLeaveRoomResponse;
        public event Action<GameStateUpdateData>? OnGameStateUpdated;
        public event Action<ChatMessageData>? OnChatMessageReceived;
        public event Action<string>? OnOpponentDisconnected;
        public event Action<OpponentReconnectingData>? OnOpponentReconnecting;
        public event Action<OpponentReconnectedData>? OnOpponentReconnected;
        public event Action<ReconnectRespData>? OnReconnectResponse;
        public event Action<string>? OnErrorOccurred;
        public event Action<List<PlayerInfoData>>? OnPlayerListReceived;
        public event Action<RoomListData>? OnRoomListReceived;
        public event Action<SpectateRoomRespData>? OnSpectateRoomResponse;
        public event Action<List<MatchHistoryItemData>>? OnMatchHistoryReceived;
        public event Action<List<LeaderboardItemData>>? OnLeaderboardReceived;
        public event Action<EloUpdateData>? OnEloUpdated;

        public NetworkClient()
        {
            _syncContext = SynchronizationContext.Current;
        }

        public async Task<bool> EnsureConnectedAsync(string ip, int port)
        {
            if (IsConnected) return true;

            if (_syncContext == null && SynchronizationContext.Current != null)
            {
                _syncContext = SynchronizationContext.Current;
            }

            try
            {
                _tcpClient = new TcpClient();
                using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                await _tcpClient.ConnectAsync(ip, port, timeoutCts.Token);
                ServerIp = ip;
                ServerPort = port;
                _stream = _tcpClient.GetStream();
                _cts = new CancellationTokenSource();

                // Start Listen Loop
                _ = Task.Run(() => ListenLoopAsync(_cts.Token));
                return true;
            }
            catch (Exception ex)
            {
                Disconnect();
                PostToUI(() => OnErrorOccurred?.Invoke($"Không thể kết nối đến máy chủ ({ip}:{port}): {ex.Message}"));
                return false;
            }
        }

        public async Task ConnectAndLoginAsync(string ip, int port, string username, string password)
        {
            if (!await EnsureConnectedAsync(ip, port)) return;

            PlayerName = username;
            var packet = Packet.Create(PacketType.LoginReq, new LoginReqData
            {
                Username = username,
                Password = password
            });

            if (_stream != null && _cts != null)
            {
                await PacketFraming.SendPacketAsync(_stream, packet, _cts.Token);
            }
        }

        public async Task ConnectAndRegisterAsync(string ip, int port, string username, string password)
        {
            if (!await EnsureConnectedAsync(ip, port)) return;

            var packet = Packet.Create(PacketType.RegisterReq, new RegisterReqData
            {
                Username = username,
                Password = password
            });

            if (_stream != null && _cts != null)
            {
                await PacketFraming.SendPacketAsync(_stream, packet, _cts.Token);
            }
        }

        public async Task ConnectAsync(string ip, int port, string playerName)
        {
            if (!await EnsureConnectedAsync(ip, port)) return;

            PlayerName = playerName;
            var packet = Packet.Create(PacketType.ConnectReq, new ConnectReqData { PlayerName = playerName });
            if (_stream != null && _cts != null)
            {
                await PacketFraming.SendPacketAsync(_stream, packet, _cts.Token);
            }
        }

        private async Task ListenLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && _stream != null)
                {
                    Packet? packet = await PacketFraming.ReceivePacketAsync(_stream, token);
                    if (packet == null) break;

                    HandleReceivedPacket(packet);
                }
            }
            catch (Exception ex)
            {
                PostToUI(() => OnErrorOccurred?.Invoke($"Mất kết nối với Server: {ex.Message}"));
            }
            finally
            {
                Disconnect();
            }
        }

        private void HandleReceivedPacket(Packet packet)
        {
            switch (packet.Type)
            {
                case PacketType.LoginResp:
                    var loginResp = packet.DeserializePayload<LoginRespData>();
                    if (loginResp != null)
                    {
                        if (loginResp.Success)
                        {
                            PlayerId = loginResp.PlayerId;
                            PlayerName = loginResp.Username;
                            Elo = loginResp.Elo;
                            Wins = loginResp.Wins;
                            Losses = loginResp.Losses;
                            Draws = loginResp.Draws;
                        }
                        PostToUI(() => OnLoginCompleted?.Invoke(loginResp));
                    }
                    break;

                case PacketType.RegisterResp:
                    var regResp = packet.DeserializePayload<RegisterRespData>();
                    if (regResp != null)
                    {
                        PostToUI(() => OnRegisterCompleted?.Invoke(regResp));
                    }
                    break;

                case PacketType.ConnectResp:
                    var connResp = packet.DeserializePayload<ConnectRespData>();
                    if (connResp != null)
                    {
                        PlayerId = connResp.PlayerId;
                        ServerHostName = connResp.ServerHostName;
                        Elo = connResp.Elo;
                        Wins = connResp.Wins;
                        Losses = connResp.Losses;
                        Draws = connResp.Draws;
                        PostToUI(() => OnConnectionCompleted?.Invoke(connResp.Success, connResp.Message));
                    }
                    break;

                case PacketType.PlayerList:
                    var listData = packet.DeserializePayload<PlayerListData>();
                    if (listData != null)
                    {
                        PostToUI(() => OnPlayerListReceived?.Invoke(listData.Players));
                    }
                    break;

                case PacketType.CreateRoomResp:
                    var createResp = packet.DeserializePayload<CreateRoomRespData>();
                    if (createResp != null)
                    {
                        PostToUI(() => OnCreateRoomResponse?.Invoke(createResp));
                    }
                    break;

                case PacketType.JoinRoomResp:
                    var joinResp = packet.DeserializePayload<JoinRoomRespData>();
                    if (joinResp != null)
                    {
                        PostToUI(() => OnJoinRoomResponse?.Invoke(joinResp));
                    }
                    break;

                case PacketType.GameStateUpdate:
                    var stateData = packet.DeserializePayload<GameStateUpdateData>();
                    if (stateData != null)
                    {
                        PostToUI(() => OnGameStateUpdated?.Invoke(stateData));
                    }
                    break;

                case PacketType.ChatMessage:
                    var chatData = packet.DeserializePayload<ChatMessageData>();
                    if (chatData != null)
                    {
                        PostToUI(() => OnChatMessageReceived?.Invoke(chatData));
                    }
                    break;

                case PacketType.OpponentDisconnected:
                    var dcData = packet.DeserializePayload<ChatMessageData>();
                    PostToUI(() => OnOpponentDisconnected?.Invoke(dcData?.Message ?? "Đối thủ đã ngắt kết nối."));
                    break;

                case PacketType.OpponentReconnecting:
                    var reconnectingData = packet.DeserializePayload<OpponentReconnectingData>();
                    if (reconnectingData != null)
                    {
                        PostToUI(() => OnOpponentReconnecting?.Invoke(reconnectingData));
                    }
                    break;

                case PacketType.OpponentReconnected:
                    var reconnectedData = packet.DeserializePayload<OpponentReconnectedData>();
                    if (reconnectedData != null)
                    {
                        PostToUI(() => OnOpponentReconnected?.Invoke(reconnectedData));
                    }
                    break;

                case PacketType.ReconnectResp:
                    var reconnectResp = packet.DeserializePayload<ReconnectRespData>();
                    if (reconnectResp != null)
                    {
                        PostToUI(() => OnReconnectResponse?.Invoke(reconnectResp));
                    }
                    break;

                case PacketType.LeaveRoomResp:
                    var leaveResp = packet.DeserializePayload<LeaveRoomRespData>();
                    if (leaveResp != null)
                    {
                        if (leaveResp.Success)
                        {
                            IsSpectator = false;
                        }
                        PostToUI(() => OnLeaveRoomResponse?.Invoke(leaveResp));
                    }
                    break;

                case PacketType.RoomList:
                    var roomListData = packet.DeserializePayload<RoomListData>();
                    if (roomListData != null)
                    {
                        PostToUI(() => OnRoomListReceived?.Invoke(roomListData));
                    }
                    break;

                case PacketType.SpectateRoomResp:
                    var specResp = packet.DeserializePayload<SpectateRoomRespData>();
                    if (specResp != null)
                    {
                        if (specResp.Success)
                        {
                            IsSpectator = true;
                        }
                        PostToUI(() => OnSpectateRoomResponse?.Invoke(specResp));
                    }
                    break;

                case PacketType.GetHistoryResp:
                    var historyResp = packet.DeserializePayload<MatchHistoryRespData>();
                    if (historyResp != null)
                    {
                        PostToUI(() => OnMatchHistoryReceived?.Invoke(historyResp.History));
                    }
                    break;

                case PacketType.GetLeaderboardResp:
                    var lbResp = packet.DeserializePayload<LeaderboardRespData>();
                    if (lbResp != null)
                    {
                        PostToUI(() => OnLeaderboardReceived?.Invoke(lbResp.TopPlayers));
                    }
                    break;

                case PacketType.EloUpdate:
                    var eloData = packet.DeserializePayload<EloUpdateData>();
                    if (eloData != null)
                    {
                        Elo = eloData.NewElo;
                        Wins = eloData.Wins;
                        Losses = eloData.Losses;
                        Draws = eloData.Draws;
                        PostToUI(() => OnEloUpdated?.Invoke(eloData));
                    }
                    break;
            }
        }

        public async Task RequestMatchHistoryAsync()
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.GetHistoryReq, new object());
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task RequestLeaderboardAsync()
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.GetLeaderboardReq, new object());
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task RequestRoomListAsync()
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.GetRoomsReq, new object());
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task SendSpectateRoomAsync(string roomId)
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.SpectateRoomReq, new SpectateRoomReqData { RoomId = roomId });
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task SendMoveAsync(int row, int col)
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.MoveReq, new MoveReqData { Row = row, Col = col });
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task CreateRoomAsync(GameMode mode, AIDifficulty difficulty, int boardSize = 15, bool blockBothEndsRule = true, int timePerTurn = 30)
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.CreateRoomReq, new CreateRoomReqData
            {
                Mode = mode,
                AIDifficulty = difficulty,
                BoardSize = boardSize,
                BlockBothEndsRule = blockBothEndsRule,
                TimePerTurn = timePerTurn
            });
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task JoinRoomAsync(string roomId)
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.JoinRoomReq, new JoinRoomReqData { RoomId = roomId });
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task SendSurrenderAsync()
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.SurrenderReq, new object());
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task SendRestartAsync()
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.RestartReq, new object());
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task SendLeaveRoomAsync()
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.LeaveRoomReq, new object());
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public async Task SendChatMessageAsync(string msg)
        {
            if (!IsConnected || _stream == null) return;
            var packet = Packet.Create(PacketType.ChatMessage, new ChatMessageData { Message = msg });
            await PacketFraming.SendPacketAsync(_stream, packet);
        }

        public void Disconnect()
        {
            _cts?.Cancel();
            try { _stream?.Close(); } catch { }
            try { _tcpClient?.Close(); } catch { }
            _tcpClient = null;
            _stream = null;
            IsSpectator = false;
            ServerHostName = string.Empty;
            ServerIp = string.Empty;
            PlayerName = "Khách (Offline)";
            Elo = 1000;
            Wins = 0;
            Losses = 0;
            Draws = 0;
        }

        private void PostToUI(Action action)
        {
            if (_syncContext != null)
            {
                _syncContext.Post(_ => action(), null);
            }
            else
            {
                action();
            }
        }
    }
}
