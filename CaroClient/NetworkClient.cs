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
        public string PlayerName { get; private set; } = string.Empty;
        public string ServerHostName { get; private set; } = string.Empty;
        public bool IsSpectator { get; set; } = false;

        // --- UI Events ---
        public event Action<bool, string>? OnConnectionCompleted;
        public event Action<CreateRoomRespData>? OnCreateRoomResponse;
        public event Action<JoinRoomRespData>? OnJoinRoomResponse;
        public event Action<LeaveRoomRespData>? OnLeaveRoomResponse;
        public event Action<GameStateUpdateData>? OnGameStateUpdated;
        public event Action<ChatMessageData>? OnChatMessageReceived;
        public event Action<string>? OnOpponentDisconnected;
        public event Action<string>? OnErrorOccurred;
        public event Action<List<PlayerInfoData>>? OnPlayerListReceived;
        public event Action<RoomListData>? OnRoomListReceived;
        public event Action<SpectateRoomRespData>? OnSpectateRoomResponse;

        public NetworkClient()
        {
            _syncContext = SynchronizationContext.Current;
        }

        public async Task ConnectAsync(string ip, int port, string playerName)
        {
            if (_syncContext == null && SynchronizationContext.Current != null)
            {
                _syncContext = SynchronizationContext.Current;
            }

            PlayerName = playerName;
            try
            {
                _tcpClient = new TcpClient();
                using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                await _tcpClient.ConnectAsync(ip, port, timeoutCts.Token);
                _stream = _tcpClient.GetStream();
                _cts = new CancellationTokenSource();

                // Send ConnectReq
                var packet = Packet.Create(PacketType.ConnectReq, new ConnectReqData { PlayerName = playerName });
                await PacketFraming.SendPacketAsync(_stream, packet, _cts.Token);

                // Start Listen Loop
                _ = Task.Run(() => ListenLoopAsync(_cts.Token));
            }
            catch (Exception ex)
            {
                Disconnect();
                PostToUI(() => OnConnectionCompleted?.Invoke(false, $"Không thể kết nối đến Server ({ip}:{port}): {ex.Message}\n\nLưu ý: Hãy chắc chắn bạn đã chạy dự án CaroServer!"));
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
                case PacketType.ConnectResp:
                    var connResp = packet.DeserializePayload<ConnectRespData>();
                    if (connResp != null)
                    {
                        PlayerId = connResp.PlayerId;
                        ServerHostName = connResp.ServerHostName;
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
            }
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
