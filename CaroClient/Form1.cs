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

            // Khởi tạo NetworkClient sau khi Form và SynchronizationContext đã sẵn sàng
            _netClient = new NetworkClient();
            _netClient.OnConnectionCompleted += NetClient_OnConnectionCompleted;
            _netClient.OnCreateRoomResponse += NetClient_OnCreateRoomResponse;
            _netClient.OnJoinRoomResponse += NetClient_OnJoinRoomResponse;
            _netClient.OnLeaveRoomResponse += NetClient_OnLeaveRoomResponse;
            _netClient.OnGameStateUpdated += NetClient_OnGameStateUpdated;
            _netClient.OnChatMessageReceived += NetClient_OnChatMessageReceived;
            _netClient.OnOpponentDisconnected += NetClient_OnOpponentDisconnected;
            _netClient.OnErrorOccurred += NetClient_OnErrorOccurred;
            _netClient.OnPlayerListReceived += NetClient_OnPlayerListReceived;
            _netClient.OnRoomListReceived += NetClient_OnRoomListReceived;
            _netClient.OnSpectateRoomResponse += NetClient_OnSpectateRoomResponse;

            // Detect and display initial info
            InitLanInformation();

            // Setup Featured Rooms table
            InitFeaturedRoomsTable();
            UpdateRoomListUI(new List<RoomInfoData>());

            // Wire up Connected Players owner-draw
            lstConnectedPlayers.DrawItem += lstConnectedPlayers_DrawItem;

            // Board Click Event
            boardControl1.OnCellClicked += BoardControl1_OnCellClicked;

            // Start on Home View
            SwitchToView(pnlHomeView, btnNavHome);
            UpdateUIState(false);
            UpdateServerAndPlayerInfo(false);
        }

        private void InitLanInformation()
        {
            lblProfileName.Text = string.IsNullOrWhiteSpace(txtName.Text) ? "Player" : txtName.Text.Trim();
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
            pnlSettingsView.Visible = (targetView == pnlSettingsView);

            // Cập nhật trạng thái hiển thị của các nút menu thanh bên
            CyberButton[] navs = { btnNavHome, btnNavGame, btnNavCreate, btnNavJoin, btnNavSettings };
            foreach (var btn in navs)
            {
                bool isActive = (btn == activeNav);
                btn.IsPrimaryGradient = isActive;
                btn.NormalColor = isActive ? UITheme.AccentBlue : Color.FromArgb(14, 24, 46);
                btn.Invalidate();
            }

            if (targetView == pnlCreateRoomView)
            {
                RelayoutCreateBox();
            }

            // Tự động làm mới danh sách phòng khi vào màn hình Tham gia phòng
            if (targetView == pnlJoinRoomView && _netClient != null && _netClient.IsConnected)
            {
                _ = _netClient.RequestRoomListAsync();
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
        // Network Connection Logic
        // =============================================================

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_netClient.IsConnected)
            {
                _netClient.Disconnect();
                UpdateUIState(false);
                UpdateServerAndPlayerInfo(false);
                UpdateRoomListUI(new List<RoomInfoData>());
                lblStatusBadge.Text = "● Chưa kết nối";
                lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
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
            lblStatusBadge.Text = "● Đang kết nối...";
            lblStatusBadge.ForeColor = Color.FromArgb(255, 145, 0);

            await _netClient.ConnectAsync(ip, port, name);
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
                AppendLog("Hệ thống", message);
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
            // Vị trí Y bắt đầu dưới Card thời gian (cardTime ở Y=228, cao 68 => 228 + 68 + 12 = 308)
            int currentY = 308;

            bool showTimeLimit = _isTimeLimitMode;
            bool showAiDiff = cardModeAI.IsSelected;

            pnlTimeLimitOptions.Visible = showTimeLimit;
            if (showTimeLimit)
            {
                pnlTimeLimitOptions.Left = 24;
                pnlTimeLimitOptions.Top = currentY;
                currentY += pnlTimeLimitOptions.Height + 10;
            }

            pnlAiDifficultyOptions.Visible = showAiDiff;
            if (showAiDiff)
            {
                pnlAiDifficultyOptions.Left = 24;
                pnlAiDifficultyOptions.Top = currentY;
                currentY += pnlAiDifficultyOptions.Height + 10;
            }

            secHeaderRule.Left = 24;
            secHeaderRule.Top = currentY;
            currentY += secHeaderRule.Height + 8;

            pillRuleStandard.Left = 24;
            pillRuleStandard.Top = currentY;
            pillRuleFree.Left = 276;
            pillRuleFree.Top = currentY;
            currentY += pillRuleStandard.Height + 18;

            btnCreateRoom.Left = 24;
            btnCreateRoom.Top = currentY;
            btnCancelCreate.Left = 376;
            btnCancelCreate.Top = currentY;
            currentY += btnCreateRoom.Height + 20;

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

            // Nếu là chế độ Đấu với Máy (AI) và chưa kết nối Server -> Khởi động trận đấu Offline trực tiếp!
            if (isAi && !_netClient.IsConnected)
            {
                StartOfflineAiGame(diff, blockBothEnds, timeLimit);
                return;
            }

            // Chế độ PvP yêu cầu kết nối Server
            if (!_netClient.IsConnected)
            {
                SwitchToView(pnlSettingsView, btnNavSettings);
                MessageBox.Show("Chế độ đấu Người 1v1 yêu cầu kết nối Server LAN! Bạn có thể kết nối trước hoặc chọn 'Vs AI' để chơi Offline trực tiếp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ResetTimer(_timePerTurn);
            UpdateGameActionButtons();
            AppendLog("Hệ thống", $"Bắt đầu ván cờ Offline với Máy ({diffName}). Chúc bạn may mắn!");

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
                MessageBox.Show("Vui lòng kết nối Server trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    AppendLog("Hệ thống", "Bạn đã đầu hàng. Máy (AI) thắng ván này!");
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
                AppendLog("Hệ thống", "Bắt đầu ván đấu mới với Máy. Bạn đi trước (X).");
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
            AppendLog("Lỗi", errorMsg);
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
                        AppendLog("Hệ thống", "Hết thời gian lượt đi! Bạn bị xử thua vì quá giờ.");
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
        }

        private void ExitRoomUI()
        {
            _isInRoom = false;
            _isOfflineGame = false;
            _offlineEngine = null;
            _netClient.IsSpectator = false;
            _currentRoomId = string.Empty;
            _currentGameStatus = GameStateStatus.WaitingForOpponent;
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

            if (cardModeAI.IsSelected)
            {
                btnCreateRoom.Text = connected ? "▶  Tạo phòng" : "▶  Chơi với Máy ngay";
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
            }
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
            string displayName = isSelf ? $"{player.PlayerName} (Bạn)" : player.PlayerName;
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
