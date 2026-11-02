using CaroProtocol;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace CaroClient
{
    public partial class Form1 : Form
    {
        private NetworkClient _netClient = null!;
        private char _mySymbol = 'X';
        private string _currentRoomId = string.Empty;
        private int _remainingSeconds = 30;
        private int _timePerTurn = 30;
        private char _currentTurnSymbol = 'X';
        private bool _isInRoom = false;
        private GameStateStatus _currentGameStatus = GameStateStatus.WaitingForOpponent;
        private GameMode _currentRoomMode = GameMode.PvP;
        private List<RoomInfoData> _currentRoomList = new List<RoomInfoData>();

        // Offline AI Game State
        private bool _isOfflineGame = false;
        private GameEngine? _offlineEngine;
        private AIDifficulty _selectedDifficulty = AIDifficulty.Medium;
        private bool _selectedBlockBothEnds = true;
        private int _selectedTimeLimit = 30;
        private bool _isTimeLimitMode = true;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
            turnTimer.Interval = 1000;
            turnTimer.Tick += turnTimer_Tick;

            // Wire up selection cards & pills
            cardModePvP.Click += (s, e) => SelectGameMode(false);
            cardModeAI.Click += (s, e) => SelectGameMode(true);
            cardTimeTurn.Click += (s, e) => SelectTimeMode(true);
            cardTimeFree.Click += (s, e) => SelectTimeMode(false);

            pillTime15.Click += (s, e) => SelectTimeSeconds(15);
            pillTime30.Click += (s, e) => SelectTimeSeconds(30);
            pillTime60.Click += (s, e) => SelectTimeSeconds(60);

            pillAiEasy.Click += (s, e) => SelectAiDifficulty(AIDifficulty.Easy);
            pillAiMedium.Click += (s, e) => SelectAiDifficulty(AIDifficulty.Medium);
            pillAiHard.Click += (s, e) => SelectAiDifficulty(AIDifficulty.Hard);

            pillRuleStandard.Click += (s, e) => SelectRule(true);
            pillRuleFree.Click += (s, e) => SelectRule(false);

            RelayoutCreateBox();
            this.Shown += (s, e) => RelayoutCreateBox();

            _netClient = new NetworkClient();
            WireUpNetClientEvents();

            // Detect and display initial info
            InitLanInformation();

            // Setup Tables
            InitFeaturedRoomsTable();
            InitMatchHistoryTable();
            InitLeaderboardTable();
            UpdateRoomListUI(new List<RoomInfoData>());

            // Wire up Connected Players owner-draw
            lstConnectedPlayers.DrawItem += lstConnectedPlayers_DrawItem;

            // Board Click Event
            boardControl1.OnCellClicked += BoardControl1_OnCellClicked;

            UpdateUIState(false);
            UpdateServerAndPlayerInfo(false);
            SwitchToView(pnlHomeView, btnNavHome);
        }

        private void WireUpNetClientEvents()
        {
            _netClient.OnConnectionCompleted += NetClient_OnConnectionCompleted;
            _netClient.OnLoginCompleted += NetClient_OnLoginCompleted;
            _netClient.OnRegisterCompleted += NetClient_OnRegisterCompleted;
            _netClient.OnCreateRoomResponse += NetClient_OnCreateRoomResponse;
            _netClient.OnJoinRoomResponse += NetClient_OnJoinRoomResponse;
            _netClient.OnLeaveRoomResponse += NetClient_OnLeaveRoomResponse;
            _netClient.OnGameStateUpdated += NetClient_OnGameStateUpdated;
            _netClient.OnChatMessageReceived += NetClient_OnChatMessageReceived;
            _netClient.OnOpponentDisconnected += NetClient_OnOpponentDisconnected;
            _netClient.OnOpponentReconnecting += NetClient_OnOpponentReconnecting;
            _netClient.OnOpponentReconnected += NetClient_OnOpponentReconnected;
            _netClient.OnReconnectResponse += NetClient_OnReconnectResponse;
            _netClient.OnErrorOccurred += NetClient_OnErrorOccurred;
            _netClient.OnPlayerListReceived += NetClient_OnPlayerListReceived;
            _netClient.OnRoomListReceived += NetClient_OnRoomListReceived;
            _netClient.OnSpectateRoomResponse += NetClient_OnSpectateRoomResponse;
            _netClient.OnMatchHistoryReceived += NetClient_OnMatchHistoryReceived;
            _netClient.OnLeaderboardReceived += NetClient_OnLeaderboardReceived;
            _netClient.OnEloUpdated += NetClient_OnEloUpdated;
        }

        private void InitLanInformation()
        {
            string localIp = GetLocalIpAddress();
            txtIp.Text = localIp;
            txtPort.Text = "8888";
            txtRegIp.Text = localIp;
            txtRegPort.Text = "8888";
            lblProfileName.Text = string.IsNullOrWhiteSpace(txtName.Text) ? "Khách (Offline)" : txtName.Text.Trim();
            lblProfileLevel.Text = "Chế độ Offline";
            lblProfileXp.Text = "Chơi với Máy";
            btnLogout.Visible = false;
            UpdateProfileViewUI();
        }

        private string GetLocalIpAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip))
                    {
                        return ip.ToString();
                    }
                }
            }
            catch { }
            return "192.168.1.12";
        }

        private void InitFeaturedRoomsTable()
        {
            dgvFeaturedRooms.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn
            {
                Name = "RoomId",
                HeaderText = "Mã phòng",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFeaturedRooms.Columns.Add(colId);

            var colMode = new DataGridViewTextBoxColumn
            {
                Name = "Mode",
                HeaderText = "Chế độ",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colMode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFeaturedRooms.Columns.Add(colMode);

            var colPlayers = new DataGridViewTextBoxColumn
            {
                Name = "Players",
                HeaderText = "Người chơi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colPlayers.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colPlayers.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvFeaturedRooms.Columns.Add(colPlayers);

            var colSpectators = new DataGridViewTextBoxColumn
            {
                Name = "Spectators",
                HeaderText = "Xem 👁",
                Width = 60,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colSpectators.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSpectators.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFeaturedRooms.Columns.Add(colSpectators);

            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Trạng thái",
                Width = 85,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFeaturedRooms.Columns.Add(colStatus);

            var colAction = new DataGridViewTextBoxColumn
            {
                Name = "Action",
                HeaderText = "Thao tác",
                Width = 95,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAction.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFeaturedRooms.Columns.Add(colAction);
        }

        private void NetClient_OnRoomListReceived(RoomListData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnRoomListReceived(data)));
                return;
            }

            _currentRoomList = data.Rooms ?? new List<RoomInfoData>();
            UpdateRoomListUI(_currentRoomList);
        }

        private void UpdateRoomListUI(List<RoomInfoData> rooms)
        {
            dgvFeaturedRooms.Rows.Clear();

            if (_netClient == null || !_netClient.IsConnected)
            {
                lblNoRooms.Text = "Chưa kết nối đến máy chủ";
                lblNoRooms.Visible = true;
                dgvFeaturedRooms.Visible = false;
                return;
            }

            if (rooms == null || rooms.Count == 0)
            {
                lblNoRooms.Text = "Hiện chưa có phòng nào đang mở.\nHãy bấm '+ Tạo phòng' ở menu bên trái để bắt đầu trận đấu!";
                lblNoRooms.Visible = true;
                dgvFeaturedRooms.Visible = false;
                return;
            }

            lblNoRooms.Visible = false;
            dgvFeaturedRooms.Visible = true;

            foreach (var r in rooms)
            {
                string modeStr = (r.Mode == GameMode.PvAI) ? "Đấu AI" : "PvP 1v1";
                string playersStr;
                if (r.Mode == GameMode.PvAI)
                {
                    playersStr = $"{r.Player1Name} vs Máy";
                }
                else
                {
                    playersStr = !string.IsNullOrWhiteSpace(r.Player2Name)
                        ? $"{r.Player1Name} vs {r.Player2Name}"
                        : $"{r.Player1Name} (Đang chờ)";
                }

                string statusStr = r.Status switch
                {
                    GameStateStatus.WaitingForOpponent => "Đang chờ",
                    GameStateStatus.Playing => "Đang chơi",
                    GameStateStatus.GameOver => "Kết thúc",
                    _ => "Đang chờ"
                };

                string actionStr = r.CanJoinAsPlayer ? "▶ Tham gia" : "👁 Xem trận";

                int rowIndex = dgvFeaturedRooms.Rows.Add(
                    r.RoomId,
                    modeStr,
                    playersStr,
                    $"{r.SpectatorCount}",
                    statusStr,
                    actionStr
                );

                var row = dgvFeaturedRooms.Rows[rowIndex];

                // Styling Status
                if (statusStr == "Đang chờ")
                {
                    row.Cells["Status"].Style.ForeColor = UITheme.AccentGreen;
                }
                else if (statusStr == "Đang chơi")
                {
                    row.Cells["Status"].Style.ForeColor = UITheme.AccentOrange;
                }
                else
                {
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(160, 180, 215);
                }

                // Styling Action
                if (r.CanJoinAsPlayer)
                {
                    row.Cells["Action"].Style.ForeColor = UITheme.AccentCyan;
                    row.Cells["Action"].Style.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                }
                else
                {
                    row.Cells["Action"].Style.ForeColor = UITheme.AccentOrange;
                    row.Cells["Action"].Style.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                }
            }
        }

        private async void btnRefreshRooms_Click(object? sender, EventArgs e)
        {
            if (!_netClient.IsConnected)
            {
                MessageBox.Show("Vui lòng kết nối Server trước khi làm mới danh sách phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            await _netClient.RequestRoomListAsync();
        }

        private void dgvFeaturedRooms_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvFeaturedRooms.Rows.Count) return;
            HandleRoomAction(e.RowIndex);
        }

        private void dgvFeaturedRooms_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvFeaturedRooms.Rows.Count) return;
            HandleRoomAction(e.RowIndex);
        }

        private async void HandleRoomAction(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _currentRoomList.Count) return;
            var room = _currentRoomList[rowIndex];

            if (!_netClient.IsConnected)
            {
                SwitchToView(pnlSettingsView, btnNavSettings);
                MessageBox.Show("Vui lòng Kết nối Server trước khi vào phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isInRoom)
            {
                MessageBox.Show("Bạn đang ở trong phòng khác. Hãy thoát phòng hiện tại trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtRoomId.Text = room.RoomId;

            if (room.CanJoinAsPlayer)
            {
                _currentRoomMode = room.Mode;
                await _netClient.JoinRoomAsync(room.RoomId);
            }
            else
            {
                await _netClient.SendSpectateRoomAsync(room.RoomId);
            }
        }

        private void NetClient_OnSpectateRoomResponse(SpectateRoomRespData resp)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnSpectateRoomResponse(resp)));
                return;
            }

            if (resp.Success)
            {
                _isInRoom = true;
                _mySymbol = ' '; // Khán giả không có quân đi
                _currentRoomId = resp.RoomId;
                _currentRoomMode = resp.Mode;
                _timePerTurn = resp.TimePerTurn;
                _currentGameStatus = resp.CurrentState?.Status ?? GameStateStatus.Playing;

                string ruleText = resp.BlockBothEndsRule ? "Chặn 2 đầu" : "Tự do";
                string timeText = resp.TimePerTurn > 0 ? $"{resp.TimePerTurn}s" : "Vô hạn";
                lblRoomIdInfo.Text = $"Phòng: {resp.RoomId} (Đang xem 👁) ({ruleText} | {timeText})";
                lblPlayersInfo.Text = (resp.Mode == GameMode.PvAI)
                    ? $"X: {resp.Player1Name}   VS   O: Máy (AI)"
                    : $"X: {resp.Player1Name}   VS   O: {resp.Player2Name}";
                lblTurnInfo.Text = $"● {resp.CurrentState?.StatusMessage ?? resp.Message}";

                txtRoomId.Text = resp.RoomId;
                EnterRoomUI();

                if (resp.CurrentState != null)
                {
                    _currentTurnSymbol = resp.CurrentState.CurrentTurnSymbol;
                    boardControl1.UpdateBoardState(resp.CurrentState, ' ');
                    if (resp.CurrentState.Status == GameStateStatus.Playing)
                    {
                        ResetTimer(resp.TimePerTurn);
                    }
                }

                UpdateGameActionButtons();
                AppendLog("Hệ thống", resp.Message);
                SwitchToView(pnlGameView, btnNavGame);
            }
            else
            {
                MessageBox.Show(resp.Message, "Không thể xem phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNavGame_Click(object? sender, EventArgs e)
        {
            if (!_isInRoom)
            {
                MessageBox.Show("Bạn chưa ở trong phòng nào. Hãy Tạo phòng hoặc Chọn phòng để bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SwitchToView(pnlGameView, btnNavGame);
        }

        private void SwitchToView(Panel targetView, CyberButton activeNav)
        {
            pnlHomeView.Visible = (targetView == pnlHomeView);
            pnlGameView.Visible = (targetView == pnlGameView);
            pnlCreateRoomView.Visible = (targetView == pnlCreateRoomView);
            pnlJoinRoomView.Visible = (targetView == pnlJoinRoomView);
            pnlHistoryView.Visible = (targetView == pnlHistoryView);
            pnlLeaderboardView.Visible = (targetView == pnlLeaderboardView);
            pnlSettingsView.Visible = (targetView == pnlSettingsView);

            // Phần chat chỉ hiện ở phần bàn cờ khi 2 người chơi online đánh với nhau (hoặc đang xem phòng)
            bool showChat = (targetView == pnlGameView) && !_isOfflineGame && (_isInRoom || _netClient.IsSpectator);
            pnlRightBar.Visible = showChat;

            // Cập nhật trạng thái hiển thị của các nút menu thanh bên
            CyberButton[] navs = { btnNavHome, btnNavGame, btnNavCreate, btnNavJoin, btnNavLeaderboard, btnNavHistory };
            foreach (var btn in navs)
            {
                bool isActive = (btn == activeNav);
                btn.IsPrimaryGradient = isActive;
                btn.NormalColor = isActive ? UITheme.AccentBlue : Color.FromArgb(14, 24, 46);
                btn.Invalidate();
            }

            if (!_netClient.IsConnected)
            {
                btnNavSettings.Text = "⚡ ĐĂNG NHẬP";
                btnNavSettings.IsPrimaryGradient = (activeNav == btnNavSettings);
                btnNavSettings.NormalColor = (activeNav == btnNavSettings) ? UITheme.AccentBlue : Color.FromArgb(14, 24, 46);
            }
            else
            {
                btnNavSettings.Text = "⎋ Đăng xuất";
                btnNavSettings.IsPrimaryGradient = false;
                btnNavSettings.NormalColor = Color.FromArgb(36, 20, 32);
            }
            btnNavSettings.Invalidate();

            if (targetView == pnlCreateRoomView)
            {
                RelayoutCreateBox();
            }

            // Tự động làm mới danh sách phòng khi vào màn hình Tham gia phòng
            if (targetView == pnlJoinRoomView && _netClient != null && _netClient.IsConnected)
            {
                _ = _netClient.RequestRoomListAsync();
            }

            // Tự động làm mới hồ sơ & lịch sử đấu khi vào màn hình Hồ sơ cá nhân
            if (targetView == pnlHistoryView)
            {
                UpdateProfileViewUI();
                if (_netClient != null && _netClient.IsConnected)
                {
                    lblNoHistory.Text = "Đang tải dữ liệu lịch sử thi đấu...";
                    lblNoHistory.Visible = true;
                    dgvMatchHistory.Visible = false;
                    _ = _netClient.RequestMatchHistoryAsync();
                }
                else
                {
                    lblNoHistory.Text = "Bạn đang chơi ở chế độ Ngoại tuyến (Khách).\nHãy bấm nút '⚡ ĐĂNG NHẬP' ở góc dưới bên trái để lưu trữ lịch sử thi đấu online!";
                    lblNoHistory.Visible = true;
                    dgvMatchHistory.Visible = false;
                }
            }

            // Tự động làm mới bảng xếp hạng khi vào màn hình Bảng xếp hạng
            if (targetView == pnlLeaderboardView)
            {
                if (_netClient != null && _netClient.IsConnected)
                {
                    lblNoLeaderboard.Text = "Đang tải dữ liệu Bảng xếp hạng...";
                    lblNoLeaderboard.Visible = true;
                    dgvLeaderboard.Visible = false;
                    _ = _netClient.RequestLeaderboardAsync();
                }
                else
                {
                    lblNoLeaderboard.Text = "Bạn chưa kết nối Server.\nVui lòng bấm nút '⚡ ĐĂNG NHẬP' ở góc dưới bên trái để xem Bảng xếp hạng!";
                    lblNoLeaderboard.Visible = true;
                    dgvLeaderboard.Visible = false;
                }
            }
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void pnlLogoBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var pen = new Pen(Color.FromArgb(0, 194, 255), 1.8f))
            {
                g.DrawRectangle(pen, 2, 2, 27, 27);
            }
            using (var font = new Font("Segoe UI", 7.5f, FontStyle.Bold))
            {
                using (var brushX = new SolidBrush(Color.FromArgb(255, 46, 99)))
                {
                    g.DrawString("X", font, brushX, 5, 2);
                    g.DrawString("O", font, brushX, 16, 14);
                }
                using (var brushO = new SolidBrush(Color.FromArgb(0, 194, 255)))
                {
                    g.DrawString("O", font, brushO, 16, 2);
                    g.DrawString("X", font, brushO, 5, 14);
                }
            }
        }

        // =============================================================
        // Network Connection & Auth Logic
        // =============================================================

        private void SwitchAuthTab(bool isLogin)
        {
            pnlAuthBodyLogin.Visible = isLogin;
            pnlAuthBodyRegister.Visible = !isLogin;

            if (isLogin)
            {
                btnTabLogin.IsPrimaryGradient = true;
                btnTabLogin.NormalColor = Color.Empty;
                btnTabRegister.IsPrimaryGradient = false;
                btnTabRegister.NormalColor = Color.FromArgb(14, 25, 48);
                txtName.Focus();
            }
            else
            {
                btnTabLogin.IsPrimaryGradient = false;
                btnTabLogin.NormalColor = Color.FromArgb(14, 25, 48);
                btnTabRegister.IsPrimaryGradient = true;
                txtRegIp.Text = txtIp.Text;
                txtRegPort.Text = txtPort.Text;
                txtRegName.Focus();
            }
        }

        private void HandleNavSettingsClick()
        {
            if (_netClient.IsConnected)
            {
                var ask = MessageBox.Show("Bạn đang đăng nhập. Bạn có muốn đăng xuất để chuyển về chế độ Khách (Offline)?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask == DialogResult.Yes)
                {
                    btnLogout_Click(this, EventArgs.Empty);
                }
            }
            else
            {
                SwitchToView(pnlSettingsView, btnNavSettings);
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_netClient.IsConnected)
            {
                btnLogout_Click(sender, e);
                return;
            }

            string ip = txtIp.Text.Trim();
            if (string.IsNullOrWhiteSpace(ip)) ip = "127.0.0.1";
            int port = 8888;
            if (int.TryParse(txtPort.Text.Trim(), out int customPort))
            {
                port = customPort;
            }

            string username = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập (Username).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu (Password).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            btnConnect.Enabled = false;
            lblStatusBadge.Text = "● Đang kết nối & đăng nhập...";
            lblStatusBadge.ForeColor = Color.FromArgb(255, 145, 0);

            await _netClient.ConnectAndLoginAsync(ip, port, username, password);
        }

        private string _lastRegUsername = "";
        private string _lastRegPassword = "";
        private string _lastRegIp = "";
        private int _lastRegPort = 8888;

        private async void btnRegister_Click(object? sender, EventArgs e)
        {
            string ip = txtRegIp.Text.Trim();
            if (string.IsNullOrWhiteSpace(ip)) ip = "127.0.0.1";
            int port = 8888;
            if (int.TryParse(txtRegPort.Text.Trim(), out int customPort))
            {
                port = customPort;
            }

            string username = txtRegName.Text.Trim();
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản muốn tạo (ít nhất 3 ký tự).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegName.Focus();
                return;
            }

            string password = txtRegPassword.Text;
            if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu (ít nhất 4 ký tự).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegPassword.Focus();
                return;
            }

            string confirm = txtRegConfirmPassword.Text;
            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp! Vui lòng nhập lại chính xác.", "Xác nhận không khớp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegConfirmPassword.Focus();
                return;
            }

            _lastRegUsername = username;
            _lastRegPassword = password;
            _lastRegIp = ip;
            _lastRegPort = port;

            btnRegister.Enabled = false;
            lblRegStatusBadge.Text = "● Đang gửi yêu cầu tạo tài khoản lên Server...";
            lblRegStatusBadge.ForeColor = Color.FromArgb(255, 145, 0);

            await _netClient.ConnectAndRegisterAsync(ip, port, username, password);
        }

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            if (_netClient.IsConnected)
            {
                _netClient.Disconnect();
            }

            // Xóa mật khẩu trong các ô nhập
            txtPassword.Text = "";
            txtRegPassword.Text = "";
            txtRegConfirmPassword.Text = "";

            // Xóa sạch bảng lịch sử thi đấu và bảng xếp hạng trên giao diện
            dgvMatchHistory.Rows.Clear();
            lblNoHistory.Text = "Bạn đang ở chế độ Ngoại tuyến (Khách).\nHãy bấm nút '⚡ ĐĂNG NHẬP' ở góc dưới bên trái để lưu trữ lịch sử thi đấu online!";
            lblNoHistory.Visible = true;
            dgvMatchHistory.Visible = false;

            dgvLeaderboard.Rows.Clear();
            lblNoLeaderboard.Text = "Bạn chưa kết nối Server.\nVui lòng bấm nút '⚡ ĐĂNG NHẬP' ở góc dưới bên trái để xem Bảng xếp hạng!";
            lblNoLeaderboard.Visible = true;
            dgvLeaderboard.Visible = false;

            UpdateUIState(false);
            UpdateServerAndPlayerInfo(false);
            UpdateProfileViewUI();
            UpdateRoomListUI(new List<RoomInfoData>());

            lblStatusBadge.Text = "● Chưa kết nối";
            lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
            btnConnect.Text = "⚡ KẾT NỐI & ĐĂNG NHẬP";
            btnConnect.Enabled = true;

            btnNavSettings.Text = "⚡ ĐĂNG NHẬP";
            btnNavSettings.IsPrimaryGradient = true;
            btnNavSettings.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavSettings.HoverColor = Color.FromArgb(24, 42, 80);

            btnLogout.Visible = false;
            SwitchToView(pnlHomeView, btnNavHome);
        }

        private void NetClient_OnLoginCompleted(LoginRespData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnLoginCompleted(data)));
                return;
            }

            btnConnect.Enabled = true;

            if (data.Success)
            {
                lblStatusBadge.Text = $"● Đã kết nối: {data.Username}";
                lblStatusBadge.ForeColor = Color.FromArgb(0, 230, 118);
                btnConnect.Text = "Ngắt kết nối";

                btnNavSettings.Text = "⎋ Đăng xuất";
                btnNavSettings.IsPrimaryGradient = false;
                btnNavSettings.NormalColor = Color.FromArgb(36, 20, 32);
                btnNavSettings.HoverColor = Color.FromArgb(200, 40, 70);

                UpdateUIState(true);
                UpdateServerAndPlayerInfo(true);
                UpdateProfileViewUI();

                // Chuyển tới màn hình Hồ sơ cá nhân & Lịch sử đấu
                SwitchToView(pnlHistoryView, btnNavHistory);
                _ = _netClient.RequestRoomListAsync();
            }
            else
            {
                UpdateServerAndPlayerInfo(false);
                UpdateProfileViewUI();
                UpdateRoomListUI(new List<RoomInfoData>());
                lblStatusBadge.Text = "🔴 " + data.Message;
                lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
                MessageBox.Show(data.Message, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void NetClient_OnRegisterCompleted(RegisterRespData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnRegisterCompleted(data)));
                return;
            }

            btnRegister.Enabled = true;
            if (data.Success)
            {
                lblRegStatusBadge.Text = "● Đăng ký thành công! Đang tự động đăng nhập...";
                lblRegStatusBadge.ForeColor = Color.FromArgb(0, 230, 118);

                txtName.Text = _lastRegUsername;
                txtPassword.Text = _lastRegPassword;
                txtIp.Text = _lastRegIp;
                txtPort.Text = _lastRegPort.ToString();

                MessageBox.Show($"{data.Message}\nHệ thống sẽ tự động đăng nhập ngay bây giờ!", "Đăng ký thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tự động chuyển tab đăng nhập và thực hiện đăng nhập
                SwitchAuthTab(true);
                btnConnect.Enabled = false;
                lblStatusBadge.Text = "● Đang tự động kết nối & đăng nhập...";
                lblStatusBadge.ForeColor = Color.FromArgb(255, 145, 0);

                await _netClient.ConnectAndLoginAsync(_lastRegIp, _lastRegPort, _lastRegUsername, _lastRegPassword);
            }
            else
            {
                lblRegStatusBadge.Text = "🔴 " + data.Message;
                lblRegStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
                MessageBox.Show(data.Message, "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateProfileViewUI()
        {
            if (_netClient.IsConnected)
            {
                string uname = _netClient.PlayerName;
                int elo = _netClient.Elo;
                int wins = _netClient.Wins;
                int losses = _netClient.Losses;
                int draws = _netClient.Draws;
                int total = wins + losses + draws;
                int winRate = total > 0 ? (int)Math.Round((double)wins / total * 100) : 0;

                lblProfileAvatar.Text = string.IsNullOrEmpty(uname) ? "👤" : uname.Substring(0, 1).ToUpper();
                lblProfileDetailName.Text = uname;
                lblProfileDetailRank.Text = $"★ {elo} ELO — {GetRankTitle(elo)}";
                lblProfileDetailStatus.Text = $"● Trực tuyến - Máy chủ LAN ({_netClient.ServerIp}:{_netClient.ServerPort})";
                lblProfileDetailStatus.ForeColor = Color.FromArgb(0, 230, 118);

                lblStatTotalVal.Text = total.ToString();
                lblStatWinsVal.Text = wins.ToString();
                lblStatLossesVal.Text = losses.ToString();
                lblStatWinRateVal.Text = $"{winRate}%";

                lblProfileName.Text = uname;
                lblProfileLevel.Text = $"★ {elo} ELO";
                lblProfileXp.Text = $"{wins}W - {losses}L - {draws}D ({winRate}%)";
            }
            else
            {
                lblProfileAvatar.Text = "👤";
                lblProfileDetailName.Text = "Khách (Offline)";
                lblProfileDetailRank.Text = "★ 1000 ELO — 🥉 Tập sự";
                lblProfileDetailStatus.Text = "● Ngoại tuyến (Chế độ Khách - Đang chơi với Máy)";
                lblProfileDetailStatus.ForeColor = Color.FromArgb(255, 145, 0);

                lblStatTotalVal.Text = "0";
                lblStatWinsVal.Text = "0";
                lblStatLossesVal.Text = "0";
                lblStatWinRateVal.Text = "0%";

                lblProfileName.Text = "Khách (Offline)";
                lblProfileLevel.Text = "Chế độ Offline";
                lblProfileXp.Text = "Chơi với Máy";
            }
        }

        private string GetRankTitle(int elo)
        {
            if (elo >= 1800) return "👑 Đại Kiện Tướng";
            if (elo >= 1500) return "💎 Kiện Tướng";
            if (elo >= 1300) return "🥇 Cao Thủ";
            if (elo >= 1100) return "🥈 Chiến Binh";
            return "🥉 Tập Sự";
        }

        private void NetClient_OnConnectionCompleted(bool success, string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnConnectionCompleted(success, message)));
                return;
            }

            btnConnect.Enabled = true;
            if (success)
            {
                lblStatusBadge.Text = "● Đã kết nối";
                lblStatusBadge.ForeColor = Color.FromArgb(0, 230, 118);
                btnConnect.Text = "Ngắt kết nối";
                lblProfileName.Text = _netClient.PlayerName;
                UpdateUIState(true);
                UpdateServerAndPlayerInfo(true);
                // Return to Home view upon connection
                SwitchToView(pnlHomeView, btnNavHome);
                _ = _netClient.RequestRoomListAsync();
            }
            else
            {
                UpdateServerAndPlayerInfo(false);
                UpdateRoomListUI(new List<RoomInfoData>());
                lblStatusBadge.Text = "🔴 Thất bại";
                lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
                MessageBox.Show(message, "Kết nối thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================================
        // Room Management Logic
        // =============================================================

        private void SelectGameMode(bool isAI)
        {
            cardModePvP.IsSelected = !isAI;
            cardModeAI.IsSelected = isAI;
            pnlAiDifficultyOptions.Visible = isAI;
            btnCreateRoom.Text = isAI && !_netClient.IsConnected ? "▶  Chơi với Máy ngay" : "▶  Tạo phòng";
            RelayoutCreateBox();
        }

        private void SelectTimeMode(bool isTurnMode)
        {
            _isTimeLimitMode = isTurnMode;
            cardTimeTurn.IsSelected = isTurnMode;
            cardTimeFree.IsSelected = !isTurnMode;
            pnlTimeLimitOptions.Visible = isTurnMode;
            _selectedTimeLimit = isTurnMode ? GetSelectedTimePillSeconds() : 0;
            RelayoutCreateBox();
        }

        private void RelayoutCreateBox()
        {
            // Vị trí Y bắt đầu dưới Card thời gian (cardTime ở Y=248, cao 74 => 248 + 74 + 12 = 334)
            int currentY = 334;

            bool showTimeLimit = _isTimeLimitMode;
            bool showAiDiff = cardModeAI.IsSelected;

            pnlTimeLimitOptions.Visible = showTimeLimit;
            if (showTimeLimit)
            {
                pnlTimeLimitOptions.Left = 28;
                pnlTimeLimitOptions.Top = currentY;
                pnlTimeLimitOptions.Width = 624;
                currentY += pnlTimeLimitOptions.Height + 12;
            }

            pnlAiDifficultyOptions.Visible = showAiDiff;
            if (showAiDiff)
            {
                pnlAiDifficultyOptions.Left = 28;
                pnlAiDifficultyOptions.Top = currentY;
                pnlAiDifficultyOptions.Width = 624;
                currentY += pnlAiDifficultyOptions.Height + 12;
            }

            secHeaderRule.Left = 28;
            secHeaderRule.Top = currentY;
            secHeaderRule.Width = 624;
            currentY += secHeaderRule.Height + 10;

            pillRuleStandard.Left = 28;
            pillRuleStandard.Top = currentY;
            pillRuleStandard.Width = 306;
            pillRuleStandard.Height = 42;

            pillRuleFree.Left = 346;
            pillRuleFree.Top = currentY;
            pillRuleFree.Width = 306;
            pillRuleFree.Height = 42;
            currentY += pillRuleStandard.Height + 22;

            btnCreateRoom.Left = 28;
            btnCreateRoom.Top = currentY;
            btnCreateRoom.Width = 430;
            btnCreateRoom.Height = 48;
            btnCreateRoom.Enabled = true;

            btnCancelCreate.Left = 470;
            btnCancelCreate.Top = currentY;
            btnCancelCreate.Width = 182;
            btnCancelCreate.Height = 48;
            currentY += btnCreateRoom.Height + 24;

            cardCreateBox.Width = 680;
            cardCreateBox.Height = currentY;
            pnlCreateRoomView_Resize(null, EventArgs.Empty);
        }

        private int GetSelectedTimePillSeconds()
        {
            if (pillTime15.IsSelected) return 15;
            if (pillTime60.IsSelected) return 60;
            return 30;
        }

        private void SelectTimeSeconds(int seconds)
        {
            pillTime15.IsSelected = (seconds == 15);
            pillTime30.IsSelected = (seconds == 30);
            pillTime60.IsSelected = (seconds == 60);
            _selectedTimeLimit = seconds;
        }

        private void SelectAiDifficulty(AIDifficulty diff)
        {
            _selectedDifficulty = diff;
            pillAiEasy.IsSelected = (diff == AIDifficulty.Easy);
            pillAiMedium.IsSelected = (diff == AIDifficulty.Medium);
            pillAiHard.IsSelected = (diff == AIDifficulty.Hard);
        }

        private void SelectRule(bool standard)
        {
            _selectedBlockBothEnds = standard;
            pillRuleStandard.IsSelected = standard;
            pillRuleFree.IsSelected = !standard;
        }

        private async void btnCreateRoom_Click(object sender, EventArgs e)
        {
            bool isAi = cardModeAI.IsSelected;
            int timeLimit = _isTimeLimitMode ? _selectedTimeLimit : 0;
            bool blockBothEnds = _selectedBlockBothEnds;
            AIDifficulty diff = _selectedDifficulty;

            // Nếu là chế độ Đấu với Máy (AI) -> Khởi động trận đấu Offline trực tiếp, không cần mạng
            if (isAi)
            {
                StartOfflineAiGame(diff, blockBothEnds, timeLimit);
                return;
            }

            // Chế độ PvP yêu cầu kết nối Server & đăng nhập
            if (!_netClient.IsConnected)
            {
                SwitchToView(pnlSettingsView, btnNavSettings);
                MessageBox.Show("Chế độ đấu Người 1v1 (PvP) yêu cầu kết nối Server và đăng nhập tài khoản.\nVui lòng nhập Server IP, Username, Password rồi bấm 'Kết nối & Đăng nhập'!", "Yêu cầu kết nối", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isOfflineGame = false;
            _currentRoomMode = isAi ? GameMode.PvAI : GameMode.PvP;
            _timePerTurn = timeLimit;

            await _netClient.CreateRoomAsync(_currentRoomMode, diff, 15, blockBothEnds, timeLimit);
        }

        private void StartOfflineAiGame(AIDifficulty diff, bool blockBothEnds, int timeLimit)
        {
            _isOfflineGame = true;
            _offlineEngine = new GameEngine(15, blockBothEndsRule: blockBothEnds);
            _selectedDifficulty = diff;
            _selectedBlockBothEnds = blockBothEnds;
            _timePerTurn = timeLimit;
            _mySymbol = 'X';
            _currentTurnSymbol = 'X';
            _currentRoomId = "OFFLINE-AI";
            _currentRoomMode = GameMode.PvAI;
            _isInRoom = true;
            _currentGameStatus = GameStateStatus.Playing;

            string diffName = diff switch
            {
                AIDifficulty.Easy => "Dễ",
                AIDifficulty.Hard => "Khó",
                _ => "Trung bình"
            };
            string ruleText = blockBothEnds ? "Chặn 2 đầu" : "Tự do";
            string timeText = timeLimit > 0 ? $"{timeLimit}s" : "Vô hạn";

            lblRoomIdInfo.Text = $"Chế độ: Đấu với Máy (Offline) ({ruleText} | {timeText})";
            string playerName = string.IsNullOrWhiteSpace(txtName.Text) ? "Bạn" : txtName.Text.Trim();
            lblPlayersInfo.Text = $"X: {playerName} (Bạn)   VS   O: Máy (AI - {diffName})";
            lblTurnInfo.Text = "● Lượt đi của bạn (X)";

            EnterRoomUI();
            boardControl1.InitEmptyBoard(15);
            boardControl1.MySymbol = 'X';
            boardControl1.IsMyTurn = true;
            boardControl1.Status = GameStateStatus.Playing;
            UpdateOfflineBoardUI();
            ResetTimer(_timePerTurn);
            UpdateGameActionButtons();

            SwitchToView(pnlGameView, btnNavGame);
        }

        private void HandleOfflineCellClicked(int row, int col)
        {
            if (_offlineEngine == null || _offlineEngine.Status != GameStateStatus.Playing) return;
            if (_offlineEngine.CurrentTurnSymbol != 'X') return; // Chưa đến lượt người chơi

            if (!_offlineEngine.MakeMove(row, col, 'X', out string err))
            {
                return;
            }

            UpdateOfflineBoardUI();

            if (_offlineEngine.Status == GameStateStatus.GameOver)
            {
                turnTimer.Stop();
                lblTimer.Text = "● KẾT THÚC";
                UpdateGameActionButtons();
                return;
            }

            // Đến lượt Máy (AI)
            _currentTurnSymbol = 'O';
            lblTurnInfo.Text = "● Máy (AI) đang suy nghĩ nước cờ...";
            ResetTimer(_timePerTurn);

            _ = Task.Run(() =>
            {
                Thread.Sleep(200);
                CaroProtocol.Point aiMove = MinimaxAI.FindBestMove(_offlineEngine, 'O', _selectedDifficulty);
                BeginInvoke(new Action(() =>
                {
                    if (!_isOfflineGame || _offlineEngine == null || _offlineEngine.Status != GameStateStatus.Playing) return;
                    _offlineEngine.MakeMove(aiMove.Row, aiMove.Col, 'O', out _);
                    UpdateOfflineBoardUI();

                    if (_offlineEngine.Status == GameStateStatus.GameOver)
                    {
                        turnTimer.Stop();
                        lblTimer.Text = "● KẾT THÚC";
                    }
                    else
                    {
                        _currentTurnSymbol = 'X';
                        ResetTimer(_timePerTurn);
                    }
                    UpdateGameActionButtons();
                }));
            });
        }

        private void UpdateOfflineBoardUI()
        {
            if (_offlineEngine == null) return;
            string statusMsg = _offlineEngine.Status switch
            {
                GameStateStatus.GameOver => (_offlineEngine.WinnerSymbol == 'X')
                    ? "Bạn đã chiến thắng xuất sắc! 🎉"
                    : (_offlineEngine.WinnerSymbol == 'O' ? "Máy (AI) đã giành chiến thắng!" : "Ván cờ Hòa!"),
                _ => (_offlineEngine.CurrentTurnSymbol == 'X') ? "Lượt đi: Bạn (X)" : "Lượt đi: Máy (AI)"
            };

            GameStateUpdateData state = new GameStateUpdateData
            {
                Board = _offlineEngine.GetBoardMatrix(),
                CurrentTurnSymbol = _offlineEngine.CurrentTurnSymbol,
                Status = _offlineEngine.Status,
                StatusMessage = statusMsg,
                WinnerSymbol = _offlineEngine.WinnerSymbol,
                WinLine = _offlineEngine.WinLine,
                LastMoveRow = _offlineEngine.LastMove?.Row ?? -1,
                LastMoveCol = _offlineEngine.LastMove?.Col ?? -1,
                TimePerTurn = _timePerTurn,
                BlockBothEndsRule = _offlineEngine.BlockBothEndsRule
            };

            boardControl1.UpdateBoardState(state, 'X');
            lblTurnInfo.Text = $"● {statusMsg}";
            _currentGameStatus = _offlineEngine.Status;
        }

        private async void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (!_netClient.IsConnected)
            {
                SwitchToView(pnlSettingsView, btnNavSettings);
                MessageBox.Show("Vui lòng kết nối Server & Đăng nhập tại mục 'Cài đặt & Kết nối' trước khi tham gia phòng đấu!", "Yêu cầu kết nối", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string roomId = txtRoomId.Text.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(roomId))
            {
                MessageBox.Show("Vui lòng nhập Mã phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currentRoomMode = GameMode.PvP;
            await _netClient.JoinRoomAsync(roomId);
        }

        private void NetClient_OnCreateRoomResponse(CreateRoomRespData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnCreateRoomResponse(data)));
                return;
            }

            if (data.Success)
            {
                _mySymbol = data.PlayerSymbol;
                _currentRoomId = data.RoomId;
                _isInRoom = true;
                _currentGameStatus = (_currentRoomMode == GameMode.PvAI) ? GameStateStatus.Playing : GameStateStatus.WaitingForOpponent;
                string ruleText = _selectedBlockBothEnds ? "Chặn 2 đầu" : "Tự do";
                string timeText = _timePerTurn > 0 ? $"{_timePerTurn}s" : "Vô hạn";
                lblRoomIdInfo.Text = $"Phòng: {data.RoomId} ({ruleText} | {timeText})";
                lblPlayersInfo.Text = (_currentRoomMode == GameMode.PvAI)
                    ? $"X: {_netClient.PlayerName} (Bạn)   VS   O: Máy (AI)"
                    : $"X: {_netClient.PlayerName} (Bạn)   VS   O: ---";
                txtRoomId.Text = data.RoomId;
                EnterRoomUI();
                UpdateGameActionButtons();
                AppendLog("Hệ thống", data.Message);
                if (_currentRoomMode == GameMode.PvP)
                {
                    AppendLog("Hệ thống", $"Mã phòng của bạn là: {data.RoomId} — Hãy chia sẻ mã này để bạn bè vào chơi!");
                }

                // Switch to game arena!
                SwitchToView(pnlGameView, btnNavGame);
            }
            else
            {
                MessageBox.Show(data.Message, "Lỗi tạo phòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NetClient_OnJoinRoomResponse(JoinRoomRespData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnJoinRoomResponse(data)));
                return;
            }

            if (data.Success)
            {
                _currentRoomMode = GameMode.PvP;
                _mySymbol = data.PlayerSymbol;
                _currentRoomId = data.RoomId;
                _isInRoom = true;
                _currentGameStatus = GameStateStatus.Playing;
                lblRoomIdInfo.Text = $"Phòng: {data.RoomId}";
                lblPlayersInfo.Text = $"X: {data.OpponentName}   VS   O: {_netClient.PlayerName} (Bạn)";
                EnterRoomUI();
                UpdateGameActionButtons();
                AppendLog("Hệ thống", data.Message);

                // Switch to game arena!
                SwitchToView(pnlGameView, btnNavGame);
            }
            else
            {
                MessageBox.Show(data.Message, "Lỗi tham gia phòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLeaveRoom_Click(object sender, EventArgs e)
        {
            if (_isOfflineGame)
            {
                if (_currentGameStatus == GameStateStatus.Playing)
                {
                    var confirm = MessageBox.Show("Bạn có chắc muốn thoát ván đấu với Máy?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                }
                _isOfflineGame = false;
                _offlineEngine = null;
                ExitRoomUI();
                SwitchToView(pnlHomeView, btnNavHome);
                return;
            }

            if (!_netClient.IsConnected) return;

            // Người xem có thể thoát phòng xem trực tiếp bất kỳ lúc nào mà không cần đầu hàng
            if (_netClient.IsSpectator)
            {
                await _netClient.SendLeaveRoomAsync();
                return;
            }

            // Nếu ván PvP đang chơi, nhắc người chơi đầu hàng để thoát
            if (_currentGameStatus == GameStateStatus.Playing && _currentRoomMode == GameMode.PvP)
            {
                var confirmSurrender = MessageBox.Show(
                    "Trận đấu đang diễn ra! Bạn có muốn đầu hàng để thoát phòng không?",
                    "Xác nhận thoát phòng",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmSurrender == DialogResult.Yes)
                {
                    await _netClient.SendSurrenderAsync();
                    await _netClient.SendLeaveRoomAsync();
                }
                return;
            }

            var confirmOnline = MessageBox.Show("Bạn có chắc muốn thoát phòng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmOnline == DialogResult.Yes)
            {
                await _netClient.SendLeaveRoomAsync();
            }
        }

        private void NetClient_OnLeaveRoomResponse(LeaveRoomRespData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnLeaveRoomResponse(data)));
                return;
            }

            if (data.Success)
            {
                ExitRoomUI();
                AppendLog("Hệ thống", data.Message);
                SwitchToView(pnlHomeView, btnNavHome);
            }
            else
            {
                MessageBox.Show(data.Message, "Không thể thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =============================================================
        // Game Logic & Events
        // =============================================================

        private void NetClient_OnGameStateUpdated(GameStateUpdateData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnGameStateUpdated(data)));
                return;
            }

            _timePerTurn = data.TimePerTurn;
            _currentTurnSymbol = data.CurrentTurnSymbol;
            string ruleText = data.BlockBothEndsRule ? "Chặn 2 đầu" : "Tự do";
            string timeText = data.TimePerTurn > 0 ? $"{data.TimePerTurn}s" : "Vô hạn";
            string spectatePrefix = _netClient.IsSpectator ? " (Đang xem 👁)" : "";
            lblRoomIdInfo.Text = $"Phòng: {_currentRoomId}{spectatePrefix} ({ruleText} | {timeText})";

            boardControl1.UpdateBoardState(data, _mySymbol);
            lblTurnInfo.Text = $"● {data.StatusMessage}";
            _currentGameStatus = data.Status;

            if (data.Status == GameStateStatus.Playing)
            {
                ResetTimer(data.TimePerTurn);
            }
            else if (data.Status == GameStateStatus.GameOver)
            {
                turnTimer.Stop();
                lblTimer.Text = "● KẾT THÚC";
                lblTimer.ForeColor = Color.FromArgb(160, 180, 215);
                AppendLog("Hệ thống", data.StatusMessage);
            }

            UpdateGameActionButtons();
        }

        private async void BoardControl1_OnCellClicked(int row, int col)
        {
            if (_isOfflineGame)
            {
                HandleOfflineCellClicked(row, col);
                return;
            }

            if (_netClient.IsSpectator) return; // Người xem không được đánh cờ
            await _netClient.SendMoveAsync(row, col);
        }

        private async void btnSurrender_Click(object sender, EventArgs e)
        {
            if (_isOfflineGame)
            {
                if (_currentGameStatus != GameStateStatus.Playing)
                {
                    MessageBox.Show("Trận đấu chưa bắt đầu hoặc đã kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirmOffline = MessageBox.Show("Bạn có chắc chắn muốn đầu hàng trận đấu này?", "Xác nhận đầu hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmOffline == DialogResult.Yes)
                {
                    _offlineEngine?.Surrender('O');
                    UpdateOfflineBoardUI();
                    turnTimer.Stop();
                    lblTimer.Text = "● KẾT THÚC";
                    UpdateGameActionButtons();
                }
                return;
            }

            if (!_netClient.IsConnected) return;

            if (_currentGameStatus != GameStateStatus.Playing)
            {
                MessageBox.Show("Trận đấu chưa bắt đầu hoặc đã kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đầu hàng trận đấu này?", "Xác nhận đầu hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                await _netClient.SendSurrenderAsync();
            }
        }

        private async void btnRestart_Click(object sender, EventArgs e)
        {
            if (_isOfflineGame)
            {
                if (_currentGameStatus != GameStateStatus.GameOver)
                {
                    MessageBox.Show("Trận đấu chưa kết thúc! Bạn chỉ có thể chơi lại khi ván cờ đã ngã ngũ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _offlineEngine?.Reset();
                _currentTurnSymbol = 'X';
                boardControl1.InitEmptyBoard(15);
                UpdateOfflineBoardUI();
                ResetTimer(_timePerTurn);
                UpdateGameActionButtons();
                return;
            }

            if (!_netClient.IsConnected) return;

            // Trận đấu chưa kết thúc thì KHÔNG ĐƯỢC nhấn chơi lại
            if (_currentGameStatus != GameStateStatus.GameOver)
            {
                MessageBox.Show("Trận đấu chưa kết thúc! Bạn chỉ có thể chơi lại (tái đấu) khi ván đấu đã phân định thắng bại hoặc đầu hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            await _netClient.SendRestartAsync();
        }

        private void UpdateGameActionButtons()
        {
            if (!_isInRoom)
            {
                btnSurrender.Enabled = false;
                btnRestart.Enabled = false;
                btnLeaveRoom.Enabled = false;
                btnLeaveRoom.Visible = false;
                btnLeaveRoom.Text = "Thoát phòng";
                return;
            }

            if (_netClient.IsSpectator)
            {
                btnSurrender.Visible = false;
                btnSurrender.Enabled = false;
                btnRestart.Visible = false;
                btnRestart.Enabled = false;
                btnLeaveRoom.Visible = true;
                btnLeaveRoom.Enabled = true;
                btnLeaveRoom.Text = "Thoát xem ✕";
                return;
            }

            btnSurrender.Visible = true;
            btnRestart.Visible = true;
            btnLeaveRoom.Text = "Thoát phòng";

            if (_currentGameStatus == GameStateStatus.Playing)
            {
                btnSurrender.Enabled = true;
                btnRestart.Enabled = false; // Trận đấu chưa kết thúc thì KHÔNG ĐƯỢC nhấn chơi lại
                btnLeaveRoom.Visible = true;
                btnLeaveRoom.Enabled = true; // Luôn cho phép bấm, nếu PvP đang chơi sẽ hỏi xác nhận đầu hàng
            }
            else if (_currentGameStatus == GameStateStatus.GameOver)
            {
                btnSurrender.Enabled = false; // Đã kết thúc thì không thể đầu hàng nữa
                btnRestart.Enabled = true;  // Đã kết thúc thì ĐƯỢC nhấn chơi lại
                btnLeaveRoom.Visible = true;
                btnLeaveRoom.Enabled = true;  // Đã kết thúc thì ĐƯỢC thoát phòng
            }
            else // WaitingForOpponent
            {
                btnSurrender.Enabled = false;
                btnRestart.Enabled = false;
                btnLeaveRoom.Visible = true;
                btnLeaveRoom.Enabled = true;
            }
        }

        // =============================================================
        // Chat Logic
        // =============================================================

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
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnChatMessageReceived(chat)));
                return;
            }

            AppendLog(chat.SenderName, chat.Message);
        }

        private void NetClient_OnOpponentDisconnected(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnOpponentDisconnected(message)));
                return;
            }

            turnTimer.Stop();
            _currentGameStatus = GameStateStatus.GameOver;
            btnLeaveRoom.Visible = true;
            AppendLog("Hệ thống", message);
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NetClient_OnOpponentReconnecting(OpponentReconnectingData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnOpponentReconnecting(data)));
                return;
            }

            AppendLog("Hệ thống", $"⚠ {data.DisconnectedPlayerName} đã mất kết nối! Đang chờ kết nối lại ({data.GracePeriodSeconds}s)... Thời gian ván đấu vẫn đang tiếp tục tính.");
            lblTurnInfo.Text = $"⚠ {data.DisconnectedPlayerName} mất mạng! Chờ kết nối lại ({data.GracePeriodSeconds}s)...";
            // Lưu ý: Không dừng turnTimer - thời gian lượt đi của ván đấu vẫn chạy bình thường theo yêu cầu
        }

        private void NetClient_OnOpponentReconnected(OpponentReconnectedData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnOpponentReconnected(data)));
                return;
            }

            AppendLog("Hệ thống", $"✓ {data.ReconnectedPlayerName} đã kết nối lại ván đấu!");
        }

        private void NetClient_OnReconnectResponse(ReconnectRespData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnReconnectResponse(data)));
                return;
            }

            if (data.Success)
            {
                _currentRoomMode = data.Mode;
                _mySymbol = data.PlayerSymbol;
                _currentRoomId = data.RoomId;
                _isInRoom = true;
                _currentGameStatus = data.CurrentState.Status;
                _timePerTurn = data.TimePerTurn;
                _currentTurnSymbol = data.CurrentState.CurrentTurnSymbol;

                string ruleText = data.BlockBothEndsRule ? "Chặn 2 đầu" : "Tự do";
                string timeText = data.TimePerTurn > 0 ? $"{data.TimePerTurn}s" : "Vô hạn";
                lblRoomIdInfo.Text = $"Phòng: {data.RoomId} ({ruleText} | {timeText})";

                string p1Name = (_mySymbol == 'X') ? _netClient.PlayerName : data.OpponentName;
                string p2Name = (_mySymbol == 'O') ? _netClient.PlayerName : data.OpponentName;
                lblPlayersInfo.Text = $"X: {p1Name}   VS   O: {p2Name}";

                EnterRoomUI();
                UpdateGameActionButtons();
                boardControl1.UpdateBoardState(data.CurrentState, _mySymbol);
                lblTurnInfo.Text = $"● {data.CurrentState.StatusMessage}";

                if (data.CurrentState.Status == GameStateStatus.Playing)
                {
                    ResetTimer(data.TimePerTurn);
                }

                AppendLog("Hệ thống", data.Message);
                SwitchToView(pnlGameView, btnNavGame);
                MessageBox.Show($"Bạn đã kết nối lại ván đấu tại phòng {data.RoomId} thành công!", "Kết nối lại ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NetClient_OnErrorOccurred(string errorMsg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnErrorOccurred(errorMsg)));
                return;
            }

            turnTimer.Stop();
            lblStatusBadge.Text = "● Lỗi kết nối";
            lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
            btnConnect.Text = "Kết nối Server";
            UpdateUIState(false);
            UpdateServerAndPlayerInfo(false);

            if (_isInRoom && !_isOfflineGame)
            {
                AppendLog("Lỗi", errorMsg);
                if (_currentGameStatus == GameStateStatus.Playing)
                {
                    AppendLog("Hệ thống", "⚠ Bạn bị mất kết nối mạng! Bạn có 30 giây để Đăng nhập lại tiếp tục ván đấu.");
                }
            }
        }

        private async void turnTimer_Tick(object? sender, EventArgs e)
        {
            if (_remainingSeconds <= 0) return;

            _remainingSeconds--;

            if (_remainingSeconds > 10)
            {
                lblTimer.ForeColor = UITheme.AccentCyan;
                lblTimer.Text = $"● {_remainingSeconds}s";
            }
            else if (_remainingSeconds > 5)
            {
                lblTimer.ForeColor = UITheme.AccentOrange;
                lblTimer.Text = $"● {_remainingSeconds}s ⏳";
            }
            else if (_remainingSeconds > 0)
            {
                lblTimer.ForeColor = UITheme.AccentRed;
                lblTimer.Text = $"● {_remainingSeconds}s ⚠";
            }
            else
            {
                turnTimer.Stop();
                lblTimer.ForeColor = UITheme.AccentRed;
                lblTimer.Text = "● Hết giờ lượt đi!";

                if (_isOfflineGame)
                {
                    if (_currentTurnSymbol == 'X' && _offlineEngine != null && _offlineEngine.Status == GameStateStatus.Playing)
                    {
                        _offlineEngine.Surrender('O');
                        UpdateOfflineBoardUI();
                        UpdateGameActionButtons();
                    }
                    return;
                }

                // Nếu là lượt của mình trong ván đấu online, tự động đầu hàng vì quá giờ
                if (!_netClient.IsSpectator && _currentGameStatus == GameStateStatus.Playing && _mySymbol == _currentTurnSymbol)
                {
                    AppendLog("Hệ thống", "Hết thời gian lượt đi! Bạn đã bị xử thua vì quá giờ.");
                    await _netClient.SendSurrenderAsync();
                }
            }
        }

        private void ResetTimer(int seconds = 30)
        {
            turnTimer.Stop();
            if (seconds <= 0)
            {
                _remainingSeconds = 0;
                lblTimer.Text = "● Vô hạn";
                lblTimer.ForeColor = UITheme.AccentGreen;
                return;
            }

            _remainingSeconds = seconds;
            lblTimer.Text = $"● {_remainingSeconds}s";
            lblTimer.ForeColor = UITheme.AccentCyan;
            turnTimer.Start();
        }

        private void pnlCreateRoomView_Resize(object? sender, EventArgs e)
        {
            cardCreateBox.Left = Math.Max(20, (pnlCreateRoomView.ClientSize.Width - cardCreateBox.Width) / 2);
            cardCreateBox.Top = Math.Max(20, (pnlCreateRoomView.ClientSize.Height - cardCreateBox.Height) / 2);
        }

        private void EnterRoomUI()
        {
            btnCreateRoom.Enabled = false;
            btnJoinRoom.Enabled = false;
            txtRoomId.ReadOnly = true;
            cardModePvP.Enabled = false;
            cardModeAI.Enabled = false;
            cardTimeTurn.Enabled = false;
            cardTimeFree.Enabled = false;
            pillTime15.Enabled = false;
            pillTime30.Enabled = false;
            pillTime60.Enabled = false;
            pillAiEasy.Enabled = false;
            pillAiMedium.Enabled = false;
            pillAiHard.Enabled = false;
            pillRuleStandard.Enabled = false;
            pillRuleFree.Enabled = false;
            UpdateGameActionButtons();

            if (!_isOfflineGame)
            {
                rtbChatLog.Clear();
                pnlRightBar.Visible = true;
            }
            else
            {
                pnlRightBar.Visible = false;
            }
        }

        private void ExitRoomUI()
        {
            _isInRoom = false;
            _isOfflineGame = false;
            _offlineEngine = null;
            _netClient.IsSpectator = false;
            _currentRoomId = string.Empty;
            _currentGameStatus = GameStateStatus.WaitingForOpponent;
            pnlRightBar.Visible = false;
            rtbChatLog.Clear();
            btnCreateRoom.Enabled = true;
            btnJoinRoom.Enabled = true;
            txtRoomId.ReadOnly = false;
            txtRoomId.Clear();
            cardModePvP.Enabled = true;
            cardModeAI.Enabled = true;
            cardTimeTurn.Enabled = true;
            cardTimeFree.Enabled = true;
            pillTime15.Enabled = true;
            pillTime30.Enabled = true;
            pillTime60.Enabled = true;
            pillAiEasy.Enabled = true;
            pillAiMedium.Enabled = true;
            pillAiHard.Enabled = true;
            pillRuleStandard.Enabled = true;
            pillRuleFree.Enabled = true;
            btnCreateRoom.Text = cardModeAI.IsSelected && !_netClient.IsConnected ? "▶  Chơi với Máy ngay" : "▶  Tạo phòng";
            lblRoomIdInfo.Text = "Phòng: ---";
            lblPlayersInfo.Text = "X: ---   VS   O: ---";
            lblTurnInfo.Text = "● Lượt đi: ---";
            lblTimer.Text = "● 30s";
            lblTimer.ForeColor = UITheme.AccentOrange;
            turnTimer.Stop();
            boardControl1.InitEmptyBoard(15);
            btnSurrender.Visible = true;
            btnRestart.Visible = true;
            btnLeaveRoom.Text = "Thoát phòng";
            UpdateGameActionButtons();
        }

        private void UpdateUIState(bool connected)
        {
            txtIp.Enabled = !connected;
            txtPort.Enabled = !connected;
            txtName.Enabled = !connected;
            txtPassword.Enabled = !connected;
            btnRegister.Enabled = !connected;

            if (cardModeAI.IsSelected)
            {
                btnCreateRoom.Text = "▶  Chơi với Máy ngay";
            }

            if (!connected && !_isOfflineGame)
            {
                ExitRoomUI();
            }
        }

        private void AppendLog(string sender, string message)
        {
            rtbChatLog.SelectionStart = rtbChatLog.TextLength;
            rtbChatLog.SelectionLength = 0;

            Color timeColor = Color.FromArgb(120, 140, 175);
            Color nameColor = sender == "Hệ thống" ? Color.FromArgb(180, 130, 255) :
                              (sender == "Lỗi" ? Color.FromArgb(255, 75, 75) :
                              (sender == _netClient?.PlayerName ? Color.FromArgb(0, 230, 118) : Color.FromArgb(0, 194, 255)));

            rtbChatLog.SelectionColor = timeColor;
            rtbChatLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ");

            rtbChatLog.SelectionColor = nameColor;
            rtbChatLog.SelectionFont = new Font(rtbChatLog.Font, FontStyle.Bold);
            rtbChatLog.AppendText($"{sender}: ");

            rtbChatLog.SelectionColor = Color.FromArgb(235, 245, 255);
            rtbChatLog.SelectionFont = new Font(rtbChatLog.Font, FontStyle.Regular);
            rtbChatLog.AppendText($"{message}\n");

            rtbChatLog.ScrollToCaret();
        }

        private void UpdateServerAndPlayerInfo(bool connected)
        {
            if (connected)
            {
                lblHomeServerStatus.Text = "● Đã kết nối";
                lblHomeServerStatus.ForeColor = Color.FromArgb(0, 230, 118);
                string hostName = string.IsNullOrWhiteSpace(_netClient.ServerHostName) ? Environment.MachineName : _netClient.ServerHostName;
                lblHomeServerHost.Text = $"💻 Máy chủ: {hostName}";
                lblHomeServerIp.Text = $"📡 IP: {txtIp.Text.Trim()}:{txtPort.Text.Trim()}";

                lblProfileName.Text = _netClient.PlayerName;
                lblProfileLevel.Text = $"★ {_netClient.Elo} ELO";
                lblProfileXp.Text = $"{_netClient.Wins}W - {_netClient.Losses}L - {_netClient.Draws}D";
                btnLogout.Visible = false; // Nút Đăng xuất chính đã được tích hợp ngay trên thẻ hồ sơ (btnNavSettings)
            }
            else
            {
                lblHomeServerStatus.Text = "○ Chưa kết nối";
                lblHomeServerStatus.ForeColor = Color.FromArgb(255, 75, 75);
                lblHomeServerHost.Text = "💻 Máy chủ: ---";
                lblHomeServerIp.Text = "📡 IP: ---";
                lblHomePlayersHeader.Text = "NGƯỜI CHƠI ONLINE (0)";
                lstConnectedPlayers.Items.Clear();
                lblNoPlayers.Text = "Chưa kết nối đến máy chủ";
                lblNoPlayers.Visible = true;

                lblProfileName.Text = "Khách (Offline)";
                lblProfileLevel.Text = "Chế độ Offline";
                lblProfileXp.Text = "Chơi với Máy";
                btnLogout.Visible = false;
            }
        }

        private void InitMatchHistoryTable()
        {
            dgvMatchHistory.Columns.Clear();

            var colDate = new DataGridViewTextBoxColumn
            {
                Name = "PlayedAt",
                HeaderText = "Thời gian",
                Width = 140,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colDate);

            var colOpponent = new DataGridViewTextBoxColumn
            {
                Name = "Opponent",
                HeaderText = "Đối thủ",
                Width = 160,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colOpponent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colOpponent.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvMatchHistory.Columns.Add(colOpponent);

            var colResult = new DataGridViewTextBoxColumn
            {
                Name = "Result",
                HeaderText = "Kết quả",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colResult.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colResult.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colResult);

            var colEloChange = new DataGridViewTextBoxColumn
            {
                Name = "EloChange",
                HeaderText = "Biến động ELO",
                Width = 120,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colEloChange.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colEloChange.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colEloChange);

            var colMoves = new DataGridViewTextBoxColumn
            {
                Name = "Moves",
                HeaderText = "Số nước cờ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colMoves.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMoves.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colMoves);
        }

        private void InitLeaderboardTable()
        {
            dgvLeaderboard.Columns.Clear();

            var colRank = new DataGridViewTextBoxColumn
            {
                Name = "Rank",
                HeaderText = "Hạng",
                Width = 70,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colRank.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRank.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaderboard.Columns.Add(colRank);

            var colUser = new DataGridViewTextBoxColumn
            {
                Name = "Username",
                HeaderText = "Kỳ thủ",
                Width = 180,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvLeaderboard.Columns.Add(colUser);

            var colElo = new DataGridViewTextBoxColumn
            {
                Name = "Elo",
                HeaderText = "Điểm ELO",
                Width = 110,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colElo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colElo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaderboard.Columns.Add(colElo);

            var colRecord = new DataGridViewTextBoxColumn
            {
                Name = "Record",
                HeaderText = "Thắng - Thua - Hòa",
                Width = 160,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colRecord.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRecord.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaderboard.Columns.Add(colRecord);

            var colWinRate = new DataGridViewTextBoxColumn
            {
                Name = "WinRate",
                HeaderText = "Tỉ lệ thắng",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colWinRate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colWinRate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaderboard.Columns.Add(colWinRate);
        }

        private void NetClient_OnMatchHistoryReceived(List<MatchHistoryItemData> list)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnMatchHistoryReceived(list)));
                return;
            }

            dgvMatchHistory.Rows.Clear();
            if (list == null || list.Count == 0)
            {
                if (_netClient != null && _netClient.IsConnected)
                {
                    lblNoHistory.Text = "Tài khoản của bạn chưa có lịch sử đấu online nào.\nHãy vào mục 'Tạo phòng' hoặc 'Tham gia phòng' để bắt đầu ván đấu đầu tiên!";
                }
                else
                {
                    lblNoHistory.Text = "Bạn đang chơi ở chế độ Ngoại tuyến (Khách).\nHãy bấm nút '⚡ ĐĂNG NHẬP' ở góc dưới bên trái để lưu trữ lịch sử thi đấu online!";
                }
                lblNoHistory.Visible = true;
                dgvMatchHistory.Visible = false;
                return;
            }

            lblNoHistory.Visible = false;
            dgvMatchHistory.Visible = true;

            foreach (var item in list)
            {
                string eloSign = item.EloChange > 0 ? $"+{item.EloChange}" : $"{item.EloChange}";
                int idx = dgvMatchHistory.Rows.Add(
                    item.PlayedAt,
                    item.OpponentName,
                    item.Result,
                    eloSign,
                    $"{item.TotalMoves} nước"
                );

                var row = dgvMatchHistory.Rows[idx];
                if (item.Result == "Thắng")
                {
                    row.Cells["Result"].Style.ForeColor = UITheme.AccentGreen;
                    row.Cells["EloChange"].Style.ForeColor = UITheme.AccentGreen;
                    row.Cells["Result"].Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
                else if (item.Result == "Thua")
                {
                    row.Cells["Result"].Style.ForeColor = UITheme.AccentRed;
                    row.Cells["EloChange"].Style.ForeColor = UITheme.AccentRed;
                    row.Cells["Result"].Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
                else
                {
                    row.Cells["Result"].Style.ForeColor = UITheme.AccentOrange;
                    row.Cells["EloChange"].Style.ForeColor = UITheme.AccentOrange;
                }
            }
        }

        private void NetClient_OnLeaderboardReceived(List<LeaderboardItemData> list)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnLeaderboardReceived(list)));
                return;
            }

            dgvLeaderboard.Rows.Clear();
            if (list == null || list.Count == 0)
            {
                if (_netClient != null && _netClient.IsConnected)
                {
                    lblNoLeaderboard.Text = "Chưa có dữ liệu người chơi trên Bảng xếp hạng Server.";
                }
                else
                {
                    lblNoLeaderboard.Text = "Bạn chưa kết nối Server.\nVui lòng bấm nút '⚡ ĐĂNG NHẬP' ở góc dưới bên trái để xem Bảng xếp hạng!";
                }
                lblNoLeaderboard.Visible = true;
                dgvLeaderboard.Visible = false;
                return;
            }

            lblNoLeaderboard.Visible = false;
            dgvLeaderboard.Visible = true;

            foreach (var item in list)
            {
                string rankStr = item.Rank switch
                {
                    1 => "🥇 #1",
                    2 => "🥈 #2",
                    3 => "🥉 #3",
                    _ => $"#{item.Rank}"
                };

                int idx = dgvLeaderboard.Rows.Add(
                    rankStr,
                    item.Username,
                    $"★ {item.Elo}",
                    $"{item.Wins}W - {item.Losses}L - {item.Draws}D",
                    $"{item.WinRate}%"
                );

                var row = dgvLeaderboard.Rows[idx];
                if (item.Rank == 1)
                {
                    row.Cells["Rank"].Style.ForeColor = Color.FromArgb(255, 215, 0);
                    row.Cells["Rank"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
                else if (item.Rank == 2)
                {
                    row.Cells["Rank"].Style.ForeColor = Color.FromArgb(192, 192, 192);
                    row.Cells["Rank"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
                else if (item.Rank == 3)
                {
                    row.Cells["Rank"].Style.ForeColor = Color.FromArgb(205, 127, 50);
                    row.Cells["Rank"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }

                row.Cells["Elo"].Style.ForeColor = UITheme.AccentCyan;
                row.Cells["Elo"].Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
        }

        private void NetClient_OnEloUpdated(EloUpdateData data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnEloUpdated(data)));
                return;
            }

            lblProfileLevel.Text = $"★ {data.NewElo} ELO";
            lblProfileXp.Text = $"{data.Wins}W - {data.Losses}L - {data.Draws}D";
            string changeSign = (data.EloChange >= 0) ? "+" : "";
            AppendLog("Hệ thống", $"⭐ Điểm ELO của bạn đã cập nhật: {changeSign}{data.EloChange} (Hiện tại: {data.NewElo} ELO)!");
            UpdateProfileViewUI();
        }

        private async void btnRefreshHistory_Click(object? sender, EventArgs e)
        {
            if (_netClient == null || !_netClient.IsConnected)
            {
                MessageBox.Show("Vui lòng kết nối Server để xem lịch sử đấu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            await _netClient.RequestMatchHistoryAsync();
        }

        private async void btnRefreshLeaderboard_Click(object? sender, EventArgs e)
        {
            if (_netClient == null || !_netClient.IsConnected)
            {
                MessageBox.Show("Vui lòng kết nối Server để xem Bảng xếp hạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            await _netClient.RequestLeaderboardAsync();
        }

        private void NetClient_OnPlayerListReceived(List<PlayerInfoData> players)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnPlayerListReceived(players)));
                return;
            }

            lblHomePlayersHeader.Text = $"NGƯỜI CHƠI ONLINE ({players.Count})";
            lstConnectedPlayers.BeginUpdate();
            lstConnectedPlayers.Items.Clear();
            foreach (var p in players)
            {
                lstConnectedPlayers.Items.Add(p);
            }
            lstConnectedPlayers.EndUpdate();

            lblNoPlayers.Visible = (players.Count == 0);
            if (players.Count == 0)
            {
                lblNoPlayers.Text = "Không có người chơi nào khác";
            }
        }

        private void lstConnectedPlayers_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lstConnectedPlayers.Items.Count) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var player = lstConnectedPlayers.Items[e.Index] as PlayerInfoData;
            if (player == null) return;

            Rectangle bounds = e.Bounds;

            // Background
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(13, 24, 48)))
            {
                g.FillRectangle(bgBrush, bounds);
            }

            // Status indicator dot
            bool isWaiting = player.Status == "Đang chờ";
            Color statusColor = isWaiting ? UITheme.AccentGreen : UITheme.AccentOrange;
            int dotSize = 8;
            int dotY = bounds.Y + (bounds.Height - dotSize) / 2;
            using (SolidBrush dotBrush = new SolidBrush(statusColor))
            {
                g.FillEllipse(dotBrush, bounds.X + 8, dotY, dotSize, dotSize);
            }

            // Player name
            bool isSelf = (player.PlayerId == _netClient.PlayerId);
            string eloBadge = player.Elo > 0 ? $" (★ {player.Elo})" : "";
            string displayName = isSelf ? $"{player.PlayerName}{eloBadge} (Bạn)" : $"{player.PlayerName}{eloBadge}";
            Color nameColor = isSelf ? UITheme.AccentCyan : Color.White;

            using (Font nameFont = new Font("Segoe UI", 9f, isSelf ? FontStyle.Bold : FontStyle.Regular))
            using (SolidBrush nameBrush = new SolidBrush(nameColor))
            {
                int textY = bounds.Y + (bounds.Height - 16) / 2;
                g.DrawString(displayName, nameFont, nameBrush, bounds.X + 22, textY);
            }

            // Status text on right
            string statusText = player.Status;
            using (Font statusFont = new Font("Segoe UI", 8f))
            using (SolidBrush statusBrush = new SolidBrush(statusColor))
            {
                SizeF size = g.MeasureString(statusText, statusFont);
                float statusX = bounds.Right - size.Width - 8;
                float statusY = bounds.Y + (bounds.Height - size.Height) / 2;
                g.DrawString(statusText, statusFont, statusBrush, statusX, statusY);
            }

            // Divider line
            using (Pen divPen = new Pen(Color.FromArgb(22, 38, 72), 1f))
            {
                g.DrawLine(divPen, bounds.X + 6, bounds.Bottom - 1, bounds.Right - 6, bounds.Bottom - 1);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _netClient.Disconnect();
        }
    }
}
