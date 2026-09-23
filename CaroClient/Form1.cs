using CaroProtocol;

namespace CaroClient
{
    public partial class Form1 : Form
    {
        private readonly NetworkClient _netClient;
        private char _mySymbol = 'X';
        private string _currentRoomId = string.Empty;
        private int _remainingSeconds = 30;
        private bool _isInRoom = false;
        private GameStateStatus _currentGameStatus = GameStateStatus.WaitingForOpponent;

        public Form1()
        {
            InitializeComponent();
            cmbDifficulty.SelectedIndex = 1; // Default Medium

            // Must create NetworkClient AFTER InitializeComponent so SynchronizationContext is available
            _netClient = new NetworkClient();

            // Wire up Network Events
            _netClient.OnConnectionCompleted += NetClient_OnConnectionCompleted;
            _netClient.OnCreateRoomResponse += NetClient_OnCreateRoomResponse;
            _netClient.OnJoinRoomResponse += NetClient_OnJoinRoomResponse;
            _netClient.OnLeaveRoomResponse += NetClient_OnLeaveRoomResponse;
            _netClient.OnGameStateUpdated += NetClient_OnGameStateUpdated;
            _netClient.OnChatMessageReceived += NetClient_OnChatMessageReceived;
            _netClient.OnOpponentDisconnected += NetClient_OnOpponentDisconnected;
            _netClient.OnErrorOccurred += NetClient_OnErrorOccurred;

            // Board Click Event
            boardControl1.OnCellClicked += BoardControl1_OnCellClicked;

            UpdateUIState(false);
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_netClient.IsConnected)
            {
                _netClient.Disconnect();
                UpdateUIState(false);
                lblStatusBadge.Text = "🔴 Chưa kết nối";
                lblStatusBadge.ForeColor = Color.Red;
                btnConnect.Text = "Kết nối Server";
                AppendLog("Hệ thống", "Đã ngắt kết nối khỏi Server.");
                return;
            }

