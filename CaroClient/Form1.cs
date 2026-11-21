using CaroProtocol;
using Point = System.Drawing.Point;
using System.Drawing.Drawing2D;
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
        private readonly Dictionary<string, Image> _avatarCache = new(StringComparer.OrdinalIgnoreCase);
        private Label? lblChangeAvatarLink;

        // Offline AI Game State
        private bool _isOfflineGame = false;
        private GameEngine? _offlineEngine;
        private int _offlineAiSessionId = 0;
        private AIDifficulty _selectedDifficulty = AIDifficulty.Medium;
        private bool _selectedBlockBothEnds = true;
        private int _selectedTimeLimit = 30;
        private bool _isTimeLimitMode = true;
        private bool _isCountdownGame = false;

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

            pillCountdown1m.Click += (s, e) => SelectCountdownMinutes(1);
            pillCountdown3m.Click += (s, e) => SelectCountdownMinutes(3);
            pillCountdown5m.Click += (s, e) => SelectCountdownMinutes(5);
            pillCountdownCustom.Click += (s, e) => SelectCountdownCustom();
            txtCustomMinutes.TextChanged += (s, e) => UpdateCustomMinutes();

            gameOverModal.PlayAgainClicked += (s, e) =>
            {
                gameOverModal.Visible = false;
                btnRestart_Click(btnRestart, EventArgs.Empty);
            };

            gameOverModal.HomeClicked += (s, e) =>
            {
                gameOverModal.Visible = false;
                btnLeaveRoom_Click(btnLeaveRoom, EventArgs.Empty);
            };

            gameOverModal.CloseClicked += (s, e) =>
            {
                gameOverModal.Visible = false;
                btnLeaveRoom.Visible = true;
                btnLeaveRoom.Enabled = true;
                btnLeaveRoom.Text = "Thoát phòng";
                btnLeaveRoom.Location = new System.Drawing.Point(6, 17);
                btnLeaveRoom.Size = new Size(124, 42);
                btnLeaveRoom.CustomTextAlign = ContentAlignment.MiddleCenter;
                btnLeaveRoom.TextLeftPadding = 0;
            };

            pillAiEasy.Click += (s, e) => SelectAiDifficulty(AIDifficulty.Easy);
            pillAiMedium.Click += (s, e) => SelectAiDifficulty(AIDifficulty.Medium);
            pillAiHard.Click += (s, e) => SelectAiDifficulty(AIDifficulty.Hard);

            pillRuleStandard.Click += (s, e) => SelectRule(true);
            pillRuleFree.Click += (s, e) => SelectRule(false);

            SelectTimeMode(true);
            RelayoutCreateBox();
            this.Shown += (s, e) => { RelayoutCreateBox(); RelayoutGameHeader(); };

            if (pnlGameHeader != null)
            {
                pnlGameHeader.Resize += (s, e) => RelayoutGameHeader();
            }
            if (pnlGameView != null)
            {
                pnlGameView.Resize += (s, e) => RelayoutGameHeader();
            }
            RelayoutGameHeader();

            _netClient = new NetworkClient();
            WireUpNetClientEvents();

            // Detect and display initial info
            InitLanInformation();

            // Setup Tables & Lobby
            InitLobbyUI();
            InitMatchHistoryTable();
            InitLeaderboardTable();
            InitAuthUI();
            UpdateRoomListUI(new List<RoomInfoData>());

            // Setup Profile & Match History
            avatarControl.IsEditable = true;
            avatarControl.AvatarClicked += AvatarControl_AvatarClicked;
            avatarControl.Click += AvatarControl_AvatarClicked;

            lblChangeAvatarLink = new Label
            {
                Text = "Đổi ảnh",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                ForeColor = Color.FromArgb(0, 194, 255),
                Cursor = Cursors.Hand,
                Size = new Size(80, 18),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(avatarControl.Left + (avatarControl.Width - 80) / 2, avatarControl.Bottom + 2),
                BackColor = Color.Transparent
            };
            lblChangeAvatarLink.MouseEnter += (s, e) => { if (lblChangeAvatarLink != null) lblChangeAvatarLink.Font = new Font("Segoe UI", 8.5f, FontStyle.Underline); };
            lblChangeAvatarLink.MouseLeave += (s, e) => { if (lblChangeAvatarLink != null) lblChangeAvatarLink.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular); };
            lblChangeAvatarLink.Click += AvatarControl_AvatarClicked;
            cardProfileHero.Controls.Add(lblChangeAvatarLink);

            dgvMatchHistory.CellPainting += dgvMatchHistory_CellPainting;
            lblEditNameIcon.Click += lblEditNameIcon_Click;
            LoadProfileAssets();
            this.Shown += (s, e) => RelayoutProfileView();
            pnlHistoryView.Resize += (s, e) => RelayoutProfileView();

            // Wire up Connected Players owner-draw and interactions
            lstConnectedPlayers.DrawItem += lstConnectedPlayers_DrawItem;
            lstConnectedPlayers.MouseClick += lstConnectedPlayers_MouseClick;
            txtSearchFriend.TextChanged += (s, e) => ApplyPlayerFilter();

            // Wire up Home cards & Hero Banner
            heroBanner.PlayNowClicked += (s, e) => { SwitchToView(pnlJoinRoomView, btnNavJoin); };
            heroBanner.CreateRoomClicked += (s, e) => { SwitchToView(pnlCreateRoomView, btnNavCreate); };
            pnlHomeView.Resize += pnlHomeView_Resize;
            pnlHomeView_Resize(this, EventArgs.Empty);
            cardEvent.Click += (s, e) => MessageBox.Show("Sự kiện 'Đại Chiến Caro Mùa 1' đang diễn ra sôi nổi!\nTham gia ngay để tranh cúp và nhận giải thưởng lên đến 100.000 VBD + 250.000đ quà tặng!", "Sự kiện nổi bật", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cardTopPlayers.Click += (s, e) => { SwitchToView(pnlLeaderboardView, btnNavLeaderboard); };
            cardTopPlayers.AvatarProvider = GetOnlinePlayerAvatar;

            // Board Click Event
            boardControl1.OnCellClicked += BoardControl1_OnCellClicked;

            pnlSettingsView.CardBox = cardSettingsBox;
            pnlSettingsView_Resize(this, EventArgs.Empty);

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
            InitMascotAnimations();
        }

        private void InitAuthUI()
        {
            lblUserPrefix.Icon = CyberIcon.User;
            lblUserPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblPassPrefix.Icon = CyberIcon.Lock;
            lblPassPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblTogglePassword.Icon = CyberIcon.Eye;
            lblTogglePassword.IconColor = Color.FromArgb(140, 170, 215);

            btnConnect.IsPurpleGradient = true;
            btnConnect.BorderLineColor = Color.FromArgb(192, 38, 211);
            btnConnect.CornerRadius = 10;
            btnConnect.NavIcon = CyberIcon.Lightning;
            btnConnect.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnConnect.ForeColor = Color.White;
            btnConnect.Text = "KẾT NỐI & ĐĂNG NHẬP";

            lblRegUserPrefix.Icon = CyberIcon.User;
            lblRegUserPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblRegPassPrefix.Icon = CyberIcon.Lock;
            lblRegPassPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblToggleRegPass.Icon = CyberIcon.Eye;
            lblToggleRegPass.IconColor = Color.FromArgb(140, 170, 215);
            lblRegConfirmPassPrefix.Icon = CyberIcon.Lock;
            lblRegConfirmPassPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblToggleRegConfirm.Icon = CyberIcon.Eye;
            lblToggleRegConfirm.IconColor = Color.FromArgb(140, 170, 215);

            btnRegister.IsPrimaryGradient = true;
            btnRegister.CornerRadius = 10;
            btnRegister.NavIcon = CyberIcon.Star;
            btnRegister.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnRegister.ForeColor = Color.White;
            btnRegister.Text = "TẠO TÀI KHOẢN & ĐĂNG NHẬP";

            btnTabLogin.Text = "🔍  ĐĂNG NHẬP";
            btnTabRegister.Text = "✏  ĐĂNG KÝ MỚI";

            lblSearchIcon.Icon = CyberIcon.Search;
            lblSearchIcon.IconColor = Color.FromArgb(0, 194, 255);
        }

        private void InitMascotAnimations()
        {
            txtPassword.GotFocus += (s, e) => UpdateMascotReaction();
            txtPassword.LostFocus += (s, e) => UpdateMascotReaction();
            txtPassword.TextChanged += (s, e) => UpdateMascotReaction();

            txtRegPassword.GotFocus += (s, e) => UpdateMascotReaction();
            txtRegPassword.LostFocus += (s, e) => UpdateMascotReaction();
            txtRegPassword.TextChanged += (s, e) => UpdateMascotReaction();

            txtRegConfirmPassword.GotFocus += (s, e) => UpdateMascotReaction();
            txtRegConfirmPassword.LostFocus += (s, e) => UpdateMascotReaction();
            txtRegConfirmPassword.TextChanged += (s, e) => UpdateMascotReaction();

            txtName.GotFocus += (s, e) => UpdateMascotReaction();
            txtRegName.GotFocus += (s, e) => UpdateMascotReaction();
            txtIp.GotFocus += (s, e) => UpdateMascotReaction();
            txtPort.GotFocus += (s, e) => UpdateMascotReaction();
        }

        private void UpdateMascotReaction()
        {
            if (cardSettingsBox == null) return;

            bool isPasswordFocused = (txtPassword != null && txtPassword.Focused) ||
                                     (txtRegPassword != null && txtRegPassword.Focused) ||
                                     (txtRegConfirmPassword != null && txtRegConfirmPassword.Focused);

            bool hasPasswordText = (txtPassword != null && !string.IsNullOrEmpty(txtPassword.Text)) ||
                                   (txtRegPassword != null && !string.IsNullOrEmpty(txtRegPassword.Text)) ||
                                   (txtRegConfirmPassword != null && !string.IsNullOrEmpty(txtRegConfirmPassword.Text));

            if (isPasswordFocused || hasPasswordText)
            {
                bool isPeek = (txtPassword != null && txtPassword.PasswordChar == '\0') ||
                              (txtRegPassword != null && txtRegPassword.PasswordChar == '\0') ||
                              (txtRegConfirmPassword != null && txtRegConfirmPassword.PasswordChar == '\0');

                if (isPeek)
                {
                    cardSettingsBox.MascotState = MascotReactionState.PasswordPeek;
                    pnlSettingsView.MascotState = MascotReactionState.PasswordPeek;
                }
                else
                {
                    cardSettingsBox.MascotState = MascotReactionState.PasswordMasked;
                    pnlSettingsView.MascotState = MascotReactionState.PasswordMasked;
                }
            }
            else
            {
                cardSettingsBox.MascotState = MascotReactionState.Idle;
                pnlSettingsView.MascotState = MascotReactionState.Idle;
            }
            pnlSettingsView.Invalidate();
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

        private string _currentFilter = "all";
        private string _currentSearchText = string.Empty;
        private int _suggestedRoomIndex = 0;

        private void InitLobbyUI()
        {
            if (pnlLobbyHeaderBar == null)
            {
                pnlLobbyHeaderBar = new LobbyBannerPanel();
            }

            pnlLobbyHeaderBar.Dock = DockStyle.Top;
            pnlLobbyHeaderBar.Height = 112;
            pnlLobbyHeaderBar.BackColor = Color.Transparent;

            if (pnlLobbyIconBox == null)
            {
                pnlLobbyIconBox = new LobbyHeaderIconBox();
            }
            pnlLobbyIconBox.Location = new Point(22, 26);
            pnlLobbyIconBox.Size = new Size(60, 60);
            pnlLobbyIconBox.BackColor = Color.Transparent;

            if (lblLobbyTitle == null)
            {
                lblLobbyTitle = new Label();
            }
            lblLobbyTitle.Text = "Sảnh phòng đấu";
            lblLobbyTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblLobbyTitle.ForeColor = Color.White;
            lblLobbyTitle.BackColor = Color.Transparent;
            lblLobbyTitle.AutoSize = true;
            lblLobbyTitle.Location = new Point(98, 20);

            if (lblLobbySubtitle == null)
            {
                lblLobbySubtitle = new Label();
            }
            lblLobbySubtitle.Text = "Khám phá phòng đang mở trong mạng LAN";
            lblLobbySubtitle.Font = new Font("Segoe UI", 10.5F);
            lblLobbySubtitle.ForeColor = Color.FromArgb(165, 190, 225);
            lblLobbySubtitle.BackColor = Color.Transparent;
            lblLobbySubtitle.AutoSize = true;
            lblLobbySubtitle.Location = new Point(100, 66);

            if (!pnlLobbyHeaderBar.Controls.Contains(pnlLobbyIconBox))
                pnlLobbyHeaderBar.Controls.Add(pnlLobbyIconBox);
            if (!pnlLobbyHeaderBar.Controls.Contains(lblLobbyTitle))
                pnlLobbyHeaderBar.Controls.Add(lblLobbyTitle);
            if (!pnlLobbyHeaderBar.Controls.Contains(lblLobbySubtitle))
                pnlLobbyHeaderBar.Controls.Add(lblLobbySubtitle);

            if (pnlJoinRoomView != null)
            {
                if (!pnlJoinRoomView.Controls.Contains(pnlLobbyHeaderBar))
                {
                    pnlJoinRoomView.Controls.Add(pnlLobbyHeaderBar);
                }

                // In WinForms docking:
                // SendToBack() moves a control to the back of z-order so it docks FIRST at the outermost edge.
                // BringToFront() moves a control to the front so it docks LAST (Fill).
                if (lblLobbyLastUpdated != null) lblLobbyLastUpdated.SendToBack();
                if (pnlLobbyFilterBar != null) pnlLobbyFilterBar.SendToBack();
                pnlLobbyHeaderBar.SendToBack();
                if (pnlLobbyBody != null) pnlLobbyBody.BringToFront();
            }

            _currentFilter = "all";
            _currentSearchText = string.Empty;
            if (lblLobbyLastUpdated != null)
                lblLobbyLastUpdated.Text = $"Cập nhật vừa xong • {DateTime.Now:HH:mm}";
            if (pnlJoinRoomView != null)
                pnlJoinRoomView.Resize += (s, e) => RelayoutLobbyView();
            RelayoutLobbyView();
        }

        private void RelayoutGameHeader()
        {
            if (pnlGameHeader == null || pnlGameActions == null || timerWidget == null || pnlGameInfo == null) return;
            int pad = 12;
            int top = 10;
            int h = Math.Max(40, pnlGameHeader.ClientSize.Height - 20);

            int right = pnlGameHeader.ClientSize.Width - pad;

            pnlGameActions.SetBounds(right - pnlGameActions.Width, top, pnlGameActions.Width, h);
            right = pnlGameActions.Left - 8;

            timerWidget.SetBounds(right - timerWidget.Width, top, timerWidget.Width, h);
            right = timerWidget.Left - 8;

            pnlGameInfo.SetBounds(pad, top, Math.Max(120, right - pad), h);
        }

        private void RelayoutLobbyView()
        {
            pnlLobbyBody_Resize(pnlLobbyBody, EventArgs.Empty);
        }

        private void pnlLobbyBody_Resize(object? sender, EventArgs e)
        {
            if (pnlLobbyBody == null || cardOpenRooms == null || pnlLobbyRightCol == null) return;

            int totalW = pnlLobbyBody.ClientSize.Width;
            int totalH = pnlLobbyBody.ClientSize.Height;
            int rightColW = 280;
            int gap = 12;

            int leftW = Math.Max(320, totalW - rightColW - gap);
            cardOpenRooms.SetBounds(0, 0, leftW, totalH);
            pnlLobbyRightCol.SetBounds(leftW + gap, 0, rightColW, totalH);

            cardJoinByCode.Width = rightColW;
            pnlCodeWrapper.Width = cardJoinByCode.Width - 28;
            txtRoomId.Width = pnlCodeWrapper.Width - 36;
            btnJoinRoom.Width = cardJoinByCode.Width - 28;
            btnJoinRoom.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnJoinRoom.TextLeftPadding = 0;
            lblCodeFootnote.Width = cardJoinByCode.Width - 28;

            cardFeaturedRoom.Top = cardJoinByCode.Bottom + gap;
            cardFeaturedRoom.Width = rightColW;
            cardFeaturedRoom.Height = Math.Max(215, totalH - cardFeaturedRoom.Top);

            if (miniBoardPreview != null)
            {
                miniBoardPreview.Size = new Size(82, 82);
                miniBoardPreview.Location = new Point(14, 42);
            }

            if (lblFeaturedRoomName != null) lblFeaturedRoomName.Location = new Point(106, 38);
            if (lblFeaturedHost != null) lblFeaturedHost.Location = new Point(106, 62);
            if (lblFeaturedMode != null) lblFeaturedMode.Location = new Point(106, 82);
            if (lblFeaturedStatus != null) lblFeaturedStatus.Location = new Point(106, 102);

            if (lblFeaturedTitle != null)
            {
                lblFeaturedTitle.Location = new Point(12, 12);
                lblFeaturedTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            if (lblViewAllRooms != null)
            {
                lblViewAllRooms.Left = cardFeaturedRoom.Width - lblViewAllRooms.Width - 14;
            }
            btnFeaturedAction.Top = cardFeaturedRoom.Height - 48;
            btnFeaturedAction.Width = cardFeaturedRoom.Width - 28;
            btnFeaturedAction.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnFeaturedAction.TextLeftPadding = 0;

            if (pnlOpenRoomsScroll != null)
            {
                int itemW = Math.Max(280, pnlOpenRoomsScroll.ClientSize.Width - 16);
                foreach (Control c in pnlOpenRoomsScroll.Controls)
                {
                    if (c is RoomListItemControl item)
                    {
                        item.Width = itemW;
                    }
                }
            }
        }

        private void PillFilter_Click(object? sender, EventArgs e)
        {
            if (sender is LobbyFilterPill pill)
            {
                pillFilterAll.IsSelected = (pill == pillFilterAll);
                pillFilter2p.IsSelected = (pill == pillFilter2p);
                pillFilter4p.IsSelected = (pill == pillFilter4p);
                pillFilterWaiting.IsSelected = (pill == pillFilterWaiting);

                _currentFilter = pill.FilterKey;
                FilterAndRenderRooms();
            }
        }

        private void txtSearchRoom_TextChanged(object? sender, EventArgs e)
        {
            _currentSearchText = txtSearchRoom.Text.Trim();
            FilterAndRenderRooms();
        }

        private async void btnRefreshLobby_Click(object? sender, EventArgs e)
        {
            if (_netClient == null || !_netClient.IsConnected)
            {
                MessageBox.Show("Vui lòng kết nối Server trước khi làm mới danh sách phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            await _netClient.RequestRoomListAsync();
            lblLobbyLastUpdated.Text = $"Cập nhật vừa xong • {DateTime.Now:HH:mm}";
        }

        private void lblViewAllRooms_Click(object? sender, EventArgs e)
        {
            var rawList = _currentRoomList ?? new List<RoomInfoData>();
            var joinableRooms = rawList.Where(r => r.CanJoinAsPlayer).ToList();
            var candidates = joinableRooms.Count > 0 ? joinableRooms : rawList;

            if (candidates.Count > 1)
            {
                _suggestedRoomIndex = (_suggestedRoomIndex + 1) % candidates.Count;
                FilterAndRenderRooms();
            }
            else
            {
                pillFilterAll.IsSelected = true;
                pillFilter2p.IsSelected = false;
                pillFilter4p.IsSelected = false;
                pillFilterWaiting.IsSelected = false;
                _currentFilter = "all";
                _currentSearchText = string.Empty;
                txtSearchRoom.Text = string.Empty;
                FilterAndRenderRooms();
            }
        }

        private async void btnFeaturedAction_Click(object? sender, EventArgs e)
        {
            if (!_netClient.IsConnected)
            {
                SwitchToView(pnlSettingsView, btnNavSettings);
                MessageBox.Show("Vui lòng Kết nối Server trước khi vào phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isInRoom)
            {
                MessageBox.Show("Bạn đang ở trong phòng đấu. Vui lòng thoát phòng hiện tại trước khi tham gia phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnFeaturedAction.Tag is RoomInfoData room)
            {
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
            else
            {
                SwitchToView(pnlCreateRoomView, btnNavCreate);
            }
        }

        private Image? GetPlayerAvatarImage(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName)) playerName = "Player";
            string key = playerName.Trim();

            if (_avatarCache.TryGetValue(key, out var cached))
            {
                return cached;
            }

            try
            {
                string cleanName = key.ToLowerInvariant();
                string[] searchDirs = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets"),
                    Path.Combine(Directory.GetCurrentDirectory(), "Assets"),
                    Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets"),
                    @"d:\PBL4\CaroClient\Assets"
                };

                // 1. Specific player avatar (e.g. avatar_minhquan.png, avatar_ducanh.png, avatar_baonam.png, avatar_proplayer.png, avatar_huy.png)
                foreach (string dir in searchDirs)
                {
                    if (Directory.Exists(dir))
                    {
                        string specific = Path.Combine(dir, $"avatar_{cleanName}.png");
                        if (File.Exists(specific))
                        {
                            using var fs = new FileStream(specific, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            using var bmp = new Bitmap(fs);
                            var img = new Bitmap(bmp);
                            _avatarCache[key] = img;
                            return img;
                        }
                    }
                }

                // 2. Check if current user
                string myName = !string.IsNullOrWhiteSpace(_netClient?.PlayerName) ? _netClient.PlayerName : txtName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(myName) && key.Equals(myName, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (string dir in searchDirs)
                    {
                        if (Directory.Exists(dir))
                        {
                            string huy = Path.Combine(dir, "avatar_huy.png");
                            if (File.Exists(huy))
                            {
                                using var fs = new FileStream(huy, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                                using var bmp = new Bitmap(fs);
                                var img = new Bitmap(bmp);
                                _avatarCache[key] = img;
                                return img;
                            }
                        }
                    }
                }

                // 3. Stylized dynamic avatar generator
                var generated = GenerateStylizedAvatar(key);
                _avatarCache[key] = generated;
                return generated;
            }
            catch
            {
                var fallback = GenerateStylizedAvatar(key);
                _avatarCache[key] = fallback;
                return fallback;
            }
        }

        private Image GenerateStylizedAvatar(string name)
        {
            int sz = 64;
            Bitmap bmp = new Bitmap(sz, sz, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                int hash = Math.Abs(name.GetHashCode());
                Color c1, c2;
                switch (hash % 4)
                {
                    case 0:
                        c1 = Color.FromArgb(0, 194, 255);
                        c2 = Color.FromArgb(0, 90, 220);
                        break;
                    case 1:
                        c1 = Color.FromArgb(180, 80, 255);
                        c2 = Color.FromArgb(100, 30, 200);
                        break;
                    case 2:
                        c1 = Color.FromArgb(0, 230, 140);
                        c2 = Color.FromArgb(0, 140, 70);
                        break;
                    default:
                        c1 = Color.FromArgb(255, 170, 30);
                        c2 = Color.FromArgb(220, 80, 10);
                        break;
                }

                Rectangle r = new Rectangle(0, 0, sz - 1, sz - 1);
                using (LinearGradientBrush lgb = new LinearGradientBrush(r, c1, c2, LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(lgb, r);
                }
                using (Pen ring = new Pen(Color.FromArgb(220, 255, 255, 255), 2f))
                {
                    g.DrawEllipse(ring, 1, 1, sz - 3, sz - 3);
                }

                string initial = !string.IsNullOrWhiteSpace(name) ? name.Substring(0, 1).ToUpper() : "P";
                using (Font f = new Font("Segoe UI", 24f, FontStyle.Bold, GraphicsUnit.Pixel))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    g.DrawString(initial, f, textBrush, new RectangleF(0, 0, sz, sz), sf);
                }
            }
            return bmp;
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
            _currentRoomList = rooms ?? new List<RoomInfoData>();
            lblLobbyLastUpdated.Text = $"Cập nhật vừa xong • {DateTime.Now:HH:mm}";
            FilterAndRenderRooms();
        }

        private void FilterAndRenderRooms()
        {
            var list = _currentRoomList ?? new List<RoomInfoData>();

            IEnumerable<RoomInfoData> filtered = list;
            if (_currentFilter == "2p")
            {
                filtered = filtered.Where(r => r.Mode == GameMode.PvP);
            }
            else if (_currentFilter == "4p")
            {
                filtered = filtered.Where(r => r.SpectatorCount >= 2 || r.Mode == GameMode.PvAI);
            }
            else if (_currentFilter == "waiting")
            {
                filtered = filtered.Where(r => r.CanJoinAsPlayer);
            }

            if (!string.IsNullOrWhiteSpace(_currentSearchText))
            {
                filtered = filtered.Where(r =>
                    (r.RoomId != null && r.RoomId.Contains(_currentSearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (r.Player1Name != null && r.Player1Name.Contains(_currentSearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (r.Player2Name != null && r.Player2Name.Contains(_currentSearchText, StringComparison.OrdinalIgnoreCase)));
            }

            var filteredList = filtered.ToList();
            lblOpenRoomsCount.Text = $"{filteredList.Count} phòng";

            // Intelligent Room Suggestion Engine
            var joinableRooms = list.Where(r => r.CanJoinAsPlayer).ToList();
            var candidates = joinableRooms.Count > 0 ? joinableRooms : list;

            if (candidates.Count > 0)
            {
                if (_suggestedRoomIndex >= candidates.Count) _suggestedRoomIndex = 0;
                var suggested = candidates[_suggestedRoomIndex];

                lblFeaturedTitle.Text = "★ Gợi ý phòng";
                if (candidates.Count > 1)
                {
                    lblViewAllRooms.Text = $"Đổi gợi ý ↻ ({_suggestedRoomIndex + 1}/{candidates.Count})";
                }
                else
                {
                    lblViewAllRooms.Text = "Xem tất cả →";
                }
                lblViewAllRooms.Visible = true;
                lblViewAllRooms.Left = cardFeaturedRoom.Width - lblViewAllRooms.Width - 14;

                Image? hostAvatar = GetPlayerAvatarImage(suggested.Player1Name);
                miniBoardPreview.SetCustomImage(hostAvatar);

                lblFeaturedRoomName.Text = !string.IsNullOrWhiteSpace(suggested.Player1Name) ? $"Phòng {suggested.Player1Name}" : $"Phòng #{suggested.RoomId}";
                lblFeaturedHost.Text = $"Chủ: {suggested.Player1Name}";
                lblFeaturedMode.Text = $"2 người • {(suggested.TimePerTurn > 0 ? suggested.TimePerTurn + "s" : "Tự do")}";
                lblFeaturedStatus.Text = suggested.CanJoinAsPlayer ? "● Đang chờ bạn" : "● Đang thi đấu";
                lblFeaturedStatus.ForeColor = suggested.CanJoinAsPlayer ? Color.FromArgb(0, 220, 120) : Color.FromArgb(0, 194, 255);
                btnFeaturedAction.Text = suggested.CanJoinAsPlayer ? "⚡ Vào chơi ngay" : "👁 Xem phòng";
                btnFeaturedAction.NavIcon = suggested.CanJoinAsPlayer ? CyberIcon.Door : CyberIcon.Eye;
                btnFeaturedAction.IsPrimaryGradient = suggested.CanJoinAsPlayer;
                btnFeaturedAction.Tag = suggested;
            }
            else
            {
                lblFeaturedTitle.Text = "💡 Gợi ý cho bạn";
                lblViewAllRooms.Text = "Mạng LAN";
                lblViewAllRooms.Visible = true;
                lblViewAllRooms.Left = cardFeaturedRoom.Width - lblViewAllRooms.Width - 14;

                miniBoardPreview.SetCustomImage(null);

                lblFeaturedRoomName.Text = "Mở phòng mới";
                lblFeaturedHost.Text = "Chưa có phòng";
                lblFeaturedMode.Text = "2 người (LAN)";
                lblFeaturedStatus.Text = "● Đang chờ mở phòng";
                lblFeaturedStatus.ForeColor = Color.FromArgb(0, 194, 255);
                btnFeaturedAction.Text = "⚡ Tạo phòng ngay";
                btnFeaturedAction.NavIcon = CyberIcon.Plus;
                btnFeaturedAction.IsPrimaryGradient = true;
                btnFeaturedAction.Tag = null;
            }

            pnlOpenRoomsScroll.SuspendLayout();
            pnlOpenRoomsScroll.Controls.Clear();

            if (filteredList.Count == 0)
            {
                lblNoRooms.Text = list.Count == 0
                    ? "Hiện chưa có phòng nào đang mở trong mạng LAN.\nHãy bấm '+ Tạo phòng' ở menu bên trái để tạo phòng thi đấu mới!"
                    : "Không tìm thấy phòng nào phù hợp với bộ lọc.";
                lblNoRooms.Visible = true;
                pnlOpenRoomsScroll.Controls.Add(lblNoRooms);
            }
            else
            {
                lblNoRooms.Visible = false;
                int itemW = Math.Max(280, pnlOpenRoomsScroll.ClientSize.Width - 16);
                int topY = 4;
                int itemH = 66;
                int gap = 8;

                foreach (var room in filteredList)
                {
                    Image? hostAvatar = GetPlayerAvatarImage(room.Player1Name);
                    var item = new RoomListItemControl(room, hostAvatar)
                    {
                        Location = new Point(4, topY),
                        Width = itemW,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left
                    };

                    item.JoinClicked += async (r) =>
                    {
                        if (_isInRoom)
                        {
                            MessageBox.Show("Bạn đang ở trong phòng đấu. Vui lòng thoát phòng hiện tại trước khi tham gia phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (_netClient == null || !_netClient.IsConnected)
                        {
                            SwitchToView(pnlSettingsView, btnNavSettings);
                            MessageBox.Show("Vui lòng kết nối máy chủ LAN để tham gia phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        txtRoomId.Text = r.RoomId;
                        _currentRoomMode = r.Mode;
                        await _netClient.JoinRoomAsync(r.RoomId);
                    };

                    item.SpectateClicked += async (r) =>
                    {
                        if (_isInRoom)
                        {
                            MessageBox.Show("Bạn đang ở trong phòng đấu. Vui lòng thoát phòng hiện tại trước khi xem phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (_netClient == null || !_netClient.IsConnected)
                        {
                            SwitchToView(pnlSettingsView, btnNavSettings);
                            MessageBox.Show("Vui lòng kết nối máy chủ LAN để xem phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        txtRoomId.Text = r.RoomId;
                        await _netClient.SendSpectateRoomAsync(r.RoomId);
                    };

                    pnlOpenRoomsScroll.Controls.Add(item);
                    topY += itemH + gap;
                }
            }

            pnlOpenRoomsScroll.ResumeLayout(true);
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
                lblRoomIdInfo.Text = $"Phòng: {resp.RoomId} (Đang xem) ({ruleText} | {timeText})";
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

        private void btnMin_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMax_Click(object? sender, EventArgs e)
        {
            this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNavHome_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlHomeView, btnNavHome);
        }

        private void btnNavCreate_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlCreateRoomView, btnNavCreate);
        }

        private void btnNavJoin_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlJoinRoomView, btnNavJoin);
        }

        private void btnNavLeaderboard_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlLeaderboardView, btnNavLeaderboard);
        }

        private void btnNavHistory_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlHistoryView, btnNavHistory);
        }

        private void btnNavSettings_Click(object? sender, EventArgs e)
        {
            HandleNavSettingsClick();
        }

        private void pnlProfileCard_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlHistoryView, btnNavHistory);
        }

        private void btnHomeCreateRoom_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlCreateRoomView, btnNavCreate);
        }

        private void btnHomeJoinRoom_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlJoinRoomView, btnNavJoin);
        }

        private void btnHomeSettings_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlSettingsView, btnNavSettings);
        }

        private void btnCancelCreate_Click(object? sender, EventArgs e)
        {
            SwitchToView(pnlHomeView, btnNavHome);
        }

        private void btnTabLogin_Click(object? sender, EventArgs e)
        {
            SwitchAuthTab(true);
        }

        private void btnTabRegister_Click(object? sender, EventArgs e)
        {
            SwitchAuthTab(false);
        }

        private void lblSwitchToRegister_Click(object? sender, EventArgs e)
        {
            SwitchAuthTab(false);
        }

        private void lblSwitchToLogin_Click(object? sender, EventArgs e)
        {
            SwitchAuthTab(true);
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
                btnNavSettings.Text = "ĐĂNG NHẬP";
                btnNavSettings.NavIcon = CyberIcon.Lightning;
                btnNavSettings.IsPrimaryGradient = (activeNav == btnNavSettings);
                btnNavSettings.NormalColor = (activeNav == btnNavSettings) ? UITheme.AccentBlue : Color.FromArgb(14, 24, 46);
            }
            else
            {
                btnNavSettings.Text = "Đăng xuất";
                btnNavSettings.NavIcon = CyberIcon.Power;
                btnNavSettings.IsPrimaryGradient = false;
                btnNavSettings.NormalColor = Color.FromArgb(36, 20, 32);
            }
            btnNavSettings.Invalidate();

            if (targetView == pnlGameView)
            {
                RelayoutGameHeader();
            }

            if (targetView == pnlCreateRoomView)
            {
                RelayoutCreateBox();
            }

            // Tự động làm mới danh sách phòng khi vào màn hình Tham gia phòng
            if (targetView == pnlJoinRoomView)
            {
                RelayoutLobbyView();
                if (_netClient != null && _netClient.IsConnected)
                {
                    _ = _netClient.RequestRoomListAsync();
                }
            }

            // Tự động làm mới hồ sơ & lịch sử đấu khi vào màn hình Hồ sơ cá nhân
            if (targetView == pnlHistoryView)
            {
                RelayoutProfileView();
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
                    lblNoHistory.Text = "Bạn đang chơi ở chế độ Ngoại tuyến (Khách).\nHãy bấm nút 'ĐĂNG NHẬP' ở góc dưới bên trái để lưu trữ lịch sử thi đấu online!";
                    lblNoHistory.Visible = true;
                    dgvMatchHistory.Visible = false;
                }
            }

            // Tự động làm mới bảng xếp hạng khi vào màn hình Bảng xếp hạng
            if (targetView == pnlLeaderboardView)
            {
                pnlLeaderboardView_Resize(this, EventArgs.Empty);
                if (_netClient != null && _netClient.IsConnected)
                {
                    _ = _netClient.RequestLeaderboardAsync();
                }
                else
                {
                    leaderboardPodium?.ClearData();
                    leaderboardList?.ClearData();
                }
            }

            if (targetView == pnlSettingsView)
            {
                pnlSettingsView_Resize(this, EventArgs.Empty);
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

            btnTabLogin.Text = "🔍  ĐĂNG NHẬP";
            btnTabRegister.Text = "✏  ĐĂNG KÝ MỚI";

            if (isLogin)
            {
                btnTabLogin.IsPrimaryGradient = false;
                btnTabLogin.NormalColor = Color.FromArgb(14, 42, 68);
                btnTabLogin.BorderLineColor = Color.FromArgb(0, 194, 255);
                btnTabLogin.ForeColor = Color.FromArgb(0, 230, 255);

                btnTabRegister.IsPrimaryGradient = false;
                btnTabRegister.NormalColor = Color.FromArgb(32, 20, 16);
                btnTabRegister.BorderLineColor = Color.FromArgb(180, 80, 30);
                btnTabRegister.ForeColor = Color.FromArgb(220, 150, 110);

                cardSettingsBox.Height = 412;
                txtName.Focus();
            }
            else
            {
                btnTabLogin.IsPrimaryGradient = false;
                btnTabLogin.NormalColor = Color.FromArgb(14, 25, 48);
                btnTabLogin.BorderLineColor = Color.FromArgb(40, 65, 100);
                btnTabLogin.ForeColor = Color.FromArgb(130, 160, 200);

                btnTabRegister.IsPrimaryGradient = false;
                btnTabRegister.NormalColor = Color.FromArgb(64, 30, 16);
                btnTabRegister.BorderLineColor = Color.FromArgb(255, 107, 0);
                btnTabRegister.ForeColor = Color.FromArgb(255, 175, 75);

                cardSettingsBox.Height = 412;
                txtRegIp.Text = txtIp.Text;
                txtRegPort.Text = txtPort.Text;
                txtRegName.Focus();
            }
            btnTabLogin.Invalidate();
            btnTabRegister.Invalidate();
            pnlSettingsView_Resize(this, EventArgs.Empty);
            UpdateMascotReaction();
        }

        private void pnlSettingsView_Resize(object? sender, EventArgs e)
        {
            if (cardSettingsBox != null && pnlSettingsView != null && pnlSettingsView.Width > 0 && pnlSettingsView.Height > 0)
            {
                int cx = Math.Max(10, (pnlSettingsView.Width - cardSettingsBox.Width) / 2);
                int totalH = cardSettingsBox.Height + 90;
                int cy = Math.Max(95, (pnlSettingsView.Height - totalH) / 2 + 85);
                cardSettingsBox.Location = new Point(cx, cy);
                pnlSettingsView.CardBox = cardSettingsBox;
                pnlSettingsView.Invalidate();
            }
        }

        private void pnlLeaderboardView_Resize(object? sender, EventArgs e)
        {
            if (leaderboardPodium != null && cardLeaderboardBox != null && cardLeaderboardBox.Height > 0)
            {
                int targetH = Math.Min(280, Math.Max(220, (int)(cardLeaderboardBox.Height * 0.38f)));
                if (leaderboardPodium.Height != targetH)
                {
                    leaderboardPodium.Height = targetH;
                }
            }
        }

        private void lblTogglePassword_Click(object? sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '●')
            {
                txtPassword.PasswordChar = '\0';
                lblTogglePassword.ForeColor = Color.FromArgb(0, 194, 255);
            }
            else
            {
                txtPassword.PasswordChar = '●';
                lblTogglePassword.ForeColor = Color.FromArgb(120, 150, 190);
            }
            UpdateMascotReaction();
        }

        private void lblToggleRegPass_Click(object? sender, EventArgs e)
        {
            if (txtRegPassword.PasswordChar == '●')
            {
                txtRegPassword.PasswordChar = '\0';
                lblToggleRegPass.ForeColor = Color.FromArgb(0, 194, 255);
            }
            else
            {
                txtRegPassword.PasswordChar = '●';
                lblToggleRegPass.ForeColor = Color.FromArgb(120, 150, 190);
            }
            UpdateMascotReaction();
        }

        private void lblToggleRegConfirm_Click(object? sender, EventArgs e)
        {
            if (txtRegConfirmPassword.PasswordChar == '●')
            {
                txtRegConfirmPassword.PasswordChar = '\0';
                lblToggleRegConfirm.ForeColor = Color.FromArgb(0, 194, 255);
            }
            else
            {
                txtRegConfirmPassword.PasswordChar = '●';
                lblToggleRegConfirm.ForeColor = Color.FromArgb(120, 150, 190);
            }
            UpdateMascotReaction();
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

            ForfeitAndExitOfflineGame();

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
            ForfeitAndExitOfflineGame();

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
            ForfeitAndExitOfflineGame();

            if (_isInRoom && !_isOfflineGame)
            {
                if (_currentGameStatus == GameStateStatus.Playing)
                {
                    _ = _netClient.SendSurrenderAsync();
                }
                _ = _netClient.SendLeaveRoomAsync();
                ExitRoomUI();
            }

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
            lblNoHistory.Text = "Bạn đang ở chế độ Ngoại tuyến (Khách).\nHãy bấm nút 'ĐĂNG NHẬP' ở góc dưới bên trái để lưu trữ lịch sử thi đấu online!";
            lblNoHistory.Visible = true;
            dgvMatchHistory.Visible = false;

            dgvLeaderboard.Rows.Clear();
            lblNoLeaderboard.Text = "Bạn chưa kết nối Server.\nVui lòng bấm nút 'ĐĂNG NHẬP' ở góc dưới bên trái để xem Bảng xếp hạng!";
            lblNoLeaderboard.Visible = true;
            dgvLeaderboard.Visible = false;

            UpdateUIState(false);
            UpdateServerAndPlayerInfo(false);
            UpdateProfileViewUI();
            UpdateRoomListUI(new List<RoomInfoData>());

            lblStatusBadge.Text = "● Chưa kết nối";
            lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
            btnConnect.Text = "KẾT NỐI & ĐĂNG NHẬP";
            btnConnect.NavIcon = CyberIcon.Lightning;
            btnConnect.IsPurpleGradient = true;
            btnConnect.BorderLineColor = Color.FromArgb(192, 38, 211);
            btnConnect.Enabled = true;

            btnNavSettings.Text = "ĐĂNG NHẬP";
            btnNavSettings.NavIcon = CyberIcon.Lightning;
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
                ForfeitAndExitOfflineGame();

                lblStatusBadge.Text = $"● Đã kết nối: {data.Username}";
                lblStatusBadge.ForeColor = Color.FromArgb(0, 230, 118);
                btnConnect.Text = "Ngắt kết nối";
                btnConnect.NavIcon = CyberIcon.Power;
                btnConnect.IsPurpleGradient = false;
                btnConnect.NormalColor = Color.FromArgb(45, 20, 25);
                btnConnect.BorderLineColor = Color.FromArgb(200, 50, 70);

                btnNavSettings.Text = "Đăng xuất";
                btnNavSettings.NavIcon = CyberIcon.Power;
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
                lblStatusBadge.Text = "● " + data.Message;
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
                lblRegStatusBadge.Text = "● " + data.Message;
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

                lblProfileDetailName.Text = uname;
                lblProfileDetailElo.Text = $"★ {elo} ELO";
                lblProfileDetailRank.Text = GetRankTitle(elo);
                lblProfileDetailStatus.Text = $"● Trực tuyến • Máy chủ LAN ({_netClient.ServerIp}:{_netClient.ServerPort})";
                lblProfileDetailStatus.ForeColor = Color.FromArgb(0, 230, 118);
                avatarControl.IsOnline = true;
                LoadProfileAssets();

                // KPI Widgets in Hero Card
                kpiTotal.IconType = KpiIconType.Trophy;
                kpiTotal.LabelText = "Tổng trận";
                kpiTotal.ValueText = total.ToString();

                kpiWins.IconType = KpiIconType.Checkmark;
                kpiWins.LabelText = "Thắng";
                kpiWins.ValueText = wins.ToString();

                kpiLosses.IconType = KpiIconType.Cross;
                kpiLosses.LabelText = "Thua";
                kpiLosses.ValueText = losses.ToString();
                gaugeWinRate.WinRate = winRate;

                // Quick Stats in Middle Card
                quickStatTotal.ValueText = total.ToString();
                quickStatWins.ValueText = wins.ToString();
                quickStatElo.ValueText = elo.ToString();

                // Dynamic Badges based on ELO & Wins
                badgeTanThu.BadgeTitle = "Tân Thủ";
                badgeChienBinh.BadgeTitle = (elo >= 1100 || wins >= 5) ? "Chiến Binh" : "???";
                badgeCaoThu.BadgeTitle = (elo >= 1300 || wins >= 15) ? "Cao Thủ" : "???";
                badgeLocked.BadgeTitle = "???";

                // Sidebar Mini Card
                lblProfileName.Text = $"{uname}  ●";
                lblProfileLevel.Text = $"★ {elo} ELO";
                lblProfileXp.Text = $"{wins}W - {losses}L - {draws}D ({winRate}%)";
                btnLogout.Visible = true;
            }
            else
            {
                lblProfileDetailName.Text = "Khách (Offline)";
                lblProfileDetailElo.Text = "★ 1000 ELO";
                lblProfileDetailRank.Text = "Tập Sự";
                lblProfileDetailStatus.Text = "● Ngoại tuyến • Chế độ chơi Offline với Máy";
                lblProfileDetailStatus.ForeColor = Color.FromArgb(255, 145, 0);
                avatarControl.IsOnline = false;

                kpiTotal.IconType = KpiIconType.Trophy;
                kpiTotal.LabelText = "Tổng trận";
                kpiTotal.ValueText = "0";

                kpiWins.IconType = KpiIconType.Checkmark;
                kpiWins.LabelText = "Thắng";
                kpiWins.ValueText = "0";

                kpiLosses.IconType = KpiIconType.Cross;
                kpiLosses.LabelText = "Thua";
                kpiLosses.ValueText = "0";
                gaugeWinRate.WinRate = 0;

                quickStatTotal.ValueText = "0";
                quickStatWins.ValueText = "0";
                quickStatElo.ValueText = "1000";

                badgeTanThu.BadgeTitle = "Tân Thủ";
                badgeChienBinh.BadgeTitle = "???";
                badgeCaoThu.BadgeTitle = "???";
                badgeLocked.BadgeTitle = "???";

                lblProfileName.Text = "Khách (Offline)";
                lblProfileLevel.Text = "Chế độ Offline";
                lblProfileXp.Text = "Chơi với Máy";
                btnLogout.Visible = false;
            }

            lblEditNameIcon.Left = lblProfileDetailName.Right + 8;
            lblEditNameIcon.Top = lblProfileDetailName.Top + (lblProfileDetailName.Height - lblEditNameIcon.Height) / 2;
        }

        private void cardProfileHero_Resize(object? sender, EventArgs e)
        {
            if (cardProfileHero == null || gaugeWinRate == null) return;
            int rightEdge = cardProfileHero.Width - 18;

            gaugeWinRate.Left = rightEdge - gaugeWinRate.Width;
            kpiLosses.Left = gaugeWinRate.Left - kpiLosses.Width - 14;
            kpiWins.Left = kpiLosses.Left - kpiWins.Width - 10;
            kpiTotal.Left = kpiWins.Left - kpiTotal.Width - 10;

            gaugeWinRate.Top = Math.Max(4, (cardProfileHero.Height - gaugeWinRate.Height) / 2);
            int kpiY = Math.Max(4, (cardProfileHero.Height - kpiTotal.Height) / 2);
            kpiTotal.Top = kpiY;
            kpiWins.Top = kpiY;
            kpiLosses.Top = kpiY;
        }

        private void pnlMiddleRow_Resize(object? sender, EventArgs e)
        {
            if (pnlMiddleRow == null || cardQuickStats == null || cardAchievements == null) return;

            // Chia đều 50% - 50% giữa 2 thẻ Thống kê nhanh và Thành tích nổi bật
            int halfW = (pnlMiddleRow.Width - pnlMiddleSpacer.Width) / 2;
            cardQuickStats.Width = Math.Max(280, halfW);

            // Căn chỉnh 3 mục thống kê nhanh bên trong
            int quickItemW = Math.Max(80, (cardQuickStats.Width - 32) / 3);
            quickStatTotal.Width = quickItemW;
            quickStatWins.Width = quickItemW;
            quickStatElo.Width = quickItemW;

            quickStatTotal.Left = 14;
            quickStatWins.Left = quickStatTotal.Right + 2;
            quickStatElo.Left = quickStatWins.Right + 2;

            // Căn chỉnh 4 huy hiệu thành tích bên phải
            int badgeCount = 4;
            int totalBadgeW = badgeTanThu.Width * badgeCount;
            int availW = cardAchievements.Width - 28;
            int gap = Math.Max(4, (availW - totalBadgeW) / (badgeCount + 1));

            int curX = 14 + gap;
            badgeTanThu.Left = curX;
            curX += badgeTanThu.Width + gap;
            badgeChienBinh.Left = curX;
            curX += badgeChienBinh.Width + gap;
            badgeCaoThu.Left = curX;
            curX += badgeCaoThu.Width + gap;
            badgeLocked.Left = curX;
        }

        private void RelayoutProfileView()
        {
            cardProfileHero_Resize(cardProfileHero, EventArgs.Empty);
            pnlMiddleRow_Resize(pnlMiddleRow, EventArgs.Empty);
        }

        private void lblEditNameIcon_Click(object? sender, EventArgs e)
        {
            if (!_netClient.IsConnected)
            {
                MessageBox.Show("Vui lòng kết nối Server để đổi tên hồ sơ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show($"Tài khoản: {_netClient.PlayerName}\nĐiểm xếp hạng: {_netClient.Elo} ELO\nĐể đổi tên hiển thị, vui lòng liên hệ Ban quản trị.", "Hồ sơ người chơi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static Image? SafeLoadBitmap(string path)
        {
            try
            {
                if (!File.Exists(path)) return null;
                byte[] bytes = File.ReadAllBytes(path);
                using var ms = new MemoryStream(bytes);
                using var temp = Image.FromStream(ms);
                return new Bitmap(temp);
            }
            catch
            {
                return null;
            }
        }

        private void AvatarControl_AvatarClicked(object? sender, EventArgs e)
        {
            string currentName = !string.IsNullOrWhiteSpace(_netClient?.PlayerName) ? _netClient.PlayerName : txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(currentName)) currentName = "huy";

            using var modal = new AvatarSelectionModal(currentName, avatarControl.AvatarImage);
            if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedAvatarImage != null)
            {
                ApplyNewAvatar(currentName, modal.SelectedAvatarImage);
            }
        }

        private void ApplyNewAvatar(string playerName, Image newAvatar)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string assetsDir = Path.Combine(baseDir, "Assets");
                if (!Directory.Exists(assetsDir))
                    assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
                if (!Directory.Exists(assetsDir))
                    assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets");
                if (!Directory.Exists(assetsDir))
                    assetsDir = @"d:\PBL4\CaroClient\Assets";

                Directory.CreateDirectory(assetsDir);

                string cleanName = playerName.Trim().ToLowerInvariant();
                string targetFile = Path.Combine(assetsDir, $"avatar_{cleanName}.png");

                // Save to assets directory
                using (var copy = new Bitmap(newAvatar))
                {
                    copy.Save(targetFile, System.Drawing.Imaging.ImageFormat.Png);
                    if (cleanName == "huy")
                    {
                        string huyPath = Path.Combine(assetsDir, "avatar_huy.png");
                        if (!string.Equals(targetFile, huyPath, StringComparison.OrdinalIgnoreCase))
                        {
                            copy.Save(huyPath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }

                // If running in bin/Debug, save there too
                string binAssets = Path.Combine(baseDir, "Assets");
                if (binAssets != assetsDir && Directory.Exists(binAssets))
                {
                    try
                    {
                        using var copy = new Bitmap(newAvatar);
                        copy.Save(Path.Combine(binAssets, $"avatar_{cleanName}.png"), System.Drawing.Imaging.ImageFormat.Png);
                        if (cleanName == "huy") copy.Save(Path.Combine(binAssets, "avatar_huy.png"), System.Drawing.Imaging.ImageFormat.Png);
                    }
                    catch { }
                }

                // Update in-memory cache
                var avatarCopy = new Bitmap(newAvatar);
                _avatarCache[playerName] = avatarCopy;
                _avatarCache[cleanName] = avatarCopy;
                if (cleanName == "huy") _avatarCache["huy"] = avatarCopy;

                // Update controls
                avatarControl.AvatarImage = avatarCopy;
                avatarControl.Invalidate();

                // Refresh lobby room previews so user's hosted room shows the new avatar immediately
                FilterAndRenderRooms();

                MessageBox.Show("Cập nhật ảnh đại diện thành công!", "Ảnh đại diện", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lưu ảnh đại diện: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProfileAssets()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string assetsDir = Path.Combine(baseDir, "Assets");
                if (!Directory.Exists(assetsDir))
                    assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
                if (!Directory.Exists(assetsDir))
                    assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets");
                if (!Directory.Exists(assetsDir))
                    assetsDir = @"d:\PBL4\CaroClient\Assets";

                string currentName = !string.IsNullOrWhiteSpace(_netClient?.PlayerName) ? _netClient.PlayerName : txtName.Text.Trim();
                if (string.IsNullOrWhiteSpace(currentName)) currentName = "huy";

                Image? loadedAvatar = GetPlayerAvatarImage(currentName);
                if (loadedAvatar != null)
                {
                    avatarControl.AvatarImage = loadedAvatar;
                }

                badgeTanThu.BadgeImage = SafeLoadBitmap(Path.Combine(assetsDir, "badge_tanthu.png"));
                badgeChienBinh.BadgeImage = SafeLoadBitmap(Path.Combine(assetsDir, "badge_chienbinh.png"));
                badgeCaoThu.BadgeImage = SafeLoadBitmap(Path.Combine(assetsDir, "badge_caothu.png"));
                badgeLocked.BadgeImage = SafeLoadBitmap(Path.Combine(assetsDir, "badge_locked.png"));
            }
            catch { }
        }

        private void dgvMatchHistory_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Graphics == null) return;

            // 1. Cột Kết quả -> Vẽ Capsule Pill bo tròn mềm mại
            if (dgvMatchHistory.Columns[e.ColumnIndex].Name == "Result")
            {
                e.PaintBackground(e.CellBounds, true);
                string? val = e.Value?.ToString();
                if (!string.IsNullOrEmpty(val))
                {
                    Color pillColor = val switch
                    {
                        "Thắng" => Color.FromArgb(0, 160, 80),
                        "Thua" => Color.FromArgb(205, 38, 62),
                        _ => Color.FromArgb(100, 115, 140)
                    };

                    int pillW = 68;
                    int pillH = 24;
                    int pillX = e.CellBounds.X + (e.CellBounds.Width - pillW) / 2;
                    int pillY = e.CellBounds.Y + (e.CellBounds.Height - pillH) / 2;
                    Rectangle pillRect = new Rectangle(pillX, pillY, pillW, pillH);

                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(pillRect, pillH / 2))
                    using (SolidBrush b = new SolidBrush(pillColor))
                    {
                        g.FillPath(b, path);
                    }

                    using (Font f = new Font("Segoe UI", 9f, FontStyle.Bold))
                    using (SolidBrush textBrush = new SolidBrush(Color.White))
                    {
                        SizeF sz = g.MeasureString(val, f);
                        g.DrawString(val, f, textBrush, pillX + (pillW - sz.Width) / 2f, pillY + (pillH - sz.Height) / 2f);
                    }
                }
                e.Handled = true;
            }
            // 2. Cột Thời gian -> Vẽ biểu tượng lịch 📅 nhỏ màu xanh tinh tế
            else if (dgvMatchHistory.Columns[e.ColumnIndex].Name == "PlayedAt")
            {
                e.PaintBackground(e.CellBounds, true);
                string? val = e.Value?.ToString();
                if (!string.IsNullOrEmpty(val))
                {
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    int iconX = e.CellBounds.X + 16;
                    int iconY = e.CellBounds.Y + (e.CellBounds.Height - 14) / 2;
                    Rectangle calRect = new Rectangle(iconX, iconY, 14, 14);

                    using (GraphicsPath cp = UITheme.CreateRoundedRectanglePath(calRect, 2))
                    using (SolidBrush cb = new SolidBrush(Color.FromArgb(25, 45, 90)))
                    {
                        g.FillPath(cb, cp);
                    }
                    using (Pen pen = new Pen(Color.FromArgb(0, 194, 255), 1.2f))
                    {
                        g.DrawRectangle(pen, calRect);
                        g.DrawLine(pen, iconX + 2, iconY + 5, iconX + 12, iconY + 5);
                    }

                    using (Font f = new Font("Segoe UI", 9f))
                    using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(220, 235, 255)))
                    {
                        SizeF sz = g.MeasureString(val, f);
                        g.DrawString(val, f, textBrush, iconX + 20, e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2f);
                    }
                }
                e.Handled = true;
            }
        }

        private string GetRankTitle(int elo)
        {
            if (elo >= 1800) return "Đại Kiện Tướng";
            if (elo >= 1500) return "Kiện Tướng";
            if (elo >= 1300) return "Cao Thủ";
            if (elo >= 1100) return "Chiến Binh";
            return "Tập Sự";
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
                btnConnect.NavIcon = CyberIcon.Power;
                btnConnect.IsPurpleGradient = false;
                btnConnect.NormalColor = Color.FromArgb(45, 20, 25);
                btnConnect.BorderLineColor = Color.FromArgb(200, 50, 70);
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
                lblStatusBadge.Text = "● Thất bại";
                lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
                btnConnect.Text = "KẾT NỐI & ĐĂNG NHẬP";
                btnConnect.NavIcon = CyberIcon.Lightning;
                btnConnect.IsPurpleGradient = true;
                btnConnect.BorderLineColor = Color.FromArgb(192, 38, 211);
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
            btnCreateRoom.Text = _isInRoom ? "Quay lại phòng" : (isAI && !_netClient.IsConnected ? "Chơi với Máy ngay" : "Tạo phòng");
            RelayoutCreateBox();
        }

        private void SelectTimeMode(bool isTurnMode)
        {
            _isTimeLimitMode = isTurnMode;
            _isCountdownGame = !isTurnMode;
            timerWidget.IsCountdownMode = _isCountdownGame;
            cardTimeTurn.IsSelected = isTurnMode;
            cardTimeFree.IsSelected = !isTurnMode;
            pnlTimeLimitOptions.Visible = true;

            // Nhãn hiển thị chế độ
            lblTimePillLabel.Text = isTurnMode ? "Thời gian mỗi lượt:" : "Thời gian đếm ngược:";

            // Chế độ Theo lượt (15s, 30s, 60s)
            pillTime15.Visible = isTurnMode;
            pillTime30.Visible = isTurnMode;
            pillTime60.Visible = isTurnMode;

            // Chế độ Đếm ngược (1p, 3p, 5p, Tùy chỉnh)
            pillCountdown1m.Visible = !isTurnMode;
            pillCountdown3m.Visible = !isTurnMode;
            pillCountdown5m.Visible = !isTurnMode;
            pillCountdownCustom.Visible = !isTurnMode;
            txtCustomMinutes.Visible = !isTurnMode && pillCountdownCustom.IsSelected;
            lblCustomMinutesUnit.Visible = !isTurnMode && pillCountdownCustom.IsSelected;

            _selectedTimeLimit = isTurnMode ? GetSelectedTimePillSeconds() : GetSelectedCountdownSeconds();
            RelayoutCreateBox();
        }

        private void SelectCountdownMinutes(int minutes)
        {
            pillCountdown1m.IsSelected = (minutes == 1);
            pillCountdown3m.IsSelected = (minutes == 3);
            pillCountdown5m.IsSelected = (minutes == 5);
            pillCountdownCustom.IsSelected = false;
            txtCustomMinutes.Visible = false;
            lblCustomMinutesUnit.Visible = false;
            _selectedTimeLimit = minutes * 60;
            LayoutTimeAndAiOptions();
        }

        private void SelectCountdownCustom()
        {
            pillCountdown1m.IsSelected = false;
            pillCountdown3m.IsSelected = false;
            pillCountdown5m.IsSelected = false;
            pillCountdownCustom.IsSelected = true;
            txtCustomMinutes.Visible = true;
            lblCustomMinutesUnit.Visible = true;
            LayoutTimeAndAiOptions();
            txtCustomMinutes.Focus();
            txtCustomMinutes.SelectAll();
            UpdateCustomMinutes();
        }

        private void UpdateCustomMinutes()
        {
            if (!pillCountdownCustom.IsSelected) return;
            if (int.TryParse(txtCustomMinutes.Text.Trim(), out int mins) && mins > 0)
            {
                if (mins > 60) mins = 60;
                _selectedTimeLimit = mins * 60;
            }
            else
            {
                _selectedTimeLimit = 120; // Mặc định 2 phút
            }
        }

        private int GetSelectedCountdownSeconds()
        {
            if (pillCountdown1m.IsSelected) return 60;
            if (pillCountdown3m.IsSelected) return 180;
            if (pillCountdown5m.IsSelected) return 300;
            if (pillCountdownCustom.IsSelected)
            {
                if (int.TryParse(txtCustomMinutes.Text.Trim(), out int mins) && mins > 0)
                {
                    if (mins > 60) mins = 60;
                    return mins * 60;
                }
                return 120;
            }
            return 180; // Mặc định 3 phút
        }

        private string FormatTimePerTurn(int seconds)
        {
            if (seconds <= 0) return "Vô hạn";
            if (seconds >= 60 && seconds % 60 == 0) return $"{seconds / 60} phút";
            if (seconds >= 60) return $"{seconds / 60}p{seconds % 60}s";
            return $"{seconds}s";
        }

        private void LayoutTimeAndAiOptions()
        {
            // Căn giữa nhãn theo trục dọc với chiều cao 38 của Panel
            lblTimePillLabel.Top = Math.Max(0, (pnlTimeLimitOptions.Height - lblTimePillLabel.Height) / 2);

            // Khoảng cách an toàn tối thiểu 20px sau nhãn để tránh dính chữ kể cả ở màn hình DPI cao
            int pillStartX = Math.Max(165, lblTimePillLabel.Right + 20);

            if (_isTimeLimitMode) // Theo lượt
            {
                int curX = pillStartX;
                pillTime15.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - pillTime15.Height) / 2);
                curX += pillTime15.Width + 12;

                pillTime30.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - pillTime30.Height) / 2);
                curX += pillTime30.Width + 12;

                pillTime60.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - pillTime60.Height) / 2);
            }
            else // Đếm ngược
            {
                int curX = pillStartX;
                pillCountdown1m.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - pillCountdown1m.Height) / 2);
                curX += pillCountdown1m.Width + 10;

                pillCountdown3m.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - pillCountdown3m.Height) / 2);
                curX += pillCountdown3m.Width + 10;

                pillCountdown5m.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - pillCountdown5m.Height) / 2);
                curX += pillCountdown5m.Width + 10;

                pillCountdownCustom.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - pillCountdownCustom.Height) / 2);
                curX += pillCountdownCustom.Width + 8;

                txtCustomMinutes.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - txtCustomMinutes.Height) / 2);
                curX += txtCustomMinutes.Width + 6;

                lblCustomMinutesUnit.Location = new System.Drawing.Point(curX, (pnlTimeLimitOptions.Height - lblCustomMinutesUnit.Height) / 2);
            }

            // Đồng bộ hàng Mức độ AI thẳng cột với hàng thời gian
            lblAiDifficultyLabel.Top = Math.Max(0, (pnlAiDifficultyOptions.Height - lblAiDifficultyLabel.Height) / 2);
            int aiX = pillStartX;
            pillAiEasy.Location = new System.Drawing.Point(aiX, (pnlAiDifficultyOptions.Height - pillAiEasy.Height) / 2);
            aiX += pillAiEasy.Width + 12;

            pillAiMedium.Location = new System.Drawing.Point(aiX, (pnlAiDifficultyOptions.Height - pillAiMedium.Height) / 2);
            aiX += pillAiMedium.Width + 12;

            pillAiHard.Location = new System.Drawing.Point(aiX, (pnlAiDifficultyOptions.Height - pillAiHard.Height) / 2);
        }

        private void RelayoutCreateBox()
        {
            // Vị trí Y bắt đầu dưới Card thời gian (cardTime ở Y=248, cao 74 => 248 + 74 + 12 = 334)
            int currentY = 334;

            bool showAiDiff = cardModeAI.IsSelected;

            // Luôn hiện pnlTimeLimitOptions để chọn theo lượt hoặc đếm ngược
            pnlTimeLimitOptions.Visible = true;
            pnlTimeLimitOptions.Left = 28;
            pnlTimeLimitOptions.Top = currentY;
            pnlTimeLimitOptions.Width = 624;
            LayoutTimeAndAiOptions();
            currentY += pnlTimeLimitOptions.Height + 14;

            pnlAiDifficultyOptions.Visible = showAiDiff;
            if (showAiDiff)
            {
                pnlAiDifficultyOptions.Left = 28;
                pnlAiDifficultyOptions.Top = currentY;
                pnlAiDifficultyOptions.Width = 624;
                currentY += pnlAiDifficultyOptions.Height + 14;
            }

            currentY += 4; // Khoảng thở thoải mái trước Section 3 Luật thi đấu

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
            btnCreateRoom.Text = _isInRoom ? "Quay lại phòng" : (cardModeAI.IsSelected && !_netClient.IsConnected ? "Chơi với Máy ngay" : "Tạo phòng");

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
            if (_isInRoom)
            {
                SwitchToView(pnlGameView, btnNavGame);
                return;
            }

            bool isAi = cardModeAI.IsSelected;
            int timeLimit = cardTimeTurn.IsSelected ? GetSelectedTimePillSeconds() : GetSelectedCountdownSeconds();
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
            _isCountdownGame = cardTimeFree.IsSelected;
            timerWidget.IsCountdownMode = _isCountdownGame;

            await _netClient.CreateRoomAsync(_currentRoomMode, diff, 15, blockBothEnds, timeLimit);
        }

        private void ForfeitAndExitOfflineGame()
        {
            if (_isOfflineGame && _isInRoom)
            {
                if (_offlineEngine != null && _currentGameStatus == GameStateStatus.Playing)
                {
                    _offlineEngine.Surrender('O');
                }
                CleanUpOfflineAiGame();
                ExitRoomUI();
            }
        }

        private void CleanUpOfflineAiGame()
        {
            _offlineAiSessionId++;
            turnTimer.Stop();
            _isOfflineGame = false;
            _offlineEngine = null;
            _isInRoom = false;
            _currentGameStatus = GameStateStatus.WaitingForOpponent;
            gameOverModal.Visible = false;
            timerWidget.UpdateTime(0, 0);
        }

        private void StartOfflineAiGame(AIDifficulty diff, bool blockBothEnds, int timeLimit)
        {
            CleanUpOfflineAiGame();

            _isOfflineGame = true;
            _offlineEngine = new GameEngine(15, blockBothEndsRule: blockBothEnds);
            _selectedDifficulty = diff;
            _selectedBlockBothEnds = blockBothEnds;
            _timePerTurn = timeLimit;
            _isCountdownGame = cardTimeFree.IsSelected;
            timerWidget.IsCountdownMode = _isCountdownGame;
            _mySymbol = 'X';
            _currentTurnSymbol = 'X';
            _currentRoomId = "OFFLINE-AI";
            _currentRoomMode = GameMode.PvAI;
            _isInRoom = true;
            _currentGameStatus = GameStateStatus.Playing;
            gameOverModal.Visible = false;

            string diffName = diff switch
            {
                AIDifficulty.Easy => "Dễ",
                AIDifficulty.Hard => "Khó",
                _ => "Trung bình"
            };
            string ruleText = blockBothEnds ? "Chặn 2 đầu" : "Tự do";
            string timeText = FormatTimePerTurn(timeLimit);

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
                timerWidget.SetGameOver();
                lblTimer.Text = "● KẾT THÚC";
                UpdateGameActionButtons();

                if (_offlineEngine.WinnerSymbol == 'X')
                {
                    ShowGameOverModal(GameResultType.Win, "Bạn Thắng!", "Chúc mừng bạn đã giành chiến thắng xuất sắc!");
                }
                else if (_offlineEngine.WinnerSymbol == 'O')
                {
                    ShowGameOverModal(GameResultType.Loss, "Bạn Thua!", "Tiếc quá! Lần sau cố gắng hơn nhé!");
                }
                else
                {
                    ShowGameOverModal(GameResultType.Draw, "Hòa Cờ!", "Hai bên bất phân thắng bại!");
                }
                return;
            }

            // Đến lượt Máy (AI)
            _currentTurnSymbol = 'O';
            lblTurnInfo.Text = "● Máy (AI) đang suy nghĩ nước cờ...";
            if (!_isCountdownGame)
            {
                ResetTimer(_timePerTurn);
            }

            int sessionId = ++_offlineAiSessionId;
            GameEngine engineCopy = _offlineEngine.Clone();

            _ = Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(200);
                    if (sessionId != _offlineAiSessionId) return;
                    CaroProtocol.Point aiMove = MinimaxAI.FindBestMove(engineCopy, 'O', _selectedDifficulty);
                    if (sessionId != _offlineAiSessionId) return;

                    BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            if (sessionId != _offlineAiSessionId || !_isOfflineGame || _offlineEngine == null || _offlineEngine.Status != GameStateStatus.Playing) return;
                            _offlineEngine.MakeMove(aiMove.Row, aiMove.Col, 'O', out _);
                            UpdateOfflineBoardUI();

                            if (_offlineEngine.Status == GameStateStatus.GameOver)
                            {
                                turnTimer.Stop();
                                timerWidget.SetGameOver();
                                lblTimer.Text = "● KẾT THÚC";
                                if (_offlineEngine.WinnerSymbol == 'O')
                                {
                                    ShowGameOverModal(GameResultType.Loss, "Bạn Thua!", "Tiếc quá! Lần sau cố gắng hơn nhé!");
                                }
                                else if (_offlineEngine.WinnerSymbol == 'X')
                                {
                                    ShowGameOverModal(GameResultType.Win, "Bạn Thắng!", "Chúc mừng bạn đã giành chiến thắng xuất sắc!");
                                }
                                else
                                {
                                    ShowGameOverModal(GameResultType.Draw, "Hòa Cờ!", "Hai bên bất phân thắng bại!");
                                }
                            }
                            else
                            {
                                _currentTurnSymbol = 'X';
                                if (!_isCountdownGame)
                                {
                                    ResetTimer(_timePerTurn);
                                }
                            }
                            UpdateGameActionButtons();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"[OfflineAI Callback Error] {ex.Message}");
                        }
                    }));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[OfflineAI Thread Error] {ex.Message}");
                }
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
            if (_isInRoom)
            {
                MessageBox.Show("Bạn đang ở trong phòng đấu. Vui lòng thoát phòng hiện tại trước khi tham gia phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                gameOverModal.Visible = false;
                string ruleText = _selectedBlockBothEnds ? "Chặn 2 đầu" : "Tự do";
                string timeText = FormatTimePerTurn(_timePerTurn);
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
                gameOverModal.Visible = false;
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
            gameOverModal.Visible = false;
            if (_isOfflineGame)
            {
                if (_currentGameStatus == GameStateStatus.Playing)
                {
                    var confirm = MessageBox.Show("Bạn có chắc muốn thoát ván đấu với Máy?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                }
                CleanUpOfflineAiGame();
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
            string timeText = FormatTimePerTurn(data.TimePerTurn);
            string spectatePrefix = _netClient.IsSpectator ? " (Đang xem)" : "";
            lblRoomIdInfo.Text = $"Phòng: {_currentRoomId}{spectatePrefix} ({ruleText} | {timeText})";

            boardControl1.UpdateBoardState(data, _mySymbol);
            lblTurnInfo.Text = $"● {data.StatusMessage}";
            _currentGameStatus = data.Status;
            timerWidget.IsCountdownMode = _isCountdownGame;

            if (data.Status == GameStateStatus.Playing)
            {
                gameOverModal.Visible = false;
                if (!_isCountdownGame)
                {
                    ResetTimer(data.TimePerTurn);
                }
                else if (!turnTimer.Enabled && _remainingSeconds > 0)
                {
                    turnTimer.Start();
                }
            }
            else if (data.Status == GameStateStatus.GameOver)
            {
                turnTimer.Stop();
                timerWidget.SetGameOver();
                lblTimer.Text = "● KẾT THÚC";
                lblTimer.ForeColor = Color.FromArgb(160, 180, 215);
                AppendLog("Hệ thống", data.StatusMessage);

                if (!_netClient.IsSpectator)
                {
                    if (data.WinnerSymbol == _mySymbol)
                    {
                        ShowGameOverModal(GameResultType.Win, "Bạn Thắng!", string.IsNullOrWhiteSpace(data.StatusMessage) ? "Chúc mừng bạn đã giành chiến thắng xuất sắc!" : data.StatusMessage);
                    }
                    else if (data.WinnerSymbol != ' ' && data.WinnerSymbol != '\0')
                    {
                        ShowGameOverModal(GameResultType.Loss, "Bạn Thua!", string.IsNullOrWhiteSpace(data.StatusMessage) ? "Tiếc quá! Lần sau cố gắng hơn nhé!" : data.StatusMessage);
                    }
                    else
                    {
                        ShowGameOverModal(GameResultType.Draw, "Hòa Cờ!", "Hai bên bất phân thắng bại!");
                    }
                }
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
                    timerWidget.SetGameOver();
                    lblTimer.Text = "● KẾT THÚC";
                    UpdateGameActionButtons();
                    ShowGameOverModal(GameResultType.Loss, "Bạn Thua!", "Bạn đã đầu hàng ván đấu này.");
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
            gameOverModal.Visible = false;
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
            // 2 nút Chơi lại và Thoát phòng cũ trên header không còn cần thiết, thay thế bởi Modal Game Over
            btnRestart.Visible = false;

            if (!_isInRoom)
            {
                btnSurrender.Visible = false;
                btnSurrender.Enabled = false;
                btnLeaveRoom.Visible = false;
                btnLeaveRoom.Enabled = false;
                return;
            }

            if (_netClient.IsSpectator)
            {
                btnSurrender.Visible = false;
                btnSurrender.Enabled = false;
                btnLeaveRoom.Visible = true;
                btnLeaveRoom.Enabled = true;
                btnLeaveRoom.Text = "Thoát xem ✕";
                btnLeaveRoom.Location = new System.Drawing.Point(6, 17);
                btnLeaveRoom.Size = new Size(124, 42);
                btnLeaveRoom.CustomTextAlign = ContentAlignment.MiddleCenter;
                btnLeaveRoom.TextLeftPadding = 0;
                return;
            }

            if (_currentGameStatus == GameStateStatus.Playing)
            {
                // Khi ván đấu đang diễn ra: Chỉ hiện duy nhất nút "Đầu hàng"
                btnSurrender.Visible = true;
                btnSurrender.Enabled = true;
                btnSurrender.Location = new System.Drawing.Point(6, 17);
                btnSurrender.Size = new Size(124, 42);
                btnSurrender.CustomTextAlign = ContentAlignment.MiddleCenter;
                btnSurrender.TextLeftPadding = 0;
                btnLeaveRoom.Visible = false;
            }
            else if (_currentGameStatus == GameStateStatus.GameOver)
            {
                // Khi ván đấu kết thúc: Nút "Đầu hàng" ẩn đi, Modal Game Over hiện lên với 2 nút to đẹp
                btnSurrender.Visible = false;
                // Nếu modal bị đóng bằng nút (X), nút Thoát phòng sẽ xuất hiện
                btnLeaveRoom.Visible = !gameOverModal.Visible;
                btnLeaveRoom.Enabled = true;
                btnLeaveRoom.Text = "Thoát phòng";
                btnLeaveRoom.Location = new System.Drawing.Point(6, 17);
                btnLeaveRoom.Size = new Size(124, 42);
                btnLeaveRoom.CustomTextAlign = ContentAlignment.MiddleCenter;
                btnLeaveRoom.TextLeftPadding = 0;
            }
            else // WaitingForOpponent (đang chờ đối thủ vào)
            {
                btnSurrender.Visible = false;
                btnLeaveRoom.Visible = true;
                btnLeaveRoom.Enabled = true;
                btnLeaveRoom.Text = "Hủy phòng";
                btnLeaveRoom.Location = new System.Drawing.Point(6, 17);
                btnLeaveRoom.Size = new Size(124, 42);
                btnLeaveRoom.CustomTextAlign = ContentAlignment.MiddleCenter;
                btnLeaveRoom.TextLeftPadding = 0;
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
            timerWidget.SetGameOver();
            _currentGameStatus = GameStateStatus.GameOver;
            lblTimer.Text = "● KẾT THÚC";
            lblTimer.ForeColor = Color.FromArgb(160, 180, 215);
            btnLeaveRoom.Visible = true;
            UpdateGameActionButtons();
            AppendLog("Hệ thống", message);
            ShowGameOverModal(GameResultType.Win, "Bạn Thắng!", message);
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
                gameOverModal.Visible = false;

                string ruleText = data.BlockBothEndsRule ? "Chặn 2 đầu" : "Tự do";
                string timeText = FormatTimePerTurn(data.TimePerTurn);
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
            btnConnect.Text = "KẾT NỐI & ĐĂNG NHẬP";
            btnConnect.NavIcon = CyberIcon.Lightning;
            btnConnect.IsPurpleGradient = true;
            btnConnect.BorderLineColor = Color.FromArgb(192, 38, 211);
            btnConnect.Enabled = true;
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
            timerWidget.UpdateTime(_remainingSeconds, _timePerTurn);

            string timeDisplay = (_remainingSeconds >= 60)
                ? $"{_remainingSeconds / 60:D2}:{_remainingSeconds % 60:D2}"
                : $"{_remainingSeconds}s";

            if (_remainingSeconds > 10)
            {
                lblTimer.ForeColor = UITheme.AccentCyan;
                lblTimer.Text = $"● {timeDisplay}";
            }
            else if (_remainingSeconds > 5)
            {
                lblTimer.ForeColor = UITheme.AccentOrange;
                lblTimer.Text = $"● {timeDisplay} ⏳";
            }
            else if (_remainingSeconds > 0)
            {
                lblTimer.ForeColor = UITheme.AccentRed;
                lblTimer.Text = $"● {timeDisplay} ⚠";
            }
            else
            {
                turnTimer.Stop();
                timerWidget.UpdateTime(0, _timePerTurn);
                lblTimer.ForeColor = UITheme.AccentRed;
                lblTimer.Text = "● Hết giờ!";

                if (_isOfflineGame)
                {
                    if (_currentTurnSymbol == 'X' && _offlineEngine != null && _offlineEngine.Status == GameStateStatus.Playing)
                    {
                        _offlineEngine.Surrender('O');
                        UpdateOfflineBoardUI();
                        timerWidget.SetGameOver();
                        UpdateGameActionButtons();
                        ShowGameOverModal(GameResultType.Loss, "Bạn Thua!", "Bạn đã hết thời gian quy định!");
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
                _timePerTurn = 0;
                lblTimer.Text = "● Vô hạn";
                lblTimer.ForeColor = UITheme.AccentGreen;
                timerWidget.SetInfinite();
                return;
            }

            _remainingSeconds = seconds;
            _timePerTurn = seconds;
            string timeDisplay = (seconds >= 60)
                ? $"{seconds / 60:D2}:{seconds % 60:D2}"
                : $"{seconds}s";
            lblTimer.Text = $"● {timeDisplay}";
            lblTimer.ForeColor = UITheme.AccentCyan;
            timerWidget.UpdateTime(_remainingSeconds, _timePerTurn);
            turnTimer.Start();
        }

        private void ShowGameOverModal(GameResultType result, string title, string subtitle)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => ShowGameOverModal(result, title, subtitle)));
                return;
            }
            gameOverModal.ShowResult(result, title, subtitle);
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
            _offlineAiSessionId++;
            turnTimer.Stop();
            _isInRoom = false;
            _isOfflineGame = false;
            _offlineEngine = null;
            _netClient.IsSpectator = false;
            _currentRoomId = string.Empty;
            _currentGameStatus = GameStateStatus.WaitingForOpponent;
            pnlRightBar.Visible = false;
            rtbChatLog.Clear();
            gameOverModal.Visible = false;
            timerWidget.UpdateTime(0, 0);
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
            btnCreateRoom.Text = cardModeAI.IsSelected && !_netClient.IsConnected ? "Chơi với Máy ngay" : "Tạo phòng";
            lblRoomIdInfo.Text = "Phòng: ---";
            lblPlayersInfo.Text = "X: ---   VS   O: ---";
            lblTurnInfo.Text = "● Lượt đi: ---";
            lblTimer.Text = "● 30s";
            lblTimer.ForeColor = UITheme.AccentOrange;
            turnTimer.Stop();
            timerWidget.UpdateTime(30, 30);
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

            if (_isInRoom)
            {
                btnCreateRoom.Text = "Quay lại phòng";
            }
            else if (cardModeAI.IsSelected)
            {
                btnCreateRoom.Text = "Chơi với Máy ngay";
            }

            if (!connected)
            {
                ForfeitAndExitOfflineGame();
                if (_isInRoom)
                {
                    ExitRoomUI();
                }
            }

            if (!connected)
            {
                leaderboardPodium?.ClearData();
                leaderboardList?.ClearData();
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
                lblHomeServerHost.Text = $"Máy chủ: {hostName}";
                lblHomeServerIp.Text = $"IP: {txtIp.Text.Trim()}:{txtPort.Text.Trim()}";

                lblProfileName.Text = _netClient.PlayerName;
                lblProfileLevel.Text = $"★ {_netClient.Elo} ELO";
                lblProfileXp.Text = $"{_netClient.Wins}W - {_netClient.Losses}L - {_netClient.Draws}D";
                btnLogout.Visible = false; // Nút Đăng xuất chính đã được tích hợp ngay trên thẻ hồ sơ (btnNavSettings)

                _ = _netClient.RequestLeaderboardAsync();
                _ = _netClient.RequestRoomListAsync();
            }
            else
            {
                lblHomeServerStatus.Text = "○ Chưa kết nối";
                lblHomeServerStatus.ForeColor = Color.FromArgb(255, 75, 75);
                lblHomeServerHost.Text = "Máy chủ: ---";
                lblHomeServerIp.Text = "IP: ---";

                // Real offline state - no mock players
                _allOnlinePlayers.Clear();
                lstConnectedPlayers.Items.Clear();
                lblOnlineCountBadge.Text = "● 0";
                lblHomePlayersHeader.Text = "NGƯỜI CHƠI ONLINE";
                lblNoPlayers.Text = "Chưa kết nối đến máy chủ\nĐăng nhập để xem người chơi online";
                lblNoPlayers.Visible = true;

                if (cardTopPlayers != null)
                {
                    cardTopPlayers.SetLeaderboardData(null);
                }

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
                Width = 170,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvMatchHistory.Columns.Add(colDate);

            var colOpponent = new DataGridViewTextBoxColumn
            {
                Name = "Opponent",
                HeaderText = "Phòng",
                Width = 140,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colOpponent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colOpponent.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colOpponent);

            var colResult = new DataGridViewTextBoxColumn
            {
                Name = "Result",
                HeaderText = "Kết quả",
                Width = 130,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colResult.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colResult.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colResult);

            var colEloChange = new DataGridViewTextBoxColumn
            {
                Name = "EloChange",
                HeaderText = "ELO thay đổi",
                Width = 140,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colEloChange.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colEloChange.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colEloChange);

            var colMoves = new DataGridViewTextBoxColumn
            {
                Name = "Moves",
                HeaderText = "Thời lượng",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            colMoves.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMoves.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMatchHistory.Columns.Add(colMoves);
        }

        private void InitLeaderboardTable()
        {
            if (leaderboardPodium != null)
            {
                leaderboardPodium.AvatarProvider = (name) => GetPlayerAvatarImage(name);
            }
            if (leaderboardList != null)
            {
                leaderboardList.AvatarProvider = (name) => GetPlayerAvatarImage(name);
            }

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
                    lblNoHistory.Text = "Bạn đang chơi ở chế độ Ngoại tuyến (Khách).\nHãy bấm nút 'ĐĂNG NHẬP' ở góc dưới bên trái để lưu trữ lịch sử thi đấu online!";
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

            // Update Home Top Podium & Runner-ups with REAL leaderboard data
            if (cardTopPlayers != null)
            {
                cardTopPlayers.SetLeaderboardData(list);
            }

            // Update dedicated Leaderboard view (3D Podium + Rank Card List)
            if (leaderboardPodium != null)
            {
                leaderboardPodium.SetData(list);
            }
            if (leaderboardList != null)
            {
                string myName = !string.IsNullOrWhiteSpace(_netClient?.PlayerName) ? _netClient.PlayerName : txtName.Text.Trim();
                leaderboardList.SetData(list, myName);
            }

            dgvLeaderboard.Rows.Clear();
            if (list == null || list.Count == 0)
            {
                leaderboardPodium?.ClearData();
                leaderboardList?.ClearData();
                if (_netClient != null && _netClient.IsConnected)
                {
                    lblNoLeaderboard.Text = "Chưa có dữ liệu người chơi trên Bảng xếp hạng Server.";
                }
                else
                {
                    lblNoLeaderboard.Text = "Bạn chưa kết nối Server.\nVui lòng bấm nút 'ĐĂNG NHẬP' ở góc dưới bên trái để xem Bảng xếp hạng!";
                }
                return;
            }

            lblNoLeaderboard.Visible = false;

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

        private List<PlayerInfoData> _allOnlinePlayers = new List<PlayerInfoData>();

        private void NetClient_OnPlayerListReceived(List<PlayerInfoData> players)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => NetClient_OnPlayerListReceived(players)));
                return;
            }

            _allOnlinePlayers = players ?? new List<PlayerInfoData>();
            ApplyPlayerFilter();
        }

        private void ApplyPlayerFilter()
        {
            string filter = txtSearchFriend.Text.Trim();
            var filtered = string.IsNullOrEmpty(filter)
                ? _allOnlinePlayers
                : _allOnlinePlayers.Where(p => p.PlayerName.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

            lblOnlineCountBadge.Text = $"● {_allOnlinePlayers.Count}";
            lblHomePlayersHeader.Text = "NGƯỜI CHƠI ONLINE";

            lstConnectedPlayers.BeginUpdate();
            lstConnectedPlayers.Items.Clear();
            foreach (var p in filtered)
            {
                lstConnectedPlayers.Items.Add(p);
            }
            lstConnectedPlayers.EndUpdate();

            lblNoPlayers.Visible = (filtered.Count == 0);
            if (filtered.Count == 0)
            {
                lblNoPlayers.Text = _allOnlinePlayers.Count == 0
                    ? "Không có người chơi nào khác online"
                    : "Không tìm thấy bạn bè phù hợp";
            }
        }

        private void lstConnectedPlayers_MouseClick(object? sender, MouseEventArgs e)
        {
            int index = lstConnectedPlayers.IndexFromPoint(e.Location);
            if (index >= 0 && index < lstConnectedPlayers.Items.Count)
            {
                var player = lstConnectedPlayers.Items[index] as PlayerInfoData;
                if (player == null) return;

                bool isSelf = (!string.IsNullOrEmpty(_netClient.PlayerName) && player.PlayerName.Equals(_netClient.PlayerName, StringComparison.OrdinalIgnoreCase))
                           || (!string.IsNullOrEmpty(_netClient.PlayerId) && player.PlayerId == _netClient.PlayerId);
                if (isSelf) return;

                Rectangle itemRect = lstConnectedPlayers.GetItemRectangle(index);
                int btnW = 76;
                int btnH = 26;
                int btnX = itemRect.Right - btnW - 10;
                int btnY = itemRect.Y + (itemRect.Height - btnH) / 2;
                Rectangle btnRect = new Rectangle(btnX, btnY, btnW, btnH);

                if (btnRect.Contains(e.Location))
                {
                    if (player.Status.Contains("trận") || player.Status.Contains("phòng"))
                    {
                        MessageBox.Show($"Người chơi {player.PlayerName} hiện đang trong trận đấu! Lời mời thách đấu đã được gửi để giao chiến khi xong trận.", "Thách đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Đã gửi lời mời giao đấu cờ Caro tới {player.PlayerName}!", "Mời chơi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void pnlHomeView_Resize(object? sender, EventArgs e)
        {
            if (pnlHomeView == null || heroBanner == null || pnlMiddleGrid == null) return;
            int padX = 16;
            int padTop = 12;
            int padBottom = 14;
            int gapY = 12;

            int totalW = pnlHomeView.ClientSize.Width;
            int totalH = pnlHomeView.ClientSize.Height;
            if (totalW <= 100 || totalH <= 100) return;

            int usableW = Math.Max(100, totalW - padX * 2);

            // Banner height: enlarge to give the artwork more breathing room and grandeur (~215-235px)
            int bannerH = Math.Min(235, Math.Max(195, (int)(usableW * 0.30f)));

            heroBanner.SetBounds(padX, padTop, usableW, bannerH);

            int gridY = padTop + bannerH + gapY;
            int gridH = Math.Max(100, totalH - gridY - padBottom);

            pnlMiddleGrid.SetBounds(padX, gridY, usableW, gridH);
        }

        private void pnlMiddleGrid_Resize(object? sender, EventArgs e)
        {
            if (pnlMiddleGrid == null || colEvent == null || colTopPlayers == null || colOnlinePlayers == null) return;
            int w = pnlMiddleGrid.ClientSize.Width;
            int h = pnlMiddleGrid.ClientSize.Height;
            if (w <= 100 || h <= 100) return;

            int gap = 12;
            int totalGap = gap * 2;
            int availW = Math.Max(100, w - totalGap);

            // Proportions:
            // Col 1: ~36% (Sự kiện nổi bật - enlarged to showcase poster comfortably)
            // Col 2: ~35% (Bảng xếp hạng Top Cao Thủ)
            // Col 3: ~29% (Người chơi online)
            int w1 = (int)(availW * 0.36f);
            int w2 = (int)(availW * 0.35f);
            int w3 = availW - w1 - w2;

            colEvent.SetBounds(0, 0, w1, h);
            colTopPlayers.SetBounds(w1 + gap, 0, w2, h);
            colOnlinePlayers.SetBounds(w1 + gap + w2 + gap, 0, w3, h);
        }

        private Image? GetOnlinePlayerAvatar(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName) || playerName == "---") return null;
            return GetPlayerAvatarImage(playerName);
        }

        private void lstConnectedPlayers_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lstConnectedPlayers.Items.Count) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var player = lstConnectedPlayers.Items[e.Index] as PlayerInfoData;
            if (player == null) return;

            Rectangle bounds = e.Bounds;

            // Background
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(13, 24, 48)))
            {
                g.FillRectangle(bgBrush, bounds);
            }

            // Avatar circle
            int avatarSize = 34;
            int avatarX = bounds.X + 8;
            int avatarY = bounds.Y + (bounds.Height - avatarSize) / 2;
            Rectangle avatarRect = new Rectangle(avatarX, avatarY, avatarSize, avatarSize);

            Image? avatarImg = GetOnlinePlayerAvatar(player.PlayerName);
            if (avatarImg != null)
            {
                using (GraphicsPath circlePath = new GraphicsPath())
                {
                    circlePath.AddEllipse(avatarRect);
                    g.SetClip(circlePath);
                    g.DrawImage(avatarImg, avatarRect);
                    g.ResetClip();
                }
            }
            else
            {
                using (SolidBrush avBg = new SolidBrush(Color.FromArgb(20, 36, 68)))
                {
                    g.FillEllipse(avBg, avatarRect);
                }
            }

            // Avatar border
            using (Pen avPen = new Pen(Color.FromArgb(40, 75, 130), 1.2f))
            {
                g.DrawEllipse(avPen, avatarRect);
            }

            // Status dot on avatar bottom-right
            bool isInMatch = player.Status.Contains("trận") || player.Status.Contains("phòng");
            Color statusColor = isInMatch ? Color.FromArgb(255, 171, 0) : Color.FromArgb(0, 230, 118);
            int dotSize = 8;
            int dotX = avatarX + avatarSize - dotSize;
            int dotY = avatarY + avatarSize - dotSize;

            using (SolidBrush dotDark = new SolidBrush(Color.FromArgb(13, 24, 48)))
            {
                g.FillEllipse(dotDark, dotX - 1, dotY - 1, dotSize + 2, dotSize + 2);
            }
            using (SolidBrush dotBrush = new SolidBrush(statusColor))
            {
                g.FillEllipse(dotBrush, dotX, dotY, dotSize, dotSize);
            }

            // Player Name & Status
            int textX = avatarX + avatarSize + 10;
            int nameY = bounds.Y + 8;
            using (Font nameFont = new Font("Segoe UI", 9f, FontStyle.Bold))
            using (SolidBrush nameBrush = new SolidBrush(Color.FromArgb(240, 246, 255)))
            {
                g.DrawString(player.PlayerName, nameFont, nameBrush, textX, nameY);
            }

            int statusY = bounds.Y + 28;
            using (Font statusFont = new Font("Segoe UI", 7.8f))
            using (SolidBrush subBrush = new SolidBrush(Color.FromArgb(135, 155, 190)))
            {
                string statusDesc = isInMatch ? "Trong trận ⚔" : "Đang rảnh ●";
                g.DrawString(statusDesc, statusFont, subBrush, textX, statusY);
            }

            // Right Action Button Pill
            int btnW = 76;
            int btnH = 26;
            int btnX = bounds.Right - btnW - 10;
            int btnY = bounds.Y + (bounds.Height - btnH) / 2;
            Rectangle btnRect = new Rectangle(btnX, btnY, btnW, btnH);

            bool isSelf = (!string.IsNullOrEmpty(_netClient.PlayerName) && player.PlayerName.Equals(_netClient.PlayerName, StringComparison.OrdinalIgnoreCase))
                       || (!string.IsNullOrEmpty(_netClient.PlayerId) && player.PlayerId == _netClient.PlayerId);

            using (GraphicsPath btnPath = UITheme.CreateRoundedRectanglePath(btnRect, 8))
            {
                if (isSelf)
                {
                    // Self badge
                    using (SolidBrush selfBg = new SolidBrush(Color.FromArgb(20, 36, 68)))
                    using (Pen bp = new Pen(Color.FromArgb(42, 75, 130), 1f))
                    using (Font selfFont = new Font("Segoe UI", 8.2f, FontStyle.Italic))
                    using (SolidBrush selfTxt = new SolidBrush(Color.FromArgb(145, 185, 235)))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.FillPath(selfBg, btnPath);
                        g.DrawPath(bp, btnPath);
                        g.DrawString("(Bạn)", selfFont, selfTxt, btnRect, sf);
                    }
                }
                else if (isInMatch)
                {
                    // Cyan action button - Thách đấu
                    using (LinearGradientBrush lgb = new LinearGradientBrush(btnRect, Color.FromArgb(0, 194, 255), Color.FromArgb(0, 140, 255), LinearGradientMode.Vertical))
                    {
                        g.FillPath(lgb, btnPath);
                    }
                    using (Font btnFont = new Font("Segoe UI", 8.2f, FontStyle.Bold))
                    using (SolidBrush txtB = new SolidBrush(Color.FromArgb(5, 16, 32)))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString("Thách đấu", btnFont, txtB, btnRect, sf);
                    }
                }
                else
                {
                    // Dark pill button - Mời chơi
                    using (SolidBrush bg = new SolidBrush(Color.FromArgb(16, 28, 54)))
                    {
                        g.FillPath(bg, btnPath);
                    }
                    using (Pen bp = new Pen(Color.FromArgb(32, 54, 96), 1f))
                    {
                        g.DrawPath(bp, btnPath);
                    }
                    using (Font btnFont = new Font("Segoe UI", 8.2f, FontStyle.Regular))
                    using (SolidBrush txtB = new SolidBrush(Color.FromArgb(200, 218, 245)))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString("Mời chơi", btnFont, txtB, btnRect, sf);
                    }
                }
            }

            // Bottom subtle divider
            using (Pen divPen = new Pen(Color.FromArgb(20, 34, 64), 1f))
            {
                g.DrawLine(divPen, bounds.X + 8, bounds.Bottom - 1, bounds.Right - 8, bounds.Bottom - 1);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _netClient.Disconnect();
        }
    }
}
