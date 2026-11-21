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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pnlTitleBar = new Panel();
            pnlLogoBox = new Panel();
            lblAppTitle = new Label();
            lblAppSubtitle = new Label();
            pnlWindowButtons = new Panel();
            btnMin = new Button();
            btnMax = new Button();
            btnClose = new Button();
            pnlSidebar = new Panel();
            pnlNavList = new Panel();
            btnNavHome = new CyberButton();
            btnNavGame = new CyberButton();
            btnNavCreate = new CyberButton();
            btnNavJoin = new CyberButton();
            btnNavLeaderboard = new CyberButton();
            btnNavHistory = new CyberButton();
            pnlSidebarBottom = new Panel();
            btnNavSettings = new CyberButton();
            pnlProfileCard = new RoundedCard();
            lblProfileName = new Label();
            lblProfileLevel = new Label();
            lblProfileXp = new Label();
            btnLogout = new CyberButton();
            pnlRightBar = new Panel();
            cardChat = new RoundedCard();
            rtbChatLog = new RichTextBox();
            pnlChatBottom = new Panel();
            txtChatInput = new TextBox();
            btnSendChat = new CyberButton();
            lblChatTitle = new Label();
            pnlMainContent = new Panel();
            pnlHomeView = new Panel();
            heroBanner = new HeroBannerPanel();
            lblHeroTitle = new Label();
            lblHeroSubtitle = new Label();
            pnlMiddleGrid = new Panel();

            // View 1: 3 Column Panels & Controls
            colEvent = new Panel();
            lblEventHeader = new Label();
            cardEvent = new HomeEventCard();

            colTopPlayers = new Panel();
            lblTopPlayersHeader = new Label();
            cardTopPlayers = new HomePodiumPanel();

            colOnlinePlayers = new Panel();
            cardConnectedPlayers = new RoundedCard();
            pnlOnlineHeaderBar = new Panel();
            lblHomePlayersHeader = new Label();
            lblOnlineCountBadge = new Label();
            pnlOnlineSearchBar = new Panel();
            pnlSearchFriendWrapper = new Panel();
            txtSearchFriend = new TextBox();
            lstConnectedPlayers = new ListBox();
            lblNoPlayers = new Label();

            // Legacy dummy controls for backward compatibility
            pnlPlayersContainer = new Panel();
            pnlServerInfoContainer = new Panel();
            cardServerInfo = new RoundedCard();
            btnHomeSettings = new CyberButton();
            btnHomeJoinRoom = new CyberButton();
            btnHomeCreateRoom = new CyberButton();
            lblHomeServerIp = new Label();
            lblHomeServerHost = new Label();
            lblHomeServerStatus = new Label();
            lblHomeServerHeader = new Label();
            pnlGameView = new GameArenaBackgroundPanel();
            gameOverModal = new GameOverModal();
            boardControl1 = new BoardControl();
            pnlGameHeader = new RoundedCard();
            pnlGameActions = new Panel();
            btnSurrender = new CyberButton();
            btnRestart = new CyberButton();
            btnLeaveRoom = new CyberButton();
            timerWidget = new CyberTimerWidget();
            pnlGameInfo = new Panel();
            lblRoomIdInfo = new Label();
            lblPlayersInfo = new Label();
            lblTurnInfo = new Label();
            pnlCreateRoomView = new CreateRoomBackgroundPanel();
            cardCreateBox = new RoundedCard();
            btnCancelCreate = new CyberButton();
            btnCreateRoom = new CyberButton();
            pillRuleFree = new OptionPill();
            pillRuleStandard = new OptionPill();
            secHeaderRule = new SectionHeaderLabel();
            pnlAiDifficultyOptions = new Panel();
            lblAiDifficultyLabel = new Label();
            pillAiEasy = new OptionPill();
            pillAiMedium = new OptionPill();
            pillAiHard = new OptionPill();
            pnlTimeLimitOptions = new Panel();
            lblTimePillLabel = new Label();
            pillTime15 = new OptionPill();
            pillTime30 = new OptionPill();
            pillTime60 = new OptionPill();
            pillCountdown1m = new OptionPill();
            pillCountdown3m = new OptionPill();
            pillCountdown5m = new OptionPill();
            pillCountdownCustom = new OptionPill();
            txtCustomMinutes = new TextBox();
            lblCustomMinutesUnit = new Label();
            cardTimeFree = new ModeSelectionCard();
            cardTimeTurn = new ModeSelectionCard();
            secHeaderTime = new SectionHeaderLabel();
            cardModeAI = new ModeSelectionCard();
            cardModePvP = new ModeSelectionCard();
            secHeaderMode = new SectionHeaderLabel();
            pnlCreateHeader = new Panel();
            badgeCreatePlus = new CircleBadge();
            lblCreateTitle = new Label();
            lblCreateSubtitle = new Label();
            pnlJoinRoomView = new Panel();
            lblLobbyLastUpdated = new Label();
            pnlLobbyBody = new Panel();
            pnlLobbyRightCol = new Panel();
            cardFeaturedRoom = new RoundedCard();
            btnFeaturedAction = new CyberButton();
            lblFeaturedStatus = new Label();
            lblFeaturedMode = new Label();
            lblFeaturedHost = new Label();
            lblFeaturedRoomName = new Label();
            miniBoardPreview = new MiniBoardPreviewControl();
            lblViewAllRooms = new Label();
            lblFeaturedTitle = new Label();
            cardJoinByCode = new RoundedCard();
            lblCodeFootnote = new Label();
            btnJoinRoom = new CyberButton();
            pnlCodeWrapper = new RoundedCard();
            txtRoomId = new TextBox();
            lblCodeHash = new Label();
            lblJoinByCodeTitle = new Label();
            cardOpenRooms = new RoundedCard();
            pnlOpenRoomsScroll = new Panel();
            lblNoRooms = new Label();
            pnlOpenRoomsHeader = new Panel();
            lblOpenRoomsCount = new Label();
            lblOpenRoomsTitle = new Label();
            pnlLobbyFilterBar = new Panel();
            btnRefreshLobby = new CyberButton();
            pnlFilterPills = new Panel();
            pillFilterWaiting = new LobbyFilterPill();
            pillFilter4p = new LobbyFilterPill();
            pillFilter2p = new LobbyFilterPill();
            pillFilterAll = new LobbyFilterPill();
            pnlSearchBox = new RoundedCard();
            txtSearchRoom = new TextBox();
            lblSearchIcon = new VectorIconLabel();
            pnlLobbyHeaderBar = new LobbyBannerPanel();
            lblLobbySubtitle = new Label();
            lblLobbyTitle = new Label();
            pnlLobbyIconBox = new LobbyHeaderIconBox();
            pnlHistoryView = new Panel();
            cardHistoryBox = new RoundedCard();
            lblNoHistory = new Label();
            dgvMatchHistory = new DataGridView();
            pnlHistoryHeaderBar = new Panel();
            btnRefreshHistory = new Button();
            lblHistoryHeader = new Label();
            pnlHistorySpacer2 = new Panel();
            pnlMiddleRow = new Panel();
            cardAchievements = new RoundedCard();
            lblAchievementsHeader = new Label();
            badgeTanThu = new AchievementBadgeControl();
            badgeChienBinh = new AchievementBadgeControl();
            badgeCaoThu = new AchievementBadgeControl();
            badgeLocked = new AchievementBadgeControl();
            pnlMiddleSpacer = new Panel();
            cardQuickStats = new RoundedCard();
            lblQuickStatsHeader = new Label();
            quickStatTotal = new QuickStatItem();
            quickStatWins = new QuickStatItem();
            quickStatElo = new QuickStatItem();
            pnlHistorySpacer1 = new Panel();
            cardProfileHero = new RoundedCard();
            avatarControl = new CircularAvatarControl();
            lblProfileDetailName = new Label();
            lblEditNameIcon = new VectorIconLabel();
            lblProfileDetailElo = new Label();
            lblProfileDetailRank = new Label();
            lblProfileDetailStatus = new Label();
            kpiTotal = new KpiStatBox();
            kpiWins = new KpiStatBox();
            kpiLosses = new KpiStatBox();
            gaugeWinRate = new WinRateDonutGauge();
            pnlLeaderboardView = new Panel();
            cardLeaderboardBox = new RoundedCard();
            lblNoLeaderboard = new Label();
            dgvLeaderboard = new DataGridView();
            pnlLeaderboardHeaderBar = new Panel();
            btnRefreshLeaderboard = new Button();
            lblLeaderboardHeader = new Label();
            leaderboardPodium = new LeaderboardPodiumControl();
            pnlLeaderboardSubHeader = new Panel();
            lblLeaderboardSubHeader = new Label();
            btnRefreshLeaderboardList = new Button();
            leaderboardList = new LeaderboardListControl();
            pnlSettingsView = new SettingsBackgroundPanel();
            cardSettingsBox = new CyberLoginCard();
            btnTabLogin = new CyberButton();
            btnTabRegister = new CyberButton();
            pnlAuthBodyLogin = new Panel();
            lblSettingsTitle = new Label();
            lblSettingsNotice = new Label();
            lblServerIp = new Label();
            lblPort = new Label();
            pnlIpWrapper = new RoundedCard();
            lblIpPrefix = new Label();
            txtIp = new TextBox();
            pnlPortWrapper = new RoundedCard();
            txtPort = new TextBox();
            pnlUserWrapper = new RoundedCard();
            lblUserPrefix = new VectorIconLabel();
            txtName = new TextBox();
            pnlPassWrapper = new RoundedCard();
            lblPassPrefix = new VectorIconLabel();
            txtPassword = new TextBox();
            lblTogglePassword = new VectorIconLabel();
            lblStatusBadge = new Label();
            btnConnect = new CyberButton();
            lblSwitchToRegister = new Label();
            lblOfflineTip = new Label();
            pnlAuthBodyRegister = new Panel();
            lblRegTitle = new Label();
            lblRegNotice = new Label();
            lblRegServerIp = new Label();
            lblRegPort = new Label();
            pnlRegIpWrapper = new RoundedCard();
            lblRegIpPrefix = new Label();
            txtRegIp = new TextBox();
            pnlRegPortWrapper = new RoundedCard();
            txtRegPort = new TextBox();
            pnlRegUserWrapper = new RoundedCard();
            lblRegUserPrefix = new VectorIconLabel();
            txtRegName = new TextBox();
            pnlRegPassWrapper = new RoundedCard();
            lblRegPassPrefix = new VectorIconLabel();
            txtRegPassword = new TextBox();
            lblToggleRegPass = new VectorIconLabel();
            pnlRegConfirmPassWrapper = new RoundedCard();
            lblRegConfirmPassPrefix = new VectorIconLabel();
            txtRegConfirmPassword = new TextBox();
            lblToggleRegConfirm = new VectorIconLabel();
            lblRegStatusBadge = new Label();
            btnRegister = new CyberButton();
            lblSwitchToLogin = new Label();
            lblRegOfflineTip = new Label();
            lblTimer = new Label();
            lblPlayerName = new Label();
            lblPassword = new Label();
            lblRegPlayerName = new Label();
            lblRegPassword = new Label();
            lblRegConfirmPassword = new Label();
            turnTimer = new System.Windows.Forms.Timer(components);
            pnlTitleBar.SuspendLayout();
            pnlWindowButtons.SuspendLayout();
            pnlSidebar.SuspendLayout();
            pnlNavList.SuspendLayout();
            pnlSidebarBottom.SuspendLayout();
            pnlProfileCard.SuspendLayout();
            pnlRightBar.SuspendLayout();
            cardChat.SuspendLayout();
            pnlChatBottom.SuspendLayout();
            pnlMainContent.SuspendLayout();
            pnlHomeView.SuspendLayout();
            pnlMiddleGrid.SuspendLayout();
            colEvent.SuspendLayout();
            colTopPlayers.SuspendLayout();
            colOnlinePlayers.SuspendLayout();
            cardConnectedPlayers.SuspendLayout();
            pnlOnlineHeaderBar.SuspendLayout();
            pnlOnlineSearchBar.SuspendLayout();
            pnlSearchFriendWrapper.SuspendLayout();
            heroBanner.SuspendLayout();
            pnlGameView.SuspendLayout();
            pnlGameHeader.SuspendLayout();
            pnlGameActions.SuspendLayout();
            pnlGameInfo.SuspendLayout();
            pnlCreateRoomView.SuspendLayout();
            cardCreateBox.SuspendLayout();
            pnlAiDifficultyOptions.SuspendLayout();
            pnlTimeLimitOptions.SuspendLayout();
            pnlCreateHeader.SuspendLayout();
            pnlJoinRoomView.SuspendLayout();
            pnlLobbyBody.SuspendLayout();
            pnlLobbyRightCol.SuspendLayout();
            cardFeaturedRoom.SuspendLayout();
            cardJoinByCode.SuspendLayout();
            pnlCodeWrapper.SuspendLayout();
            cardOpenRooms.SuspendLayout();
            pnlOpenRoomsScroll.SuspendLayout();
            pnlOpenRoomsHeader.SuspendLayout();
            pnlLobbyFilterBar.SuspendLayout();
            pnlFilterPills.SuspendLayout();
            pnlSearchBox.SuspendLayout();
            pnlLobbyHeaderBar.SuspendLayout();
            pnlHistoryView.SuspendLayout();
            cardHistoryBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatchHistory).BeginInit();
            pnlHistoryHeaderBar.SuspendLayout();
            pnlMiddleRow.SuspendLayout();
            cardAchievements.SuspendLayout();
            cardQuickStats.SuspendLayout();
            cardProfileHero.SuspendLayout();
            pnlLeaderboardView.SuspendLayout();
            cardLeaderboardBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).BeginInit();
            pnlLeaderboardHeaderBar.SuspendLayout();
            pnlLeaderboardSubHeader.SuspendLayout();
            pnlSettingsView.SuspendLayout();
            cardSettingsBox.SuspendLayout();
            pnlAuthBodyLogin.SuspendLayout();
            pnlIpWrapper.SuspendLayout();
            pnlPortWrapper.SuspendLayout();
            pnlUserWrapper.SuspendLayout();
            pnlPassWrapper.SuspendLayout();
            pnlAuthBodyRegister.SuspendLayout();
            pnlRegIpWrapper.SuspendLayout();
            pnlRegPortWrapper.SuspendLayout();
            pnlRegUserWrapper.SuspendLayout();
            pnlRegPassWrapper.SuspendLayout();
            pnlRegConfirmPassWrapper.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitleBar
            // 
            pnlTitleBar.BackColor = Color.FromArgb(7, 12, 24);
            pnlTitleBar.Controls.Add(pnlLogoBox);
            pnlTitleBar.Controls.Add(lblAppTitle);
            pnlTitleBar.Controls.Add(lblAppSubtitle);
            pnlTitleBar.Controls.Add(pnlWindowButtons);
            pnlTitleBar.Dock = DockStyle.Top;
            pnlTitleBar.Location = new Point(0, 0);
            pnlTitleBar.Name = "pnlTitleBar";
            pnlTitleBar.Size = new Size(1260, 48);
            pnlTitleBar.TabIndex = 3;
            pnlTitleBar.MouseDown += TitleBar_MouseDown;
            // 
            // pnlLogoBox
            // 
            pnlLogoBox.Location = new Point(12, 8);
            pnlLogoBox.Name = "pnlLogoBox";
            pnlLogoBox.Size = new Size(32, 32);
            pnlLogoBox.TabIndex = 0;
            pnlLogoBox.Paint += pnlLogoBox_Paint;
            // 
            // lblAppTitle
            // 
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(50, 6);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(91, 28);
            lblAppTitle.TabIndex = 1;
            lblAppTitle.Text = "CaroKan";
            lblAppTitle.MouseDown += TitleBar_MouseDown;
            // 
            // lblAppSubtitle
            // 
            lblAppSubtitle.AutoSize = true;
            lblAppSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblAppSubtitle.ForeColor = Color.FromArgb(130, 150, 185);
            lblAppSubtitle.Location = new Point(51, 27);
            lblAppSubtitle.Name = "lblAppSubtitle";
            lblAppSubtitle.Size = new Size(283, 20);
            lblAppSubtitle.TabIndex = 2;
            lblAppSubtitle.Text = "Kết nối bạn bè - Thắng trong từng ván cờ";
            lblAppSubtitle.MouseDown += TitleBar_MouseDown;
            // 
            // pnlWindowButtons
            // 
            pnlWindowButtons.BackColor = Color.Transparent;
            pnlWindowButtons.Controls.Add(btnMin);
            pnlWindowButtons.Controls.Add(btnMax);
            pnlWindowButtons.Controls.Add(btnClose);
            pnlWindowButtons.Dock = DockStyle.Right;
            pnlWindowButtons.Location = new Point(1125, 0);
            pnlWindowButtons.Name = "pnlWindowButtons";
            pnlWindowButtons.Size = new Size(135, 48);
            pnlWindowButtons.TabIndex = 3;
            // 
            // btnMin
            // 
            btnMin.BackColor = Color.FromArgb(16, 26, 50);
            btnMin.Cursor = Cursors.Hand;
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.FlatStyle = FlatStyle.Flat;
            btnMin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMin.ForeColor = Color.FromArgb(200, 215, 240);
            btnMin.Location = new Point(4, 9);
            btnMin.Name = "btnMin";
            btnMin.Size = new Size(38, 30);
            btnMin.TabIndex = 0;
            btnMin.Text = "—";
            btnMin.UseVisualStyleBackColor = false;
            btnMin.Click += btnMin_Click;
            // 
            // btnMax
            // 
            btnMax.BackColor = Color.FromArgb(16, 26, 50);
            btnMax.Cursor = Cursors.Hand;
            btnMax.FlatAppearance.BorderSize = 0;
            btnMax.FlatStyle = FlatStyle.Flat;
            btnMax.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMax.ForeColor = Color.FromArgb(200, 215, 240);
            btnMax.Location = new Point(46, 9);
            btnMax.Name = "btnMax";
            btnMax.Size = new Size(38, 30);
            btnMax.TabIndex = 1;
            btnMax.Text = "▢";
            btnMax.UseVisualStyleBackColor = false;
            btnMax.Click += btnMax_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(16, 26, 50);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.ForeColor = Color.FromArgb(200, 215, 240);
            btnClose.Location = new Point(88, 9);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(38, 30);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(9, 15, 30);
            pnlSidebar.Controls.Add(pnlNavList);
            pnlSidebar.Controls.Add(pnlSidebarBottom);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 48);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Padding = new Padding(12, 14, 12, 14);
            pnlSidebar.Size = new Size(230, 732);
            pnlSidebar.TabIndex = 2;
            // 
            // pnlNavList
            // 
            pnlNavList.BackColor = Color.Transparent;
            pnlNavList.Controls.Add(btnNavHome);
            pnlNavList.Controls.Add(btnNavGame);
            pnlNavList.Controls.Add(btnNavCreate);
            pnlNavList.Controls.Add(btnNavJoin);
            pnlNavList.Controls.Add(btnNavLeaderboard);
            pnlNavList.Controls.Add(btnNavHistory);
            pnlNavList.Dock = DockStyle.Top;
            pnlNavList.Location = new Point(12, 14);
            pnlNavList.Name = "pnlNavList";
            pnlNavList.Size = new Size(206, 270);
            pnlNavList.TabIndex = 0;
            // 
            // btnNavHome
            // 
            btnNavHome.BackColor = Color.FromArgb(9, 15, 30);
            btnNavHome.BorderLineColor = Color.Transparent;
            btnNavHome.ClickColor = Color.FromArgb(10, 18, 36);
            btnNavHome.CornerRadius = 10;
            btnNavHome.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnNavHome.FlatStyle = FlatStyle.Flat;
            btnNavHome.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavHome.ForeColor = Color.FromArgb(245, 248, 255);
            btnNavHome.HoverColor = Color.FromArgb(24, 42, 80);
            btnNavHome.IsPrimaryGradient = false;
            btnNavHome.IsPurpleGradient = false;
            btnNavHome.Location = new Point(0, 5);
            btnNavHome.Name = "btnNavHome";
            btnNavHome.NavIcon = CyberIcon.None;
            btnNavHome.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavHome.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnNavHome.Size = new Size(206, 38);
            btnNavHome.TabIndex = 0;
            btnNavHome.Text = "Trang chủ";
            btnNavHome.TextLeftPadding = 16;
            btnNavHome.UseVisualStyleBackColor = false;
            btnNavHome.Click += btnNavHome_Click;
            // 
            // btnNavGame
            // 
            btnNavGame.BackColor = Color.FromArgb(9, 15, 30);
            btnNavGame.BorderLineColor = Color.Transparent;
            btnNavGame.ClickColor = Color.FromArgb(10, 18, 36);
            btnNavGame.CornerRadius = 10;
            btnNavGame.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnNavGame.FlatStyle = FlatStyle.Flat;
            btnNavGame.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavGame.ForeColor = Color.FromArgb(245, 248, 255);
            btnNavGame.HoverColor = Color.FromArgb(24, 42, 80);
            btnNavGame.IsPrimaryGradient = false;
            btnNavGame.IsPurpleGradient = false;
            btnNavGame.Location = new Point(0, 48);
            btnNavGame.Name = "btnNavGame";
            btnNavGame.NavIcon = CyberIcon.None;
            btnNavGame.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavGame.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnNavGame.Size = new Size(206, 38);
            btnNavGame.TabIndex = 1;
            btnNavGame.Text = "Bàn cờ";
            btnNavGame.TextLeftPadding = 16;
            btnNavGame.UseVisualStyleBackColor = false;
            btnNavGame.Click += btnNavGame_Click;
            // 
            // btnNavCreate
            // 
            btnNavCreate.BackColor = Color.FromArgb(9, 15, 30);
            btnNavCreate.BorderLineColor = Color.Transparent;
            btnNavCreate.ClickColor = Color.FromArgb(10, 18, 36);
            btnNavCreate.CornerRadius = 10;
            btnNavCreate.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnNavCreate.FlatStyle = FlatStyle.Flat;
            btnNavCreate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavCreate.ForeColor = Color.FromArgb(245, 248, 255);
            btnNavCreate.HoverColor = Color.FromArgb(24, 42, 80);
            btnNavCreate.IsPrimaryGradient = false;
            btnNavCreate.IsPurpleGradient = false;
            btnNavCreate.Location = new Point(0, 91);
            btnNavCreate.Name = "btnNavCreate";
            btnNavCreate.NavIcon = CyberIcon.None;
            btnNavCreate.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavCreate.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnNavCreate.Size = new Size(206, 38);
            btnNavCreate.TabIndex = 2;
            btnNavCreate.Text = "Tạo phòng";
            btnNavCreate.TextLeftPadding = 16;
            btnNavCreate.UseVisualStyleBackColor = false;
            btnNavCreate.Click += btnNavCreate_Click;
            // 
            // btnNavJoin
            // 
            btnNavJoin.BackColor = Color.FromArgb(9, 15, 30);
            btnNavJoin.BorderLineColor = Color.Transparent;
            btnNavJoin.ClickColor = Color.FromArgb(10, 18, 36);
            btnNavJoin.CornerRadius = 10;
            btnNavJoin.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnNavJoin.FlatStyle = FlatStyle.Flat;
            btnNavJoin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavJoin.ForeColor = Color.FromArgb(245, 248, 255);
            btnNavJoin.HoverColor = Color.FromArgb(24, 42, 80);
            btnNavJoin.IsPrimaryGradient = false;
            btnNavJoin.IsPurpleGradient = false;
            btnNavJoin.Location = new Point(0, 134);
            btnNavJoin.Name = "btnNavJoin";
            btnNavJoin.NavIcon = CyberIcon.None;
            btnNavJoin.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavJoin.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnNavJoin.Size = new Size(206, 38);
            btnNavJoin.TabIndex = 3;
            btnNavJoin.Text = "Tham gia phòng";
            btnNavJoin.TextLeftPadding = 16;
            btnNavJoin.UseVisualStyleBackColor = false;
            btnNavJoin.Click += btnNavJoin_Click;
            // 
            // btnNavLeaderboard
            // 
            btnNavLeaderboard.BackColor = Color.FromArgb(9, 15, 30);
            btnNavLeaderboard.BorderLineColor = Color.Transparent;
            btnNavLeaderboard.ClickColor = Color.FromArgb(10, 18, 36);
            btnNavLeaderboard.CornerRadius = 10;
            btnNavLeaderboard.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnNavLeaderboard.FlatStyle = FlatStyle.Flat;
            btnNavLeaderboard.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavLeaderboard.ForeColor = Color.FromArgb(245, 248, 255);
            btnNavLeaderboard.HoverColor = Color.FromArgb(24, 42, 80);
            btnNavLeaderboard.IsPrimaryGradient = false;
            btnNavLeaderboard.IsPurpleGradient = false;
            btnNavLeaderboard.Location = new Point(0, 177);
            btnNavLeaderboard.Name = "btnNavLeaderboard";
            btnNavLeaderboard.NavIcon = CyberIcon.None;
            btnNavLeaderboard.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavLeaderboard.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnNavLeaderboard.Size = new Size(206, 38);
            btnNavLeaderboard.TabIndex = 4;
            btnNavLeaderboard.Text = "Bảng xếp hạng";
            btnNavLeaderboard.TextLeftPadding = 16;
            btnNavLeaderboard.UseVisualStyleBackColor = false;
            btnNavLeaderboard.Click += btnNavLeaderboard_Click;
            // 
            // btnNavHistory
            // 
            btnNavHistory.BackColor = Color.FromArgb(9, 15, 30);
            btnNavHistory.BorderLineColor = Color.Transparent;
            btnNavHistory.ClickColor = Color.FromArgb(10, 18, 36);
            btnNavHistory.CornerRadius = 10;
            btnNavHistory.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnNavHistory.FlatStyle = FlatStyle.Flat;
            btnNavHistory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavHistory.ForeColor = Color.FromArgb(245, 248, 255);
            btnNavHistory.HoverColor = Color.FromArgb(24, 42, 80);
            btnNavHistory.IsPrimaryGradient = false;
            btnNavHistory.IsPurpleGradient = false;
            btnNavHistory.Location = new Point(0, 220);
            btnNavHistory.Name = "btnNavHistory";
            btnNavHistory.NavIcon = CyberIcon.None;
            btnNavHistory.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavHistory.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnNavHistory.Size = new Size(206, 38);
            btnNavHistory.TabIndex = 5;
            btnNavHistory.Text = "Hồ sơ cá nhân";
            btnNavHistory.TextLeftPadding = 16;
            btnNavHistory.UseVisualStyleBackColor = false;
            btnNavHistory.Click += btnNavHistory_Click;
            // 
            // pnlSidebarBottom
            // 
            pnlSidebarBottom.BackColor = Color.Transparent;
            pnlSidebarBottom.Controls.Add(btnNavSettings);
            pnlSidebarBottom.Controls.Add(pnlProfileCard);
            pnlSidebarBottom.Dock = DockStyle.Bottom;
            pnlSidebarBottom.Location = new Point(12, 592);
            pnlSidebarBottom.Name = "pnlSidebarBottom";
            pnlSidebarBottom.Size = new Size(206, 126);
            pnlSidebarBottom.TabIndex = 1;
            // 
            // btnNavSettings
            // 
            btnNavSettings.BackColor = Color.FromArgb(9, 15, 30);
            btnNavSettings.BorderLineColor = Color.Transparent;
            btnNavSettings.ClickColor = Color.FromArgb(10, 18, 36);
            btnNavSettings.CornerRadius = 10;
            btnNavSettings.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnNavSettings.FlatStyle = FlatStyle.Flat;
            btnNavSettings.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNavSettings.ForeColor = Color.FromArgb(245, 248, 255);
            btnNavSettings.HoverColor = Color.FromArgb(24, 42, 80);
            btnNavSettings.IsPrimaryGradient = false;
            btnNavSettings.IsPurpleGradient = false;
            btnNavSettings.Location = new Point(0, 0);
            btnNavSettings.Name = "btnNavSettings";
            btnNavSettings.NavIcon = CyberIcon.None;
            btnNavSettings.NormalColor = Color.FromArgb(14, 24, 46);
            btnNavSettings.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnNavSettings.Size = new Size(206, 42);
            btnNavSettings.TabIndex = 0;
            btnNavSettings.Text = "⚡ ĐĂNG NHẬP";
            btnNavSettings.TextLeftPadding = 16;
            btnNavSettings.UseVisualStyleBackColor = false;
            btnNavSettings.Click += btnNavSettings_Click;
            // 
            // pnlProfileCard
            // 
            pnlProfileCard.BackColor = Color.Transparent;
            pnlProfileCard.BorderColor = Color.FromArgb(25, 45, 85);
            pnlProfileCard.BorderWidth = 1.2F;
            pnlProfileCard.Controls.Add(lblProfileName);
            pnlProfileCard.Controls.Add(lblProfileLevel);
            pnlProfileCard.Controls.Add(lblProfileXp);
            pnlProfileCard.CornerRadius = 14;
            pnlProfileCard.Cursor = Cursors.Hand;
            pnlProfileCard.FillColor = Color.FromArgb(13, 24, 48);
            pnlProfileCard.Location = new Point(0, 48);
            pnlProfileCard.Name = "pnlProfileCard";
            pnlProfileCard.Padding = new Padding(12);
            pnlProfileCard.Size = new Size(206, 74);
            pnlProfileCard.TabIndex = 1;
            pnlProfileCard.Click += pnlProfileCard_Click;
            // 
            // lblProfileName
            // 
            lblProfileName.AutoSize = true;
            lblProfileName.Cursor = Cursors.Hand;
            lblProfileName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProfileName.ForeColor = Color.White;
            lblProfileName.Location = new Point(12, 8);
            lblProfileName.Name = "lblProfileName";
            lblProfileName.Size = new Size(126, 21);
            lblProfileName.TabIndex = 0;
            lblProfileName.Text = "Khách (Offline)";
            lblProfileName.Click += pnlProfileCard_Click;
            // 
            // lblProfileLevel
            // 
            lblProfileLevel.AutoSize = true;
            lblProfileLevel.Cursor = Cursors.Hand;
            lblProfileLevel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblProfileLevel.ForeColor = Color.FromArgb(0, 194, 255);
            lblProfileLevel.Location = new Point(12, 28);
            lblProfileLevel.Name = "lblProfileLevel";
            lblProfileLevel.Size = new Size(105, 19);
            lblProfileLevel.TabIndex = 1;
            lblProfileLevel.Text = "Chế độ Offline";
            lblProfileLevel.Click += pnlProfileCard_Click;
            // 
            // lblProfileXp
            // 
            lblProfileXp.AutoSize = true;
            lblProfileXp.Cursor = Cursors.Hand;
            lblProfileXp.Font = new Font("Segoe UI", 7.5F);
            lblProfileXp.ForeColor = Color.FromArgb(160, 185, 220);
            lblProfileXp.Location = new Point(12, 46);
            lblProfileXp.Name = "lblProfileXp";
            lblProfileXp.Size = new Size(84, 17);
            lblProfileXp.TabIndex = 2;
            lblProfileXp.Text = "Chơi với Máy";
            lblProfileXp.Click += pnlProfileCard_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(9, 15, 30);
            btnLogout.BorderLineColor = Color.Transparent;
            btnLogout.ClickColor = Color.FromArgb(10, 18, 36);
            btnLogout.CornerRadius = 10;
            btnLogout.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.HoverColor = Color.FromArgb(24, 42, 80);
            btnLogout.IsPrimaryGradient = false;
            btnLogout.IsPurpleGradient = false;
            btnLogout.Location = new Point(12, 68);
            btnLogout.Name = "btnLogout";
            btnLogout.NavIcon = CyberIcon.None;
            btnLogout.NormalColor = Color.FromArgb(14, 24, 46);
            btnLogout.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnLogout.Size = new Size(182, 30);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "⎋ Đăng xuất";
            btnLogout.TextLeftPadding = 16;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Visible = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // pnlRightBar
            // 
            pnlRightBar.BackColor = Color.FromArgb(8, 14, 28);
            pnlRightBar.Controls.Add(cardChat);
            pnlRightBar.Dock = DockStyle.Right;
            pnlRightBar.Location = new Point(980, 48);
            pnlRightBar.Name = "pnlRightBar";
            pnlRightBar.Padding = new Padding(10);
            pnlRightBar.Size = new Size(280, 732);
            pnlRightBar.TabIndex = 1;
            pnlRightBar.Visible = false;
            // 
            // cardChat
            // 
            cardChat.BackColor = Color.Transparent;
            cardChat.BorderColor = Color.FromArgb(25, 45, 85);
            cardChat.BorderWidth = 1.2F;
            cardChat.Controls.Add(rtbChatLog);
            cardChat.Controls.Add(pnlChatBottom);
            cardChat.Controls.Add(lblChatTitle);
            cardChat.CornerRadius = 14;
            cardChat.Dock = DockStyle.Fill;
            cardChat.FillColor = Color.FromArgb(13, 24, 48);
            cardChat.Location = new Point(10, 10);
            cardChat.Name = "cardChat";
            cardChat.Padding = new Padding(10, 8, 10, 8);
            cardChat.Size = new Size(260, 712);
            cardChat.TabIndex = 0;
            // 
            // rtbChatLog
            // 
            rtbChatLog.BackColor = Color.FromArgb(10, 18, 36);
            rtbChatLog.BorderStyle = BorderStyle.None;
            rtbChatLog.Dock = DockStyle.Fill;
            rtbChatLog.Font = new Font("Segoe UI", 8.75F);
            rtbChatLog.ForeColor = Color.FromArgb(220, 235, 255);
            rtbChatLog.Location = new Point(10, 36);
            rtbChatLog.Name = "rtbChatLog";
            rtbChatLog.ReadOnly = true;
            rtbChatLog.Size = new Size(240, 628);
            rtbChatLog.TabIndex = 0;
            rtbChatLog.Text = "";
            // 
            // pnlChatBottom
            // 
            pnlChatBottom.Controls.Add(txtChatInput);
            pnlChatBottom.Controls.Add(btnSendChat);
            pnlChatBottom.Dock = DockStyle.Bottom;
            pnlChatBottom.Location = new Point(10, 664);
            pnlChatBottom.Name = "pnlChatBottom";
            pnlChatBottom.Padding = new Padding(0, 4, 0, 0);
            pnlChatBottom.Size = new Size(240, 40);
            pnlChatBottom.TabIndex = 1;
            // 
            // txtChatInput
            // 
            txtChatInput.BackColor = Color.FromArgb(14, 25, 48);
            txtChatInput.BorderStyle = BorderStyle.FixedSingle;
            txtChatInput.Dock = DockStyle.Fill;
            txtChatInput.Font = new Font("Segoe UI", 9.5F);
            txtChatInput.ForeColor = Color.White;
            txtChatInput.Location = new Point(0, 4);
            txtChatInput.Name = "txtChatInput";
            txtChatInput.Size = new Size(194, 29);
            txtChatInput.TabIndex = 0;
            txtChatInput.KeyDown += txtChatInput_KeyDown;
            // 
            // btnSendChat
            // 
            btnSendChat.BackColor = Color.FromArgb(9, 15, 30);
            btnSendChat.BorderLineColor = Color.Transparent;
            btnSendChat.ClickColor = Color.FromArgb(10, 18, 36);
            btnSendChat.CornerRadius = 10;
            btnSendChat.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnSendChat.Dock = DockStyle.Right;
            btnSendChat.FlatStyle = FlatStyle.Flat;
            btnSendChat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSendChat.ForeColor = Color.FromArgb(245, 248, 255);
            btnSendChat.HoverColor = Color.FromArgb(24, 42, 80);
            btnSendChat.IsPrimaryGradient = false;
            btnSendChat.IsPurpleGradient = false;
            btnSendChat.Location = new Point(194, 4);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.NavIcon = CyberIcon.None;
            btnSendChat.NormalColor = Color.FromArgb(14, 24, 46);
            btnSendChat.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnSendChat.Size = new Size(46, 36);
            btnSendChat.TabIndex = 1;
            btnSendChat.Text = "➤";
            btnSendChat.TextLeftPadding = 16;
            btnSendChat.UseVisualStyleBackColor = false;
            btnSendChat.Click += btnSendChat_Click;
            // 
            // lblChatTitle
            // 
            lblChatTitle.Dock = DockStyle.Top;
            lblChatTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblChatTitle.ForeColor = Color.White;
            lblChatTitle.Location = new Point(10, 8);
            lblChatTitle.Name = "lblChatTitle";
            lblChatTitle.Size = new Size(240, 28);
            lblChatTitle.TabIndex = 2;
            lblChatTitle.Text = "Chat phòng";
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = Color.FromArgb(8, 13, 26);
            pnlMainContent.Controls.Add(pnlHomeView);
            pnlMainContent.Controls.Add(pnlGameView);
            pnlMainContent.Controls.Add(pnlCreateRoomView);
            pnlMainContent.Controls.Add(pnlJoinRoomView);
            pnlMainContent.Controls.Add(pnlHistoryView);
            pnlMainContent.Controls.Add(pnlLeaderboardView);
            pnlMainContent.Controls.Add(pnlSettingsView);
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(230, 48);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(750, 732);
            pnlMainContent.TabIndex = 0;
            // 
            // pnlHomeView
            // 
            pnlHomeView.Controls.Add(pnlMiddleGrid);
            pnlHomeView.Controls.Add(heroBanner);
            pnlHomeView.Dock = DockStyle.Fill;
            pnlHomeView.Location = new Point(0, 0);
            pnlHomeView.Name = "pnlHomeView";
            pnlHomeView.Padding = new Padding(16, 12, 16, 14);
            pnlHomeView.Size = new Size(750, 732);
            pnlHomeView.TabIndex = 0;
            pnlHomeView.Resize += pnlHomeView_Resize;
            // 
            // heroBanner
            // 
            heroBanner.BackColor = Color.Transparent;
            heroBanner.Controls.Add(lblHeroTitle);
            heroBanner.Controls.Add(lblHeroSubtitle);
            heroBanner.Location = new Point(16, 12);
            heroBanner.Name = "heroBanner";
            heroBanner.Size = new Size(718, 180);
            heroBanner.TabIndex = 0;
            // 
            // lblHeroTitle
            // 
            lblHeroTitle.Visible = false;
            // 
            // lblHeroSubtitle
            // 
            lblHeroSubtitle.Visible = false;
            // 
            // pnlMiddleGrid
            // 
            pnlMiddleGrid.Controls.Add(colOnlinePlayers);
            pnlMiddleGrid.Controls.Add(colTopPlayers);
            pnlMiddleGrid.Controls.Add(colEvent);
            pnlMiddleGrid.Location = new Point(16, 204);
            pnlMiddleGrid.Name = "pnlMiddleGrid";
            pnlMiddleGrid.Padding = new Padding(0);
            pnlMiddleGrid.Size = new Size(718, 514);
            pnlMiddleGrid.TabIndex = 1;
            pnlMiddleGrid.Resize += pnlMiddleGrid_Resize;
            // 
            // colEvent
            // 
            colEvent.BackColor = Color.Transparent;
            colEvent.Controls.Add(cardEvent);
            colEvent.Controls.Add(lblEventHeader);
            colEvent.Location = new Point(0, 0);
            colEvent.Name = "colEvent";
            colEvent.Size = new Size(200, 564);
            colEvent.TabIndex = 0;
            // 
            // lblEventHeader
            // 
            lblEventHeader.Dock = DockStyle.Top;
            lblEventHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblEventHeader.ForeColor = Color.White;
            lblEventHeader.Location = new Point(0, 0);
            lblEventHeader.Name = "lblEventHeader";
            lblEventHeader.Size = new Size(200, 28);
            lblEventHeader.TabIndex = 0;
            lblEventHeader.Text = "SỰ KIỆN NỔI BẬT";
            lblEventHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardEvent
            // 
            cardEvent.Dock = DockStyle.Fill;
            cardEvent.Location = new Point(0, 28);
            cardEvent.Name = "cardEvent";
            cardEvent.Size = new Size(200, 536);
            cardEvent.TabIndex = 1;
            // 
            // colTopPlayers
            // 
            colTopPlayers.BackColor = Color.Transparent;
            colTopPlayers.Controls.Add(cardTopPlayers);
            colTopPlayers.Controls.Add(lblTopPlayersHeader);
            colTopPlayers.Location = new Point(212, 0);
            colTopPlayers.Name = "colTopPlayers";
            colTopPlayers.Size = new Size(260, 564);
            colTopPlayers.TabIndex = 1;
            // 
            // lblTopPlayersHeader
            // 
            lblTopPlayersHeader.Dock = DockStyle.Top;
            lblTopPlayersHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTopPlayersHeader.ForeColor = Color.White;
            lblTopPlayersHeader.Location = new Point(0, 0);
            lblTopPlayersHeader.Name = "lblTopPlayersHeader";
            lblTopPlayersHeader.Size = new Size(260, 28);
            lblTopPlayersHeader.TabIndex = 0;
            lblTopPlayersHeader.Text = "BẢNG XẾP HẠNG TOP CAO THỦ";
            lblTopPlayersHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardTopPlayers
            // 
            cardTopPlayers.Dock = DockStyle.Fill;
            cardTopPlayers.Location = new Point(0, 28);
            cardTopPlayers.Name = "cardTopPlayers";
            cardTopPlayers.Size = new Size(260, 536);
            cardTopPlayers.TabIndex = 1;
            // 
            // colOnlinePlayers
            // 
            colOnlinePlayers.BackColor = Color.Transparent;
            colOnlinePlayers.Controls.Add(cardConnectedPlayers);
            colOnlinePlayers.Controls.Add(pnlOnlineHeaderBar);
            colOnlinePlayers.Location = new Point(484, 0);
            colOnlinePlayers.Name = "colOnlinePlayers";
            colOnlinePlayers.Size = new Size(234, 564);
            colOnlinePlayers.TabIndex = 2;
            // 
            // pnlOnlineHeaderBar
            // 
            pnlOnlineHeaderBar.BackColor = Color.Transparent;
            pnlOnlineHeaderBar.Controls.Add(lblOnlineCountBadge);
            pnlOnlineHeaderBar.Controls.Add(lblHomePlayersHeader);
            pnlOnlineHeaderBar.Dock = DockStyle.Top;
            pnlOnlineHeaderBar.Location = new Point(0, 0);
            pnlOnlineHeaderBar.Name = "pnlOnlineHeaderBar";
            pnlOnlineHeaderBar.Size = new Size(234, 28);
            pnlOnlineHeaderBar.TabIndex = 0;
            // 
            // lblHomePlayersHeader
            // 
            lblHomePlayersHeader.Dock = DockStyle.Fill;
            lblHomePlayersHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblHomePlayersHeader.ForeColor = Color.White;
            lblHomePlayersHeader.Location = new Point(0, 0);
            lblHomePlayersHeader.Name = "lblHomePlayersHeader";
            lblHomePlayersHeader.Size = new Size(160, 28);
            lblHomePlayersHeader.TabIndex = 0;
            lblHomePlayersHeader.Text = "NGƯỜI CHƠI ONLINE";
            lblHomePlayersHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOnlineCountBadge
            // 
            lblOnlineCountBadge.AutoSize = true;
            lblOnlineCountBadge.Dock = DockStyle.Right;
            lblOnlineCountBadge.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblOnlineCountBadge.ForeColor = Color.FromArgb(0, 230, 118);
            lblOnlineCountBadge.Location = new Point(174, 0);
            lblOnlineCountBadge.Name = "lblOnlineCountBadge";
            lblOnlineCountBadge.Size = new Size(60, 28);
            lblOnlineCountBadge.TabIndex = 1;
            lblOnlineCountBadge.Text = "● 0";
            lblOnlineCountBadge.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cardConnectedPlayers
            // 
            cardConnectedPlayers.BackColor = Color.Transparent;
            cardConnectedPlayers.BorderColor = Color.FromArgb(25, 45, 85);
            cardConnectedPlayers.BorderWidth = 1.2F;
            cardConnectedPlayers.Controls.Add(lblNoPlayers);
            cardConnectedPlayers.Controls.Add(lstConnectedPlayers);
            cardConnectedPlayers.Controls.Add(pnlOnlineSearchBar);
            cardConnectedPlayers.CornerRadius = 14;
            cardConnectedPlayers.Dock = DockStyle.Fill;
            cardConnectedPlayers.FillColor = Color.FromArgb(13, 24, 48);
            cardConnectedPlayers.Location = new Point(0, 28);
            cardConnectedPlayers.Name = "cardConnectedPlayers";
            cardConnectedPlayers.Padding = new Padding(12, 10, 12, 10);
            cardConnectedPlayers.Size = new Size(234, 536);
            cardConnectedPlayers.TabIndex = 1;
            // 
            // pnlOnlineSearchBar
            // 
            pnlOnlineSearchBar.BackColor = Color.Transparent;
            pnlOnlineSearchBar.Controls.Add(pnlSearchFriendWrapper);
            pnlOnlineSearchBar.Dock = DockStyle.Top;
            pnlOnlineSearchBar.Location = new Point(12, 10);
            pnlOnlineSearchBar.Name = "pnlOnlineSearchBar";
            pnlOnlineSearchBar.Padding = new Padding(0, 0, 0, 6);
            pnlOnlineSearchBar.Size = new Size(210, 36);
            pnlOnlineSearchBar.TabIndex = 0;
            // 
            // pnlSearchFriendWrapper
            // 
            pnlSearchFriendWrapper.BackColor = Color.FromArgb(9, 15, 30);
            pnlSearchFriendWrapper.Controls.Add(txtSearchFriend);
            pnlSearchFriendWrapper.Dock = DockStyle.Fill;
            pnlSearchFriendWrapper.Location = new Point(0, 0);
            pnlSearchFriendWrapper.Margin = new Padding(0);
            pnlSearchFriendWrapper.Name = "pnlSearchFriendWrapper";
            pnlSearchFriendWrapper.Padding = new Padding(8, 5, 8, 5);
            pnlSearchFriendWrapper.Size = new Size(210, 30);
            pnlSearchFriendWrapper.TabIndex = 0;
            // 
            // txtSearchFriend
            // 
            txtSearchFriend.BackColor = Color.FromArgb(9, 15, 30);
            txtSearchFriend.BorderStyle = BorderStyle.None;
            txtSearchFriend.Dock = DockStyle.Fill;
            txtSearchFriend.Font = new Font("Segoe UI", 9F);
            txtSearchFriend.ForeColor = Color.FromArgb(180, 200, 230);
            txtSearchFriend.Location = new Point(8, 5);
            txtSearchFriend.Name = "txtSearchFriend";
            txtSearchFriend.PlaceholderText = "🔍 Tìm kiếm bạn bè...";
            txtSearchFriend.Size = new Size(194, 20);
            txtSearchFriend.TabIndex = 0;
            // 
            // lstConnectedPlayers
            // 
            lstConnectedPlayers.BackColor = Color.FromArgb(13, 24, 48);
            lstConnectedPlayers.BorderStyle = BorderStyle.None;
            lstConnectedPlayers.Dock = DockStyle.Fill;
            lstConnectedPlayers.DrawMode = DrawMode.OwnerDrawFixed;
            lstConnectedPlayers.ItemHeight = 52;
            lstConnectedPlayers.Location = new Point(12, 74);
            lstConnectedPlayers.Name = "lstConnectedPlayers";
            lstConnectedPlayers.SelectionMode = SelectionMode.None;
            lstConnectedPlayers.Size = new Size(210, 480);
            lstConnectedPlayers.TabIndex = 2;
            // 
            // lblNoPlayers
            // 
            lblNoPlayers.Dock = DockStyle.Fill;
            lblNoPlayers.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblNoPlayers.ForeColor = Color.FromArgb(120, 140, 175);
            lblNoPlayers.Location = new Point(12, 74);
            lblNoPlayers.Name = "lblNoPlayers";
            lblNoPlayers.Size = new Size(210, 480);
            lblNoPlayers.TabIndex = 3;
            lblNoPlayers.Text = "Không có người chơi online";
            lblNoPlayers.TextAlign = ContentAlignment.MiddleCenter;
            lblNoPlayers.Visible = false;
            // 
            // cardServerInfo
            // 
            cardServerInfo.Controls.Add(lblHomeServerStatus);
            cardServerInfo.Controls.Add(lblHomeServerHost);
            cardServerInfo.Controls.Add(lblHomeServerIp);
            cardServerInfo.Controls.Add(lblHomeServerHeader);
            cardServerInfo.Controls.Add(btnHomeCreateRoom);
            cardServerInfo.Controls.Add(btnHomeJoinRoom);
            cardServerInfo.Controls.Add(btnHomeSettings);
            cardServerInfo.Visible = false;
            // 
            // pnlGameView
            // 
            pnlGameView.Controls.Add(gameOverModal);
            pnlGameView.Controls.Add(boardControl1);
            pnlGameView.Controls.Add(pnlGameHeader);
            pnlGameView.Dock = DockStyle.Fill;
            pnlGameView.Location = new Point(0, 0);
            pnlGameView.Name = "pnlGameView";
            pnlGameView.Padding = new Padding(12);
            pnlGameView.Size = new Size(750, 732);
            pnlGameView.TabIndex = 1;
            // 
            // gameOverModal
            // 
            gameOverModal.BackColor = Color.FromArgb(170, 5, 10, 22);
            gameOverModal.Dock = DockStyle.Fill;
            gameOverModal.Location = new Point(12, 108);
            gameOverModal.Name = "gameOverModal";
            gameOverModal.Size = new Size(726, 612);
            gameOverModal.TabIndex = 0;
            gameOverModal.Visible = false;
            // 
            // boardControl1
            // 
            boardControl1.Dock = DockStyle.Fill;
            boardControl1.Location = new Point(12, 108);
            boardControl1.Name = "boardControl1";
            boardControl1.Size = new Size(726, 612);
            boardControl1.TabIndex = 1;
            // 
            // pnlGameHeader
            // 
            pnlGameHeader.BackColor = Color.Transparent;
            pnlGameHeader.BorderColor = Color.FromArgb(40, 75, 130);
            pnlGameHeader.BorderWidth = 1.2F;
            pnlGameHeader.Controls.Add(pnlGameActions);
            pnlGameHeader.Controls.Add(timerWidget);
            pnlGameHeader.Controls.Add(pnlGameInfo);
            pnlGameHeader.CornerRadius = 14;
            pnlGameHeader.Dock = DockStyle.Top;
            pnlGameHeader.FillColor = Color.FromArgb(160, 10, 18, 36);
            pnlGameHeader.Location = new Point(12, 12);
            pnlGameHeader.Name = "pnlGameHeader";
            pnlGameHeader.Padding = new Padding(12, 10, 12, 10);
            pnlGameHeader.Size = new Size(726, 96);
            pnlGameHeader.TabIndex = 2;
            // 
            // pnlGameActions
            // 
            pnlGameActions.BackColor = Color.Transparent;
            pnlGameActions.Controls.Add(btnSurrender);
            pnlGameActions.Controls.Add(btnRestart);
            pnlGameActions.Controls.Add(btnLeaveRoom);
            pnlGameActions.Location = new Point(428, 10);
            pnlGameActions.Name = "pnlGameActions";
            pnlGameActions.Size = new Size(136, 76);
            pnlGameActions.TabIndex = 0;
            // 
            // btnSurrender
            // 
            btnSurrender.BackColor = Color.FromArgb(9, 15, 30);
            btnSurrender.BorderLineColor = Color.Transparent;
            btnSurrender.ClickColor = Color.FromArgb(150, 25, 55);
            btnSurrender.CornerRadius = 10;
            btnSurrender.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnSurrender.FlatStyle = FlatStyle.Flat;
            btnSurrender.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSurrender.ForeColor = Color.White;
            btnSurrender.HoverColor = Color.FromArgb(225, 45, 85);
            btnSurrender.IsPrimaryGradient = false;
            btnSurrender.IsPurpleGradient = false;
            btnSurrender.Location = new Point(6, 17);
            btnSurrender.Name = "btnSurrender";
            btnSurrender.NavIcon = CyberIcon.None;
            btnSurrender.NormalColor = Color.FromArgb(190, 35, 70);
            btnSurrender.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnSurrender.Size = new Size(124, 42);
            btnSurrender.TabIndex = 0;
            btnSurrender.Text = "Đầu hàng";
            btnSurrender.TextLeftPadding = 0;
            btnSurrender.UseVisualStyleBackColor = false;
            btnSurrender.Click += btnSurrender_Click;
            // 
            // btnRestart
            // 
            btnRestart.BackColor = Color.FromArgb(9, 15, 30);
            btnRestart.BorderLineColor = Color.Transparent;
            btnRestart.ClickColor = Color.FromArgb(0, 130, 75);
            btnRestart.CornerRadius = 10;
            btnRestart.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRestart.ForeColor = Color.White;
            btnRestart.HoverColor = Color.FromArgb(0, 200, 110);
            btnRestart.IsPrimaryGradient = false;
            btnRestart.IsPurpleGradient = false;
            btnRestart.Location = new Point(6, 17);
            btnRestart.Name = "btnRestart";
            btnRestart.NavIcon = CyberIcon.None;
            btnRestart.NormalColor = Color.FromArgb(0, 160, 90);
            btnRestart.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnRestart.Size = new Size(124, 42);
            btnRestart.TabIndex = 1;
            btnRestart.Text = "Chơi lại";
            btnRestart.TextLeftPadding = 0;
            btnRestart.UseVisualStyleBackColor = false;
            btnRestart.Visible = false;
            btnRestart.Click += btnRestart_Click;
            // 
            // btnLeaveRoom
            // 
            btnLeaveRoom.BackColor = Color.FromArgb(9, 15, 30);
            btnLeaveRoom.BorderLineColor = Color.Transparent;
            btnLeaveRoom.ClickColor = Color.FromArgb(140, 25, 50);
            btnLeaveRoom.CornerRadius = 10;
            btnLeaveRoom.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnLeaveRoom.FlatStyle = FlatStyle.Flat;
            btnLeaveRoom.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLeaveRoom.ForeColor = Color.White;
            btnLeaveRoom.HoverColor = Color.FromArgb(215, 50, 80);
            btnLeaveRoom.IsPrimaryGradient = false;
            btnLeaveRoom.IsPurpleGradient = false;
            btnLeaveRoom.Location = new Point(6, 17);
            btnLeaveRoom.Name = "btnLeaveRoom";
            btnLeaveRoom.NavIcon = CyberIcon.None;
            btnLeaveRoom.NormalColor = Color.FromArgb(180, 40, 65);
            btnLeaveRoom.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnLeaveRoom.Size = new Size(124, 42);
            btnLeaveRoom.TabIndex = 2;
            btnLeaveRoom.Text = "Thoát phòng";
            btnLeaveRoom.TextLeftPadding = 0;
            btnLeaveRoom.UseVisualStyleBackColor = false;
            btnLeaveRoom.Visible = false;
            btnLeaveRoom.Click += btnLeaveRoom_Click;
            // 
            // timerWidget
            // 
            timerWidget.BackColor = Color.Transparent;
            timerWidget.CornerRadius = 12;
            timerWidget.IsCountdownMode = false;
            timerWidget.Location = new Point(564, 10);
            timerWidget.Name = "timerWidget";
            timerWidget.Size = new Size(150, 76);
            timerWidget.TabIndex = 1;
            // 
            // pnlGameInfo
            // 
            pnlGameInfo.BackColor = Color.Transparent;
            pnlGameInfo.Controls.Add(lblRoomIdInfo);
            pnlGameInfo.Controls.Add(lblPlayersInfo);
            pnlGameInfo.Controls.Add(lblTurnInfo);
            pnlGameInfo.Location = new Point(12, 10);
            pnlGameInfo.Name = "pnlGameInfo";
            pnlGameInfo.Size = new Size(702, 76);
            pnlGameInfo.TabIndex = 2;
            // 
            // lblRoomIdInfo
            // 
            lblRoomIdInfo.AutoSize = true;
            lblRoomIdInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRoomIdInfo.ForeColor = Color.White;
            lblRoomIdInfo.Location = new Point(6, 4);
            lblRoomIdInfo.Name = "lblRoomIdInfo";
            lblRoomIdInfo.Size = new Size(92, 23);
            lblRoomIdInfo.TabIndex = 0;
            lblRoomIdInfo.Text = "Phòng: ---";
            // 
            // lblPlayersInfo
            // 
            lblPlayersInfo.AutoSize = true;
            lblPlayersInfo.Font = new Font("Segoe UI", 9F);
            lblPlayersInfo.ForeColor = Color.FromArgb(170, 190, 220);
            lblPlayersInfo.Location = new Point(6, 26);
            lblPlayersInfo.Name = "lblPlayersInfo";
            lblPlayersInfo.Size = new Size(120, 20);
            lblPlayersInfo.TabIndex = 1;
            lblPlayersInfo.Text = "X: ---   VS   O: ---";
            // 
            // lblTurnInfo
            // 
            lblTurnInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTurnInfo.AutoEllipsis = true;
            lblTurnInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTurnInfo.ForeColor = Color.FromArgb(0, 194, 255);
            lblTurnInfo.Location = new Point(6, 48);
            lblTurnInfo.Name = "lblTurnInfo";
            lblTurnInfo.Size = new Size(962, 22);
            lblTurnInfo.TabIndex = 2;
            lblTurnInfo.Text = "● Lượt đi: ---";
            // 
            // pnlCreateRoomView
            // 
            pnlCreateRoomView.BackColor = Color.FromArgb(8, 13, 26);
            pnlCreateRoomView.Controls.Add(cardCreateBox);
            pnlCreateRoomView.Dock = DockStyle.Fill;
            pnlCreateRoomView.Location = new Point(0, 0);
            pnlCreateRoomView.Name = "pnlCreateRoomView";
            pnlCreateRoomView.Padding = new Padding(20);
            pnlCreateRoomView.Size = new Size(750, 732);
            pnlCreateRoomView.TabIndex = 2;
            pnlCreateRoomView.Resize += pnlCreateRoomView_Resize;
            // 
            // cardCreateBox
            // 
            cardCreateBox.BackColor = Color.Transparent;
            cardCreateBox.BorderColor = Color.FromArgb(0, 220, 255);
            cardCreateBox.BorderWidth = 1.8F;
            cardCreateBox.CornerRadius = 14;
            cardCreateBox.IsTechCircuitFrame = true;
            cardCreateBox.Controls.Add(btnCancelCreate);
            cardCreateBox.Controls.Add(btnCreateRoom);
            cardCreateBox.Controls.Add(pillRuleFree);
            cardCreateBox.Controls.Add(pillRuleStandard);
            cardCreateBox.Controls.Add(secHeaderRule);
            cardCreateBox.Controls.Add(pnlAiDifficultyOptions);
            cardCreateBox.Controls.Add(pnlTimeLimitOptions);
            cardCreateBox.Controls.Add(cardTimeFree);
            cardCreateBox.Controls.Add(cardTimeTurn);
            cardCreateBox.Controls.Add(secHeaderTime);
            cardCreateBox.Controls.Add(cardModeAI);
            cardCreateBox.Controls.Add(cardModePvP);
            cardCreateBox.Controls.Add(secHeaderMode);
            cardCreateBox.Controls.Add(pnlCreateHeader);
            cardCreateBox.FillColor = Color.FromArgb(238, 7, 34, 52);
            cardCreateBox.Location = new Point(80, 20);
            cardCreateBox.Name = "cardCreateBox";
            cardCreateBox.Padding = new Padding(28, 20, 28, 20);
            cardCreateBox.Size = new Size(680, 560);
            cardCreateBox.TabIndex = 0;
            // 
            // btnCancelCreate
            // 
            btnCancelCreate.BackColor = Color.FromArgb(9, 15, 30);
            btnCancelCreate.BorderLineColor = Color.FromArgb(38, 75, 125);
            btnCancelCreate.ClickColor = Color.FromArgb(10, 18, 36);
            btnCancelCreate.CornerRadius = 12;
            btnCancelCreate.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnCancelCreate.FlatStyle = FlatStyle.Flat;
            btnCancelCreate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancelCreate.ForeColor = Color.FromArgb(210, 230, 255);
            btnCancelCreate.HoverColor = Color.FromArgb(24, 44, 76);
            btnCancelCreate.IsPrimaryGradient = false;
            btnCancelCreate.IsPurpleGradient = false;
            btnCancelCreate.Location = new Point(470, 510);
            btnCancelCreate.Name = "btnCancelCreate";
            btnCancelCreate.NavIcon = CyberIcon.None;
            btnCancelCreate.NormalColor = Color.FromArgb(14, 26, 48);
            btnCancelCreate.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnCancelCreate.Size = new Size(182, 48);
            btnCancelCreate.TabIndex = 0;
            btnCancelCreate.Text = "←  Quay lại";
            btnCancelCreate.TextLeftPadding = 16;
            btnCancelCreate.UseVisualStyleBackColor = false;
            btnCancelCreate.Click += btnCancelCreate_Click;
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.BackColor = Color.FromArgb(9, 15, 30);
            btnCreateRoom.BorderLineColor = Color.FromArgb(120, 245, 255);
            btnCreateRoom.ClickColor = Color.FromArgb(10, 18, 36);
            btnCreateRoom.CornerRadius = 12;
            btnCreateRoom.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnCreateRoom.FlatStyle = FlatStyle.Flat;
            btnCreateRoom.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCreateRoom.ForeColor = Color.White;
            btnCreateRoom.HoverColor = Color.FromArgb(24, 42, 80);
            btnCreateRoom.IsPrimaryGradient = false;
            btnCreateRoom.IsCyanEmeraldGradient = true;
            btnCreateRoom.IsPurpleGradient = false;
            btnCreateRoom.Location = new Point(28, 510);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.NavIcon = CyberIcon.Play;
            btnCreateRoom.NormalColor = Color.FromArgb(14, 24, 46);
            btnCreateRoom.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnCreateRoom.Size = new Size(430, 48);
            btnCreateRoom.TabIndex = 1;
            btnCreateRoom.Text = "Tạo phòng";
            btnCreateRoom.TextLeftPadding = 16;
            btnCreateRoom.UseVisualStyleBackColor = false;
            btnCreateRoom.Click += btnCreateRoom_Click;
            // 
            // pillRuleFree
            // 
            pillRuleFree.CornerRadius = 8;
            pillRuleFree.IsSelected = false;
            pillRuleFree.Location = new Point(346, 452);
            pillRuleFree.Name = "pillRuleFree";
            pillRuleFree.OptionText = "Tự do (5 ô là thắng)";
            pillRuleFree.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillRuleFree.PillText = "Tự do (5 ô là thắng)";
            pillRuleFree.Size = new Size(306, 42);
            pillRuleFree.TabIndex = 2;
            // 
            // pillRuleStandard
            // 
            pillRuleStandard.CornerRadius = 8;
            pillRuleStandard.IsSelected = true;
            pillRuleStandard.Location = new Point(28, 452);
            pillRuleStandard.Name = "pillRuleStandard";
            pillRuleStandard.OptionText = "Chặn 2 đầu không thắng";
            pillRuleStandard.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillRuleStandard.PillText = "Chặn 2 đầu không thắng";
            pillRuleStandard.Size = new Size(306, 42);
            pillRuleStandard.TabIndex = 3;
            // 
            // secHeaderRule
            // 
            secHeaderRule.HeaderText = "Luật thi đấu";
            secHeaderRule.Location = new Point(28, 420);
            secHeaderRule.Name = "secHeaderRule";
            secHeaderRule.ParentBgColor = Color.FromArgb(13, 24, 48);
            secHeaderRule.Size = new Size(624, 26);
            secHeaderRule.StepNumber = 3;
            secHeaderRule.TabIndex = 4;
            // 
            // pnlAiDifficultyOptions
            // 
            pnlAiDifficultyOptions.BackColor = Color.Transparent;
            pnlAiDifficultyOptions.Controls.Add(lblAiDifficultyLabel);
            pnlAiDifficultyOptions.Controls.Add(pillAiEasy);
            pnlAiDifficultyOptions.Controls.Add(pillAiMedium);
            pnlAiDifficultyOptions.Controls.Add(pillAiHard);
            pnlAiDifficultyOptions.Location = new Point(28, 376);
            pnlAiDifficultyOptions.Name = "pnlAiDifficultyOptions";
            pnlAiDifficultyOptions.Size = new Size(624, 38);
            pnlAiDifficultyOptions.TabIndex = 5;
            pnlAiDifficultyOptions.Visible = false;
            // 
            // lblAiDifficultyLabel
            // 
            lblAiDifficultyLabel.AutoSize = true;
            lblAiDifficultyLabel.Font = new Font("Segoe UI", 9F);
            lblAiDifficultyLabel.ForeColor = Color.FromArgb(150, 175, 215);
            lblAiDifficultyLabel.Location = new Point(0, 10);
            lblAiDifficultyLabel.Name = "lblAiDifficultyLabel";
            lblAiDifficultyLabel.Size = new Size(81, 20);
            lblAiDifficultyLabel.TabIndex = 0;
            lblAiDifficultyLabel.Text = "Mức độ AI:";
            // 
            // pillAiEasy
            // 
            pillAiEasy.CornerRadius = 8;
            pillAiEasy.IsSelected = false;
            pillAiEasy.Location = new Point(165, 1);
            pillAiEasy.Name = "pillAiEasy";
            pillAiEasy.OptionText = "Dễ";
            pillAiEasy.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillAiEasy.PillText = "Dễ";
            pillAiEasy.Size = new Size(95, 36);
            pillAiEasy.TabIndex = 1;
            // 
            // pillAiMedium
            // 
            pillAiMedium.CornerRadius = 8;
            pillAiMedium.IsSelected = true;
            pillAiMedium.Location = new Point(272, 1);
            pillAiMedium.Name = "pillAiMedium";
            pillAiMedium.OptionText = "Trung bình";
            pillAiMedium.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillAiMedium.PillText = "Trung bình";
            pillAiMedium.Size = new Size(115, 36);
            pillAiMedium.TabIndex = 2;
            // 
            // pillAiHard
            // 
            pillAiHard.CornerRadius = 8;
            pillAiHard.IsSelected = false;
            pillAiHard.Location = new Point(399, 1);
            pillAiHard.Name = "pillAiHard";
            pillAiHard.OptionText = "Khó";
            pillAiHard.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillAiHard.PillText = "Khó";
            pillAiHard.Size = new Size(95, 36);
            pillAiHard.TabIndex = 3;
            // 
            // pnlTimeLimitOptions
            // 
            pnlTimeLimitOptions.BackColor = Color.Transparent;
            pnlTimeLimitOptions.Controls.Add(lblTimePillLabel);
            pnlTimeLimitOptions.Controls.Add(pillTime15);
            pnlTimeLimitOptions.Controls.Add(pillTime30);
            pnlTimeLimitOptions.Controls.Add(pillTime60);
            pnlTimeLimitOptions.Controls.Add(pillCountdown1m);
            pnlTimeLimitOptions.Controls.Add(pillCountdown3m);
            pnlTimeLimitOptions.Controls.Add(pillCountdown5m);
            pnlTimeLimitOptions.Controls.Add(pillCountdownCustom);
            pnlTimeLimitOptions.Controls.Add(txtCustomMinutes);
            pnlTimeLimitOptions.Controls.Add(lblCustomMinutesUnit);
            pnlTimeLimitOptions.Location = new Point(28, 332);
            pnlTimeLimitOptions.Name = "pnlTimeLimitOptions";
            pnlTimeLimitOptions.Size = new Size(624, 38);
            pnlTimeLimitOptions.TabIndex = 6;
            // 
            // lblTimePillLabel
            // 
            lblTimePillLabel.AutoSize = true;
            lblTimePillLabel.Font = new Font("Segoe UI", 9F);
            lblTimePillLabel.ForeColor = Color.FromArgb(150, 175, 215);
            lblTimePillLabel.Location = new Point(0, 10);
            lblTimePillLabel.Name = "lblTimePillLabel";
            lblTimePillLabel.Size = new Size(135, 20);
            lblTimePillLabel.TabIndex = 0;
            lblTimePillLabel.Text = "Thời gian mỗi lượt:";
            // 
            // pillTime15
            // 
            pillTime15.CornerRadius = 8;
            pillTime15.IsSelected = false;
            pillTime15.Location = new Point(165, 1);
            pillTime15.Name = "pillTime15";
            pillTime15.OptionText = "15s";
            pillTime15.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillTime15.PillText = "15s";
            pillTime15.Size = new Size(95, 36);
            pillTime15.TabIndex = 1;
            // 
            // pillTime30
            // 
            pillTime30.CornerRadius = 8;
            pillTime30.IsSelected = true;
            pillTime30.Location = new Point(272, 1);
            pillTime30.Name = "pillTime30";
            pillTime30.OptionText = "30s";
            pillTime30.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillTime30.PillText = "30s";
            pillTime30.Size = new Size(95, 36);
            pillTime30.TabIndex = 2;
            // 
            // pillTime60
            // 
            pillTime60.CornerRadius = 8;
            pillTime60.IsSelected = false;
            pillTime60.Location = new Point(379, 1);
            pillTime60.Name = "pillTime60";
            pillTime60.OptionText = "60s";
            pillTime60.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillTime60.PillText = "60s";
            pillTime60.Size = new Size(95, 36);
            pillTime60.TabIndex = 3;
            // 
            // pillCountdown1m
            // 
            pillCountdown1m.CornerRadius = 8;
            pillCountdown1m.IsSelected = false;
            pillCountdown1m.Location = new Point(165, 1);
            pillCountdown1m.Name = "pillCountdown1m";
            pillCountdown1m.OptionText = "1 phút";
            pillCountdown1m.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillCountdown1m.PillText = "1 phút";
            pillCountdown1m.Size = new Size(80, 36);
            pillCountdown1m.TabIndex = 4;
            pillCountdown1m.Visible = false;
            // 
            // pillCountdown3m
            // 
            pillCountdown3m.CornerRadius = 8;
            pillCountdown3m.IsSelected = true;
            pillCountdown3m.Location = new Point(255, 1);
            pillCountdown3m.Name = "pillCountdown3m";
            pillCountdown3m.OptionText = "3 phút";
            pillCountdown3m.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillCountdown3m.PillText = "3 phút";
            pillCountdown3m.Size = new Size(80, 36);
            pillCountdown3m.TabIndex = 5;
            pillCountdown3m.Visible = false;
            // 
            // pillCountdown5m
            // 
            pillCountdown5m.CornerRadius = 8;
            pillCountdown5m.IsSelected = false;
            pillCountdown5m.Location = new Point(345, 1);
            pillCountdown5m.Name = "pillCountdown5m";
            pillCountdown5m.OptionText = "5 phút";
            pillCountdown5m.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillCountdown5m.PillText = "5 phút";
            pillCountdown5m.Size = new Size(80, 36);
            pillCountdown5m.TabIndex = 6;
            pillCountdown5m.Visible = false;
            // 
            // pillCountdownCustom
            // 
            pillCountdownCustom.CornerRadius = 8;
            pillCountdownCustom.IsSelected = false;
            pillCountdownCustom.Location = new Point(435, 1);
            pillCountdownCustom.Name = "pillCountdownCustom";
            pillCountdownCustom.OptionText = "Tự chọn";
            pillCountdownCustom.ParentBgColor = Color.FromArgb(13, 24, 48);
            pillCountdownCustom.PillText = "Tự chọn";
            pillCountdownCustom.Size = new Size(90, 36);
            pillCountdownCustom.TabIndex = 7;
            pillCountdownCustom.Visible = false;
            // 
            // txtCustomMinutes
            // 
            txtCustomMinutes.BackColor = Color.FromArgb(16, 28, 54);
            txtCustomMinutes.BorderStyle = BorderStyle.FixedSingle;
            txtCustomMinutes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtCustomMinutes.ForeColor = Color.FromArgb(0, 194, 255);
            txtCustomMinutes.Location = new Point(533, 5);
            txtCustomMinutes.Name = "txtCustomMinutes";
            txtCustomMinutes.Size = new Size(40, 29);
            txtCustomMinutes.TabIndex = 8;
            txtCustomMinutes.Text = "2";
            txtCustomMinutes.TextAlign = HorizontalAlignment.Center;
            txtCustomMinutes.Visible = false;
            // 
            // lblCustomMinutesUnit
            // 
            lblCustomMinutesUnit.AutoSize = true;
            lblCustomMinutesUnit.Font = new Font("Segoe UI", 9F);
            lblCustomMinutesUnit.ForeColor = Color.FromArgb(150, 175, 215);
            lblCustomMinutesUnit.Location = new Point(579, 10);
            lblCustomMinutesUnit.Name = "lblCustomMinutesUnit";
            lblCustomMinutesUnit.Size = new Size(39, 20);
            lblCustomMinutesUnit.TabIndex = 9;
            lblCustomMinutesUnit.Text = "phút";
            lblCustomMinutesUnit.Visible = false;
            // 
            // cardTimeFree
            // 
            cardTimeFree.CornerRadius = 12;
            cardTimeFree.IconType = CardIconType.Stopwatch;
            cardTimeFree.IsSelected = false;
            cardTimeFree.Location = new Point(346, 248);
            cardTimeFree.Name = "cardTimeFree";
            cardTimeFree.ParentBgColor = Color.FromArgb(13, 24, 48);
            cardTimeFree.Size = new Size(306, 74);
            cardTimeFree.Subtitle = "1 - 5 phút hoặc tự chọn";
            cardTimeFree.TabIndex = 7;
            cardTimeFree.Title = "Đếm ngược";
            // 
            // cardTimeTurn
            // 
            cardTimeTurn.CornerRadius = 12;
            cardTimeTurn.IconType = CardIconType.ClockTurn;
            cardTimeTurn.IsSelected = true;
            cardTimeTurn.Location = new Point(28, 248);
            cardTimeTurn.Name = "cardTimeTurn";
            cardTimeTurn.ParentBgColor = Color.FromArgb(13, 24, 48);
            cardTimeTurn.Size = new Size(306, 74);
            cardTimeTurn.Subtitle = "15s, 30s hoặc 60s";
            cardTimeTurn.TabIndex = 8;
            cardTimeTurn.Title = "Theo lượt";
            // 
            // secHeaderTime
            // 
            secHeaderTime.HeaderText = "Quy định thời gian";
            secHeaderTime.Location = new Point(28, 218);
            secHeaderTime.Name = "secHeaderTime";
            secHeaderTime.ParentBgColor = Color.FromArgb(13, 24, 48);
            secHeaderTime.Size = new Size(624, 26);
            secHeaderTime.StepNumber = 2;
            secHeaderTime.TabIndex = 9;
            // 
            // cardModeAI
            // 
            cardModeAI.CornerRadius = 12;
            cardModeAI.IconType = CardIconType.RobotAI;
            cardModeAI.IsSelected = false;
            cardModeAI.Location = new Point(346, 116);
            cardModeAI.Name = "cardModeAI";
            cardModeAI.ParentBgColor = Color.FromArgb(13, 24, 48);
            cardModeAI.Size = new Size(306, 90);
            cardModeAI.Subtitle = "Chơi với máy";
            cardModeAI.TabIndex = 10;
            cardModeAI.Title = "Vs AI";
            // 
            // cardModePvP
            // 
            cardModePvP.CornerRadius = 12;
            cardModePvP.IconType = CardIconType.TwoPlayers;
            cardModePvP.IsSelected = true;
            cardModePvP.Location = new Point(28, 116);
            cardModePvP.Name = "cardModePvP";
            cardModePvP.ParentBgColor = Color.FromArgb(13, 24, 48);
            cardModePvP.Size = new Size(306, 90);
            cardModePvP.Subtitle = "Đấu trực tiếp";
            cardModePvP.TabIndex = 11;
            cardModePvP.Title = "2 người";
            // 
            // secHeaderMode
            // 
            secHeaderMode.HeaderText = "Chọn chế độ chơi";
            secHeaderMode.Location = new Point(28, 86);
            secHeaderMode.Name = "secHeaderMode";
            secHeaderMode.ParentBgColor = Color.FromArgb(13, 24, 48);
            secHeaderMode.Size = new Size(624, 26);
            secHeaderMode.StepNumber = 1;
            secHeaderMode.TabIndex = 12;
            // 
            // pnlCreateHeader
            // 
            pnlCreateHeader.BackColor = Color.Transparent;
            pnlCreateHeader.Controls.Add(badgeCreatePlus);
            pnlCreateHeader.Controls.Add(lblCreateTitle);
            pnlCreateHeader.Controls.Add(lblCreateSubtitle);
            pnlCreateHeader.Location = new Point(28, 20);
            pnlCreateHeader.Name = "pnlCreateHeader";
            pnlCreateHeader.Size = new Size(624, 58);
            pnlCreateHeader.TabIndex = 13;
            // 
            // badgeCreatePlus
            // 
            badgeCreatePlus.Location = new Point(0, 6);
            badgeCreatePlus.Name = "badgeCreatePlus";
            badgeCreatePlus.ParentBgColor = Color.FromArgb(13, 24, 48);
            badgeCreatePlus.Size = new Size(42, 42);
            badgeCreatePlus.TabIndex = 0;
            // 
            // lblCreateTitle
            // 
            lblCreateTitle.AutoSize = true;
            lblCreateTitle.Font = new Font("Segoe UI", 14.5F, FontStyle.Bold);
            lblCreateTitle.ForeColor = Color.White;
            lblCreateTitle.Location = new Point(52, 2);
            lblCreateTitle.Name = "lblCreateTitle";
            lblCreateTitle.Size = new Size(194, 35);
            lblCreateTitle.TabIndex = 1;
            lblCreateTitle.Text = "Tạo phòng mới";
            // 
            // lblCreateSubtitle
            // 
            lblCreateSubtitle.AutoSize = true;
            lblCreateSubtitle.Font = new Font("Segoe UI", 9F);
            lblCreateSubtitle.ForeColor = Color.FromArgb(140, 168, 208);
            lblCreateSubtitle.Location = new Point(54, 32);
            lblCreateSubtitle.Name = "lblCreateSubtitle";
            lblCreateSubtitle.Size = new Size(350, 20);
            lblCreateSubtitle.TabIndex = 2;
            lblCreateSubtitle.Text = "Thiết lập chế độ chơi và mời bạn bè cùng tham gia!";
            // 
            // pnlJoinRoomView
            // 
            pnlJoinRoomView.Controls.Add(lblLobbyLastUpdated);
            pnlJoinRoomView.Controls.Add(pnlLobbyBody);
            pnlJoinRoomView.Controls.Add(pnlLobbyFilterBar);
            pnlJoinRoomView.Controls.Add(pnlLobbyHeaderBar);
            pnlJoinRoomView.Dock = DockStyle.Fill;
            pnlJoinRoomView.Location = new Point(0, 0);
            pnlJoinRoomView.Name = "pnlJoinRoomView";
            pnlJoinRoomView.Padding = new Padding(18, 12, 18, 10);
            pnlJoinRoomView.Size = new Size(750, 732);
            pnlJoinRoomView.TabIndex = 3;
            // 
            // lblLobbyLastUpdated
            // 
            lblLobbyLastUpdated.Dock = DockStyle.Bottom;
            lblLobbyLastUpdated.Font = new Font("Segoe UI", 8F);
            lblLobbyLastUpdated.ForeColor = Color.FromArgb(107, 122, 153);
            lblLobbyLastUpdated.Location = new Point(18, 700);
            lblLobbyLastUpdated.Name = "lblLobbyLastUpdated";
            lblLobbyLastUpdated.Size = new Size(714, 22);
            lblLobbyLastUpdated.TabIndex = 0;
            lblLobbyLastUpdated.Text = "Cập nhật vừa xong • 14:32";
            lblLobbyLastUpdated.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlLobbyBody
            // 
            pnlLobbyBody.BackColor = Color.Transparent;
            pnlLobbyBody.Controls.Add(pnlLobbyRightCol);
            pnlLobbyBody.Controls.Add(cardOpenRooms);
            pnlLobbyBody.Dock = DockStyle.Fill;
            pnlLobbyBody.Location = new Point(18, 172);
            pnlLobbyBody.Name = "pnlLobbyBody";
            pnlLobbyBody.Padding = new Padding(0, 6, 0, 0);
            pnlLobbyBody.Size = new Size(714, 550);
            pnlLobbyBody.TabIndex = 1;
            pnlLobbyBody.Resize += pnlLobbyBody_Resize;
            // 
            // pnlLobbyRightCol
            // 
            pnlLobbyRightCol.BackColor = Color.Transparent;
            pnlLobbyRightCol.Controls.Add(cardFeaturedRoom);
            pnlLobbyRightCol.Controls.Add(cardJoinByCode);
            pnlLobbyRightCol.Location = new Point(0, 0);
            pnlLobbyRightCol.Name = "pnlLobbyRightCol";
            pnlLobbyRightCol.Size = new Size(285, 100);
            pnlLobbyRightCol.TabIndex = 0;
            // 
            // cardFeaturedRoom
            // 
            cardFeaturedRoom.BackColor = Color.FromArgb(11, 19, 36);
            cardFeaturedRoom.BorderColor = Color.FromArgb(25, 45, 85);
            cardFeaturedRoom.BorderWidth = 1.2F;
            cardFeaturedRoom.Controls.Add(btnFeaturedAction);
            cardFeaturedRoom.Controls.Add(lblFeaturedStatus);
            cardFeaturedRoom.Controls.Add(lblFeaturedMode);
            cardFeaturedRoom.Controls.Add(lblFeaturedHost);
            cardFeaturedRoom.Controls.Add(lblFeaturedRoomName);
            cardFeaturedRoom.Controls.Add(miniBoardPreview);
            cardFeaturedRoom.Controls.Add(lblViewAllRooms);
            cardFeaturedRoom.Controls.Add(lblFeaturedTitle);
            cardFeaturedRoom.CornerRadius = 14;
            cardFeaturedRoom.FillColor = Color.FromArgb(13, 24, 48);
            cardFeaturedRoom.Location = new Point(0, 0);
            cardFeaturedRoom.Name = "cardFeaturedRoom";
            cardFeaturedRoom.Padding = new Padding(12);
            cardFeaturedRoom.Size = new Size(200, 215);
            cardFeaturedRoom.TabIndex = 0;
            // 
            // btnFeaturedAction
            // 
            btnFeaturedAction.BackColor = Color.FromArgb(9, 15, 30);
            btnFeaturedAction.BorderLineColor = Color.Transparent;
            btnFeaturedAction.ClickColor = Color.FromArgb(10, 18, 36);
            btnFeaturedAction.CornerRadius = 10;
            btnFeaturedAction.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnFeaturedAction.FlatStyle = FlatStyle.Flat;
            btnFeaturedAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFeaturedAction.ForeColor = Color.White;
            btnFeaturedAction.HoverColor = Color.FromArgb(24, 42, 80);
            btnFeaturedAction.IsPrimaryGradient = false;
            btnFeaturedAction.IsPurpleGradient = false;
            btnFeaturedAction.Location = new Point(14, 148);
            btnFeaturedAction.Name = "btnFeaturedAction";
            btnFeaturedAction.NavIcon = CyberIcon.Plus;
            btnFeaturedAction.NormalColor = Color.FromArgb(14, 24, 46);
            btnFeaturedAction.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnFeaturedAction.Size = new Size(257, 36);
            btnFeaturedAction.TabIndex = 0;
            btnFeaturedAction.Text = "Tạo phòng mới";
            btnFeaturedAction.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnFeaturedAction.TextLeftPadding = 0;
            btnFeaturedAction.UseVisualStyleBackColor = false;
            btnFeaturedAction.Click += btnFeaturedAction_Click;
            // 
            // lblFeaturedStatus
            // 
            lblFeaturedStatus.AutoSize = true;
            lblFeaturedStatus.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblFeaturedStatus.ForeColor = Color.FromArgb(140, 165, 200);
            lblFeaturedStatus.Location = new Point(118, 102);
            lblFeaturedStatus.Name = "lblFeaturedStatus";
            lblFeaturedStatus.Size = new Size(105, 19);
            lblFeaturedStatus.TabIndex = 1;
            lblFeaturedStatus.Text = "● Phòng trống";
            // 
            // lblFeaturedMode
            // 
            lblFeaturedMode.AutoSize = true;
            lblFeaturedMode.Font = new Font("Segoe UI", 8.25F);
            lblFeaturedMode.ForeColor = Color.FromArgb(142, 160, 192);
            lblFeaturedMode.Location = new Point(118, 82);
            lblFeaturedMode.Name = "lblFeaturedMode";
            lblFeaturedMode.Size = new Size(97, 19);
            lblFeaturedMode.TabIndex = 2;
            lblFeaturedMode.Text = "Chế độ Online";
            // 
            // lblFeaturedHost
            // 
            lblFeaturedHost.AutoSize = true;
            lblFeaturedHost.Font = new Font("Segoe UI", 8.25F);
            lblFeaturedHost.ForeColor = Color.FromArgb(142, 160, 192);
            lblFeaturedHost.Location = new Point(118, 62);
            lblFeaturedHost.Name = "lblFeaturedHost";
            lblFeaturedHost.Size = new Size(27, 19);
            lblFeaturedHost.TabIndex = 3;
            lblFeaturedHost.Text = "---";
            // 
            // lblFeaturedRoomName
            // 
            lblFeaturedRoomName.AutoSize = true;
            lblFeaturedRoomName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFeaturedRoomName.ForeColor = Color.White;
            lblFeaturedRoomName.Location = new Point(118, 40);
            lblFeaturedRoomName.Name = "lblFeaturedRoomName";
            lblFeaturedRoomName.Size = new Size(166, 23);
            lblFeaturedRoomName.TabIndex = 4;
            lblFeaturedRoomName.Text = "Chưa có phòng đấu";
            // 
            // miniBoardPreview
            // 
            miniBoardPreview.Location = new Point(14, 40);
            miniBoardPreview.Name = "miniBoardPreview";
            miniBoardPreview.Size = new Size(95, 95);
            miniBoardPreview.TabIndex = 5;
            // 
            // lblViewAllRooms
            // 
            lblViewAllRooms.AutoSize = true;
            lblViewAllRooms.Cursor = Cursors.Hand;
            lblViewAllRooms.Font = new Font("Segoe UI", 8.25F);
            lblViewAllRooms.ForeColor = Color.FromArgb(0, 194, 255);
            lblViewAllRooms.Location = new Point(190, 14);
            lblViewAllRooms.Name = "lblViewAllRooms";
            lblViewAllRooms.Size = new Size(90, 19);
            lblViewAllRooms.TabIndex = 6;
            lblViewAllRooms.Text = "Xem tất cả →";
            lblViewAllRooms.Click += lblViewAllRooms_Click;
            // 
            // lblFeaturedTitle
            // 
            lblFeaturedTitle.AutoSize = true;
            lblFeaturedTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFeaturedTitle.ForeColor = Color.White;
            lblFeaturedTitle.Location = new Point(12, 12);
            lblFeaturedTitle.Name = "lblFeaturedTitle";
            lblFeaturedTitle.Size = new Size(164, 23);
            lblFeaturedTitle.TabIndex = 7;
            lblFeaturedTitle.Text = "★ Gợi ý phòng đấu";
            // 
            // cardJoinByCode
            // 
            cardJoinByCode.BackColor = Color.FromArgb(11, 19, 36);
            cardJoinByCode.BorderColor = Color.FromArgb(25, 45, 85);
            cardJoinByCode.BorderWidth = 1.2F;
            cardJoinByCode.Controls.Add(lblCodeFootnote);
            cardJoinByCode.Controls.Add(btnJoinRoom);
            cardJoinByCode.Controls.Add(pnlCodeWrapper);
            cardJoinByCode.Controls.Add(lblJoinByCodeTitle);
            cardJoinByCode.CornerRadius = 14;
            cardJoinByCode.FillColor = Color.FromArgb(13, 24, 48);
            cardJoinByCode.Location = new Point(0, 0);
            cardJoinByCode.Name = "cardJoinByCode";
            cardJoinByCode.Padding = new Padding(12);
            cardJoinByCode.Size = new Size(200, 175);
            cardJoinByCode.TabIndex = 1;
            // 
            // lblCodeFootnote
            // 
            lblCodeFootnote.Font = new Font("Segoe UI", 8F);
            lblCodeFootnote.ForeColor = Color.FromArgb(107, 122, 153);
            lblCodeFootnote.Location = new Point(14, 136);
            lblCodeFootnote.Name = "lblCodeFootnote";
            lblCodeFootnote.Size = new Size(257, 20);
            lblCodeFootnote.TabIndex = 0;
            lblCodeFootnote.Text = "ⓘ Nhập mã 6 ký tự do bạn bè chia sẻ";
            lblCodeFootnote.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.BackColor = Color.FromArgb(9, 15, 30);
            btnJoinRoom.BorderLineColor = Color.Transparent;
            btnJoinRoom.ClickColor = Color.FromArgb(0, 80, 200);
            btnJoinRoom.CornerRadius = 10;
            btnJoinRoom.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnJoinRoom.FlatStyle = FlatStyle.Flat;
            btnJoinRoom.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnJoinRoom.ForeColor = Color.White;
            btnJoinRoom.HoverColor = Color.FromArgb(0, 150, 255);
            btnJoinRoom.IsPrimaryGradient = true;
            btnJoinRoom.IsPurpleGradient = false;
            btnJoinRoom.Location = new Point(14, 88);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.NavIcon = CyberIcon.None;
            btnJoinRoom.NormalColor = Color.FromArgb(13, 110, 253);
            btnJoinRoom.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnJoinRoom.Size = new Size(257, 38);
            btnJoinRoom.TabIndex = 1;
            btnJoinRoom.Text = "Vào phòng";
            btnJoinRoom.TextLeftPadding = 0;
            btnJoinRoom.UseVisualStyleBackColor = false;
            btnJoinRoom.Click += btnJoinRoom_Click;
            // 
            // pnlCodeWrapper
            // 
            pnlCodeWrapper.BackColor = Color.FromArgb(8, 14, 28);
            pnlCodeWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlCodeWrapper.BorderWidth = 1.2F;
            pnlCodeWrapper.Controls.Add(txtRoomId);
            pnlCodeWrapper.Controls.Add(lblCodeHash);
            pnlCodeWrapper.CornerRadius = 14;
            pnlCodeWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlCodeWrapper.Location = new Point(14, 40);
            pnlCodeWrapper.Name = "pnlCodeWrapper";
            pnlCodeWrapper.Padding = new Padding(12);
            pnlCodeWrapper.Size = new Size(257, 38);
            pnlCodeWrapper.TabIndex = 2;
            // 
            // txtRoomId
            // 
            txtRoomId.BackColor = Color.FromArgb(8, 14, 28);
            txtRoomId.BorderStyle = BorderStyle.None;
            txtRoomId.CharacterCasing = CharacterCasing.Upper;
            txtRoomId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtRoomId.ForeColor = Color.FromArgb(0, 194, 255);
            txtRoomId.Location = new Point(26, 9);
            txtRoomId.Name = "txtRoomId";
            txtRoomId.PlaceholderText = "Nhập mã phòng (6 ký tự)...";
            txtRoomId.Size = new Size(220, 23);
            txtRoomId.TabIndex = 0;
            // 
            // lblCodeHash
            // 
            lblCodeHash.AutoSize = true;
            lblCodeHash.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCodeHash.ForeColor = Color.FromArgb(90, 110, 140);
            lblCodeHash.Location = new Point(8, 9);
            lblCodeHash.Name = "lblCodeHash";
            lblCodeHash.Size = new Size(23, 25);
            lblCodeHash.TabIndex = 1;
            lblCodeHash.Text = "#";
            // 
            // lblJoinByCodeTitle
            // 
            lblJoinByCodeTitle.AutoSize = true;
            lblJoinByCodeTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblJoinByCodeTitle.ForeColor = Color.White;
            lblJoinByCodeTitle.Location = new Point(14, 12);
            lblJoinByCodeTitle.Name = "lblJoinByCodeTitle";
            lblJoinByCodeTitle.Size = new Size(176, 25);
            lblJoinByCodeTitle.TabIndex = 3;
            lblJoinByCodeTitle.Text = "Tham gia bằng mã";
            // 
            // cardOpenRooms
            // 
            cardOpenRooms.BackColor = Color.FromArgb(13, 24, 48);
            cardOpenRooms.BorderColor = Color.FromArgb(25, 45, 85);
            cardOpenRooms.BorderWidth = 1.2F;
            cardOpenRooms.Controls.Add(pnlOpenRoomsScroll);
            cardOpenRooms.Controls.Add(pnlOpenRoomsHeader);
            cardOpenRooms.CornerRadius = 14;
            cardOpenRooms.FillColor = Color.FromArgb(13, 24, 48);
            cardOpenRooms.Location = new Point(0, 0);
            cardOpenRooms.Name = "cardOpenRooms";
            cardOpenRooms.Padding = new Padding(12);
            cardOpenRooms.Size = new Size(200, 100);
            cardOpenRooms.TabIndex = 1;
            // 
            // pnlOpenRoomsScroll
            // 
            pnlOpenRoomsScroll.AutoScroll = true;
            pnlOpenRoomsScroll.BackColor = Color.FromArgb(13, 24, 48);
            pnlOpenRoomsScroll.Controls.Add(lblNoRooms);
            pnlOpenRoomsScroll.Dock = DockStyle.Fill;
            pnlOpenRoomsScroll.Location = new Point(12, 50);
            pnlOpenRoomsScroll.Name = "pnlOpenRoomsScroll";
            pnlOpenRoomsScroll.Padding = new Padding(8, 6, 8, 8);
            pnlOpenRoomsScroll.Size = new Size(176, 38);
            pnlOpenRoomsScroll.TabIndex = 0;
            // 
            // lblNoRooms
            // 
            lblNoRooms.Dock = DockStyle.Fill;
            lblNoRooms.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblNoRooms.ForeColor = Color.FromArgb(120, 140, 175);
            lblNoRooms.Location = new Point(8, 6);
            lblNoRooms.Name = "lblNoRooms";
            lblNoRooms.Size = new Size(160, 24);
            lblNoRooms.TabIndex = 0;
            lblNoRooms.Text = "Hiện chưa có phòng nào đang mở.\nHãy bấm '+ Tạo phòng' ở menu bên trái để bắt đầu trận đấu!";
            lblNoRooms.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlOpenRoomsHeader
            // 
            pnlOpenRoomsHeader.BackColor = Color.FromArgb(13, 24, 48);
            pnlOpenRoomsHeader.Controls.Add(lblOpenRoomsCount);
            pnlOpenRoomsHeader.Controls.Add(lblOpenRoomsTitle);
            pnlOpenRoomsHeader.Dock = DockStyle.Top;
            pnlOpenRoomsHeader.Location = new Point(12, 12);
            pnlOpenRoomsHeader.Name = "pnlOpenRoomsHeader";
            pnlOpenRoomsHeader.Padding = new Padding(14, 8, 14, 0);
            pnlOpenRoomsHeader.Size = new Size(176, 38);
            pnlOpenRoomsHeader.TabIndex = 1;
            // 
            // lblOpenRoomsCount
            // 
            lblOpenRoomsCount.Dock = DockStyle.Right;
            lblOpenRoomsCount.Font = new Font("Segoe UI", 8.5F);
            lblOpenRoomsCount.ForeColor = Color.FromArgb(107, 122, 153);
            lblOpenRoomsCount.Location = new Point(62, 8);
            lblOpenRoomsCount.Name = "lblOpenRoomsCount";
            lblOpenRoomsCount.Size = new Size(100, 30);
            lblOpenRoomsCount.TabIndex = 0;
            lblOpenRoomsCount.Text = "4 phòng";
            lblOpenRoomsCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblOpenRoomsTitle
            // 
            lblOpenRoomsTitle.AutoSize = true;
            lblOpenRoomsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOpenRoomsTitle.ForeColor = Color.White;
            lblOpenRoomsTitle.Location = new Point(12, 9);
            lblOpenRoomsTitle.Name = "lblOpenRoomsTitle";
            lblOpenRoomsTitle.Size = new Size(156, 25);
            lblOpenRoomsTitle.TabIndex = 1;
            lblOpenRoomsTitle.Text = "Phòng đang mở";
            // 
            // pnlLobbyFilterBar
            // 
            pnlLobbyFilterBar.BackColor = Color.Transparent;
            pnlLobbyFilterBar.Controls.Add(btnRefreshLobby);
            pnlLobbyFilterBar.Controls.Add(pnlFilterPills);
            pnlLobbyFilterBar.Controls.Add(pnlSearchBox);
            pnlLobbyFilterBar.Dock = DockStyle.Top;
            pnlLobbyFilterBar.Location = new Point(18, 124);
            pnlLobbyFilterBar.Name = "pnlLobbyFilterBar";
            pnlLobbyFilterBar.Size = new Size(714, 48);
            pnlLobbyFilterBar.TabIndex = 2;
            // 
            // btnRefreshLobby
            // 
            btnRefreshLobby.BackColor = Color.FromArgb(9, 15, 30);
            btnRefreshLobby.BorderLineColor = Color.FromArgb(28, 48, 80);
            btnRefreshLobby.ClickColor = Color.FromArgb(10, 18, 36);
            btnRefreshLobby.CornerRadius = 8;
            btnRefreshLobby.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnRefreshLobby.Dock = DockStyle.Right;
            btnRefreshLobby.FlatStyle = FlatStyle.Flat;
            btnRefreshLobby.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefreshLobby.ForeColor = Color.FromArgb(200, 220, 250);
            btnRefreshLobby.HoverColor = Color.FromArgb(24, 42, 80);
            btnRefreshLobby.IsPrimaryGradient = false;
            btnRefreshLobby.IsPurpleGradient = false;
            btnRefreshLobby.Location = new Point(606, 0);
            btnRefreshLobby.Name = "btnRefreshLobby";
            btnRefreshLobby.NavIcon = CyberIcon.Refresh;
            btnRefreshLobby.NormalColor = Color.FromArgb(14, 24, 46);
            btnRefreshLobby.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnRefreshLobby.Size = new Size(108, 48);
            btnRefreshLobby.TabIndex = 0;
            btnRefreshLobby.Text = "Làm mới";
            btnRefreshLobby.TextLeftPadding = 16;
            btnRefreshLobby.UseVisualStyleBackColor = false;
            btnRefreshLobby.Click += btnRefreshLobby_Click;
            // 
            // pnlFilterPills
            // 
            pnlFilterPills.BackColor = Color.Transparent;
            pnlFilterPills.Controls.Add(pillFilterWaiting);
            pnlFilterPills.Controls.Add(pillFilter4p);
            pnlFilterPills.Controls.Add(pillFilter2p);
            pnlFilterPills.Controls.Add(pillFilterAll);
            pnlFilterPills.Location = new Point(195, 4);
            pnlFilterPills.Name = "pnlFilterPills";
            pnlFilterPills.Size = new Size(405, 40);
            pnlFilterPills.TabIndex = 1;
            // 
            // pillFilterWaiting
            // 
            pillFilterWaiting.BackColor = Color.Transparent;
            pillFilterWaiting.FilterKey = "waiting";
            pillFilterWaiting.IsSelected = false;
            pillFilterWaiting.Location = new Point(280, 2);
            pillFilterWaiting.Name = "pillFilterWaiting";
            pillFilterWaiting.Size = new Size(116, 36);
            pillFilterWaiting.TabIndex = 0;
            pillFilterWaiting.Text = "Đang chờ";
            pillFilterWaiting.Click += PillFilter_Click;
            // 
            // pillFilter4p
            // 
            pillFilter4p.BackColor = Color.Transparent;
            pillFilter4p.FilterKey = "4p";
            pillFilter4p.IsSelected = false;
            pillFilter4p.Location = new Point(184, 2);
            pillFilter4p.Name = "pillFilter4p";
            pillFilter4p.Size = new Size(92, 36);
            pillFilter4p.TabIndex = 1;
            pillFilter4p.Text = "4 người";
            pillFilter4p.Click += PillFilter_Click;
            // 
            // pillFilter2p
            // 
            pillFilter2p.BackColor = Color.Transparent;
            pillFilter2p.FilterKey = "2p";
            pillFilter2p.IsSelected = false;
            pillFilter2p.Location = new Point(88, 2);
            pillFilter2p.Name = "pillFilter2p";
            pillFilter2p.Size = new Size(92, 36);
            pillFilter2p.TabIndex = 2;
            pillFilter2p.Text = "2 người";
            pillFilter2p.Click += PillFilter_Click;
            // 
            // pillFilterAll
            // 
            pillFilterAll.BackColor = Color.Transparent;
            pillFilterAll.FilterKey = "all";
            pillFilterAll.IsSelected = true;
            pillFilterAll.Location = new Point(0, 2);
            pillFilterAll.Name = "pillFilterAll";
            pillFilterAll.Size = new Size(84, 36);
            pillFilterAll.TabIndex = 3;
            pillFilterAll.Text = "Tất cả";
            pillFilterAll.Click += PillFilter_Click;
            // 
            // pnlSearchBox
            // 
            pnlSearchBox.BackColor = Color.FromArgb(14, 23, 42);
            pnlSearchBox.BorderColor = Color.FromArgb(25, 45, 85);
            pnlSearchBox.BorderWidth = 1.2F;
            pnlSearchBox.Controls.Add(txtSearchRoom);
            pnlSearchBox.Controls.Add(lblSearchIcon);
            pnlSearchBox.CornerRadius = 14;
            pnlSearchBox.FillColor = Color.FromArgb(13, 24, 48);
            pnlSearchBox.Location = new Point(0, 4);
            pnlSearchBox.Name = "pnlSearchBox";
            pnlSearchBox.Padding = new Padding(12);
            pnlSearchBox.Size = new Size(185, 40);
            pnlSearchBox.TabIndex = 2;
            // 
            // txtSearchRoom
            // 
            txtSearchRoom.BackColor = Color.FromArgb(14, 23, 42);
            txtSearchRoom.BorderStyle = BorderStyle.None;
            txtSearchRoom.Font = new Font("Segoe UI", 9.5F);
            txtSearchRoom.ForeColor = Color.FromArgb(200, 220, 250);
            txtSearchRoom.Location = new Point(34, 9);
            txtSearchRoom.Name = "txtSearchRoom";
            txtSearchRoom.PlaceholderText = "Tìm phòng...";
            txtSearchRoom.Size = new Size(145, 22);
            txtSearchRoom.TabIndex = 0;
            txtSearchRoom.TextChanged += txtSearchRoom_TextChanged;
            // 
            // lblSearchIcon
            // 
            lblSearchIcon.BackColor = Color.Transparent;
            lblSearchIcon.Icon = CyberIcon.Search;
            lblSearchIcon.IconColor = Color.FromArgb(0, 194, 255);
            lblSearchIcon.Location = new Point(10, 11);
            lblSearchIcon.Name = "lblSearchIcon";
            lblSearchIcon.Size = new Size(18, 18);
            lblSearchIcon.TabIndex = 1;
            // 
            // pnlLobbyHeaderBar
            // 
            pnlLobbyHeaderBar.BackColor = Color.Transparent;
            pnlLobbyHeaderBar.Controls.Add(lblLobbySubtitle);
            pnlLobbyHeaderBar.Controls.Add(lblLobbyTitle);
            pnlLobbyHeaderBar.Controls.Add(pnlLobbyIconBox);
            pnlLobbyHeaderBar.Dock = DockStyle.Top;
            pnlLobbyHeaderBar.Location = new Point(18, 12);
            pnlLobbyHeaderBar.Name = "pnlLobbyHeaderBar";
            pnlLobbyHeaderBar.Size = new Size(714, 112);
            pnlLobbyHeaderBar.TabIndex = 3;
            // 
            // lblLobbySubtitle
            // 
            lblLobbySubtitle.AutoSize = true;
            lblLobbySubtitle.BackColor = Color.Transparent;
            lblLobbySubtitle.Font = new Font("Segoe UI", 9.5F);
            lblLobbySubtitle.ForeColor = Color.FromArgb(160, 185, 220);
            lblLobbySubtitle.Location = new Point(86, 50);
            lblLobbySubtitle.Name = "lblLobbySubtitle";
            lblLobbySubtitle.Size = new Size(316, 21);
            lblLobbySubtitle.TabIndex = 1;
            lblLobbySubtitle.Text = "Khám phá phòng đang mở trong mạng LAN";
            // 
            // lblLobbyTitle
            // 
            lblLobbyTitle.AutoSize = true;
            lblLobbyTitle.BackColor = Color.Transparent;
            lblLobbyTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLobbyTitle.ForeColor = Color.White;
            lblLobbyTitle.Location = new Point(84, 12);
            lblLobbyTitle.Name = "lblLobbyTitle";
            lblLobbyTitle.Size = new Size(248, 41);
            lblLobbyTitle.TabIndex = 2;
            lblLobbyTitle.Text = "Sảnh phòng đấu";
            // 
            // pnlLobbyIconBox
            // 
            pnlLobbyIconBox.BackColor = Color.Transparent;
            pnlLobbyIconBox.Location = new Point(18, 15);
            pnlLobbyIconBox.Name = "pnlLobbyIconBox";
            pnlLobbyIconBox.Size = new Size(54, 54);
            pnlLobbyIconBox.TabIndex = 3;
            // 
            // pnlHistoryView
            // 
            pnlHistoryView.Controls.Add(cardHistoryBox);
            pnlHistoryView.Controls.Add(pnlHistorySpacer2);
            pnlHistoryView.Controls.Add(pnlMiddleRow);
            pnlHistoryView.Controls.Add(pnlHistorySpacer1);
            pnlHistoryView.Controls.Add(cardProfileHero);
            pnlHistoryView.Dock = DockStyle.Fill;
            pnlHistoryView.Location = new Point(0, 0);
            pnlHistoryView.Name = "pnlHistoryView";
            pnlHistoryView.Padding = new Padding(16, 12, 16, 14);
            pnlHistoryView.Size = new Size(750, 732);
            pnlHistoryView.TabIndex = 4;
            // 
            // cardHistoryBox
            // 
            cardHistoryBox.BackColor = Color.Transparent;
            cardHistoryBox.BorderColor = Color.FromArgb(25, 45, 85);
            cardHistoryBox.BorderWidth = 1.2F;
            cardHistoryBox.Controls.Add(lblNoHistory);
            cardHistoryBox.Controls.Add(dgvMatchHistory);
            cardHistoryBox.Controls.Add(pnlHistoryHeaderBar);
            cardHistoryBox.CornerRadius = 14;
            cardHistoryBox.Dock = DockStyle.Fill;
            cardHistoryBox.FillColor = Color.FromArgb(13, 24, 48);
            cardHistoryBox.Location = new Point(16, 301);
            cardHistoryBox.Name = "cardHistoryBox";
            cardHistoryBox.Padding = new Padding(16, 12, 16, 14);
            cardHistoryBox.Size = new Size(718, 417);
            cardHistoryBox.TabIndex = 0;
            // 
            // lblNoHistory
            // 
            lblNoHistory.Dock = DockStyle.Fill;
            lblNoHistory.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblNoHistory.ForeColor = Color.FromArgb(120, 140, 175);
            lblNoHistory.Location = new Point(16, 48);
            lblNoHistory.Name = "lblNoHistory";
            lblNoHistory.Size = new Size(686, 355);
            lblNoHistory.TabIndex = 0;
            lblNoHistory.Text = "Bạn chưa có trận đấu nào được lưu trữ.\nHãy tham gia phòng PvP để lưu lại lịch sử thi đấu!";
            lblNoHistory.TextAlign = ContentAlignment.MiddleCenter;
            lblNoHistory.Visible = false;
            // 
            // dgvMatchHistory
            // 
            dgvMatchHistory.AllowUserToAddRows = false;
            dgvMatchHistory.AllowUserToDeleteRows = false;
            dgvMatchHistory.AllowUserToResizeRows = false;
            dgvMatchHistory.BackgroundColor = Color.FromArgb(12, 22, 44);
            dgvMatchHistory.BorderStyle = BorderStyle.None;
            dgvMatchHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(14, 28, 56);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(160, 180, 215);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvMatchHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvMatchHistory.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(12, 22, 44);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(20, 45, 90);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvMatchHistory.DefaultCellStyle = dataGridViewCellStyle2;
            dgvMatchHistory.Dock = DockStyle.Fill;
            dgvMatchHistory.EnableHeadersVisualStyles = false;
            dgvMatchHistory.GridColor = Color.FromArgb(20, 36, 70);
            dgvMatchHistory.Location = new Point(16, 48);
            dgvMatchHistory.Name = "dgvMatchHistory";
            dgvMatchHistory.ReadOnly = true;
            dgvMatchHistory.RowHeadersVisible = false;
            dgvMatchHistory.RowHeadersWidth = 51;
            dgvMatchHistory.RowTemplate.Height = 36;
            dgvMatchHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMatchHistory.Size = new Size(686, 355);
            dgvMatchHistory.TabIndex = 1;
            // 
            // pnlHistoryHeaderBar
            // 
            pnlHistoryHeaderBar.BackColor = Color.Transparent;
            pnlHistoryHeaderBar.Controls.Add(btnRefreshHistory);
            pnlHistoryHeaderBar.Controls.Add(lblHistoryHeader);
            pnlHistoryHeaderBar.Dock = DockStyle.Top;
            pnlHistoryHeaderBar.Location = new Point(16, 12);
            pnlHistoryHeaderBar.Name = "pnlHistoryHeaderBar";
            pnlHistoryHeaderBar.Size = new Size(686, 36);
            pnlHistoryHeaderBar.TabIndex = 2;
            // 
            // btnRefreshHistory
            // 
            btnRefreshHistory.BackColor = Color.FromArgb(16, 30, 62);
            btnRefreshHistory.Cursor = Cursors.Hand;
            btnRefreshHistory.Dock = DockStyle.Right;
            btnRefreshHistory.FlatAppearance.BorderColor = Color.FromArgb(30, 55, 100);
            btnRefreshHistory.FlatStyle = FlatStyle.Flat;
            btnRefreshHistory.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnRefreshHistory.ForeColor = Color.FromArgb(0, 194, 255);
            btnRefreshHistory.Location = new Point(581, 0);
            btnRefreshHistory.Name = "btnRefreshHistory";
            btnRefreshHistory.Size = new Size(105, 36);
            btnRefreshHistory.TabIndex = 0;
            btnRefreshHistory.Text = "Xem thêm →";
            btnRefreshHistory.UseVisualStyleBackColor = false;
            btnRefreshHistory.Click += btnRefreshHistory_Click;
            // 
            // lblHistoryHeader
            // 
            lblHistoryHeader.AutoSize = true;
            lblHistoryHeader.Dock = DockStyle.Left;
            lblHistoryHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblHistoryHeader.ForeColor = Color.White;
            lblHistoryHeader.Location = new Point(0, 0);
            lblHistoryHeader.Name = "lblHistoryHeader";
            lblHistoryHeader.Size = new Size(186, 25);
            lblHistoryHeader.TabIndex = 1;
            lblHistoryHeader.Text = "🕒  Lịch sử trận đấu";
            lblHistoryHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlHistorySpacer2
            // 
            pnlHistorySpacer2.BackColor = Color.Transparent;
            pnlHistorySpacer2.Dock = DockStyle.Top;
            pnlHistorySpacer2.Location = new Point(16, 289);
            pnlHistorySpacer2.Name = "pnlHistorySpacer2";
            pnlHistorySpacer2.Size = new Size(718, 12);
            pnlHistorySpacer2.TabIndex = 1;
            // 
            // pnlMiddleRow
            // 
            pnlMiddleRow.BackColor = Color.Transparent;
            pnlMiddleRow.Controls.Add(cardAchievements);
            pnlMiddleRow.Controls.Add(pnlMiddleSpacer);
            pnlMiddleRow.Controls.Add(cardQuickStats);
            pnlMiddleRow.Dock = DockStyle.Top;
            pnlMiddleRow.Location = new Point(16, 159);
            pnlMiddleRow.Name = "pnlMiddleRow";
            pnlMiddleRow.Size = new Size(718, 130);
            pnlMiddleRow.TabIndex = 2;
            pnlMiddleRow.Resize += pnlMiddleRow_Resize;
            // 
            // cardAchievements
            // 
            cardAchievements.BackColor = Color.Transparent;
            cardAchievements.BorderColor = Color.FromArgb(25, 45, 85);
            cardAchievements.BorderWidth = 1.2F;
            cardAchievements.Controls.Add(lblAchievementsHeader);
            cardAchievements.Controls.Add(badgeTanThu);
            cardAchievements.Controls.Add(badgeChienBinh);
            cardAchievements.Controls.Add(badgeCaoThu);
            cardAchievements.Controls.Add(badgeLocked);
            cardAchievements.CornerRadius = 14;
            cardAchievements.Dock = DockStyle.Fill;
            cardAchievements.FillColor = Color.FromArgb(13, 24, 48);
            cardAchievements.Location = new Point(394, 0);
            cardAchievements.Name = "cardAchievements";
            cardAchievements.Padding = new Padding(12);
            cardAchievements.Size = new Size(324, 130);
            cardAchievements.TabIndex = 0;
            // 
            // lblAchievementsHeader
            // 
            lblAchievementsHeader.AutoSize = true;
            lblAchievementsHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblAchievementsHeader.ForeColor = Color.White;
            lblAchievementsHeader.Location = new Point(14, 10);
            lblAchievementsHeader.Name = "lblAchievementsHeader";
            lblAchievementsHeader.Size = new Size(195, 25);
            lblAchievementsHeader.TabIndex = 0;
            lblAchievementsHeader.Text = "⭐  Thành tích nổi bật";
            // 
            // badgeTanThu
            // 
            badgeTanThu.BadgeImage = null;
            badgeTanThu.BadgeTitle = "Tân Thủ";
            badgeTanThu.Location = new Point(16, 36);
            badgeTanThu.Name = "badgeTanThu";
            badgeTanThu.ParentBgColor = Color.FromArgb(12, 24, 52);
            badgeTanThu.Size = new Size(80, 80);
            badgeTanThu.TabIndex = 1;
            // 
            // badgeChienBinh
            // 
            badgeChienBinh.BadgeImage = null;
            badgeChienBinh.BadgeTitle = "Tân Thủ";
            badgeChienBinh.Location = new Point(106, 36);
            badgeChienBinh.Name = "badgeChienBinh";
            badgeChienBinh.ParentBgColor = Color.FromArgb(12, 24, 52);
            badgeChienBinh.Size = new Size(80, 80);
            badgeChienBinh.TabIndex = 2;
            // 
            // badgeCaoThu
            // 
            badgeCaoThu.BadgeImage = null;
            badgeCaoThu.BadgeTitle = "Tân Thủ";
            badgeCaoThu.Location = new Point(196, 36);
            badgeCaoThu.Name = "badgeCaoThu";
            badgeCaoThu.ParentBgColor = Color.FromArgb(12, 24, 52);
            badgeCaoThu.Size = new Size(80, 80);
            badgeCaoThu.TabIndex = 3;
            // 
            // badgeLocked
            // 
            badgeLocked.BadgeImage = null;
            badgeLocked.BadgeTitle = "Tân Thủ";
            badgeLocked.Location = new Point(286, 36);
            badgeLocked.Name = "badgeLocked";
            badgeLocked.ParentBgColor = Color.FromArgb(12, 24, 52);
            badgeLocked.Size = new Size(80, 80);
            badgeLocked.TabIndex = 4;
            // 
            // pnlMiddleSpacer
            // 
            pnlMiddleSpacer.BackColor = Color.Transparent;
            pnlMiddleSpacer.Dock = DockStyle.Left;
            pnlMiddleSpacer.Location = new Point(380, 0);
            pnlMiddleSpacer.Name = "pnlMiddleSpacer";
            pnlMiddleSpacer.Size = new Size(14, 130);
            pnlMiddleSpacer.TabIndex = 1;
            // 
            // cardQuickStats
            // 
            cardQuickStats.BackColor = Color.Transparent;
            cardQuickStats.BorderColor = Color.FromArgb(25, 45, 85);
            cardQuickStats.BorderWidth = 1.2F;
            cardQuickStats.Controls.Add(lblQuickStatsHeader);
            cardQuickStats.Controls.Add(quickStatTotal);
            cardQuickStats.Controls.Add(quickStatWins);
            cardQuickStats.Controls.Add(quickStatElo);
            cardQuickStats.CornerRadius = 14;
            cardQuickStats.Dock = DockStyle.Left;
            cardQuickStats.FillColor = Color.FromArgb(13, 24, 48);
            cardQuickStats.Location = new Point(0, 0);
            cardQuickStats.Name = "cardQuickStats";
            cardQuickStats.Padding = new Padding(12);
            cardQuickStats.Size = new Size(380, 130);
            cardQuickStats.TabIndex = 2;
            // 
            // lblQuickStatsHeader
            // 
            lblQuickStatsHeader.AutoSize = true;
            lblQuickStatsHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblQuickStatsHeader.ForeColor = Color.White;
            lblQuickStatsHeader.Location = new Point(14, 10);
            lblQuickStatsHeader.Name = "lblQuickStatsHeader";
            lblQuickStatsHeader.Size = new Size(187, 25);
            lblQuickStatsHeader.TabIndex = 0;
            lblQuickStatsHeader.Text = "📊  Thống kê nhanh";
            // 
            // quickStatTotal
            // 
            quickStatTotal.IconType = QuickStatItem.QuickStatIcon.Swords;
            quickStatTotal.LabelText = "Tổng trận";
            quickStatTotal.Location = new Point(12, 38);
            quickStatTotal.Name = "quickStatTotal";
            quickStatTotal.ParentBgColor = Color.FromArgb(12, 24, 52);
            quickStatTotal.Size = new Size(112, 78);
            quickStatTotal.TabIndex = 1;
            quickStatTotal.ValueText = "0";
            // 
            // quickStatWins
            // 
            quickStatWins.IconType = QuickStatItem.QuickStatIcon.Trophy;
            quickStatWins.LabelText = "Trận thắng";
            quickStatWins.Location = new Point(134, 38);
            quickStatWins.Name = "quickStatWins";
            quickStatWins.ParentBgColor = Color.FromArgb(12, 24, 52);
            quickStatWins.Size = new Size(112, 78);
            quickStatWins.TabIndex = 2;
            quickStatWins.ValueText = "0";
            // 
            // quickStatElo
            // 
            quickStatElo.IconType = QuickStatItem.QuickStatIcon.Star;
            quickStatElo.LabelText = "Điểm ELO";
            quickStatElo.Location = new Point(256, 38);
            quickStatElo.Name = "quickStatElo";
            quickStatElo.ParentBgColor = Color.FromArgb(12, 24, 52);
            quickStatElo.Size = new Size(112, 78);
            quickStatElo.TabIndex = 3;
            quickStatElo.ValueText = "0";
            // 
            // pnlHistorySpacer1
            // 
            pnlHistorySpacer1.BackColor = Color.Transparent;
            pnlHistorySpacer1.Dock = DockStyle.Top;
            pnlHistorySpacer1.Location = new Point(16, 147);
            pnlHistorySpacer1.Name = "pnlHistorySpacer1";
            pnlHistorySpacer1.Size = new Size(718, 12);
            pnlHistorySpacer1.TabIndex = 3;
            // 
            // cardProfileHero
            // 
            cardProfileHero.BackColor = Color.Transparent;
            cardProfileHero.BorderColor = Color.FromArgb(25, 45, 85);
            cardProfileHero.BorderWidth = 1.2F;
            cardProfileHero.Controls.Add(avatarControl);
            cardProfileHero.Controls.Add(lblProfileDetailName);
            cardProfileHero.Controls.Add(lblEditNameIcon);
            cardProfileHero.Controls.Add(lblProfileDetailElo);
            cardProfileHero.Controls.Add(lblProfileDetailRank);
            cardProfileHero.Controls.Add(lblProfileDetailStatus);
            cardProfileHero.Controls.Add(kpiTotal);
            cardProfileHero.Controls.Add(kpiWins);
            cardProfileHero.Controls.Add(kpiLosses);
            cardProfileHero.Controls.Add(gaugeWinRate);
            cardProfileHero.CornerRadius = 14;
            cardProfileHero.Dock = DockStyle.Top;
            cardProfileHero.FillColor = Color.FromArgb(13, 24, 48);
            cardProfileHero.Location = new Point(16, 12);
            cardProfileHero.Name = "cardProfileHero";
            cardProfileHero.Padding = new Padding(12);
            cardProfileHero.Size = new Size(718, 135);
            cardProfileHero.TabIndex = 4;
            cardProfileHero.Resize += cardProfileHero_Resize;
            // 
            // avatarControl
            // 
            avatarControl.AvatarImage = null;
            avatarControl.BorderRingColor = Color.FromArgb(0, 194, 255);
            avatarControl.IsEditable = false;
            avatarControl.IsOnline = true;
            avatarControl.Location = new Point(18, 16);
            avatarControl.Name = "avatarControl";
            avatarControl.ParentBgColor = Color.FromArgb(12, 24, 52);
            avatarControl.ShowStatusDot = false;
            avatarControl.Size = new Size(94, 94);
            avatarControl.TabIndex = 0;
            // 
            // lblProfileDetailName
            // 
            lblProfileDetailName.AutoSize = true;
            lblProfileDetailName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblProfileDetailName.ForeColor = Color.White;
            lblProfileDetailName.Location = new Point(124, 16);
            lblProfileDetailName.Name = "lblProfileDetailName";
            lblProfileDetailName.Size = new Size(64, 37);
            lblProfileDetailName.TabIndex = 1;
            lblProfileDetailName.Text = "huy";
            // 
            // lblEditNameIcon
            // 
            lblEditNameIcon.BackColor = Color.Transparent;
            lblEditNameIcon.Cursor = Cursors.Hand;
            lblEditNameIcon.Icon = CyberIcon.Edit;
            lblEditNameIcon.IconColor = Color.FromArgb(0, 194, 255);
            lblEditNameIcon.Location = new Point(175, 20);
            lblEditNameIcon.Name = "lblEditNameIcon";
            lblEditNameIcon.Size = new Size(22, 22);
            lblEditNameIcon.TabIndex = 2;
            // 
            // lblProfileDetailElo
            // 
            lblProfileDetailElo.AutoSize = true;
            lblProfileDetailElo.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblProfileDetailElo.ForeColor = Color.FromArgb(0, 194, 255);
            lblProfileDetailElo.Location = new Point(126, 48);
            lblProfileDetailElo.Name = "lblProfileDetailElo";
            lblProfileDetailElo.Size = new Size(111, 25);
            lblProfileDetailElo.TabIndex = 3;
            lblProfileDetailElo.Text = "★ 1053 ELO";
            // 
            // lblProfileDetailRank
            // 
            lblProfileDetailRank.AutoSize = true;
            lblProfileDetailRank.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProfileDetailRank.ForeColor = Color.FromArgb(255, 205, 50);
            lblProfileDetailRank.Location = new Point(126, 71);
            lblProfileDetailRank.Name = "lblProfileDetailRank";
            lblProfileDetailRank.Size = new Size(95, 23);
            lblProfileDetailRank.TabIndex = 4;
            lblProfileDetailRank.Text = "👑 Tập Sự";
            // 
            // lblProfileDetailStatus
            // 
            lblProfileDetailStatus.AutoSize = true;
            lblProfileDetailStatus.Font = new Font("Segoe UI", 8.5F);
            lblProfileDetailStatus.ForeColor = Color.FromArgb(0, 230, 118);
            lblProfileDetailStatus.Location = new Point(126, 96);
            lblProfileDetailStatus.Name = "lblProfileDetailStatus";
            lblProfileDetailStatus.Size = new Size(333, 20);
            lblProfileDetailStatus.TabIndex = 5;
            lblProfileDetailStatus.Text = "\U0001f7e2 Trực tuyến • Máy chủ LAN (192.168.1.22:8888)";
            // 
            // kpiTotal
            // 
            kpiTotal.IconType = KpiIconType.Trophy;
            kpiTotal.LabelText = "Tổng trận";
            kpiTotal.Location = new Point(400, 22);
            kpiTotal.Name = "kpiTotal";
            kpiTotal.ParentBgColor = Color.FromArgb(12, 24, 52);
            kpiTotal.Size = new Size(68, 86);
            kpiTotal.TabIndex = 6;
            kpiTotal.ValueText = "0";
            // 
            // kpiWins
            // 
            kpiWins.IconType = KpiIconType.Checkmark;
            kpiWins.LabelText = "Thắng";
            kpiWins.Location = new Point(478, 22);
            kpiWins.Name = "kpiWins";
            kpiWins.ParentBgColor = Color.FromArgb(12, 24, 52);
            kpiWins.Size = new Size(68, 86);
            kpiWins.TabIndex = 7;
            kpiWins.ValueText = "0";
            // 
            // kpiLosses
            // 
            kpiLosses.IconType = KpiIconType.Cross;
            kpiLosses.LabelText = "Thua";
            kpiLosses.Location = new Point(556, 22);
            kpiLosses.Name = "kpiLosses";
            kpiLosses.ParentBgColor = Color.FromArgb(12, 24, 52);
            kpiLosses.Size = new Size(68, 86);
            kpiLosses.TabIndex = 8;
            kpiLosses.ValueText = "0";
            // 
            // gaugeWinRate
            // 
            gaugeWinRate.GaugeColor = Color.FromArgb(0, 210, 255);
            gaugeWinRate.Location = new Point(640, 16);
            gaugeWinRate.Name = "gaugeWinRate";
            gaugeWinRate.ParentBgColor = Color.FromArgb(12, 24, 52);
            gaugeWinRate.Size = new Size(86, 96);
            gaugeWinRate.TabIndex = 9;
            gaugeWinRate.Title = "Tỉ lệ thắng";
            gaugeWinRate.WinRate = 0;
            // 
            // pnlLeaderboardView
            // 
            pnlLeaderboardView.Controls.Add(cardLeaderboardBox);
            pnlLeaderboardView.Dock = DockStyle.Fill;
            pnlLeaderboardView.Location = new Point(0, 0);
            pnlLeaderboardView.Name = "pnlLeaderboardView";
            pnlLeaderboardView.Padding = new Padding(20);
            pnlLeaderboardView.Size = new Size(750, 732);
            pnlLeaderboardView.TabIndex = 5;
            pnlLeaderboardView.Resize += pnlLeaderboardView_Resize;
            // 
            // cardLeaderboardBox
            // 
            cardLeaderboardBox.BackColor = Color.Transparent;
            cardLeaderboardBox.BorderColor = Color.FromArgb(0, 194, 255);
            cardLeaderboardBox.BorderWidth = 1.2F;
            cardLeaderboardBox.Controls.Add(lblNoLeaderboard);
            cardLeaderboardBox.Controls.Add(dgvLeaderboard);
            cardLeaderboardBox.Controls.Add(leaderboardList);
            cardLeaderboardBox.Controls.Add(pnlLeaderboardSubHeader);
            cardLeaderboardBox.Controls.Add(leaderboardPodium);
            cardLeaderboardBox.Controls.Add(pnlLeaderboardHeaderBar);
            cardLeaderboardBox.CornerRadius = 14;
            cardLeaderboardBox.Dock = DockStyle.Fill;
            cardLeaderboardBox.FillColor = Color.FromArgb(10, 20, 36);
            cardLeaderboardBox.IsTechCircuitFrame = true;
            cardLeaderboardBox.Location = new Point(20, 20);
            cardLeaderboardBox.Name = "cardLeaderboardBox";
            cardLeaderboardBox.Padding = new Padding(16, 12, 16, 14);
            cardLeaderboardBox.Size = new Size(710, 692);
            cardLeaderboardBox.TabIndex = 0;
            // 
            // lblNoLeaderboard
            // 
            lblNoLeaderboard.Dock = DockStyle.Fill;
            lblNoLeaderboard.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblNoLeaderboard.ForeColor = Color.FromArgb(120, 140, 175);
            lblNoLeaderboard.Location = new Point(16, 52);
            lblNoLeaderboard.Name = "lblNoLeaderboard";
            lblNoLeaderboard.Size = new Size(678, 626);
            lblNoLeaderboard.TabIndex = 0;
            lblNoLeaderboard.Text = "Chưa có dữ liệu bảng xếp hạng.";
            lblNoLeaderboard.TextAlign = ContentAlignment.MiddleCenter;
            lblNoLeaderboard.Visible = false;
            // 
            // dgvLeaderboard
            // 
            dgvLeaderboard.AllowUserToAddRows = false;
            dgvLeaderboard.AllowUserToDeleteRows = false;
            dgvLeaderboard.AllowUserToResizeRows = false;
            dgvLeaderboard.BackgroundColor = Color.FromArgb(12, 22, 44);
            dgvLeaderboard.BorderStyle = BorderStyle.None;
            dgvLeaderboard.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLeaderboard.ColumnHeadersHeight = 32;
            dgvLeaderboard.Dock = DockStyle.Fill;
            dgvLeaderboard.EnableHeadersVisualStyles = false;
            dgvLeaderboard.GridColor = Color.FromArgb(20, 36, 70);
            dgvLeaderboard.Location = new Point(16, 52);
            dgvLeaderboard.Name = "dgvLeaderboard";
            dgvLeaderboard.ReadOnly = true;
            dgvLeaderboard.RowHeadersVisible = false;
            dgvLeaderboard.RowHeadersWidth = 51;
            dgvLeaderboard.RowTemplate.Height = 36;
            dgvLeaderboard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLeaderboard.Size = new Size(678, 626);
            dgvLeaderboard.TabIndex = 1;
            dgvLeaderboard.Visible = false;
            // 
            // leaderboardList
            // 
            leaderboardList.BackColor = Color.Transparent;
            leaderboardList.Dock = DockStyle.Fill;
            leaderboardList.Location = new Point(16, 324);
            leaderboardList.Name = "leaderboardList";
            leaderboardList.Size = new Size(678, 354);
            leaderboardList.TabIndex = 5;
            // 
            // pnlLeaderboardSubHeader
            // 
            pnlLeaderboardSubHeader.BackColor = Color.Transparent;
            pnlLeaderboardSubHeader.Controls.Add(btnRefreshLeaderboardList);
            pnlLeaderboardSubHeader.Controls.Add(lblLeaderboardSubHeader);
            pnlLeaderboardSubHeader.Dock = DockStyle.Top;
            pnlLeaderboardSubHeader.Location = new Point(16, 288);
            pnlLeaderboardSubHeader.Name = "pnlLeaderboardSubHeader";
            pnlLeaderboardSubHeader.Size = new Size(678, 36);
            pnlLeaderboardSubHeader.TabIndex = 4;
            // 
            // btnRefreshLeaderboardList
            // 
            btnRefreshLeaderboardList.Cursor = Cursors.Hand;
            btnRefreshLeaderboardList.Dock = DockStyle.Right;
            btnRefreshLeaderboardList.FlatAppearance.BorderSize = 0;
            btnRefreshLeaderboardList.FlatStyle = FlatStyle.Flat;
            btnRefreshLeaderboardList.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnRefreshLeaderboardList.ForeColor = Color.FromArgb(0, 194, 255);
            btnRefreshLeaderboardList.Location = new Point(568, 0);
            btnRefreshLeaderboardList.Name = "btnRefreshLeaderboardList";
            btnRefreshLeaderboardList.Size = new Size(110, 36);
            btnRefreshLeaderboardList.TabIndex = 1;
            btnRefreshLeaderboardList.Text = "🔄 Làm mới";
            btnRefreshLeaderboardList.Click += btnRefreshLeaderboard_Click;
            // 
            // lblLeaderboardSubHeader
            // 
            lblLeaderboardSubHeader.AutoSize = true;
            lblLeaderboardSubHeader.Dock = DockStyle.Left;
            lblLeaderboardSubHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblLeaderboardSubHeader.ForeColor = Color.FromArgb(160, 185, 220);
            lblLeaderboardSubHeader.Location = new Point(0, 0);
            lblLeaderboardSubHeader.Name = "lblLeaderboardSubHeader";
            lblLeaderboardSubHeader.Size = new Size(180, 25);
            lblLeaderboardSubHeader.TabIndex = 0;
            lblLeaderboardSubHeader.Text = "Danh sách thứ hạng";
            lblLeaderboardSubHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // leaderboardPodium
            // 
            leaderboardPodium.BackColor = Color.Transparent;
            leaderboardPodium.Dock = DockStyle.Top;
            leaderboardPodium.Location = new Point(16, 48);
            leaderboardPodium.Name = "leaderboardPodium";
            leaderboardPodium.Size = new Size(678, 260);
            leaderboardPodium.TabIndex = 3;
            // 
            // pnlLeaderboardHeaderBar
            // 
            pnlLeaderboardHeaderBar.BackColor = Color.Transparent;
            pnlLeaderboardHeaderBar.Controls.Add(btnRefreshLeaderboard);
            pnlLeaderboardHeaderBar.Controls.Add(lblLeaderboardHeader);
            pnlLeaderboardHeaderBar.Dock = DockStyle.Top;
            pnlLeaderboardHeaderBar.Location = new Point(16, 12);
            pnlLeaderboardHeaderBar.Name = "pnlLeaderboardHeaderBar";
            pnlLeaderboardHeaderBar.Size = new Size(678, 36);
            pnlLeaderboardHeaderBar.TabIndex = 2;
            // 
            // btnRefreshLeaderboard
            // 
            btnRefreshLeaderboard.Cursor = Cursors.Hand;
            btnRefreshLeaderboard.Dock = DockStyle.Right;
            btnRefreshLeaderboard.FlatAppearance.BorderSize = 0;
            btnRefreshLeaderboard.FlatStyle = FlatStyle.Flat;
            btnRefreshLeaderboard.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnRefreshLeaderboard.ForeColor = Color.FromArgb(0, 194, 255);
            btnRefreshLeaderboard.Location = new Point(568, 0);
            btnRefreshLeaderboard.Name = "btnRefreshLeaderboard";
            btnRefreshLeaderboard.Size = new Size(110, 36);
            btnRefreshLeaderboard.TabIndex = 0;
            btnRefreshLeaderboard.Text = "🔄 Làm mới";
            btnRefreshLeaderboard.Click += btnRefreshLeaderboard_Click;
            // 
            // lblLeaderboardHeader
            // 
            lblLeaderboardHeader.AutoSize = true;
            lblLeaderboardHeader.Dock = DockStyle.Left;
            lblLeaderboardHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblLeaderboardHeader.ForeColor = Color.FromArgb(255, 215, 0);
            lblLeaderboardHeader.Location = new Point(0, 0);
            lblLeaderboardHeader.Name = "lblLeaderboardHeader";
            lblLeaderboardHeader.Size = new Size(350, 25);
            lblLeaderboardHeader.TabIndex = 1;
            lblLeaderboardHeader.Text = "BẢNG XẾP HẠNG CAO THỦ (TOP ELO)";
            lblLeaderboardHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlSettingsView
            // 
            pnlSettingsView.BackColor = Color.FromArgb(8, 13, 26);
            pnlSettingsView.Controls.Add(cardSettingsBox);
            pnlSettingsView.Dock = DockStyle.Fill;
            pnlSettingsView.Location = new Point(0, 0);
            pnlSettingsView.Name = "pnlSettingsView";
            pnlSettingsView.Size = new Size(750, 732);
            pnlSettingsView.TabIndex = 6;
            pnlSettingsView.Resize += pnlSettingsView_Resize;
            // 
            // cardSettingsBox
            // 
            cardSettingsBox.BackColor = Color.FromArgb(11, 20, 36);
            cardSettingsBox.Controls.Add(btnTabLogin);
            cardSettingsBox.Controls.Add(btnTabRegister);
            cardSettingsBox.Controls.Add(pnlAuthBodyLogin);
            cardSettingsBox.Controls.Add(pnlAuthBodyRegister);
            cardSettingsBox.Location = new Point(212, 110);
            cardSettingsBox.Name = "cardSettingsBox";
            cardSettingsBox.Size = new Size(420, 412);
            cardSettingsBox.TabIndex = 0;
            // 
            // btnTabLogin
            // 
            btnTabLogin.BackColor = Color.FromArgb(9, 15, 30);
            btnTabLogin.BorderLineColor = Color.FromArgb(0, 194, 255);
            btnTabLogin.ClickColor = Color.FromArgb(10, 18, 36);
            btnTabLogin.CornerRadius = 8;
            btnTabLogin.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnTabLogin.FlatStyle = FlatStyle.Flat;
            btnTabLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTabLogin.ForeColor = Color.FromArgb(0, 230, 255);
            btnTabLogin.HoverColor = Color.FromArgb(24, 42, 80);
            btnTabLogin.IsPrimaryGradient = false;
            btnTabLogin.IsPurpleGradient = false;
            btnTabLogin.Location = new Point(25, 20);
            btnTabLogin.Name = "btnTabLogin";
            btnTabLogin.NavIcon = CyberIcon.None;
            btnTabLogin.NormalColor = Color.FromArgb(14, 42, 68);
            btnTabLogin.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnTabLogin.Size = new Size(180, 34);
            btnTabLogin.TabIndex = 0;
            btnTabLogin.Text = "🔍  ĐĂNG NHẬP";
            btnTabLogin.TextLeftPadding = 16;
            btnTabLogin.UseVisualStyleBackColor = false;
            btnTabLogin.Click += btnTabLogin_Click;
            // 
            // btnTabRegister
            // 
            btnTabRegister.BackColor = Color.FromArgb(9, 15, 30);
            btnTabRegister.BorderLineColor = Color.FromArgb(180, 80, 30);
            btnTabRegister.ClickColor = Color.FromArgb(10, 18, 36);
            btnTabRegister.CornerRadius = 8;
            btnTabRegister.CustomTextAlign = ContentAlignment.MiddleLeft;
            btnTabRegister.FlatStyle = FlatStyle.Flat;
            btnTabRegister.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTabRegister.ForeColor = Color.FromArgb(220, 150, 110);
            btnTabRegister.HoverColor = Color.FromArgb(24, 42, 80);
            btnTabRegister.IsPrimaryGradient = false;
            btnTabRegister.IsPurpleGradient = false;
            btnTabRegister.Location = new Point(215, 20);
            btnTabRegister.Name = "btnTabRegister";
            btnTabRegister.NavIcon = CyberIcon.None;
            btnTabRegister.NormalColor = Color.FromArgb(32, 20, 16);
            btnTabRegister.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnTabRegister.Size = new Size(180, 34);
            btnTabRegister.TabIndex = 1;
            btnTabRegister.Text = "✏  ĐĂNG KÝ MỚI";
            btnTabRegister.TextLeftPadding = 16;
            btnTabRegister.UseVisualStyleBackColor = false;
            btnTabRegister.Click += btnTabRegister_Click;
            // 
            // pnlAuthBodyLogin
            // 
            pnlAuthBodyLogin.BackColor = Color.Transparent;
            pnlAuthBodyLogin.Controls.Add(lblSettingsTitle);
            pnlAuthBodyLogin.Controls.Add(lblSettingsNotice);
            pnlAuthBodyLogin.Controls.Add(lblServerIp);
            pnlAuthBodyLogin.Controls.Add(lblPort);
            pnlAuthBodyLogin.Controls.Add(pnlIpWrapper);
            pnlAuthBodyLogin.Controls.Add(pnlPortWrapper);
            pnlAuthBodyLogin.Controls.Add(pnlUserWrapper);
            pnlAuthBodyLogin.Controls.Add(pnlPassWrapper);
            pnlAuthBodyLogin.Controls.Add(lblStatusBadge);
            pnlAuthBodyLogin.Controls.Add(btnConnect);
            pnlAuthBodyLogin.Controls.Add(lblSwitchToRegister);
            pnlAuthBodyLogin.Controls.Add(lblOfflineTip);
            pnlAuthBodyLogin.Location = new Point(25, 62);
            pnlAuthBodyLogin.Name = "pnlAuthBodyLogin";
            pnlAuthBodyLogin.Size = new Size(370, 330);
            pnlAuthBodyLogin.TabIndex = 2;
            // 
            // lblSettingsTitle
            // 
            lblSettingsTitle.AutoSize = true;
            lblSettingsTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSettingsTitle.ForeColor = Color.White;
            lblSettingsTitle.Location = new Point(0, 0);
            lblSettingsTitle.Name = "lblSettingsTitle";
            lblSettingsTitle.Size = new Size(314, 25);
            lblSettingsTitle.TabIndex = 0;
            lblSettingsTitle.Text = "ĐĂNG NHẬP HỆ THỐNG (PvP LAN)";
            // 
            // lblSettingsNotice
            // 
            lblSettingsNotice.Font = new Font("Segoe UI", 7.5F);
            lblSettingsNotice.ForeColor = Color.FromArgb(140, 165, 200);
            lblSettingsNotice.Location = new Point(0, 24);
            lblSettingsNotice.Name = "lblSettingsNotice";
            lblSettingsNotice.Size = new Size(370, 24);
            lblSettingsNotice.TabIndex = 1;
            lblSettingsNotice.Text = "Đăng nhập tài khoản để vào phòng thi đấu 2 người, lưu điểm ELO và xếp hạng";
            // 
            // lblServerIp
            // 
            lblServerIp.AutoSize = true;
            lblServerIp.Font = new Font("Segoe UI", 7.5F);
            lblServerIp.ForeColor = Color.FromArgb(140, 165, 195);
            lblServerIp.Location = new Point(0, 50);
            lblServerIp.Name = "lblServerIp";
            lblServerIp.Size = new Size(119, 17);
            lblServerIp.TabIndex = 2;
            lblServerIp.Text = "1. Địa chỉ Server IP:";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("Segoe UI", 7.5F);
            lblPort.ForeColor = Color.FromArgb(140, 165, 195);
            lblPort.Location = new Point(245, 50);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(35, 17);
            lblPort.TabIndex = 3;
            lblPort.Text = "Port:";
            // 
            // pnlIpWrapper
            // 
            pnlIpWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlIpWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlIpWrapper.BorderWidth = 1.2F;
            pnlIpWrapper.Controls.Add(lblIpPrefix);
            pnlIpWrapper.Controls.Add(txtIp);
            pnlIpWrapper.CornerRadius = 14;
            pnlIpWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlIpWrapper.Location = new Point(0, 68);
            pnlIpWrapper.Name = "pnlIpWrapper";
            pnlIpWrapper.Padding = new Padding(12);
            pnlIpWrapper.Size = new Size(235, 34);
            pnlIpWrapper.TabIndex = 4;
            // 
            // lblIpPrefix
            // 
            lblIpPrefix.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblIpPrefix.ForeColor = Color.FromArgb(0, 194, 255);
            lblIpPrefix.Location = new Point(8, 8);
            lblIpPrefix.Name = "lblIpPrefix";
            lblIpPrefix.Size = new Size(22, 18);
            lblIpPrefix.TabIndex = 0;
            lblIpPrefix.Text = "IP";
            lblIpPrefix.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtIp
            // 
            txtIp.BackColor = Color.FromArgb(10, 18, 34);
            txtIp.BorderStyle = BorderStyle.None;
            txtIp.Font = new Font("Segoe UI", 9F);
            txtIp.ForeColor = Color.White;
            txtIp.Location = new Point(34, 8);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(192, 20);
            txtIp.TabIndex = 1;
            txtIp.Text = "127.0.0.1";
            // 
            // pnlPortWrapper
            // 
            pnlPortWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlPortWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlPortWrapper.BorderWidth = 1.2F;
            pnlPortWrapper.Controls.Add(txtPort);
            pnlPortWrapper.CornerRadius = 14;
            pnlPortWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlPortWrapper.Location = new Point(245, 68);
            pnlPortWrapper.Name = "pnlPortWrapper";
            pnlPortWrapper.Padding = new Padding(12);
            pnlPortWrapper.Size = new Size(125, 34);
            pnlPortWrapper.TabIndex = 5;
            // 
            // txtPort
            // 
            txtPort.BackColor = Color.FromArgb(10, 18, 34);
            txtPort.BorderStyle = BorderStyle.None;
            txtPort.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtPort.ForeColor = Color.FromArgb(0, 194, 255);
            txtPort.Location = new Point(12, 8);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(100, 20);
            txtPort.TabIndex = 0;
            txtPort.Text = "8888";
            // 
            // pnlUserWrapper
            // 
            pnlUserWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlUserWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlUserWrapper.BorderWidth = 1.2F;
            pnlUserWrapper.Controls.Add(lblUserPrefix);
            pnlUserWrapper.Controls.Add(txtName);
            pnlUserWrapper.CornerRadius = 14;
            pnlUserWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlUserWrapper.Location = new Point(0, 110);
            pnlUserWrapper.Name = "pnlUserWrapper";
            pnlUserWrapper.Padding = new Padding(12);
            pnlUserWrapper.Size = new Size(370, 34);
            pnlUserWrapper.TabIndex = 6;
            // 
            // lblUserPrefix
            // 
            lblUserPrefix.BackColor = Color.Transparent;
            lblUserPrefix.Icon = CyberIcon.User;
            lblUserPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblUserPrefix.Location = new Point(10, 8);
            lblUserPrefix.Name = "lblUserPrefix";
            lblUserPrefix.Size = new Size(18, 18);
            lblUserPrefix.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(10, 18, 34);
            txtName.BorderStyle = BorderStyle.None;
            txtName.Font = new Font("Segoe UI", 9F);
            txtName.ForeColor = Color.White;
            txtName.Location = new Point(36, 8);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Tên đăng nhập (Player1)...";
            txtName.Size = new Size(325, 20);
            txtName.TabIndex = 1;
            txtName.Text = "Player1";
            // 
            // pnlPassWrapper
            // 
            pnlPassWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlPassWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlPassWrapper.BorderWidth = 1.2F;
            pnlPassWrapper.Controls.Add(lblPassPrefix);
            pnlPassWrapper.Controls.Add(txtPassword);
            pnlPassWrapper.Controls.Add(lblTogglePassword);
            pnlPassWrapper.CornerRadius = 14;
            pnlPassWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlPassWrapper.Location = new Point(0, 152);
            pnlPassWrapper.Name = "pnlPassWrapper";
            pnlPassWrapper.Padding = new Padding(12);
            pnlPassWrapper.Size = new Size(370, 34);
            pnlPassWrapper.TabIndex = 7;
            // 
            // lblPassPrefix
            // 
            lblPassPrefix.BackColor = Color.Transparent;
            lblPassPrefix.Icon = CyberIcon.Lock;
            lblPassPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblPassPrefix.Location = new Point(10, 8);
            lblPassPrefix.Name = "lblPassPrefix";
            lblPassPrefix.Size = new Size(18, 18);
            lblPassPrefix.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(10, 18, 34);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 9F);
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(36, 8);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "Mật khẩu...";
            txtPassword.Size = new Size(295, 20);
            txtPassword.TabIndex = 1;
            // 
            // lblTogglePassword
            // 
            lblTogglePassword.BackColor = Color.Transparent;
            lblTogglePassword.Cursor = Cursors.Hand;
            lblTogglePassword.Icon = CyberIcon.Eye;
            lblTogglePassword.IconColor = Color.FromArgb(140, 170, 215);
            lblTogglePassword.Location = new Point(340, 8);
            lblTogglePassword.Name = "lblTogglePassword";
            lblTogglePassword.Size = new Size(22, 18);
            lblTogglePassword.TabIndex = 2;
            lblTogglePassword.Click += lblTogglePassword_Click;
            // 
            // lblStatusBadge
            // 
            lblStatusBadge.AutoSize = true;
            lblStatusBadge.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblStatusBadge.ForeColor = Color.FromArgb(255, 75, 75);
            lblStatusBadge.Location = new Point(0, 192);
            lblStatusBadge.Name = "lblStatusBadge";
            lblStatusBadge.Size = new Size(105, 19);
            lblStatusBadge.TabIndex = 8;
            lblStatusBadge.Text = "● Chưa kết nối";
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(9, 15, 30);
            btnConnect.BorderLineColor = Color.FromArgb(192, 38, 211);
            btnConnect.ClickColor = Color.FromArgb(10, 18, 36);
            btnConnect.CornerRadius = 10;
            btnConnect.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConnect.ForeColor = Color.White;
            btnConnect.HoverColor = Color.FromArgb(24, 42, 80);
            btnConnect.IsPrimaryGradient = false;
            btnConnect.IsPurpleGradient = true;
            btnConnect.Location = new Point(0, 214);
            btnConnect.Name = "btnConnect";
            btnConnect.NavIcon = CyberIcon.Lightning;
            btnConnect.NormalColor = Color.FromArgb(14, 24, 46);
            btnConnect.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnConnect.Size = new Size(370, 40);
            btnConnect.TabIndex = 9;
            btnConnect.Text = "KẾT NỐI & ĐĂNG NHẬP";
            btnConnect.TextLeftPadding = 16;
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // lblSwitchToRegister
            // 
            lblSwitchToRegister.Cursor = Cursors.Hand;
            lblSwitchToRegister.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSwitchToRegister.ForeColor = Color.FromArgb(0, 194, 255);
            lblSwitchToRegister.Location = new Point(0, 260);
            lblSwitchToRegister.Name = "lblSwitchToRegister";
            lblSwitchToRegister.Size = new Size(370, 20);
            lblSwitchToRegister.TabIndex = 10;
            lblSwitchToRegister.Text = "Chưa có tài khoản? Nhấn vào đây để Đăng ký ngay →";
            lblSwitchToRegister.TextAlign = ContentAlignment.MiddleCenter;
            lblSwitchToRegister.Click += lblSwitchToRegister_Click;
            // 
            // lblOfflineTip
            // 
            lblOfflineTip.Font = new Font("Segoe UI", 7.25F, FontStyle.Italic);
            lblOfflineTip.ForeColor = Color.FromArgb(0, 230, 118);
            lblOfflineTip.Location = new Point(0, 284);
            lblOfflineTip.Name = "lblOfflineTip";
            lblOfflineTip.Size = new Size(370, 20);
            lblOfflineTip.TabIndex = 11;
            lblOfflineTip.Text = "💡 Mẹo: Chơi với Máy (AI) hoàn toàn Offline, không cần mạng hay đăng nhập!";
            lblOfflineTip.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlAuthBodyRegister
            // 
            pnlAuthBodyRegister.BackColor = Color.Transparent;
            pnlAuthBodyRegister.Controls.Add(lblRegTitle);
            pnlAuthBodyRegister.Controls.Add(lblRegNotice);
            pnlAuthBodyRegister.Controls.Add(lblRegServerIp);
            pnlAuthBodyRegister.Controls.Add(lblRegPort);
            pnlAuthBodyRegister.Controls.Add(pnlRegIpWrapper);
            pnlAuthBodyRegister.Controls.Add(pnlRegPortWrapper);
            pnlAuthBodyRegister.Controls.Add(pnlRegUserWrapper);
            pnlAuthBodyRegister.Controls.Add(pnlRegPassWrapper);
            pnlAuthBodyRegister.Controls.Add(pnlRegConfirmPassWrapper);
            pnlAuthBodyRegister.Controls.Add(lblRegStatusBadge);
            pnlAuthBodyRegister.Controls.Add(btnRegister);
            pnlAuthBodyRegister.Controls.Add(lblSwitchToLogin);
            pnlAuthBodyRegister.Controls.Add(lblRegOfflineTip);
            pnlAuthBodyRegister.Location = new Point(25, 62);
            pnlAuthBodyRegister.Name = "pnlAuthBodyRegister";
            pnlAuthBodyRegister.Size = new Size(370, 335);
            pnlAuthBodyRegister.TabIndex = 3;
            pnlAuthBodyRegister.Visible = false;
            // 
            // lblRegTitle
            // 
            lblRegTitle.AutoSize = true;
            lblRegTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblRegTitle.ForeColor = Color.White;
            lblRegTitle.Location = new Point(0, 0);
            lblRegTitle.Name = "lblRegTitle";
            lblRegTitle.Size = new Size(290, 25);
            lblRegTitle.TabIndex = 0;
            lblRegTitle.Text = "TẠO TÀI KHOẢN MỚI (PvP LAN)";
            // 
            // lblRegNotice
            // 
            lblRegNotice.Font = new Font("Segoe UI", 7.5F);
            lblRegNotice.ForeColor = Color.FromArgb(140, 165, 200);
            lblRegNotice.Location = new Point(0, 22);
            lblRegNotice.Name = "lblRegNotice";
            lblRegNotice.Size = new Size(370, 22);
            lblRegNotice.TabIndex = 1;
            lblRegNotice.Text = "Tài khoản sẽ được lưu an toàn tại Database máy chủ Server.";
            // 
            // lblRegServerIp
            // 
            lblRegServerIp.AutoSize = true;
            lblRegServerIp.Font = new Font("Segoe UI", 7.5F);
            lblRegServerIp.ForeColor = Color.FromArgb(140, 165, 195);
            lblRegServerIp.Location = new Point(0, 46);
            lblRegServerIp.Name = "lblRegServerIp";
            lblRegServerIp.Size = new Size(119, 17);
            lblRegServerIp.TabIndex = 2;
            lblRegServerIp.Text = "1. Địa chỉ Server IP:";
            // 
            // lblRegPort
            // 
            lblRegPort.AutoSize = true;
            lblRegPort.Font = new Font("Segoe UI", 7.5F);
            lblRegPort.ForeColor = Color.FromArgb(140, 165, 195);
            lblRegPort.Location = new Point(245, 46);
            lblRegPort.Name = "lblRegPort";
            lblRegPort.Size = new Size(35, 17);
            lblRegPort.TabIndex = 3;
            lblRegPort.Text = "Port:";
            // 
            // pnlRegIpWrapper
            // 
            pnlRegIpWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlRegIpWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlRegIpWrapper.BorderWidth = 1.2F;
            pnlRegIpWrapper.Controls.Add(lblRegIpPrefix);
            pnlRegIpWrapper.Controls.Add(txtRegIp);
            pnlRegIpWrapper.CornerRadius = 14;
            pnlRegIpWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlRegIpWrapper.Location = new Point(0, 64);
            pnlRegIpWrapper.Name = "pnlRegIpWrapper";
            pnlRegIpWrapper.Padding = new Padding(12);
            pnlRegIpWrapper.Size = new Size(235, 32);
            pnlRegIpWrapper.TabIndex = 4;
            // 
            // lblRegIpPrefix
            // 
            lblRegIpPrefix.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblRegIpPrefix.ForeColor = Color.FromArgb(0, 194, 255);
            lblRegIpPrefix.Location = new Point(8, 7);
            lblRegIpPrefix.Name = "lblRegIpPrefix";
            lblRegIpPrefix.Size = new Size(22, 18);
            lblRegIpPrefix.TabIndex = 0;
            lblRegIpPrefix.Text = "IP";
            lblRegIpPrefix.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtRegIp
            // 
            txtRegIp.BackColor = Color.FromArgb(10, 18, 34);
            txtRegIp.BorderStyle = BorderStyle.None;
            txtRegIp.Font = new Font("Segoe UI", 9F);
            txtRegIp.ForeColor = Color.White;
            txtRegIp.Location = new Point(34, 7);
            txtRegIp.Name = "txtRegIp";
            txtRegIp.Size = new Size(192, 20);
            txtRegIp.TabIndex = 1;
            txtRegIp.Text = "127.0.0.1";
            // 
            // pnlRegPortWrapper
            // 
            pnlRegPortWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlRegPortWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlRegPortWrapper.BorderWidth = 1.2F;
            pnlRegPortWrapper.Controls.Add(txtRegPort);
            pnlRegPortWrapper.CornerRadius = 14;
            pnlRegPortWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlRegPortWrapper.Location = new Point(245, 64);
            pnlRegPortWrapper.Name = "pnlRegPortWrapper";
            pnlRegPortWrapper.Padding = new Padding(12);
            pnlRegPortWrapper.Size = new Size(125, 32);
            pnlRegPortWrapper.TabIndex = 5;
            // 
            // txtRegPort
            // 
            txtRegPort.BackColor = Color.FromArgb(10, 18, 34);
            txtRegPort.BorderStyle = BorderStyle.None;
            txtRegPort.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtRegPort.ForeColor = Color.FromArgb(0, 194, 255);
            txtRegPort.Location = new Point(12, 7);
            txtRegPort.Name = "txtRegPort";
            txtRegPort.Size = new Size(100, 20);
            txtRegPort.TabIndex = 0;
            txtRegPort.Text = "8888";
            // 
            // pnlRegUserWrapper
            // 
            pnlRegUserWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlRegUserWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlRegUserWrapper.BorderWidth = 1.2F;
            pnlRegUserWrapper.Controls.Add(lblRegUserPrefix);
            pnlRegUserWrapper.Controls.Add(txtRegName);
            pnlRegUserWrapper.CornerRadius = 14;
            pnlRegUserWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlRegUserWrapper.Location = new Point(0, 102);
            pnlRegUserWrapper.Name = "pnlRegUserWrapper";
            pnlRegUserWrapper.Padding = new Padding(12);
            pnlRegUserWrapper.Size = new Size(370, 32);
            pnlRegUserWrapper.TabIndex = 6;
            // 
            // lblRegUserPrefix
            // 
            lblRegUserPrefix.BackColor = Color.Transparent;
            lblRegUserPrefix.Icon = CyberIcon.User;
            lblRegUserPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblRegUserPrefix.Location = new Point(10, 7);
            lblRegUserPrefix.Name = "lblRegUserPrefix";
            lblRegUserPrefix.Size = new Size(18, 18);
            lblRegUserPrefix.TabIndex = 0;
            // 
            // txtRegName
            // 
            txtRegName.BackColor = Color.FromArgb(10, 18, 34);
            txtRegName.BorderStyle = BorderStyle.None;
            txtRegName.Font = new Font("Segoe UI", 9F);
            txtRegName.ForeColor = Color.White;
            txtRegName.Location = new Point(36, 7);
            txtRegName.Name = "txtRegName";
            txtRegName.PlaceholderText = "Tên tài khoản muốn tạo (Username)...";
            txtRegName.Size = new Size(325, 20);
            txtRegName.TabIndex = 1;
            // 
            // pnlRegPassWrapper
            // 
            pnlRegPassWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlRegPassWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlRegPassWrapper.BorderWidth = 1.2F;
            pnlRegPassWrapper.Controls.Add(lblRegPassPrefix);
            pnlRegPassWrapper.Controls.Add(txtRegPassword);
            pnlRegPassWrapper.Controls.Add(lblToggleRegPass);
            pnlRegPassWrapper.CornerRadius = 14;
            pnlRegPassWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlRegPassWrapper.Location = new Point(0, 140);
            pnlRegPassWrapper.Name = "pnlRegPassWrapper";
            pnlRegPassWrapper.Padding = new Padding(12);
            pnlRegPassWrapper.Size = new Size(370, 32);
            pnlRegPassWrapper.TabIndex = 7;
            // 
            // lblRegPassPrefix
            // 
            lblRegPassPrefix.BackColor = Color.Transparent;
            lblRegPassPrefix.Icon = CyberIcon.Lock;
            lblRegPassPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblRegPassPrefix.Location = new Point(10, 7);
            lblRegPassPrefix.Name = "lblRegPassPrefix";
            lblRegPassPrefix.Size = new Size(18, 18);
            lblRegPassPrefix.TabIndex = 0;
            // 
            // txtRegPassword
            // 
            txtRegPassword.BackColor = Color.FromArgb(10, 18, 34);
            txtRegPassword.BorderStyle = BorderStyle.None;
            txtRegPassword.Font = new Font("Segoe UI", 9F);
            txtRegPassword.ForeColor = Color.White;
            txtRegPassword.Location = new Point(36, 7);
            txtRegPassword.Name = "txtRegPassword";
            txtRegPassword.PasswordChar = '●';
            txtRegPassword.PlaceholderText = "Mật khẩu (tối thiểu 4 ký tự)...";
            txtRegPassword.Size = new Size(295, 20);
            txtRegPassword.TabIndex = 1;
            // 
            // lblToggleRegPass
            // 
            lblToggleRegPass.BackColor = Color.Transparent;
            lblToggleRegPass.Cursor = Cursors.Hand;
            lblToggleRegPass.Icon = CyberIcon.Eye;
            lblToggleRegPass.IconColor = Color.FromArgb(140, 170, 215);
            lblToggleRegPass.Location = new Point(340, 7);
            lblToggleRegPass.Name = "lblToggleRegPass";
            lblToggleRegPass.Size = new Size(20, 18);
            lblToggleRegPass.TabIndex = 2;
            lblToggleRegPass.Click += lblToggleRegPass_Click;
            // 
            // pnlRegConfirmPassWrapper
            // 
            pnlRegConfirmPassWrapper.BackColor = Color.FromArgb(10, 18, 34);
            pnlRegConfirmPassWrapper.BorderColor = Color.FromArgb(25, 45, 85);
            pnlRegConfirmPassWrapper.BorderWidth = 1.2F;
            pnlRegConfirmPassWrapper.Controls.Add(lblRegConfirmPassPrefix);
            pnlRegConfirmPassWrapper.Controls.Add(txtRegConfirmPassword);
            pnlRegConfirmPassWrapper.Controls.Add(lblToggleRegConfirm);
            pnlRegConfirmPassWrapper.CornerRadius = 14;
            pnlRegConfirmPassWrapper.FillColor = Color.FromArgb(13, 24, 48);
            pnlRegConfirmPassWrapper.Location = new Point(0, 178);
            pnlRegConfirmPassWrapper.Name = "pnlRegConfirmPassWrapper";
            pnlRegConfirmPassWrapper.Padding = new Padding(12);
            pnlRegConfirmPassWrapper.Size = new Size(370, 32);
            pnlRegConfirmPassWrapper.TabIndex = 8;
            // 
            // lblRegConfirmPassPrefix
            // 
            lblRegConfirmPassPrefix.BackColor = Color.Transparent;
            lblRegConfirmPassPrefix.Icon = CyberIcon.Lock;
            lblRegConfirmPassPrefix.IconColor = Color.FromArgb(0, 194, 255);
            lblRegConfirmPassPrefix.Location = new Point(10, 7);
            lblRegConfirmPassPrefix.Name = "lblRegConfirmPassPrefix";
            lblRegConfirmPassPrefix.Size = new Size(18, 18);
            lblRegConfirmPassPrefix.TabIndex = 0;
            // 
            // txtRegConfirmPassword
            // 
            txtRegConfirmPassword.BackColor = Color.FromArgb(10, 18, 34);
            txtRegConfirmPassword.BorderStyle = BorderStyle.None;
            txtRegConfirmPassword.Font = new Font("Segoe UI", 9F);
            txtRegConfirmPassword.ForeColor = Color.White;
            txtRegConfirmPassword.Location = new Point(36, 7);
            txtRegConfirmPassword.Name = "txtRegConfirmPassword";
            txtRegConfirmPassword.PasswordChar = '●';
            txtRegConfirmPassword.PlaceholderText = "Xác nhận lại mật khẩu...";
            txtRegConfirmPassword.Size = new Size(295, 20);
            txtRegConfirmPassword.TabIndex = 1;
            // 
            // lblToggleRegConfirm
            // 
            lblToggleRegConfirm.BackColor = Color.Transparent;
            lblToggleRegConfirm.Cursor = Cursors.Hand;
            lblToggleRegConfirm.Icon = CyberIcon.Eye;
            lblToggleRegConfirm.IconColor = Color.FromArgb(140, 170, 215);
            lblToggleRegConfirm.Location = new Point(340, 7);
            lblToggleRegConfirm.Name = "lblToggleRegConfirm";
            lblToggleRegConfirm.Size = new Size(20, 18);
            lblToggleRegConfirm.TabIndex = 2;
            lblToggleRegConfirm.Click += lblToggleRegConfirm_Click;
            // 
            // lblRegStatusBadge
            // 
            lblRegStatusBadge.AutoSize = true;
            lblRegStatusBadge.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblRegStatusBadge.ForeColor = Color.FromArgb(0, 194, 255);
            lblRegStatusBadge.Location = new Point(0, 214);
            lblRegStatusBadge.Name = "lblRegStatusBadge";
            lblRegStatusBadge.Size = new Size(172, 19);
            lblRegStatusBadge.TabIndex = 9;
            lblRegStatusBadge.Text = "● Sẵn sàng tạo tài khoản";
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(9, 15, 30);
            btnRegister.BorderLineColor = Color.Transparent;
            btnRegister.ClickColor = Color.FromArgb(10, 18, 36);
            btnRegister.CornerRadius = 10;
            btnRegister.CustomTextAlign = ContentAlignment.MiddleCenter;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.HoverColor = Color.FromArgb(24, 42, 80);
            btnRegister.IsPrimaryGradient = true;
            btnRegister.IsPurpleGradient = false;
            btnRegister.Location = new Point(0, 234);
            btnRegister.Name = "btnRegister";
            btnRegister.NavIcon = CyberIcon.Star;
            btnRegister.NormalColor = Color.FromArgb(14, 24, 46);
            btnRegister.ParentBgColor = Color.FromArgb(9, 15, 30);
            btnRegister.Size = new Size(370, 38);
            btnRegister.TabIndex = 10;
            btnRegister.Text = "TẠO TÀI KHOẢN & ĐĂNG NHẬP";
            btnRegister.TextLeftPadding = 16;
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblSwitchToLogin
            // 
            lblSwitchToLogin.Cursor = Cursors.Hand;
            lblSwitchToLogin.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSwitchToLogin.ForeColor = Color.FromArgb(0, 194, 255);
            lblSwitchToLogin.Location = new Point(0, 278);
            lblSwitchToLogin.Name = "lblSwitchToLogin";
            lblSwitchToLogin.Size = new Size(370, 18);
            lblSwitchToLogin.TabIndex = 11;
            lblSwitchToLogin.Text = "Đã có tài khoản rồi? Nhấn vào đây để Đăng nhập →";
            lblSwitchToLogin.TextAlign = ContentAlignment.MiddleCenter;
            lblSwitchToLogin.Click += lblSwitchToLogin_Click;
            // 
            // lblRegOfflineTip
            // 
            lblRegOfflineTip.Font = new Font("Segoe UI", 7.25F, FontStyle.Italic);
            lblRegOfflineTip.ForeColor = Color.FromArgb(0, 230, 118);
            lblRegOfflineTip.Location = new Point(0, 300);
            lblRegOfflineTip.Name = "lblRegOfflineTip";
            lblRegOfflineTip.Size = new Size(370, 18);
            lblRegOfflineTip.TabIndex = 12;
            lblRegOfflineTip.Text = "💡 Mẹo: Chơi với Máy (AI) hoàn toàn Offline, không cần mạng hay đăng nhập!";
            lblRegOfflineTip.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTimer
            // 
            lblTimer.Location = new Point(0, 0);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(100, 23);
            lblTimer.TabIndex = 0;
            // 
            // lblPlayerName
            // 
            lblPlayerName.Location = new Point(0, 0);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(100, 23);
            lblPlayerName.TabIndex = 0;
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(0, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 0;
            // 
            // lblRegPlayerName
            // 
            lblRegPlayerName.Location = new Point(0, 0);
            lblRegPlayerName.Name = "lblRegPlayerName";
            lblRegPlayerName.Size = new Size(100, 23);
            lblRegPlayerName.TabIndex = 0;
            // 
            // lblRegPassword
            // 
            lblRegPassword.Location = new Point(0, 0);
            lblRegPassword.Name = "lblRegPassword";
            lblRegPassword.Size = new Size(100, 23);
            lblRegPassword.TabIndex = 0;
            // 
            // lblRegConfirmPassword
            // 
            lblRegConfirmPassword.Location = new Point(0, 0);
            lblRegConfirmPassword.Name = "lblRegConfirmPassword";
            lblRegConfirmPassword.Size = new Size(100, 23);
            lblRegConfirmPassword.TabIndex = 0;
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(8, 13, 26);
            ClientSize = new Size(1260, 780);
            Controls.Add(pnlMainContent);
            Controls.Add(pnlRightBar);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlTitleBar);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1080, 680);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CaroKan - Chơi cờ qua mạng LAN";
            FormClosing += Form1_FormClosing;
            pnlTitleBar.ResumeLayout(false);
            pnlTitleBar.PerformLayout();
            pnlWindowButtons.ResumeLayout(false);
            pnlSidebar.ResumeLayout(false);
            pnlNavList.ResumeLayout(false);
            pnlSidebarBottom.ResumeLayout(false);
            pnlProfileCard.ResumeLayout(false);
            pnlProfileCard.PerformLayout();
            pnlRightBar.ResumeLayout(false);
            cardChat.ResumeLayout(false);
            pnlChatBottom.ResumeLayout(false);
            pnlChatBottom.PerformLayout();
            pnlMainContent.ResumeLayout(false);
            pnlHomeView.ResumeLayout(false);
            pnlMiddleGrid.ResumeLayout(false);
            colEvent.ResumeLayout(false);
            colTopPlayers.ResumeLayout(false);
            colOnlinePlayers.ResumeLayout(false);
            cardConnectedPlayers.ResumeLayout(false);
            pnlOnlineHeaderBar.ResumeLayout(false);
            pnlOnlineHeaderBar.PerformLayout();
            pnlOnlineSearchBar.ResumeLayout(false);
            pnlSearchFriendWrapper.ResumeLayout(false);
            pnlSearchFriendWrapper.PerformLayout();
            cardServerInfo.ResumeLayout(false);
            cardServerInfo.PerformLayout();
            heroBanner.ResumeLayout(false);
            heroBanner.PerformLayout();
            pnlGameView.ResumeLayout(false);
            pnlGameHeader.ResumeLayout(false);
            pnlGameActions.ResumeLayout(false);
            pnlGameInfo.ResumeLayout(false);
            pnlGameInfo.PerformLayout();
            pnlCreateRoomView.ResumeLayout(false);
            cardCreateBox.ResumeLayout(false);
            pnlAiDifficultyOptions.ResumeLayout(false);
            pnlAiDifficultyOptions.PerformLayout();
            pnlTimeLimitOptions.ResumeLayout(false);
            pnlTimeLimitOptions.PerformLayout();
            pnlCreateHeader.ResumeLayout(false);
            pnlCreateHeader.PerformLayout();
            pnlJoinRoomView.ResumeLayout(false);
            pnlLobbyBody.ResumeLayout(false);
            pnlLobbyRightCol.ResumeLayout(false);
            cardFeaturedRoom.ResumeLayout(false);
            cardFeaturedRoom.PerformLayout();
            cardJoinByCode.ResumeLayout(false);
            cardJoinByCode.PerformLayout();
            pnlCodeWrapper.ResumeLayout(false);
            pnlCodeWrapper.PerformLayout();
            cardOpenRooms.ResumeLayout(false);
            pnlOpenRoomsScroll.ResumeLayout(false);
            pnlOpenRoomsHeader.ResumeLayout(false);
            pnlOpenRoomsHeader.PerformLayout();
            pnlLobbyFilterBar.ResumeLayout(false);
            pnlFilterPills.ResumeLayout(false);
            pnlSearchBox.ResumeLayout(false);
            pnlSearchBox.PerformLayout();
            pnlLobbyHeaderBar.ResumeLayout(false);
            pnlLobbyHeaderBar.PerformLayout();
            pnlHistoryView.ResumeLayout(false);
            cardHistoryBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMatchHistory).EndInit();
            pnlHistoryHeaderBar.ResumeLayout(false);
            pnlHistoryHeaderBar.PerformLayout();
            pnlMiddleRow.ResumeLayout(false);
            cardAchievements.ResumeLayout(false);
            cardAchievements.PerformLayout();
            cardQuickStats.ResumeLayout(false);
            cardQuickStats.PerformLayout();
            cardProfileHero.ResumeLayout(false);
            cardProfileHero.PerformLayout();
            pnlLeaderboardView.ResumeLayout(false);
            cardLeaderboardBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).EndInit();
            pnlLeaderboardHeaderBar.ResumeLayout(false);
            pnlLeaderboardHeaderBar.PerformLayout();
            pnlLeaderboardSubHeader.ResumeLayout(false);
            pnlLeaderboardSubHeader.PerformLayout();
            pnlSettingsView.ResumeLayout(false);
            cardSettingsBox.ResumeLayout(false);
            pnlAuthBodyLogin.ResumeLayout(false);
            pnlAuthBodyLogin.PerformLayout();
            pnlIpWrapper.ResumeLayout(false);
            pnlIpWrapper.PerformLayout();
            pnlPortWrapper.ResumeLayout(false);
            pnlPortWrapper.PerformLayout();
            pnlUserWrapper.ResumeLayout(false);
            pnlUserWrapper.PerformLayout();
            pnlPassWrapper.ResumeLayout(false);
            pnlPassWrapper.PerformLayout();
            pnlAuthBodyRegister.ResumeLayout(false);
            pnlAuthBodyRegister.PerformLayout();
            pnlRegIpWrapper.ResumeLayout(false);
            pnlRegIpWrapper.PerformLayout();
            pnlRegPortWrapper.ResumeLayout(false);
            pnlRegPortWrapper.PerformLayout();
            pnlRegUserWrapper.ResumeLayout(false);
            pnlRegUserWrapper.PerformLayout();
            pnlRegPassWrapper.ResumeLayout(false);
            pnlRegPassWrapper.PerformLayout();
            pnlRegConfirmPassWrapper.ResumeLayout(false);
            pnlRegConfirmPassWrapper.PerformLayout();
            ResumeLayout(false);
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
        private System.Windows.Forms.Panel colEvent;
        private System.Windows.Forms.Label lblEventHeader;
        private CaroClient.HomeEventCard cardEvent;

        private System.Windows.Forms.Panel colTopPlayers;
        private System.Windows.Forms.Label lblTopPlayersHeader;
        private CaroClient.HomePodiumPanel cardTopPlayers;

        private System.Windows.Forms.Panel colOnlinePlayers;
        private CaroClient.RoundedCard cardConnectedPlayers;
        private System.Windows.Forms.Panel pnlOnlineHeaderBar;
        private System.Windows.Forms.Label lblHomePlayersHeader;
        private System.Windows.Forms.Label lblOnlineCountBadge;
        private System.Windows.Forms.Panel pnlOnlineSearchBar;
        private System.Windows.Forms.Panel pnlSearchFriendWrapper;
        private System.Windows.Forms.TextBox txtSearchFriend;
        private System.Windows.Forms.ListBox lstConnectedPlayers;
        private System.Windows.Forms.Label lblNoPlayers;

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

        // View 2: Game Board
        private CaroClient.GameArenaBackgroundPanel pnlGameView;
        private CaroClient.RoundedCard pnlGameHeader;
        private System.Windows.Forms.Panel pnlGameActions;
        private CaroClient.CyberButton btnSurrender;
        private CaroClient.CyberButton btnRestart;
        private CaroClient.CyberButton btnLeaveRoom;
        private System.Windows.Forms.Panel pnlGameInfo;
        private System.Windows.Forms.Label lblRoomIdInfo;
        private System.Windows.Forms.Label lblTimer;
        private CaroClient.CyberTimerWidget timerWidget;
        private System.Windows.Forms.Label lblPlayersInfo;
        private System.Windows.Forms.Label lblTurnInfo;
        private CaroClient.BoardControl boardControl1;

        // View 3: Create Room
        private CaroClient.CreateRoomBackgroundPanel pnlCreateRoomView;
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
        private CaroClient.OptionPill pillCountdown1m;
        private CaroClient.OptionPill pillCountdown3m;
        private CaroClient.OptionPill pillCountdown5m;
        private CaroClient.OptionPill pillCountdownCustom;
        private System.Windows.Forms.TextBox txtCustomMinutes;
        private System.Windows.Forms.Label lblCustomMinutesUnit;
        private CaroClient.GameOverModal gameOverModal;
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

        // View 4: Join Room (Sảnh phòng đấu)
        private System.Windows.Forms.Panel pnlJoinRoomView;
        private CaroClient.LobbyBannerPanel pnlLobbyHeaderBar;
        private CaroClient.LobbyHeaderIconBox pnlLobbyIconBox;
        private System.Windows.Forms.Label lblLobbyTitle;
        private System.Windows.Forms.Label lblLobbySubtitle;

        private System.Windows.Forms.Panel pnlLobbyFilterBar;
        private CaroClient.RoundedCard pnlSearchBox;
        private CaroClient.VectorIconLabel lblSearchIcon;
        private System.Windows.Forms.TextBox txtSearchRoom;
        private System.Windows.Forms.Panel pnlFilterPills;
        private CaroClient.LobbyFilterPill pillFilterAll;
        private CaroClient.LobbyFilterPill pillFilter2p;
        private CaroClient.LobbyFilterPill pillFilter4p;
        private CaroClient.LobbyFilterPill pillFilterWaiting;
        private CaroClient.CyberButton btnRefreshLobby;

        private System.Windows.Forms.Panel pnlLobbyBody;
        private CaroClient.RoundedCard cardOpenRooms;
        private System.Windows.Forms.Panel pnlOpenRoomsHeader;
        private System.Windows.Forms.Label lblOpenRoomsTitle;
        private System.Windows.Forms.Label lblOpenRoomsCount;
        private System.Windows.Forms.Panel pnlOpenRoomsScroll;

        private System.Windows.Forms.Panel pnlLobbyRightCol;
        private CaroClient.RoundedCard cardJoinByCode;
        private System.Windows.Forms.Label lblJoinByCodeTitle;
        private CaroClient.RoundedCard pnlCodeWrapper;
        private System.Windows.Forms.Label lblCodeHash;
        private System.Windows.Forms.TextBox txtRoomId;
        private CaroClient.CyberButton btnJoinRoom;
        private System.Windows.Forms.Label lblCodeFootnote;

        private CaroClient.RoundedCard cardFeaturedRoom;
        private System.Windows.Forms.Label lblFeaturedTitle;
        private System.Windows.Forms.Label lblViewAllRooms;
        private CaroClient.MiniBoardPreviewControl miniBoardPreview;
        private System.Windows.Forms.Label lblFeaturedRoomName;
        private System.Windows.Forms.Label lblFeaturedHost;
        private System.Windows.Forms.Label lblFeaturedMode;
        private System.Windows.Forms.Label lblFeaturedStatus;
        private CaroClient.CyberButton btnFeaturedAction;

        private System.Windows.Forms.Label lblLobbyLastUpdated;

        // View 5: Settings / Login
        private CaroClient.SettingsBackgroundPanel pnlSettingsView;
        private CaroClient.CyberLoginCard cardSettingsBox;
        private CaroClient.CyberButton btnTabLogin;
        private CaroClient.CyberButton btnTabRegister;
        private System.Windows.Forms.Panel pnlAuthBodyLogin;
        private System.Windows.Forms.Panel pnlAuthBodyRegister;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Label lblSettingsNotice;
        private System.Windows.Forms.Label lblServerIp;
        private CaroClient.RoundedCard pnlIpWrapper;
        private System.Windows.Forms.Label lblIpPrefix;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label lblPort;
        private CaroClient.RoundedCard pnlPortWrapper;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPlayerName;
        private CaroClient.RoundedCard pnlUserWrapper;
        private CaroClient.VectorIconLabel lblUserPrefix;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPassword;
        private CaroClient.RoundedCard pnlPassWrapper;
        private CaroClient.VectorIconLabel lblPassPrefix;
        private System.Windows.Forms.TextBox txtPassword;
        private CaroClient.VectorIconLabel lblTogglePassword;
        private System.Windows.Forms.Label lblStatusBadge;
        private CaroClient.CyberButton btnConnect;
        private System.Windows.Forms.Label lblSwitchToRegister;
        private System.Windows.Forms.Label lblOfflineTip;

        // Register Tab Controls
        private System.Windows.Forms.Label lblRegTitle;
        private System.Windows.Forms.Label lblRegNotice;
        private System.Windows.Forms.Label lblRegServerIp;
        private CaroClient.RoundedCard pnlRegIpWrapper;
        private System.Windows.Forms.Label lblRegIpPrefix;
        private System.Windows.Forms.TextBox txtRegIp;
        private System.Windows.Forms.Label lblRegPort;
        private CaroClient.RoundedCard pnlRegPortWrapper;
        private System.Windows.Forms.TextBox txtRegPort;
        private System.Windows.Forms.Label lblRegPlayerName;
        private CaroClient.RoundedCard pnlRegUserWrapper;
        private CaroClient.VectorIconLabel lblRegUserPrefix;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.Label lblRegPassword;
        private CaroClient.RoundedCard pnlRegPassWrapper;
        private CaroClient.VectorIconLabel lblRegPassPrefix;
        private System.Windows.Forms.TextBox txtRegPassword;
        private CaroClient.VectorIconLabel lblToggleRegPass;
        private System.Windows.Forms.Label lblRegConfirmPassword;
        private CaroClient.RoundedCard pnlRegConfirmPassWrapper;
        private CaroClient.VectorIconLabel lblRegConfirmPassPrefix;
        private System.Windows.Forms.TextBox txtRegConfirmPassword;
        private CaroClient.VectorIconLabel lblToggleRegConfirm;
        private System.Windows.Forms.Label lblRegStatusBadge;
        private CaroClient.CyberButton btnRegister;
        private System.Windows.Forms.Label lblSwitchToLogin;
        private System.Windows.Forms.Label lblRegOfflineTip;

        // Timer
        private System.Windows.Forms.Timer turnTimer;

        // View 6: Profile & Match History
        private System.Windows.Forms.Panel pnlHistoryView;
        private CaroClient.RoundedCard cardProfileHero;
        private CaroClient.CircularAvatarControl avatarControl;
        private System.Windows.Forms.Label lblProfileDetailName;
        private CaroClient.VectorIconLabel lblEditNameIcon;
        private System.Windows.Forms.Label lblProfileDetailElo;
        private System.Windows.Forms.Label lblProfileDetailRank;
        private System.Windows.Forms.Label lblProfileDetailStatus;
        private CaroClient.KpiStatBox kpiTotal;
        private CaroClient.KpiStatBox kpiWins;
        private CaroClient.KpiStatBox kpiLosses;
        private CaroClient.WinRateDonutGauge gaugeWinRate;
        private System.Windows.Forms.Panel pnlHistorySpacer1;
        private System.Windows.Forms.Panel pnlMiddleRow;
        private CaroClient.RoundedCard cardQuickStats;
        private System.Windows.Forms.Label lblQuickStatsHeader;
        private CaroClient.QuickStatItem quickStatTotal;
        private CaroClient.QuickStatItem quickStatWins;
        private CaroClient.QuickStatItem quickStatElo;
        private System.Windows.Forms.Panel pnlMiddleSpacer;
        private CaroClient.RoundedCard cardAchievements;
        private System.Windows.Forms.Label lblAchievementsHeader;
        private CaroClient.AchievementBadgeControl badgeTanThu;
        private CaroClient.AchievementBadgeControl badgeChienBinh;
        private CaroClient.AchievementBadgeControl badgeCaoThu;
        private CaroClient.AchievementBadgeControl badgeLocked;
        private System.Windows.Forms.Panel pnlHistorySpacer2;
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
        private CaroClient.LeaderboardPodiumControl leaderboardPodium;
        private System.Windows.Forms.Panel pnlLeaderboardSubHeader;
        private System.Windows.Forms.Label lblLeaderboardSubHeader;
        private System.Windows.Forms.Button btnRefreshLeaderboardList;
        private CaroClient.LeaderboardListControl leaderboardList;
        private System.Windows.Forms.DataGridView dgvLeaderboard;
        private System.Windows.Forms.Label lblNoLeaderboard;
    }
}