            string ip = txtIp.Text.Trim();
            if (!int.TryParse(txtPort.Text.Trim(), out int port))
            {
                MessageBox.Show("Cổng Port không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên người chơi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnConnect.Enabled = false;
            lblStatusBadge.Text = "🟡 Đang kết nối...";
            lblStatusBadge.ForeColor = Color.Orange;

            await _netClient.ConnectAsync(ip, port, name);
        }

        private void NetClient_OnConnectionCompleted(bool success, string message)
        {
            btnConnect.Enabled = true;
            if (success)
            {
                lblStatusBadge.Text = "🟢 Đã kết nối";
                lblStatusBadge.ForeColor = Color.ForestGreen;
                btnConnect.Text = "Ngắt kết nối";
                UpdateUIState(true);
                AppendLog("Hệ thống", message);
            }
            else
            {
                lblStatusBadge.Text = "🔴 Thất bại";
                lblStatusBadge.ForeColor = Color.Red;
                MessageBox.Show(message, "Kết nối thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCreateRoom_Click(object sender, EventArgs e)
        {
            if (!_netClient.IsConnected) return;

            GameMode mode = radPvAI.Checked ? GameMode.PvAI : GameMode.PvP;
            AIDifficulty diff = (AIDifficulty)(cmbDifficulty.SelectedIndex + 1);

            await _netClient.CreateRoomAsync(mode, diff, 15);
        }

        private async void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (!_netClient.IsConnected) return;

            string roomId = txtRoomId.Text.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(roomId))
            {
                MessageBox.Show("Vui lòng nhập Mã phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await _netClient.JoinRoomAsync(roomId);
        }

        private void NetClient_OnCreateRoomResponse(CreateRoomRespData data)
        {
            if (data.Success)
            {
                _mySymbol = data.PlayerSymbol;
                _currentRoomId = data.RoomId;
                _isInRoom = true;
                _currentGameStatus = GameStateStatus.WaitingForOpponent;
                lblRoomIdInfo.Text = $"🏠 Phòng: {data.RoomId}";
                lblPlayersInfo.Text = $"⚔️ X: {_netClient.PlayerName} (Bạn)  VS  O: ---";
                txtRoomId.Text = data.RoomId;
                EnterRoomUI();
                AppendLog("Hệ thống", data.Message);
                AppendLog("Hệ thống", $"📋 Mã phòng của bạn là: {data.RoomId} — Hãy gửi mã này cho bạn bè để họ vào chơi!");
            }
            else
            {
                MessageBox.Show(data.Message, "Lỗi tạo phòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NetClient_OnJoinRoomResponse(JoinRoomRespData data)
        {
            if (data.Success)
            {
                _mySymbol = data.PlayerSymbol;
                _currentRoomId = data.RoomId;
                _isInRoom = true;
                _currentGameStatus = GameStateStatus.Playing;
                lblRoomIdInfo.Text = $"🏠 Phòng: {data.RoomId}";
                lblPlayersInfo.Text = $"⚔️ X: {data.OpponentName}  VS  O: {_netClient.PlayerName} (Bạn)";
                EnterRoomUI();
                // Hide leave button when game starts immediately
                btnLeaveRoom.Visible = false;
                AppendLog("Hệ thống", data.Message);
            }
            else
            {
                MessageBox.Show(data.Message, "Lỗi tham gia phòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLeaveRoom_Click(object sender, EventArgs e)
        {
            if (!_netClient.IsConnected) return;

            var confirm = MessageBox.Show("Bạn có chắc muốn thoát phòng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                await _netClient.SendLeaveRoomAsync();
            }
        }

        private void NetClient_OnLeaveRoomResponse(LeaveRoomRespData data)
        {
            if (data.Success)
            {
                ExitRoomUI();
                AppendLog("Hệ thống", data.Message);
            }
            else
            {
                MessageBox.Show(data.Message, "Không thể thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NetClient_OnGameStateUpdated(GameStateUpdateData data)
        {
            boardControl1.UpdateBoardState(data, _mySymbol);
            lblTurnInfo.Text = $"👉 {data.StatusMessage}";
            _currentGameStatus = data.Status;

            if (data.Status == GameStateStatus.Playing)
            {
                ResetTimer();
                // Cannot leave during active game
                btnLeaveRoom.Visible = false;
            }
            else if (data.Status == GameStateStatus.GameOver)
            {
                turnTimer.Stop();
                lblTimer.Text = "⏱️ Thời gian: KẾT THÚC";
                // Can leave after game ends
                btnLeaveRoom.Visible = true;
                AppendLog("Hệ thống", data.StatusMessage);
            }
        }

        private async void BoardControl1_OnCellClicked(int row, int col)
        {
            await _netClient.SendMoveAsync(row, col);
        }

        private async void btnSurrender_Click(object sender, EventArgs e)
        {
            if (!_netClient.IsConnected) return;

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đầu hàng trận đấu này?", "Xác nhận đầu hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                await _netClient.SendSurrenderAsync();
            }
        }

        private async void btnRestart_Click(object sender, EventArgs e)
        {
            if (!_netClient.IsConnected) return;

            await _netClient.SendRestartAsync();
        }

        private async void btnSendChat_Click(object sender, EventArgs e)
        {
            await SendChatMessage();
        }

        private async void txtChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await SendChatMessage();
            }
        }

        private async Task SendChatMessage()
        {
            string msg = txtChatInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(msg) || !_netClient.IsConnected) return;

            txtChatInput.Clear();
            await _netClient.SendChatMessageAsync(msg);
        }

        private void NetClient_OnChatMessageReceived(ChatMessageData chat)
        {
            AppendLog(chat.SenderName, chat.Message);
        }

        private void NetClient_OnOpponentDisconnected(string message)
        {
            turnTimer.Stop();
            // Opponent left/disconnected -> allow leaving room
            _currentGameStatus = GameStateStatus.GameOver;
            btnLeaveRoom.Visible = true;
            AppendLog("Hệ thống", message);
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NetClient_OnErrorOccurred(string errorMsg)
        {
            turnTimer.Stop();
            lblStatusBadge.Text = "🔴 Lỗi kết nối";
            lblStatusBadge.ForeColor = Color.Red;
            btnConnect.Text = "Kết nối Server";
            UpdateUIState(false);
            AppendLog("Lỗi", errorMsg);
        }

        private void turnTimer_Tick(object sender, EventArgs e)
        {
            _remainingSeconds--;
            if (_remainingSeconds <= 0)
            {
                _remainingSeconds = 0;
                lblTimer.Text = "⏱️ Hết giờ lượt đi!";
            }
            else
            {
                lblTimer.Text = $"⏱️ Thời gian: {_remainingSeconds} Giây";
            }
        }

        private void ResetTimer()
        {
            _remainingSeconds = 30;
            lblTimer.Text = "⏱️ Thời gian: 30 Giây";
            turnTimer.Stop();
            turnTimer.Start();
        }

        private void radPvP_CheckedChanged(object sender, EventArgs e)
        {
            cmbDifficulty.Enabled = false;
        }

        private void radPvAI_CheckedChanged(object sender, EventArgs e)
        {
            cmbDifficulty.Enabled = radPvAI.Checked;
        }

        /// <summary>
        /// Called when entering a room (create or join). Disables room selection controls.
        /// </summary>
        private void EnterRoomUI()
        {
            btnCreateRoom.Enabled = false;
            btnJoinRoom.Enabled = false;
            txtRoomId.ReadOnly = true;
            radPvP.Enabled = false;
            radPvAI.Enabled = false;
            cmbDifficulty.Enabled = false;
            // Show leave button (can leave while waiting)
            btnLeaveRoom.Visible = true;
        }

        /// <summary>
        /// Called when leaving a room. Re-enables room selection controls and resets game state.
        /// </summary>
        private void ExitRoomUI()
        {
            _isInRoom = false;
            _currentRoomId = string.Empty;
            _currentGameStatus = GameStateStatus.WaitingForOpponent;
            btnCreateRoom.Enabled = true;
            btnJoinRoom.Enabled = true;
            txtRoomId.ReadOnly = false;
            txtRoomId.Clear();
            radPvP.Enabled = true;
            radPvAI.Enabled = true;
            cmbDifficulty.Enabled = radPvAI.Checked;
            btnLeaveRoom.Visible = false;
            lblRoomIdInfo.Text = "🏠 Phòng: ---";
            lblPlayersInfo.Text = "⚔️ X: ---  VS  O: ---";
            lblTurnInfo.Text = "👉 Lượt: ---";
            lblTimer.Text = "⏱️ Thời gian: 30 Giây";
            turnTimer.Stop();
            boardControl1.InitEmptyBoard(15);
        }

        private void UpdateUIState(bool connected)
        {
            grpMode.Enabled = connected;
            grpGameStatus.Enabled = connected;
            grpChat.Enabled = connected;
            txtIp.Enabled = !connected;
            txtPort.Enabled = !connected;
            txtName.Enabled = !connected;
            cmbDifficulty.Enabled = connected && radPvAI.Checked;

            if (!connected)
            {
                ExitRoomUI();
            }
        }

        private void AppendLog(string sender, string message)
        {
            rtbChatLog.SelectionStart = rtbChatLog.TextLength;
            rtbChatLog.SelectionLength = 0;

            Color timeColor = Color.Gray;
            Color nameColor = sender == "Hệ thống" ? Color.Purple : (sender == "Lỗi" ? Color.Red : Color.DarkBlue);

            rtbChatLog.SelectionColor = timeColor;
            rtbChatLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ");

            rtbChatLog.SelectionColor = nameColor;
            rtbChatLog.SelectionFont = new Font(rtbChatLog.Font, FontStyle.Bold);
            rtbChatLog.AppendText($"{sender}: ");

            rtbChatLog.SelectionColor = Color.Black;
            rtbChatLog.SelectionFont = new Font(rtbChatLog.Font, FontStyle.Regular);
            rtbChatLog.AppendText($"{message}\n");

            rtbChatLog.ScrollToCaret();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _netClient.Disconnect();
        }
    }
}
