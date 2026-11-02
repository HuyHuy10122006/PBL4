namespace CaroClient
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Window Layout Panels
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.pnlLogoBox = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.pnlWindowButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();

            // Left Sidebar
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlNavList = new System.Windows.Forms.Panel();
            this.btnNavHome = new CaroClient.CyberButton();
            this.btnNavGame = new CaroClient.CyberButton();
            this.btnNavCreate = new CaroClient.CyberButton();
            this.btnNavJoin = new CaroClient.CyberButton();
            this.btnNavHistory = new CaroClient.CyberButton();
            this.btnNavLeaderboard = new CaroClient.CyberButton();
            this.btnNavSettings = new CaroClient.CyberButton();
            this.pnlSidebarBottom = new System.Windows.Forms.Panel();
            this.pnlProfileCard = new CaroClient.RoundedCard();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.lblProfileLevel = new System.Windows.Forms.Label();
            this.lblProfileXp = new System.Windows.Forms.Label();
            this.btnLogout = new CaroClient.CyberButton();

            // Right Panel
            this.pnlRightBar = new System.Windows.Forms.Panel();
            this.cardChat = new CaroClient.RoundedCard();
            this.lblChatTitle = new System.Windows.Forms.Label();
            this.rtbChatLog = new System.Windows.Forms.RichTextBox();
            this.pnlChatBottom = new System.Windows.Forms.Panel();
            this.txtChatInput = new System.Windows.Forms.TextBox();
            this.btnSendChat = new CaroClient.CyberButton();

            // Main Content Area & Views
            this.pnlMainContent = new System.Windows.Forms.Panel();

            // View 1: Home Dashboard
            this.pnlHomeView = new System.Windows.Forms.Panel();
            this.heroBanner = new CaroClient.HeroBannerPanel();
            this.lblHeroTitle = new System.Windows.Forms.Label();
            this.lblHeroSubtitle = new System.Windows.Forms.Label();



            this.pnlMiddleGrid = new System.Windows.Forms.Panel();
            this.cardFeaturedRooms = new CaroClient.RoundedCard();
            this.pnlRoomsHeaderBar = new System.Windows.Forms.Panel();
            this.lblRoomsHeader = new System.Windows.Forms.Label();
            this.btnRefreshRooms = new System.Windows.Forms.Button();
            this.dgvFeaturedRooms = new System.Windows.Forms.DataGridView();
            this.lblNoRooms = new System.Windows.Forms.Label();

            this.btnHomeCreateRoom = new CaroClient.CyberButton();
            this.btnHomeJoinRoom = new CaroClient.CyberButton();
            this.btnHomeSettings = new CaroClient.CyberButton();
            this.pnlServerInfoContainer = new System.Windows.Forms.Panel();
            this.cardServerInfo = new CaroClient.RoundedCard();
            this.lblHomeServerHeader = new System.Windows.Forms.Label();
            this.lblHomeServerStatus = new System.Windows.Forms.Label();
            this.lblHomeServerHost = new System.Windows.Forms.Label();
            this.lblHomeServerIp = new System.Windows.Forms.Label();
            this.pnlPlayersContainer = new System.Windows.Forms.Panel();
            this.cardConnectedPlayers = new CaroClient.RoundedCard();
            this.lblHomePlayersHeader = new System.Windows.Forms.Label();
            this.lstConnectedPlayers = new System.Windows.Forms.ListBox();
            this.lblNoPlayers = new System.Windows.Forms.Label();

            // View 2: Game Arena (Phòng thi đấu)
            this.pnlGameView = new System.Windows.Forms.Panel();
            this.pnlGameHeader = new CaroClient.RoundedCard();
            this.pnlGameActions = new System.Windows.Forms.Panel();
            this.btnSurrender = new CaroClient.CyberButton();
            this.btnRestart = new CaroClient.CyberButton();
            this.btnLeaveRoom = new CaroClient.CyberButton();
            this.pnlGameInfo = new System.Windows.Forms.Panel();
            this.lblRoomIdInfo = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblPlayersInfo = new System.Windows.Forms.Label();
            this.lblTurnInfo = new System.Windows.Forms.Label();
            this.boardControl1 = new CaroClient.BoardControl();

            // View 3: Create Room
            this.pnlCreateRoomView = new System.Windows.Forms.Panel();
            this.cardCreateBox = new CaroClient.RoundedCard();
            this.pnlCreateHeader = new System.Windows.Forms.Panel();
            this.badgeCreatePlus = new CaroClient.CircleBadge();
            this.lblCreateTitle = new System.Windows.Forms.Label();
            this.lblCreateSubtitle = new System.Windows.Forms.Label();
            this.secHeaderMode = new CaroClient.SectionHeaderLabel();
            this.cardModePvP = new CaroClient.ModeSelectionCard();
            this.cardModeAI = new CaroClient.ModeSelectionCard();
            this.secHeaderTime = new CaroClient.SectionHeaderLabel();
            this.cardTimeTurn = new CaroClient.ModeSelectionCard();
            this.cardTimeFree = new CaroClient.ModeSelectionCard();
            this.pnlTimeLimitOptions = new System.Windows.Forms.Panel();
            this.lblTimePillLabel = new System.Windows.Forms.Label();
            this.pillTime15 = new CaroClient.OptionPill();
            this.pillTime30 = new CaroClient.OptionPill();
            this.pillTime60 = new CaroClient.OptionPill();
            this.pnlAiDifficultyOptions = new System.Windows.Forms.Panel();
            this.lblAiDifficultyLabel = new System.Windows.Forms.Label();
            this.pillAiEasy = new CaroClient.OptionPill();
            this.pillAiMedium = new CaroClient.OptionPill();
            this.pillAiHard = new CaroClient.OptionPill();
            this.secHeaderRule = new CaroClient.SectionHeaderLabel();
            this.pillRuleStandard = new CaroClient.OptionPill();
            this.pillRuleFree = new CaroClient.OptionPill();
            this.btnCreateRoom = new CaroClient.CyberButton();
            this.btnCancelCreate = new CaroClient.CyberButton();

            // View 4: Join Room
            this.pnlJoinRoomView = new System.Windows.Forms.Panel();
            this.cardJoinTopBar = new CaroClient.RoundedCard();
            this.pnlJoinActions = new System.Windows.Forms.Panel();
            this.lblJoinTitle = new System.Windows.Forms.Label();
            this.lblJoinSubtitle = new System.Windows.Forms.Label();
            this.lblRoomIdPrompt = new System.Windows.Forms.Label();
            this.txtRoomId = new System.Windows.Forms.TextBox();
            this.btnJoinRoom = new CaroClient.CyberButton();
            this.btnCancelJoin = new CaroClient.CyberButton();

            // View 5: Settings / Login
            this.pnlSettingsView = new System.Windows.Forms.Panel();
            this.cardSettingsBox = new CaroClient.RoundedCard();
            this.btnTabLogin = new CaroClient.CyberButton();
            this.btnTabRegister = new CaroClient.CyberButton();
            this.pnlAuthBodyLogin = new System.Windows.Forms.Panel();
            this.pnlAuthBodyRegister = new System.Windows.Forms.Panel();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.lblSettingsNotice = new System.Windows.Forms.Label();
            this.lblServerIp = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.btnConnect = new CaroClient.CyberButton();
            this.lblSwitchToRegister = new System.Windows.Forms.Label();
            this.lblOfflineTip = new System.Windows.Forms.Label();

            this.lblRegTitle = new System.Windows.Forms.Label();
            this.lblRegNotice = new System.Windows.Forms.Label();
            this.lblRegServerIp = new System.Windows.Forms.Label();
            this.txtRegIp = new System.Windows.Forms.TextBox();
            this.lblRegPort = new System.Windows.Forms.Label();
            this.txtRegPort = new System.Windows.Forms.TextBox();
            this.lblRegPlayerName = new System.Windows.Forms.Label();
            this.txtRegName = new System.Windows.Forms.TextBox();
            this.lblRegPassword = new System.Windows.Forms.Label();
            this.txtRegPassword = new System.Windows.Forms.TextBox();
            this.lblRegConfirmPassword = new System.Windows.Forms.Label();
            this.txtRegConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblRegStatusBadge = new System.Windows.Forms.Label();
            this.btnRegister = new CaroClient.CyberButton();
            this.lblSwitchToLogin = new System.Windows.Forms.Label();

            // View 6: Profile & Match History
            this.pnlHistoryView = new System.Windows.Forms.Panel();
            this.cardProfileSummary = new CaroClient.RoundedCard();
            this.lblProfileAvatar = new System.Windows.Forms.Label();
            this.lblProfileDetailName = new System.Windows.Forms.Label();
            this.lblProfileDetailRank = new System.Windows.Forms.Label();
            this.lblProfileDetailStatus = new System.Windows.Forms.Label();
            this.cardStatTotal = new CaroClient.RoundedCard();
            this.lblStatTotalVal = new System.Windows.Forms.Label();
            this.lblStatTotalTitle = new System.Windows.Forms.Label();
            this.cardStatWins = new CaroClient.RoundedCard();
            this.lblStatWinsVal = new System.Windows.Forms.Label();
            this.lblStatWinsTitle = new System.Windows.Forms.Label();
            this.cardStatLosses = new CaroClient.RoundedCard();
            this.lblStatLossesVal = new System.Windows.Forms.Label();
            this.lblStatLossesTitle = new System.Windows.Forms.Label();
            this.cardStatWinRate = new CaroClient.RoundedCard();
            this.lblStatWinRateVal = new System.Windows.Forms.Label();
            this.lblStatWinRateTitle = new System.Windows.Forms.Label();
            this.pnlHistorySpacer = new System.Windows.Forms.Panel();
            this.cardHistoryBox = new CaroClient.RoundedCard();
            this.pnlHistoryHeaderBar = new System.Windows.Forms.Panel();
            this.lblHistoryHeader = new System.Windows.Forms.Label();
            this.btnRefreshHistory = new System.Windows.Forms.Button();
            this.dgvMatchHistory = new System.Windows.Forms.DataGridView();
            this.lblNoHistory = new System.Windows.Forms.Label();

            // View 7: Leaderboard
            this.pnlLeaderboardView = new System.Windows.Forms.Panel();
            this.cardLeaderboardBox = new CaroClient.RoundedCard();
            this.pnlLeaderboardHeaderBar = new System.Windows.Forms.Panel();
            this.lblLeaderboardHeader = new System.Windows.Forms.Label();
            this.btnRefreshLeaderboard = new System.Windows.Forms.Button();
            this.dgvLeaderboard = new System.Windows.Forms.DataGridView();
            this.lblNoLeaderboard = new System.Windows.Forms.Label();

            // Turn Timer
            this.turnTimer = new System.Windows.Forms.Timer(this.components);

            this.SuspendLayout();

            // -------------------------------------------------------------
            // pnlTitleBar (Top Window Title Bar)
            // -------------------------------------------------------------
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(7, 12, 24);
            this.pnlTitleBar.Controls.Add(this.pnlLogoBox);
            this.pnlTitleBar.Controls.Add(this.lblAppTitle);
            this.pnlTitleBar.Controls.Add(this.lblAppSubtitle);
            this.pnlTitleBar.Controls.Add(this.pnlWindowButtons);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Height = 48;
            this.pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);

            // pnlLogoBox
            this.pnlLogoBox.Location = new System.Drawing.Point(12, 8);
            this.pnlLogoBox.Size = new System.Drawing.Size(32, 32);
            this.pnlLogoBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLogoBox_Paint);

            // lblAppTitle
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(50, 6);
            this.lblAppTitle.Text = "CaroKan";
            this.lblAppTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);

            // lblAppSubtitle
            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(130, 150, 185);
            this.lblAppSubtitle.Location = new System.Drawing.Point(51, 27);
            this.lblAppSubtitle.Text = "Kết nối bạn bè - Thắng trong từng ván cờ";
            this.lblAppSubtitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);

            // pnlWindowButtons
            this.pnlWindowButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlWindowButtons.Width = 135;
            this.pnlWindowButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlWindowButtons.Controls.Add(this.btnMin);
            this.pnlWindowButtons.Controls.Add(this.btnMax);
            this.pnlWindowButtons.Controls.Add(this.btnClose);

            this.btnMin.Location = new System.Drawing.Point(4, 9);
            this.btnMin.Size = new System.Drawing.Size(38, 30);
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.BackColor = System.Drawing.Color.FromArgb(16, 26, 50);
            this.btnMin.ForeColor = System.Drawing.Color.FromArgb(200, 215, 240);
            this.btnMin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMin.Text = "—";
            this.btnMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMin.Click += (s, e) => this.WindowState = System.Windows.Forms.FormWindowState.Minimized;

            this.btnMax.Location = new System.Drawing.Point(46, 9);
            this.btnMax.Size = new System.Drawing.Size(38, 30);
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.BackColor = System.Drawing.Color.FromArgb(16, 26, 50);
            this.btnMax.ForeColor = System.Drawing.Color.FromArgb(200, 215, 240);
            this.btnMax.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMax.Text = "▢";
            this.btnMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMax.Click += (s, e) => this.WindowState = (this.WindowState == System.Windows.Forms.FormWindowState.Maximized) ? System.Windows.Forms.FormWindowState.Normal : System.Windows.Forms.FormWindowState.Maximized;

            this.btnClose.Location = new System.Drawing.Point(88, 9);
            this.btnClose.Size = new System.Drawing.Size(38, 30);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(16, 26, 50);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(200, 215, 240);
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.Text = "✕";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Click += (s, e) => this.Close();

            // -------------------------------------------------------------
            // pnlSidebar (Left Navigation) - Width 230px
            // -------------------------------------------------------------
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width = 230;
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(12, 14, 12, 14);
            this.pnlSidebar.Controls.Add(this.pnlNavList);
            this.pnlSidebar.Controls.Add(this.pnlSidebarBottom);

            // pnlNavList
            this.pnlNavList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavList.Height = 270;
            this.pnlNavList.BackColor = System.Drawing.Color.Transparent;
            this.pnlNavList.Controls.Add(this.btnNavHome);
            this.pnlNavList.Controls.Add(this.btnNavGame);
            this.pnlNavList.Controls.Add(this.btnNavCreate);
            this.pnlNavList.Controls.Add(this.btnNavJoin);
            this.pnlNavList.Controls.Add(this.btnNavLeaderboard);
            this.pnlNavList.Controls.Add(this.btnNavHistory);

            // Nav Buttons with CyberIcon: Crisp, modern vector icons that never glitch or wrap
            this.btnNavHome.Location = new System.Drawing.Point(0, 5);
            this.btnNavHome.Size = new System.Drawing.Size(206, 38);
            this.btnNavHome.Text = "Trang chủ";
            this.btnNavHome.NavIcon = CaroClient.CyberIcon.Home;
            this.btnNavHome.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavHome.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavHome.IsPrimaryGradient = true;
            this.btnNavHome.Click += (s, e) => SwitchToView(this.pnlHomeView, this.btnNavHome);

            this.btnNavGame.Location = new System.Drawing.Point(0, 48);
            this.btnNavGame.Size = new System.Drawing.Size(206, 38);
            this.btnNavGame.Text = "Bàn cờ";
            this.btnNavGame.NavIcon = CaroClient.CyberIcon.Gamepad;
            this.btnNavGame.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavGame.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavGame.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavGame.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavGame.Click += new System.EventHandler(this.btnNavGame_Click);

            this.btnNavCreate.Location = new System.Drawing.Point(0, 91);
            this.btnNavCreate.Size = new System.Drawing.Size(206, 38);
            this.btnNavCreate.Text = "Tạo phòng";
            this.btnNavCreate.NavIcon = CaroClient.CyberIcon.Plus;
            this.btnNavCreate.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavCreate.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCreate.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavCreate.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavCreate.Click += (s, e) => SwitchToView(this.pnlCreateRoomView, this.btnNavCreate);

            this.btnNavJoin.Location = new System.Drawing.Point(0, 134);
            this.btnNavJoin.Size = new System.Drawing.Size(206, 38);
            this.btnNavJoin.Text = "Tham gia phòng";
            this.btnNavJoin.NavIcon = CaroClient.CyberIcon.Link;
            this.btnNavJoin.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavJoin.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavJoin.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavJoin.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavJoin.Click += (s, e) => SwitchToView(this.pnlJoinRoomView, this.btnNavJoin);

            this.btnNavLeaderboard.Location = new System.Drawing.Point(0, 177);
            this.btnNavLeaderboard.Size = new System.Drawing.Size(206, 38);
            this.btnNavLeaderboard.Text = "Bảng xếp hạng";
            this.btnNavLeaderboard.NavIcon = CaroClient.CyberIcon.Trophy;
            this.btnNavLeaderboard.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavLeaderboard.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavLeaderboard.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavLeaderboard.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavLeaderboard.Click += (s, e) => SwitchToView(this.pnlLeaderboardView, this.btnNavLeaderboard);

            this.btnNavHistory.Location = new System.Drawing.Point(0, 220);
            this.btnNavHistory.Size = new System.Drawing.Size(206, 38);
            this.btnNavHistory.Text = "Hồ sơ cá nhân";
            this.btnNavHistory.NavIcon = CaroClient.CyberIcon.User;
            this.btnNavHistory.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavHistory.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavHistory.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavHistory.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavHistory.Click += (s, e) => SwitchToView(this.pnlHistoryView, this.btnNavHistory);

            // -------------------------------------------------------------
            // pnlSidebarBottom (Bottom Sidebar: Nút Đăng nhập nổi bật + Card Hồ sơ)
            // -------------------------------------------------------------
            this.pnlSidebarBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSidebarBottom.Height = 126;
            this.pnlSidebarBottom.BackColor = System.Drawing.Color.Transparent;
            this.pnlSidebarBottom.Controls.Add(this.btnNavSettings);
            this.pnlSidebarBottom.Controls.Add(this.pnlProfileCard);

            // btnNavSettings: Nút ĐĂNG NHẬP đặt ngay gần hồ sơ luôn cho nổi bật
            this.btnNavSettings.Location = new System.Drawing.Point(0, 0);
            this.btnNavSettings.Size = new System.Drawing.Size(206, 42);
            this.btnNavSettings.Text = "⚡ ĐĂNG NHẬP";
            this.btnNavSettings.NavIcon = CaroClient.CyberIcon.Login;
            this.btnNavSettings.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavSettings.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNavSettings.IsPrimaryGradient = true;
            this.btnNavSettings.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavSettings.Click += (s, e) => HandleNavSettingsClick();

            // pnlProfileCard (Thẻ hồ sơ tóm tắt ở góc dưới)
            this.pnlProfileCard.Location = new System.Drawing.Point(0, 48);
            this.pnlProfileCard.Size = new System.Drawing.Size(206, 74);
            this.pnlProfileCard.FillColor = System.Drawing.Color.FromArgb(14, 25, 50);
            this.pnlProfileCard.BorderColor = System.Drawing.Color.FromArgb(24, 44, 84);
            this.pnlProfileCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlProfileCard.Click += (s, e) => SwitchToView(this.pnlHistoryView, this.btnNavHistory);
            this.pnlProfileCard.Controls.Add(this.lblProfileName);
            this.pnlProfileCard.Controls.Add(this.lblProfileLevel);
            this.pnlProfileCard.Controls.Add(this.lblProfileXp);

            this.lblProfileName.Text = "Khách (Offline)";
            this.lblProfileName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblProfileName.ForeColor = System.Drawing.Color.White;
            this.lblProfileName.Location = new System.Drawing.Point(12, 8);
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProfileName.Click += (s, e) => SwitchToView(this.pnlHistoryView, this.btnNavHistory);

            this.lblProfileLevel.Text = "Chế độ Offline";
            this.lblProfileLevel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblProfileLevel.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblProfileLevel.Location = new System.Drawing.Point(12, 28);
            this.lblProfileLevel.AutoSize = true;
            this.lblProfileLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProfileLevel.Click += (s, e) => SwitchToView(this.pnlHistoryView, this.btnNavHistory);

            this.lblProfileXp.Text = "Chơi với Máy";
            this.lblProfileXp.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblProfileXp.ForeColor = System.Drawing.Color.FromArgb(160, 185, 220);
            this.lblProfileXp.Location = new System.Drawing.Point(12, 46);
            this.lblProfileXp.AutoSize = true;
            this.lblProfileXp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProfileXp.Click += (s, e) => SwitchToView(this.pnlHistoryView, this.btnNavHistory);

            this.btnLogout.Location = new System.Drawing.Point(12, 68);
            this.btnLogout.Size = new System.Drawing.Size(182, 30);
            this.btnLogout.Text = "⎋ Đăng xuất";
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.ParentBgColor = System.Drawing.Color.FromArgb(14, 25, 50);
            this.btnLogout.NormalColor = System.Drawing.Color.FromArgb(28, 42, 70);
            this.btnLogout.HoverColor = System.Drawing.Color.FromArgb(255, 46, 99);
            this.btnLogout.CornerRadius = 6;
            this.btnLogout.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogout.Visible = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // -------------------------------------------------------------
            // pnlRightBar (Right Panel - Dedicated strictly to Chat)
            // -------------------------------------------------------------
            this.pnlRightBar.BackColor = System.Drawing.Color.FromArgb(8, 14, 28);
            this.pnlRightBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightBar.Width = 280;
            this.pnlRightBar.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRightBar.Visible = false;
            this.pnlRightBar.Controls.Add(this.cardChat);

            // cardChat (Takes full vertical height of right sidebar)
            this.cardChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardChat.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.cardChat.Controls.Add(this.rtbChatLog);
            this.cardChat.Controls.Add(this.pnlChatBottom);
            this.cardChat.Controls.Add(this.lblChatTitle);

            this.lblChatTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChatTitle.Height = 28;
            this.lblChatTitle.Text = "Chat phòng";
            this.lblChatTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblChatTitle.ForeColor = System.Drawing.Color.White;

            this.pnlChatBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChatBottom.Height = 40;
            this.pnlChatBottom.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlChatBottom.Controls.Add(this.txtChatInput);
            this.pnlChatBottom.Controls.Add(this.btnSendChat);

            this.txtChatInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChatInput.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtChatInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChatInput.ForeColor = System.Drawing.Color.White;
            this.txtChatInput.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtChatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChatInput_KeyDown);

            this.btnSendChat.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendChat.Width = 46;
            this.btnSendChat.Text = "➤";
            this.btnSendChat.IsPrimaryGradient = true;
            this.btnSendChat.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSendChat.Click += new System.EventHandler(this.btnSendChat_Click);

            this.rtbChatLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChatLog.BackColor = System.Drawing.Color.FromArgb(10, 18, 36);
            this.rtbChatLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChatLog.ForeColor = System.Drawing.Color.FromArgb(220, 235, 255);
            this.rtbChatLog.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.rtbChatLog.ReadOnly = true;

            // -------------------------------------------------------------
            // pnlMainContent (Center Multi-View Container)
            // -------------------------------------------------------------
            this.pnlMainContent.BackColor = System.Drawing.Color.FromArgb(8, 13, 26);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Controls.Add(this.pnlHomeView);
            this.pnlMainContent.Controls.Add(this.pnlGameView);
            this.pnlMainContent.Controls.Add(this.pnlCreateRoomView);
            this.pnlMainContent.Controls.Add(this.pnlJoinRoomView);
            this.pnlMainContent.Controls.Add(this.pnlHistoryView);
            this.pnlMainContent.Controls.Add(this.pnlLeaderboardView);
            this.pnlMainContent.Controls.Add(this.pnlSettingsView);

            // =============================================================
            // View 1: pnlHomeView (Dashboard / Trang chủ)
            // =============================================================
            this.pnlHomeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHomeView.Padding = new System.Windows.Forms.Padding(16, 12, 16, 14);
            this.pnlHomeView.Controls.Add(this.pnlMiddleGrid);
            this.pnlHomeView.Controls.Add(this.heroBanner);

            // heroBanner
            this.heroBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.heroBanner.Height = 115;
            this.heroBanner.Controls.Add(this.lblHeroTitle);
            this.heroBanner.Controls.Add(this.lblHeroSubtitle);

            this.lblHeroTitle.Text = "Chơi cờ caro qua mạng LAN - CaroKan";
            this.lblHeroTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeroTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeroTitle.Location = new System.Drawing.Point(24, 20);
            this.lblHeroTitle.AutoSize = true;

            this.lblHeroSubtitle.Text = "Kết nối cùng bạn bè trong cùng một mạng LAN.\nKhông cần Internet, giao đấu trực tiếp hoặc luyện tập cùng AI!";
            this.lblHeroSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHeroSubtitle.ForeColor = System.Drawing.Color.FromArgb(160, 185, 225);
            this.lblHeroSubtitle.Location = new System.Drawing.Point(25, 56);
            this.lblHeroSubtitle.AutoSize = true;

            // pnlMiddleGrid (Khung chính trang chủ: Thông tin máy chủ & Người chơi Online)
            this.pnlMiddleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddleGrid.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlMiddleGrid.Controls.Add(this.pnlPlayersContainer);
            this.pnlMiddleGrid.Controls.Add(this.pnlServerInfoContainer);

            // pnlServerInfoContainer (Cột trái: Thông tin máy chủ & Thao tác nhanh)
            this.pnlServerInfoContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlServerInfoContainer.Width = 330;
            this.pnlServerInfoContainer.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlServerInfoContainer.Controls.Add(this.cardServerInfo);

            // cardServerInfo
            this.cardServerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardServerInfo.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.cardServerInfo.Controls.Add(this.btnHomeSettings);
            this.cardServerInfo.Controls.Add(this.btnHomeJoinRoom);
            this.cardServerInfo.Controls.Add(this.btnHomeCreateRoom);
            this.cardServerInfo.Controls.Add(this.lblHomeServerIp);
            this.cardServerInfo.Controls.Add(this.lblHomeServerHost);
            this.cardServerInfo.Controls.Add(this.lblHomeServerStatus);
            this.cardServerInfo.Controls.Add(this.lblHomeServerHeader);

            this.lblHomeServerHeader.Text = "THÔNG TIN MÁY CHỦ";
            this.lblHomeServerHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblHomeServerHeader.ForeColor = System.Drawing.Color.White;
            this.lblHomeServerHeader.Location = new System.Drawing.Point(18, 16);
            this.lblHomeServerHeader.AutoSize = true;

            this.lblHomeServerStatus.Text = "○ Chưa kết nối";
            this.lblHomeServerStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHomeServerStatus.ForeColor = System.Drawing.Color.FromArgb(255, 75, 75);
            this.lblHomeServerStatus.Location = new System.Drawing.Point(18, 48);
            this.lblHomeServerStatus.AutoSize = true;

            this.lblHomeServerHost.Text = "💻 Tên máy chủ: ---";
            this.lblHomeServerHost.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHomeServerHost.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblHomeServerHost.Location = new System.Drawing.Point(18, 76);
            this.lblHomeServerHost.AutoSize = true;

            this.lblHomeServerIp.Text = "📡 IP & Cổng: ---";
            this.lblHomeServerIp.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHomeServerIp.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblHomeServerIp.Location = new System.Drawing.Point(18, 102);
            this.lblHomeServerIp.AutoSize = true;

            // btnHomeCreateRoom
            this.btnHomeCreateRoom.Location = new System.Drawing.Point(18, 150);
            this.btnHomeCreateRoom.Size = new System.Drawing.Size(284, 42);
            this.btnHomeCreateRoom.Text = "+  Tạo phòng mới";
            this.btnHomeCreateRoom.IsPrimaryGradient = true;
            this.btnHomeCreateRoom.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHomeCreateRoom.Click += (s, e) => SwitchToView(this.pnlCreateRoomView, this.btnNavCreate);

            // btnHomeJoinRoom
            this.btnHomeJoinRoom.Location = new System.Drawing.Point(18, 202);
            this.btnHomeJoinRoom.Size = new System.Drawing.Size(284, 40);
            this.btnHomeJoinRoom.NormalColor = System.Drawing.Color.FromArgb(16, 32, 64);
            this.btnHomeJoinRoom.HoverColor = System.Drawing.Color.FromArgb(24, 48, 96);
            this.btnHomeJoinRoom.BorderLineColor = System.Drawing.Color.FromArgb(32, 60, 115);
            this.btnHomeJoinRoom.Text = "➜  Danh sách phòng đấu";
            this.btnHomeJoinRoom.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHomeJoinRoom.Click += (s, e) => SwitchToView(this.pnlJoinRoomView, this.btnNavJoin);

            // btnHomeSettings
            this.btnHomeSettings.Location = new System.Drawing.Point(18, 252);
            this.btnHomeSettings.Size = new System.Drawing.Size(284, 38);
            this.btnHomeSettings.NormalColor = System.Drawing.Color.FromArgb(20, 36, 68);
            this.btnHomeSettings.HoverColor = System.Drawing.Color.FromArgb(30, 52, 95);
            this.btnHomeSettings.BorderLineColor = System.Drawing.Color.FromArgb(38, 65, 110);
            this.btnHomeSettings.Text = "⚙  Cài đặt kết nối...";
            this.btnHomeSettings.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHomeSettings.Click += (s, e) => SwitchToView(this.pnlSettingsView, this.btnNavSettings);

            // pnlPlayersContainer (Cột phải: Danh sách người chơi online)
            this.pnlPlayersContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlayersContainer.Padding = new System.Windows.Forms.Padding(0);
            this.pnlPlayersContainer.Controls.Add(this.cardConnectedPlayers);

            // cardConnectedPlayers
            this.cardConnectedPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardConnectedPlayers.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.cardConnectedPlayers.Controls.Add(this.lblNoPlayers);
            this.cardConnectedPlayers.Controls.Add(this.lstConnectedPlayers);
            this.cardConnectedPlayers.Controls.Add(this.lblHomePlayersHeader);

            this.lblHomePlayersHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHomePlayersHeader.Height = 32;
            this.lblHomePlayersHeader.Text = "NGƯỜI CHƠI ONLINE (0)";
            this.lblHomePlayersHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblHomePlayersHeader.ForeColor = System.Drawing.Color.White;

            this.lstConnectedPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConnectedPlayers.BackColor = System.Drawing.Color.FromArgb(13, 24, 48);
            this.lstConnectedPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstConnectedPlayers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstConnectedPlayers.ItemHeight = 34;
            this.lstConnectedPlayers.SelectionMode = System.Windows.Forms.SelectionMode.None;

            this.lblNoPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoPlayers.Text = "Chưa kết nối đến máy chủ";
            this.lblNoPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoPlayers.ForeColor = System.Drawing.Color.FromArgb(120, 140, 175);
            this.lblNoPlayers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNoPlayers.Visible = true;

            // =============================================================
            // View 2: pnlGameView (Bàn cờ / Phòng đấu)
            // =============================================================
            this.pnlGameView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGameView.Padding = new System.Windows.Forms.Padding(12);
            this.pnlGameView.Controls.Add(this.boardControl1);
            this.pnlGameView.Controls.Add(this.pnlGameHeader);

            // pnlGameHeader (Height 85px, clean 2-column layout)
            this.pnlGameHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGameHeader.Height = 85;
            this.pnlGameHeader.Controls.Add(this.pnlGameActions);
            this.pnlGameHeader.Controls.Add(this.pnlGameInfo);

            // pnlGameActions (Right side: Action Buttons ALWAYS visible side-by-side, never clipped)
            this.pnlGameActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlGameActions.Width = 320;
            this.pnlGameActions.BackColor = System.Drawing.Color.Transparent;
            this.pnlGameActions.Controls.Add(this.btnSurrender);
            this.pnlGameActions.Controls.Add(this.btnRestart);
            this.pnlGameActions.Controls.Add(this.btnLeaveRoom);

            this.btnSurrender.Location = new System.Drawing.Point(10, 22);
            this.btnSurrender.Size = new System.Drawing.Size(95, 40);
            this.btnSurrender.Text = "Đầu hàng";
            this.btnSurrender.NormalColor = System.Drawing.Color.FromArgb(190, 35, 70);
            this.btnSurrender.HoverColor = System.Drawing.Color.FromArgb(230, 45, 90);
            this.btnSurrender.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSurrender.Click += new System.EventHandler(this.btnSurrender_Click);

            this.btnRestart.Location = new System.Drawing.Point(112, 22);
            this.btnRestart.Size = new System.Drawing.Size(95, 40);
            this.btnRestart.Text = "Chơi lại";
            this.btnRestart.NormalColor = System.Drawing.Color.FromArgb(0, 160, 90);
            this.btnRestart.HoverColor = System.Drawing.Color.FromArgb(0, 200, 110);
            this.btnRestart.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);

            this.btnLeaveRoom.Location = new System.Drawing.Point(214, 22);
            this.btnLeaveRoom.Size = new System.Drawing.Size(95, 40);
            this.btnLeaveRoom.Text = "Thoát phòng";
            this.btnLeaveRoom.NormalColor = System.Drawing.Color.FromArgb(40, 50, 75);
            this.btnLeaveRoom.HoverColor = System.Drawing.Color.FromArgb(60, 75, 110);
            this.btnLeaveRoom.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLeaveRoom.Click += new System.EventHandler(this.btnLeaveRoom_Click);

            // pnlGameInfo (Left side: Room, Timer, Players, Turn - each on its own row, never overlapping buttons)
            this.pnlGameInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGameInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlGameInfo.Controls.Add(this.lblRoomIdInfo);
            this.pnlGameInfo.Controls.Add(this.lblTimer);
            this.pnlGameInfo.Controls.Add(this.lblPlayersInfo);
            this.pnlGameInfo.Controls.Add(this.lblTurnInfo);

            // Row 1: Room ID & Timer
            this.lblRoomIdInfo.Location = new System.Drawing.Point(14, 10);
            this.lblRoomIdInfo.AutoSize = true;
            this.lblRoomIdInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRoomIdInfo.ForeColor = System.Drawing.Color.White;
            this.lblRoomIdInfo.Text = "Phòng: ---";

            this.lblTimer.Location = new System.Drawing.Point(150, 10);
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.Color.FromArgb(255, 145, 0);
            this.lblTimer.Text = "● 30 Giây";

            // Row 2: Players
            this.lblPlayersInfo.Location = new System.Drawing.Point(14, 34);
            this.lblPlayersInfo.AutoSize = true;
            this.lblPlayersInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPlayersInfo.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblPlayersInfo.Text = "X: ---   VS   O: ---";

            // Row 3: Turn & Match Status (AutoEllipsis so it never overflows or touches action buttons)
            this.lblTurnInfo.Location = new System.Drawing.Point(14, 56);
            this.lblTurnInfo.Size = new System.Drawing.Size(380, 22);
            this.lblTurnInfo.AutoSize = false;
            this.lblTurnInfo.AutoEllipsis = true;
            this.lblTurnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTurnInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTurnInfo.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblTurnInfo.Text = "● Lượt đi: ---";

            // boardControl1
            this.boardControl1.Dock = System.Windows.Forms.DockStyle.Fill;

            // =============================================================
            // View 3: pnlCreateRoomView (Tạo phòng)
            // =============================================================
            this.pnlCreateRoomView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCreateRoomView.Padding = new System.Windows.Forms.Padding(20);
            this.pnlCreateRoomView.Controls.Add(this.cardCreateBox);
            this.pnlCreateRoomView.Resize += new System.EventHandler(this.pnlCreateRoomView_Resize);

            this.cardCreateBox.Size = new System.Drawing.Size(680, 560);
            this.cardCreateBox.Location = new System.Drawing.Point(80, 20);
            this.cardCreateBox.Padding = new System.Windows.Forms.Padding(28, 20, 28, 20);
            this.cardCreateBox.Controls.Add(this.btnCancelCreate);
            this.cardCreateBox.Controls.Add(this.btnCreateRoom);
            this.cardCreateBox.Controls.Add(this.pillRuleFree);
            this.cardCreateBox.Controls.Add(this.pillRuleStandard);
            this.cardCreateBox.Controls.Add(this.secHeaderRule);
            this.cardCreateBox.Controls.Add(this.pnlAiDifficultyOptions);
            this.cardCreateBox.Controls.Add(this.pnlTimeLimitOptions);
            this.cardCreateBox.Controls.Add(this.cardTimeFree);
            this.cardCreateBox.Controls.Add(this.cardTimeTurn);
            this.cardCreateBox.Controls.Add(this.secHeaderTime);
            this.cardCreateBox.Controls.Add(this.cardModeAI);
            this.cardCreateBox.Controls.Add(this.cardModePvP);
            this.cardCreateBox.Controls.Add(this.secHeaderMode);
            this.cardCreateBox.Controls.Add(this.pnlCreateHeader);

            // pnlCreateHeader
            this.pnlCreateHeader.Location = new System.Drawing.Point(28, 20);
            this.pnlCreateHeader.Size = new System.Drawing.Size(624, 58);
            this.pnlCreateHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlCreateHeader.Controls.Add(this.badgeCreatePlus);
            this.pnlCreateHeader.Controls.Add(this.lblCreateTitle);
            this.pnlCreateHeader.Controls.Add(this.lblCreateSubtitle);

            this.badgeCreatePlus.Location = new System.Drawing.Point(0, 6);
            this.badgeCreatePlus.Size = new System.Drawing.Size(42, 42);

            this.lblCreateTitle.Text = "Tạo phòng mới";
            this.lblCreateTitle.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.lblCreateTitle.ForeColor = System.Drawing.Color.White;
            this.lblCreateTitle.Location = new System.Drawing.Point(52, 2);
            this.lblCreateTitle.AutoSize = true;

            this.lblCreateSubtitle.Text = "Thiết lập chế độ chơi và mời bạn bè cùng tham gia!";
            this.lblCreateSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCreateSubtitle.ForeColor = System.Drawing.Color.FromArgb(140, 168, 208);
            this.lblCreateSubtitle.Location = new System.Drawing.Point(54, 32);
            this.lblCreateSubtitle.AutoSize = true;

            // Section 1: Mode
            this.secHeaderMode.Location = new System.Drawing.Point(28, 86);
            this.secHeaderMode.Size = new System.Drawing.Size(624, 26);
            this.secHeaderMode.StepNumber = 1;
            this.secHeaderMode.HeaderText = "Chọn chế độ chơi";

            this.cardModePvP.Location = new System.Drawing.Point(28, 116);
            this.cardModePvP.Size = new System.Drawing.Size(306, 90);
            this.cardModePvP.IconType = CaroClient.CardIconType.TwoPlayers;
            this.cardModePvP.Title = "2 người";
            this.cardModePvP.Subtitle = "Đấu trực tiếp";
            this.cardModePvP.IsSelected = true;

            this.cardModeAI.Location = new System.Drawing.Point(346, 116);
            this.cardModeAI.Size = new System.Drawing.Size(306, 90);
            this.cardModeAI.IconType = CaroClient.CardIconType.RobotAI;
            this.cardModeAI.Title = "Vs AI";
            this.cardModeAI.Subtitle = "Chơi với máy";
            this.cardModeAI.IsSelected = false;

            // Section 2: Time
            this.secHeaderTime.Location = new System.Drawing.Point(28, 218);
            this.secHeaderTime.Size = new System.Drawing.Size(624, 26);
            this.secHeaderTime.StepNumber = 2;
            this.secHeaderTime.HeaderText = "Quy định thời gian";

            this.cardTimeTurn.Location = new System.Drawing.Point(28, 248);
            this.cardTimeTurn.Size = new System.Drawing.Size(306, 74);
            this.cardTimeTurn.IconType = CaroClient.CardIconType.ClockTurn;
            this.cardTimeTurn.Title = "Theo lượt";
            this.cardTimeTurn.Subtitle = "";
            this.cardTimeTurn.IsSelected = true;

            this.cardTimeFree.Location = new System.Drawing.Point(346, 248);
            this.cardTimeFree.Size = new System.Drawing.Size(306, 74);
            this.cardTimeFree.IconType = CaroClient.CardIconType.Stopwatch;
            this.cardTimeFree.Title = "Đếm ngược";
            this.cardTimeFree.Subtitle = "";
            this.cardTimeFree.IsSelected = false;

            // pnlTimeLimitOptions
            this.pnlTimeLimitOptions.Location = new System.Drawing.Point(28, 332);
            this.pnlTimeLimitOptions.Size = new System.Drawing.Size(624, 38);
            this.pnlTimeLimitOptions.BackColor = System.Drawing.Color.Transparent;
            this.pnlTimeLimitOptions.Controls.Add(this.lblTimePillLabel);
            this.pnlTimeLimitOptions.Controls.Add(this.pillTime15);
            this.pnlTimeLimitOptions.Controls.Add(this.pillTime30);
            this.pnlTimeLimitOptions.Controls.Add(this.pillTime60);

            this.lblTimePillLabel.Text = "Thời gian mỗi lượt:";
            this.lblTimePillLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimePillLabel.ForeColor = System.Drawing.Color.FromArgb(150, 175, 215);
            this.lblTimePillLabel.Location = new System.Drawing.Point(0, 8);
            this.lblTimePillLabel.AutoSize = true;

            this.pillTime15.Location = new System.Drawing.Point(145, 0);
            this.pillTime15.Size = new System.Drawing.Size(95, 36);
            this.pillTime15.PillText = "15 giây";
            this.pillTime15.IsSelected = false;

            this.pillTime30.Location = new System.Drawing.Point(250, 0);
            this.pillTime30.Size = new System.Drawing.Size(95, 36);
            this.pillTime30.PillText = "30 giây";
            this.pillTime30.IsSelected = true;

            this.pillTime60.Location = new System.Drawing.Point(355, 0);
            this.pillTime60.Size = new System.Drawing.Size(95, 36);
            this.pillTime60.PillText = "60 giây";
            this.pillTime60.IsSelected = false;

            // pnlAiDifficultyOptions (Shown when Vs AI is selected)
            this.pnlAiDifficultyOptions.Location = new System.Drawing.Point(28, 376);
            this.pnlAiDifficultyOptions.Size = new System.Drawing.Size(624, 38);
            this.pnlAiDifficultyOptions.BackColor = System.Drawing.Color.Transparent;
            this.pnlAiDifficultyOptions.Visible = false;
            this.pnlAiDifficultyOptions.Controls.Add(this.lblAiDifficultyLabel);
            this.pnlAiDifficultyOptions.Controls.Add(this.pillAiEasy);
            this.pnlAiDifficultyOptions.Controls.Add(this.pillAiMedium);
            this.pnlAiDifficultyOptions.Controls.Add(this.pillAiHard);

            this.lblAiDifficultyLabel.Text = "Mức độ AI:";
            this.lblAiDifficultyLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAiDifficultyLabel.ForeColor = System.Drawing.Color.FromArgb(150, 175, 215);
            this.lblAiDifficultyLabel.Location = new System.Drawing.Point(0, 8);
            this.lblAiDifficultyLabel.AutoSize = true;

            this.pillAiEasy.Location = new System.Drawing.Point(145, 0);
            this.pillAiEasy.Size = new System.Drawing.Size(95, 36);
            this.pillAiEasy.PillText = "Dễ";
            this.pillAiEasy.IsSelected = false;

            this.pillAiMedium.Location = new System.Drawing.Point(250, 0);
            this.pillAiMedium.Size = new System.Drawing.Size(115, 36);
            this.pillAiMedium.PillText = "Trung bình";
            this.pillAiMedium.IsSelected = true;

            this.pillAiHard.Location = new System.Drawing.Point(375, 0);
            this.pillAiHard.Size = new System.Drawing.Size(95, 36);
            this.pillAiHard.PillText = "Khó";
            this.pillAiHard.IsSelected = false;

            // Section 3: Rule
            this.secHeaderRule.Location = new System.Drawing.Point(28, 420);
            this.secHeaderRule.Size = new System.Drawing.Size(624, 26);
            this.secHeaderRule.StepNumber = 3;
            this.secHeaderRule.HeaderText = "Luật thi đấu";

            this.pillRuleStandard.Location = new System.Drawing.Point(28, 452);
            this.pillRuleStandard.Size = new System.Drawing.Size(306, 42);
            this.pillRuleStandard.PillText = "Chặn 2 đầu không thắng";
            this.pillRuleStandard.IsSelected = true;

            this.pillRuleFree.Location = new System.Drawing.Point(346, 452);
            this.pillRuleFree.Size = new System.Drawing.Size(306, 42);
            this.pillRuleFree.PillText = "Luật tự do (5 con liên tiếp)";
            this.pillRuleFree.IsSelected = false;

            // Action Buttons
            this.btnCreateRoom.Location = new System.Drawing.Point(28, 510);
            this.btnCreateRoom.Size = new System.Drawing.Size(430, 48);
            this.btnCreateRoom.Text = "▶  Tạo phòng";
            this.btnCreateRoom.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCreateRoom.IsPrimaryGradient = true;
            this.btnCreateRoom.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);

            this.btnCancelCreate.Location = new System.Drawing.Point(470, 510);
            this.btnCancelCreate.Size = new System.Drawing.Size(182, 48);
            this.btnCancelCreate.Text = "← Quay lại";
            this.btnCancelCreate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelCreate.NormalColor = System.Drawing.Color.FromArgb(28, 42, 70);
            this.btnCancelCreate.HoverColor = System.Drawing.Color.FromArgb(42, 62, 100);
            this.btnCancelCreate.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelCreate.Click += (s, e) => SwitchToView(this.pnlHomeView, this.btnNavHome);

            // =============================================================
            // View 4: pnlJoinRoomView (Tham gia phòng & Danh sách phòng)
            // =============================================================
            this.pnlJoinRoomView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlJoinRoomView.Padding = new System.Windows.Forms.Padding(14, 10, 14, 12);
            this.pnlJoinRoomView.Controls.Add(this.cardFeaturedRooms);
            this.pnlJoinRoomView.Controls.Add(this.cardJoinTopBar);

            // cardJoinTopBar (Header bar with title and quick join by room ID)
            this.cardJoinTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardJoinTopBar.Height = 72;
            this.cardJoinTopBar.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.cardJoinTopBar.Controls.Add(this.pnlJoinActions);
            this.cardJoinTopBar.Controls.Add(this.lblJoinSubtitle);
            this.cardJoinTopBar.Controls.Add(this.lblJoinTitle);

            this.lblJoinTitle.Text = "THAM GIA PHÒNG ĐẤU";
            this.lblJoinTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblJoinTitle.ForeColor = System.Drawing.Color.White;
            this.lblJoinTitle.Location = new System.Drawing.Point(14, 12);
            this.lblJoinTitle.AutoSize = true;

            this.lblJoinSubtitle.Text = "Nhập mã phòng hoặc chọn phòng bên dưới để vào chơi / xem trực tiếp";
            this.lblJoinSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblJoinSubtitle.ForeColor = System.Drawing.Color.FromArgb(160, 185, 220);
            this.lblJoinSubtitle.Location = new System.Drawing.Point(14, 38);
            this.lblJoinSubtitle.AutoSize = true;

            // pnlJoinActions (Right container for room input & buttons)
            this.pnlJoinActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlJoinActions.Width = 460;
            this.pnlJoinActions.BackColor = System.Drawing.Color.Transparent;
            this.pnlJoinActions.Controls.Add(this.btnCancelJoin);
            this.pnlJoinActions.Controls.Add(this.btnJoinRoom);
            this.pnlJoinActions.Controls.Add(this.txtRoomId);
            this.pnlJoinActions.Controls.Add(this.lblRoomIdPrompt);

            this.lblRoomIdPrompt.Text = "Mã phòng:";
            this.lblRoomIdPrompt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRoomIdPrompt.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblRoomIdPrompt.Location = new System.Drawing.Point(6, 18);
            this.lblRoomIdPrompt.AutoSize = true;

            this.txtRoomId.Location = new System.Drawing.Point(82, 14);
            this.txtRoomId.Size = new System.Drawing.Size(115, 28);
            this.txtRoomId.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtRoomId.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtRoomId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomId.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.txtRoomId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            this.btnJoinRoom.Location = new System.Drawing.Point(208, 12);
            this.btnJoinRoom.Size = new System.Drawing.Size(115, 34);
            this.btnJoinRoom.Text = "➜ Vào phòng";
            this.btnJoinRoom.IsPrimaryGradient = true;
            this.btnJoinRoom.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);

            this.btnCancelJoin.Location = new System.Drawing.Point(333, 12);
            this.btnCancelJoin.Size = new System.Drawing.Size(115, 34);
            this.btnCancelJoin.Text = "← Trang chủ";
            this.btnCancelJoin.NormalColor = System.Drawing.Color.FromArgb(28, 42, 70);
            this.btnCancelJoin.HoverColor = System.Drawing.Color.FromArgb(42, 62, 100);
            this.btnCancelJoin.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelJoin.Click += (s, e) => SwitchToView(this.pnlHomeView, this.btnNavHome);

            // cardFeaturedRooms (Bảng danh sách phòng chơi đang mở)
            this.cardFeaturedRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardFeaturedRooms.Padding = new System.Windows.Forms.Padding(12, 10, 12, 12);
            this.cardFeaturedRooms.Controls.Add(this.lblNoRooms);
            this.cardFeaturedRooms.Controls.Add(this.dgvFeaturedRooms);
            this.cardFeaturedRooms.Controls.Add(this.pnlRoomsHeaderBar);

            // pnlRoomsHeaderBar
            this.pnlRoomsHeaderBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRoomsHeaderBar.Height = 36;
            this.pnlRoomsHeaderBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlRoomsHeaderBar.Controls.Add(this.btnRefreshRooms);
            this.pnlRoomsHeaderBar.Controls.Add(this.lblRoomsHeader);

            this.lblRoomsHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRoomsHeader.Text = "PHÒNG CHƠI ĐANG MỞ";
            this.lblRoomsHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRoomsHeader.ForeColor = System.Drawing.Color.White;
            this.lblRoomsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRoomsHeader.AutoSize = true;

            this.btnRefreshRooms.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefreshRooms.Width = 92;
            this.btnRefreshRooms.Text = "🔄 Làm mới";
            this.btnRefreshRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshRooms.FlatAppearance.BorderSize = 0;
            this.btnRefreshRooms.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRefreshRooms.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.btnRefreshRooms.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshRooms.Click += new System.EventHandler(this.btnRefreshRooms_Click);

            // dgvFeaturedRooms (Fixed sizes, no dragging, professional UI)
            this.dgvFeaturedRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFeaturedRooms.BackgroundColor = System.Drawing.Color.FromArgb(12, 22, 44);
            this.dgvFeaturedRooms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFeaturedRooms.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvFeaturedRooms.GridColor = System.Drawing.Color.FromArgb(20, 36, 70);
            this.dgvFeaturedRooms.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(12, 22, 44);
            this.dgvFeaturedRooms.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvFeaturedRooms.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(20, 45, 90);
            this.dgvFeaturedRooms.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvFeaturedRooms.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(14, 28, 56);
            this.dgvFeaturedRooms.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(160, 180, 215);
            this.dgvFeaturedRooms.EnableHeadersVisualStyles = false;
            this.dgvFeaturedRooms.ReadOnly = true;
            this.dgvFeaturedRooms.RowHeadersVisible = false;
            this.dgvFeaturedRooms.AllowUserToAddRows = false;
            this.dgvFeaturedRooms.AllowUserToDeleteRows = false;
            this.dgvFeaturedRooms.AllowUserToResizeRows = false;
            this.dgvFeaturedRooms.AllowUserToResizeColumns = false;
            this.dgvFeaturedRooms.RowTemplate.Height = 36;
            this.dgvFeaturedRooms.ColumnHeadersHeight = 32;
            this.dgvFeaturedRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFeaturedRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFeaturedRooms.MultiSelect = false;
            this.dgvFeaturedRooms.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFeaturedRooms_CellDoubleClick);
            this.dgvFeaturedRooms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFeaturedRooms_CellContentClick);

            // lblNoRooms
            this.lblNoRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoRooms.Text = "Hiện chưa có phòng nào đang mở.\nHãy bấm '+ Tạo phòng mới' ở menu bên trái để bắt đầu trận đấu!";
            this.lblNoRooms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoRooms.ForeColor = System.Drawing.Color.FromArgb(120, 140, 175);
            this.lblNoRooms.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNoRooms.Visible = false;

            // =============================================================
            // View 5: pnlSettingsView (Đăng nhập / Đăng ký tài khoản)
            // =============================================================
            this.pnlSettingsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettingsView.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.pnlSettingsView.Controls.Add(this.cardSettingsBox);

            this.cardSettingsBox.Size = new System.Drawing.Size(560, 465);
            this.cardSettingsBox.Location = new System.Drawing.Point(80, 20);
            this.cardSettingsBox.Controls.Add(this.btnTabLogin);
            this.cardSettingsBox.Controls.Add(this.btnTabRegister);
            this.cardSettingsBox.Controls.Add(this.pnlAuthBodyLogin);
            this.cardSettingsBox.Controls.Add(this.pnlAuthBodyRegister);

            // Tab Buttons
            this.btnTabLogin.Location = new System.Drawing.Point(24, 16);
            this.btnTabLogin.Size = new System.Drawing.Size(140, 36);
            this.btnTabLogin.Text = "🔑 ĐĂNG NHẬP";
            this.btnTabLogin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTabLogin.IsPrimaryGradient = true;
            this.btnTabLogin.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTabLogin.Click += (s, e) => SwitchAuthTab(true);

            this.btnTabRegister.Location = new System.Drawing.Point(170, 16);
            this.btnTabRegister.Size = new System.Drawing.Size(185, 36);
            this.btnTabRegister.Text = "📝 ĐĂNG KÝ MỚI";
            this.btnTabRegister.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTabRegister.NormalColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.btnTabRegister.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnTabRegister.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTabRegister.Click += (s, e) => SwitchAuthTab(false);

            // -------------------------------------------------------------
            // pnlAuthBodyLogin (Tab Đăng nhập)
            // -------------------------------------------------------------
            this.pnlAuthBodyLogin.Location = new System.Drawing.Point(24, 58);
            this.pnlAuthBodyLogin.Size = new System.Drawing.Size(512, 395);
            this.pnlAuthBodyLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlAuthBodyLogin.Controls.Add(this.lblSettingsTitle);
            this.pnlAuthBodyLogin.Controls.Add(this.lblSettingsNotice);
            this.pnlAuthBodyLogin.Controls.Add(this.lblServerIp);
            this.pnlAuthBodyLogin.Controls.Add(this.txtIp);
            this.pnlAuthBodyLogin.Controls.Add(this.lblPort);
            this.pnlAuthBodyLogin.Controls.Add(this.txtPort);
            this.pnlAuthBodyLogin.Controls.Add(this.lblPlayerName);
            this.pnlAuthBodyLogin.Controls.Add(this.txtName);
            this.pnlAuthBodyLogin.Controls.Add(this.lblPassword);
            this.pnlAuthBodyLogin.Controls.Add(this.txtPassword);
            this.pnlAuthBodyLogin.Controls.Add(this.lblStatusBadge);
            this.pnlAuthBodyLogin.Controls.Add(this.btnConnect);
            this.pnlAuthBodyLogin.Controls.Add(this.lblSwitchToRegister);
            this.pnlAuthBodyLogin.Controls.Add(this.lblOfflineTip);

            this.lblSettingsTitle.Text = "ĐĂNG NHẬP HỆ THỐNG (PvP LAN)";
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.White;
            this.lblSettingsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSettingsTitle.AutoSize = true;

            this.lblSettingsNotice.Text = "Đăng nhập tài khoản để vào phòng thi đấu 2 người, lưu điểm ELO và xếp hạng.";
            this.lblSettingsNotice.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSettingsNotice.ForeColor = System.Drawing.Color.FromArgb(160, 185, 220);
            this.lblSettingsNotice.Location = new System.Drawing.Point(0, 24);
            this.lblSettingsNotice.AutoSize = true;

            this.lblServerIp.Text = "1. Địa chỉ Server IP:";
            this.lblServerIp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblServerIp.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblServerIp.Location = new System.Drawing.Point(0, 52);
            this.lblServerIp.AutoSize = true;

            this.txtIp.Location = new System.Drawing.Point(0, 72);
            this.txtIp.Size = new System.Drawing.Size(240, 26);
            this.txtIp.Text = "127.0.0.1";
            this.txtIp.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIp.ForeColor = System.Drawing.Color.White;

            this.lblPort.Text = "Port:";
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblPort.Location = new System.Drawing.Point(252, 52);
            this.lblPort.AutoSize = true;

            this.txtPort.Location = new System.Drawing.Point(252, 72);
            this.txtPort.Size = new System.Drawing.Size(75, 26);
            this.txtPort.Text = "8888";
            this.txtPort.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);

            this.lblPlayerName.Text = "2. Tên đăng nhập (Username):";
            this.lblPlayerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPlayerName.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblPlayerName.Location = new System.Drawing.Point(0, 106);
            this.lblPlayerName.AutoSize = true;

            this.txtName.Location = new System.Drawing.Point(0, 126);
            this.txtName.Size = new System.Drawing.Size(327, 26);
            this.txtName.Text = "Player1";
            this.txtName.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.White;

            this.lblPassword.Text = "3. Mật khẩu (Password):";
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblPassword.Location = new System.Drawing.Point(0, 160);
            this.lblPassword.AutoSize = true;

            this.txtPassword.Location = new System.Drawing.Point(0, 180);
            this.txtPassword.Size = new System.Drawing.Size(327, 26);
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.ForeColor = System.Drawing.Color.White;

            this.lblStatusBadge.Text = "● Chưa kết nối";
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(255, 75, 75);
            this.lblStatusBadge.Location = new System.Drawing.Point(0, 214);
            this.lblStatusBadge.AutoSize = true;

            this.btnConnect.Location = new System.Drawing.Point(0, 240);
            this.btnConnect.Size = new System.Drawing.Size(327, 40);
            this.btnConnect.Text = "⚡ KẾT NỐI & ĐĂNG NHẬP";
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnConnect.IsPrimaryGradient = true;
            this.btnConnect.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            this.lblSwitchToRegister.Text = "👉 Chưa có tài khoản? Nhấn vào đây để Đăng ký ngay ➔";
            this.lblSwitchToRegister.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSwitchToRegister.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblSwitchToRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSwitchToRegister.Location = new System.Drawing.Point(0, 290);
            this.lblSwitchToRegister.AutoSize = true;
            this.lblSwitchToRegister.Click += (s, e) => SwitchAuthTab(false);

            this.lblOfflineTip.Text = "💡 Mẹo: Chơi với Máy (AI) hoàn toàn Offline, không cần mạng hay đăng nhập!";
            this.lblOfflineTip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblOfflineTip.ForeColor = System.Drawing.Color.FromArgb(0, 230, 118);
            this.lblOfflineTip.Location = new System.Drawing.Point(0, 320);
            this.lblOfflineTip.Size = new System.Drawing.Size(480, 40);

            // -------------------------------------------------------------
            // pnlAuthBodyRegister (Tab Đăng ký mới)
            // -------------------------------------------------------------
            this.pnlAuthBodyRegister.Location = new System.Drawing.Point(24, 58);
            this.pnlAuthBodyRegister.Size = new System.Drawing.Size(512, 395);
            this.pnlAuthBodyRegister.BackColor = System.Drawing.Color.Transparent;
            this.pnlAuthBodyRegister.Visible = false;
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegTitle);
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegNotice);
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegServerIp);
            this.pnlAuthBodyRegister.Controls.Add(this.txtRegIp);
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegPort);
            this.pnlAuthBodyRegister.Controls.Add(this.txtRegPort);
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegPlayerName);
            this.pnlAuthBodyRegister.Controls.Add(this.txtRegName);
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegPassword);
            this.pnlAuthBodyRegister.Controls.Add(this.txtRegPassword);
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegConfirmPassword);
            this.pnlAuthBodyRegister.Controls.Add(this.txtRegConfirmPassword);
            this.pnlAuthBodyRegister.Controls.Add(this.lblRegStatusBadge);
            this.pnlAuthBodyRegister.Controls.Add(this.btnRegister);
            this.pnlAuthBodyRegister.Controls.Add(this.lblSwitchToLogin);

            this.lblRegTitle.Text = "TẠO TÀI KHOẢN MỚI (PvP LAN)";
            this.lblRegTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRegTitle.ForeColor = System.Drawing.Color.White;
            this.lblRegTitle.Location = new System.Drawing.Point(0, 0);
            this.lblRegTitle.AutoSize = true;

            this.lblRegNotice.Text = "Tài khoản sẽ được lưu an toàn tại Database máy chủ Server.";
            this.lblRegNotice.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRegNotice.ForeColor = System.Drawing.Color.FromArgb(160, 185, 220);
            this.lblRegNotice.Location = new System.Drawing.Point(0, 24);
            this.lblRegNotice.AutoSize = true;

            this.lblRegServerIp.Text = "1. Địa chỉ Server IP:";
            this.lblRegServerIp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegServerIp.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblRegServerIp.Location = new System.Drawing.Point(0, 48);
            this.lblRegServerIp.AutoSize = true;

            this.txtRegIp.Location = new System.Drawing.Point(0, 68);
            this.txtRegIp.Size = new System.Drawing.Size(240, 26);
            this.txtRegIp.Text = "127.0.0.1";
            this.txtRegIp.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtRegIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegIp.ForeColor = System.Drawing.Color.White;

            this.lblRegPort.Text = "Port:";
            this.lblRegPort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegPort.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblRegPort.Location = new System.Drawing.Point(252, 48);
            this.lblRegPort.AutoSize = true;

            this.txtRegPort.Location = new System.Drawing.Point(252, 68);
            this.txtRegPort.Size = new System.Drawing.Size(75, 26);
            this.txtRegPort.Text = "8888";
            this.txtRegPort.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtRegPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegPort.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);

            this.lblRegPlayerName.Text = "2. Tên tài khoản muốn tạo (Username):";
            this.lblRegPlayerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegPlayerName.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblRegPlayerName.Location = new System.Drawing.Point(0, 98);
            this.lblRegPlayerName.AutoSize = true;

            this.txtRegName.Location = new System.Drawing.Point(0, 118);
            this.txtRegName.Size = new System.Drawing.Size(327, 26);
            this.txtRegName.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtRegName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegName.ForeColor = System.Drawing.Color.White;

            this.lblRegPassword.Text = "3. Mật khẩu (Password - tối thiểu 4 ký tự):";
            this.lblRegPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegPassword.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblRegPassword.Location = new System.Drawing.Point(0, 148);
            this.lblRegPassword.AutoSize = true;

            this.txtRegPassword.Location = new System.Drawing.Point(0, 168);
            this.txtRegPassword.Size = new System.Drawing.Size(327, 26);
            this.txtRegPassword.PasswordChar = '●';
            this.txtRegPassword.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtRegPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegPassword.ForeColor = System.Drawing.Color.White;

            this.lblRegConfirmPassword.Text = "4. Nhập lại mật khẩu (Xác nhận):";
            this.lblRegConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblRegConfirmPassword.Location = new System.Drawing.Point(0, 198);
            this.lblRegConfirmPassword.AutoSize = true;

            this.txtRegConfirmPassword.Location = new System.Drawing.Point(0, 218);
            this.txtRegConfirmPassword.Size = new System.Drawing.Size(327, 26);
            this.txtRegConfirmPassword.PasswordChar = '●';
            this.txtRegConfirmPassword.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtRegConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegConfirmPassword.ForeColor = System.Drawing.Color.White;

            this.lblRegStatusBadge.Text = "● Sẵn sàng tạo tài khoản";
            this.lblRegStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRegStatusBadge.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblRegStatusBadge.Location = new System.Drawing.Point(0, 250);
            this.lblRegStatusBadge.AutoSize = true;

            this.btnRegister.Location = new System.Drawing.Point(0, 274);
            this.btnRegister.Size = new System.Drawing.Size(327, 40);
            this.btnRegister.Text = "✨ TẠO TÀI KHOẢN & ĐĂNG NHẬP NGAY";
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRegister.IsPrimaryGradient = true;
            this.btnRegister.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            this.lblSwitchToLogin.Text = "👉 Đã có tài khoản rồi? Nhấn vào đây để Đăng nhập ➔";
            this.lblSwitchToLogin.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSwitchToLogin.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblSwitchToLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSwitchToLogin.Location = new System.Drawing.Point(0, 324);
            this.lblSwitchToLogin.AutoSize = true;
            this.lblSwitchToLogin.Click += (s, e) => SwitchAuthTab(true);

            // =============================================================
            // View 6: pnlHistoryView (Hồ sơ cá nhân & Lịch sử đấu)
            // =============================================================
            this.pnlHistoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistoryView.Padding = new System.Windows.Forms.Padding(16, 12, 16, 14);
            this.pnlHistoryView.Controls.Add(this.cardHistoryBox);
            this.pnlHistoryView.Controls.Add(this.pnlHistorySpacer);
            this.pnlHistoryView.Controls.Add(this.cardProfileSummary);

            // cardProfileSummary (Thẻ hồ sơ người chơi chi tiết)
            this.cardProfileSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardProfileSummary.Height = 105;
            this.cardProfileSummary.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.cardProfileSummary.FillColor = System.Drawing.Color.FromArgb(14, 25, 50);
            this.cardProfileSummary.BorderColor = System.Drawing.Color.FromArgb(24, 44, 84);
            this.cardProfileSummary.Controls.Add(this.lblProfileAvatar);
            this.cardProfileSummary.Controls.Add(this.lblProfileDetailName);
            this.cardProfileSummary.Controls.Add(this.lblProfileDetailRank);
            this.cardProfileSummary.Controls.Add(this.lblProfileDetailStatus);
            this.cardProfileSummary.Controls.Add(this.cardStatTotal);
            this.cardProfileSummary.Controls.Add(this.cardStatWins);
            this.cardProfileSummary.Controls.Add(this.cardStatLosses);
            this.cardProfileSummary.Controls.Add(this.cardStatWinRate);

            this.lblProfileAvatar.Location = new System.Drawing.Point(16, 14);
            this.lblProfileAvatar.Size = new System.Drawing.Size(56, 56);
            this.lblProfileAvatar.BackColor = System.Drawing.Color.FromArgb(20, 36, 75);
            this.lblProfileAvatar.ForeColor = System.Drawing.Color.FromArgb(0, 230, 255);
            this.lblProfileAvatar.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblProfileAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblProfileAvatar.Text = "👤";

            this.lblProfileDetailName.Location = new System.Drawing.Point(82, 12);
            this.lblProfileDetailName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblProfileDetailName.ForeColor = System.Drawing.Color.White;
            this.lblProfileDetailName.Text = "Khách (Offline)";
            this.lblProfileDetailName.AutoSize = true;

            this.lblProfileDetailRank.Location = new System.Drawing.Point(84, 40);
            this.lblProfileDetailRank.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblProfileDetailRank.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblProfileDetailRank.Text = "★ 1000 ELO — 🥉 Tập sự";
            this.lblProfileDetailRank.AutoSize = true;

            this.lblProfileDetailStatus.Location = new System.Drawing.Point(84, 62);
            this.lblProfileDetailStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblProfileDetailStatus.ForeColor = System.Drawing.Color.FromArgb(255, 145, 0);
            this.lblProfileDetailStatus.Text = "● Ngoại tuyến (Khách)";
            this.lblProfileDetailStatus.AutoSize = true;

            // Stats cards
            this.cardStatTotal.Location = new System.Drawing.Point(365, 14);
            this.cardStatTotal.Size = new System.Drawing.Size(82, 60);
            this.cardStatTotal.FillColor = System.Drawing.Color.FromArgb(10, 18, 38);
            this.cardStatTotal.BorderColor = System.Drawing.Color.FromArgb(24, 40, 75);
            this.cardStatTotal.Controls.Add(this.lblStatTotalVal);
            this.cardStatTotal.Controls.Add(this.lblStatTotalTitle);

            this.lblStatTotalVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTotalVal.Height = 36;
            this.lblStatTotalVal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblStatTotalVal.ForeColor = System.Drawing.Color.White;
            this.lblStatTotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatTotalVal.Text = "0";

            this.lblStatTotalTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatTotalTitle.Height = 18;
            this.lblStatTotalTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblStatTotalTitle.ForeColor = System.Drawing.Color.FromArgb(160, 180, 210);
            this.lblStatTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatTotalTitle.Text = "Tổng trận";

            this.cardStatWins.Location = new System.Drawing.Point(455, 14);
            this.cardStatWins.Size = new System.Drawing.Size(82, 60);
            this.cardStatWins.FillColor = System.Drawing.Color.FromArgb(10, 18, 38);
            this.cardStatWins.BorderColor = System.Drawing.Color.FromArgb(24, 40, 75);
            this.cardStatWins.Controls.Add(this.lblStatWinsVal);
            this.cardStatWins.Controls.Add(this.lblStatWinsTitle);

            this.lblStatWinsVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatWinsVal.Height = 36;
            this.lblStatWinsVal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblStatWinsVal.ForeColor = System.Drawing.Color.FromArgb(0, 230, 118);
            this.lblStatWinsVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatWinsVal.Text = "0";

            this.lblStatWinsTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatWinsTitle.Height = 18;
            this.lblStatWinsTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblStatWinsTitle.ForeColor = System.Drawing.Color.FromArgb(160, 180, 210);
            this.lblStatWinsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatWinsTitle.Text = "Thắng";

            this.cardStatLosses.Location = new System.Drawing.Point(545, 14);
            this.cardStatLosses.Size = new System.Drawing.Size(82, 60);
            this.cardStatLosses.FillColor = System.Drawing.Color.FromArgb(10, 18, 38);
            this.cardStatLosses.BorderColor = System.Drawing.Color.FromArgb(24, 40, 75);
            this.cardStatLosses.Controls.Add(this.lblStatLossesVal);
            this.cardStatLosses.Controls.Add(this.lblStatLossesTitle);

            this.lblStatLossesVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatLossesVal.Height = 36;
            this.lblStatLossesVal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblStatLossesVal.ForeColor = System.Drawing.Color.FromArgb(255, 75, 75);
            this.lblStatLossesVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatLossesVal.Text = "0";

            this.lblStatLossesTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatLossesTitle.Height = 18;
            this.lblStatLossesTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblStatLossesTitle.ForeColor = System.Drawing.Color.FromArgb(160, 180, 210);
            this.lblStatLossesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatLossesTitle.Text = "Thua";

            this.cardStatWinRate.Location = new System.Drawing.Point(635, 14);
            this.cardStatWinRate.Size = new System.Drawing.Size(82, 60);
            this.cardStatWinRate.FillColor = System.Drawing.Color.FromArgb(10, 18, 38);
            this.cardStatWinRate.BorderColor = System.Drawing.Color.FromArgb(24, 40, 75);
            this.cardStatWinRate.Controls.Add(this.lblStatWinRateVal);
            this.cardStatWinRate.Controls.Add(this.lblStatWinRateTitle);

            this.lblStatWinRateVal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatWinRateVal.Height = 36;
            this.lblStatWinRateVal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblStatWinRateVal.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblStatWinRateVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatWinRateVal.Text = "0%";

            this.lblStatWinRateTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatWinRateTitle.Height = 18;
            this.lblStatWinRateTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblStatWinRateTitle.ForeColor = System.Drawing.Color.FromArgb(160, 180, 210);
            this.lblStatWinRateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatWinRateTitle.Text = "Tỉ lệ";

            // pnlHistorySpacer
            this.pnlHistorySpacer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHistorySpacer.Height = 10;
            this.pnlHistorySpacer.BackColor = System.Drawing.Color.Transparent;
            this.pnlHistoryView.Controls.Add(this.cardHistoryBox);

            this.cardHistoryBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardHistoryBox.Padding = new System.Windows.Forms.Padding(16, 12, 16, 14);
            this.cardHistoryBox.Controls.Add(this.lblNoHistory);
            this.cardHistoryBox.Controls.Add(this.dgvMatchHistory);
            this.cardHistoryBox.Controls.Add(this.pnlHistoryHeaderBar);

            this.pnlHistoryHeaderBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHistoryHeaderBar.Height = 40;
            this.pnlHistoryHeaderBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlHistoryHeaderBar.Controls.Add(this.btnRefreshHistory);
            this.pnlHistoryHeaderBar.Controls.Add(this.lblHistoryHeader);

            this.lblHistoryHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHistoryHeader.Text = "LỊCH SỬ THI ĐẤU";
            this.lblHistoryHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryHeader.ForeColor = System.Drawing.Color.White;
            this.lblHistoryHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHistoryHeader.AutoSize = true;

            this.btnRefreshHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefreshHistory.Width = 95;
            this.btnRefreshHistory.Text = "🔄 Làm mới";
            this.btnRefreshHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshHistory.FlatAppearance.BorderSize = 0;
            this.btnRefreshHistory.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRefreshHistory.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.btnRefreshHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshHistory.Click += new System.EventHandler(this.btnRefreshHistory_Click);

            this.dgvMatchHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatchHistory.BackgroundColor = System.Drawing.Color.FromArgb(12, 22, 44);
            this.dgvMatchHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMatchHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMatchHistory.GridColor = System.Drawing.Color.FromArgb(20, 36, 70);
            this.dgvMatchHistory.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(12, 22, 44);
            this.dgvMatchHistory.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMatchHistory.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(20, 45, 90);
            this.dgvMatchHistory.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvMatchHistory.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(14, 28, 56);
            this.dgvMatchHistory.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(160, 180, 215);
            this.dgvMatchHistory.EnableHeadersVisualStyles = false;
            this.dgvMatchHistory.ReadOnly = true;
            this.dgvMatchHistory.RowHeadersVisible = false;
            this.dgvMatchHistory.AllowUserToAddRows = false;
            this.dgvMatchHistory.AllowUserToDeleteRows = false;
            this.dgvMatchHistory.AllowUserToResizeRows = false;
            this.dgvMatchHistory.RowTemplate.Height = 36;
            this.dgvMatchHistory.ColumnHeadersHeight = 32;
            this.dgvMatchHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.lblNoHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoHistory.Text = "Bạn chưa có trận đấu nào được lưu trữ.\nHãy tham gia phòng PvP để lưu lại lịch sử thi đấu!";
            this.lblNoHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoHistory.ForeColor = System.Drawing.Color.FromArgb(120, 140, 175);
            this.lblNoHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblNoHistory.Visible = false;

            // =============================================================
            // View 7: pnlLeaderboardView (Bảng xếp hạng)
            // =============================================================
            this.pnlLeaderboardView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeaderboardView.Padding = new System.Windows.Forms.Padding(20);
            this.pnlLeaderboardView.Controls.Add(this.cardLeaderboardBox);

            this.cardLeaderboardBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLeaderboardBox.Padding = new System.Windows.Forms.Padding(16, 12, 16, 14);
            this.cardLeaderboardBox.Controls.Add(this.lblNoLeaderboard);
            this.cardLeaderboardBox.Controls.Add(this.dgvLeaderboard);
            this.cardLeaderboardBox.Controls.Add(this.pnlLeaderboardHeaderBar);

            this.pnlLeaderboardHeaderBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLeaderboardHeaderBar.Height = 40;
            this.pnlLeaderboardHeaderBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeaderboardHeaderBar.Controls.Add(this.btnRefreshLeaderboard);
            this.pnlLeaderboardHeaderBar.Controls.Add(this.lblLeaderboardHeader);

            this.lblLeaderboardHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeaderboardHeader.Text = "BẢNG XẾP HẠNG CAO THỦ (TOP ELO)";
            this.lblLeaderboardHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLeaderboardHeader.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblLeaderboardHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLeaderboardHeader.AutoSize = true;

            this.btnRefreshLeaderboard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefreshLeaderboard.Width = 95;
            this.btnRefreshLeaderboard.Text = "🔄 Làm mới";
            this.btnRefreshLeaderboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshLeaderboard.FlatAppearance.BorderSize = 0;
            this.btnRefreshLeaderboard.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRefreshLeaderboard.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.btnRefreshLeaderboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshLeaderboard.Click += new System.EventHandler(this.btnRefreshLeaderboard_Click);

            this.dgvLeaderboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeaderboard.BackgroundColor = System.Drawing.Color.FromArgb(12, 22, 44);
            this.dgvLeaderboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLeaderboard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLeaderboard.GridColor = System.Drawing.Color.FromArgb(20, 36, 70);
            this.dgvLeaderboard.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(12, 22, 44);
            this.dgvLeaderboard.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvLeaderboard.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(20, 45, 90);
            this.dgvLeaderboard.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvLeaderboard.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(14, 28, 56);
            this.dgvLeaderboard.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(160, 180, 215);
            this.dgvLeaderboard.EnableHeadersVisualStyles = false;
            this.dgvLeaderboard.ReadOnly = true;
            this.dgvLeaderboard.RowHeadersVisible = false;
            this.dgvLeaderboard.AllowUserToAddRows = false;
            this.dgvLeaderboard.AllowUserToDeleteRows = false;
            this.dgvLeaderboard.AllowUserToResizeRows = false;
            this.dgvLeaderboard.RowTemplate.Height = 36;
            this.dgvLeaderboard.ColumnHeadersHeight = 32;
            this.dgvLeaderboard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.lblNoLeaderboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoLeaderboard.Text = "Chưa có dữ liệu bảng xếp hạng.";
            this.lblNoLeaderboard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoLeaderboard.ForeColor = System.Drawing.Color.FromArgb(120, 140, 175);
            this.lblNoLeaderboard.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblNoLeaderboard.Visible = false;

            // -------------------------------------------------------------
            // Main Form Settings
            // -------------------------------------------------------------
            this.ClientSize = new System.Drawing.Size(1260, 780);
            this.MinimumSize = new System.Drawing.Size(1080, 680);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(8, 13, 26);
            this.ForeColor = System.Drawing.Color.White;
            this.Text = "CaroKan - Chơi cờ qua mạng LAN";

            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlRightBar);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlTitleBar);

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
        }

        #endregion

        // Layout Containers
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Panel pnlLogoBox;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Panel pnlWindowButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnMin;

        // Sidebar
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlNavList;
        private CaroClient.CyberButton btnNavHome;
        private CaroClient.CyberButton btnNavGame;
        private CaroClient.CyberButton btnNavCreate;
        private CaroClient.CyberButton btnNavJoin;
        private CaroClient.CyberButton btnNavHistory;
        private CaroClient.CyberButton btnNavLeaderboard;
        private CaroClient.CyberButton btnNavSettings;
        private CaroClient.RoundedCard pnlProfileCard;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Label lblProfileLevel;
        private System.Windows.Forms.Label lblProfileXp;
        private CaroClient.CyberButton btnLogout;

        // Right Panel
        private System.Windows.Forms.Panel pnlRightBar;
        private CaroClient.RoundedCard cardChat;
        private System.Windows.Forms.Label lblChatTitle;
        private System.Windows.Forms.RichTextBox rtbChatLog;
        private System.Windows.Forms.Panel pnlChatBottom;
        private System.Windows.Forms.TextBox txtChatInput;
        private CaroClient.CyberButton btnSendChat;

        // Main Content Area
        private System.Windows.Forms.Panel pnlMainContent;

        // View 1: Home
        private System.Windows.Forms.Panel pnlHomeView;
        private CaroClient.HeroBannerPanel heroBanner;
        private System.Windows.Forms.Label lblHeroTitle;
        private System.Windows.Forms.Label lblHeroSubtitle;

        private System.Windows.Forms.Panel pnlMiddleGrid;
        private CaroClient.RoundedCard cardFeaturedRooms;
        private System.Windows.Forms.Panel pnlRoomsHeaderBar;
        private System.Windows.Forms.Label lblRoomsHeader;
        private System.Windows.Forms.Button btnRefreshRooms;
        private System.Windows.Forms.DataGridView dgvFeaturedRooms;
        private System.Windows.Forms.Label lblNoRooms;

        private CaroClient.CyberButton btnHomeCreateRoom;
        private CaroClient.CyberButton btnHomeJoinRoom;
        private CaroClient.CyberButton btnHomeSettings;
        private System.Windows.Forms.Panel pnlServerInfoContainer;
        private CaroClient.RoundedCard cardServerInfo;
        private System.Windows.Forms.Label lblHomeServerHeader;
        private System.Windows.Forms.Label lblHomeServerStatus;
        private System.Windows.Forms.Label lblHomeServerHost;
        private System.Windows.Forms.Label lblHomeServerIp;
        private System.Windows.Forms.Panel pnlPlayersContainer;
        private CaroClient.RoundedCard cardConnectedPlayers;
        private System.Windows.Forms.Label lblHomePlayersHeader;
        private System.Windows.Forms.ListBox lstConnectedPlayers;
        private System.Windows.Forms.Label lblNoPlayers;

        // View 2: Game Board
        private System.Windows.Forms.Panel pnlGameView;
        private CaroClient.RoundedCard pnlGameHeader;
        private System.Windows.Forms.Panel pnlGameActions;
        private CaroClient.CyberButton btnSurrender;
        private CaroClient.CyberButton btnRestart;
        private CaroClient.CyberButton btnLeaveRoom;
        private System.Windows.Forms.Panel pnlGameInfo;
        private System.Windows.Forms.Label lblRoomIdInfo;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblPlayersInfo;
        private System.Windows.Forms.Label lblTurnInfo;
        private CaroClient.BoardControl boardControl1;

        // View 3: Create Room
        private System.Windows.Forms.Panel pnlCreateRoomView;
        private CaroClient.RoundedCard cardCreateBox;
        private System.Windows.Forms.Panel pnlCreateHeader;
        private CaroClient.CircleBadge badgeCreatePlus;
        private System.Windows.Forms.Label lblCreateTitle;
        private System.Windows.Forms.Label lblCreateSubtitle;
        private CaroClient.SectionHeaderLabel secHeaderMode;
        private CaroClient.ModeSelectionCard cardModePvP;
        private CaroClient.ModeSelectionCard cardModeAI;
        private CaroClient.SectionHeaderLabel secHeaderTime;
        private CaroClient.ModeSelectionCard cardTimeTurn;
        private CaroClient.ModeSelectionCard cardTimeFree;
        private System.Windows.Forms.Panel pnlTimeLimitOptions;
        private System.Windows.Forms.Label lblTimePillLabel;
        private CaroClient.OptionPill pillTime15;
        private CaroClient.OptionPill pillTime30;
        private CaroClient.OptionPill pillTime60;
        private System.Windows.Forms.Panel pnlAiDifficultyOptions;
        private System.Windows.Forms.Label lblAiDifficultyLabel;
        private CaroClient.OptionPill pillAiEasy;
        private CaroClient.OptionPill pillAiMedium;
        private CaroClient.OptionPill pillAiHard;
        private CaroClient.SectionHeaderLabel secHeaderRule;
        private CaroClient.OptionPill pillRuleStandard;
        private CaroClient.OptionPill pillRuleFree;
        private CaroClient.CyberButton btnCreateRoom;
        private CaroClient.CyberButton btnCancelCreate;

        // View 4: Join Room
        private System.Windows.Forms.Panel pnlJoinRoomView;
        private CaroClient.RoundedCard cardJoinTopBar;
        private System.Windows.Forms.Panel pnlJoinActions;
        private System.Windows.Forms.Label lblJoinTitle;
        private System.Windows.Forms.Label lblJoinSubtitle;
        private System.Windows.Forms.Label lblRoomIdPrompt;
        private System.Windows.Forms.TextBox txtRoomId;
        private CaroClient.CyberButton btnJoinRoom;
        private CaroClient.CyberButton btnCancelJoin;

        // View 5: Settings / Login
        private System.Windows.Forms.Panel pnlSettingsView;
        private CaroClient.RoundedCard cardSettingsBox;
        private CaroClient.CyberButton btnTabLogin;
        private CaroClient.CyberButton btnTabRegister;
        private System.Windows.Forms.Panel pnlAuthBodyLogin;
        private System.Windows.Forms.Panel pnlAuthBodyRegister;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Label lblSettingsNotice;
        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblStatusBadge;
        private CaroClient.CyberButton btnConnect;
        private System.Windows.Forms.Label lblSwitchToRegister;
        private System.Windows.Forms.Label lblOfflineTip;

        // Register Tab Controls
        private System.Windows.Forms.Label lblRegTitle;
        private System.Windows.Forms.Label lblRegNotice;
        private System.Windows.Forms.Label lblRegServerIp;
        private System.Windows.Forms.TextBox txtRegIp;
        private System.Windows.Forms.Label lblRegPort;
        private System.Windows.Forms.TextBox txtRegPort;
        private System.Windows.Forms.Label lblRegPlayerName;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.Label lblRegPassword;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.Label lblRegConfirmPassword;
        private System.Windows.Forms.TextBox txtRegConfirmPassword;
        private System.Windows.Forms.Label lblRegStatusBadge;
        private CaroClient.CyberButton btnRegister;
        private System.Windows.Forms.Label lblSwitchToLogin;

        // Timer
        private System.Windows.Forms.Timer turnTimer;

        // View 6: Profile & Match History
        private System.Windows.Forms.Panel pnlHistoryView;
        private CaroClient.RoundedCard cardProfileSummary;
        private System.Windows.Forms.Label lblProfileAvatar;
        private System.Windows.Forms.Label lblProfileDetailName;
        private System.Windows.Forms.Label lblProfileDetailRank;
        private System.Windows.Forms.Label lblProfileDetailStatus;
        private CaroClient.RoundedCard cardStatTotal;
        private System.Windows.Forms.Label lblStatTotalVal;
        private System.Windows.Forms.Label lblStatTotalTitle;
        private CaroClient.RoundedCard cardStatWins;
        private System.Windows.Forms.Label lblStatWinsVal;
        private System.Windows.Forms.Label lblStatWinsTitle;
        private CaroClient.RoundedCard cardStatLosses;
        private System.Windows.Forms.Label lblStatLossesVal;
        private System.Windows.Forms.Label lblStatLossesTitle;
        private CaroClient.RoundedCard cardStatWinRate;
        private System.Windows.Forms.Label lblStatWinRateVal;
        private System.Windows.Forms.Label lblStatWinRateTitle;
        private System.Windows.Forms.Panel pnlHistorySpacer;
        private CaroClient.RoundedCard cardHistoryBox;
        private System.Windows.Forms.Panel pnlHistoryHeaderBar;
        private System.Windows.Forms.Label lblHistoryHeader;
        private System.Windows.Forms.Button btnRefreshHistory;
        private System.Windows.Forms.DataGridView dgvMatchHistory;
        private System.Windows.Forms.Label lblNoHistory;

        // Sidebar Bottom Panel
        private System.Windows.Forms.Panel pnlSidebarBottom;

        // View 7: Leaderboard
        private System.Windows.Forms.Panel pnlLeaderboardView;
        private CaroClient.RoundedCard cardLeaderboardBox;
        private System.Windows.Forms.Panel pnlLeaderboardHeaderBar;
        private System.Windows.Forms.Label lblLeaderboardHeader;
        private System.Windows.Forms.Button btnRefreshLeaderboard;
        private System.Windows.Forms.DataGridView dgvLeaderboard;
        private System.Windows.Forms.Label lblNoLeaderboard;
    }
}
