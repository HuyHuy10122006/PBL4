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
            this.btnNavSettings = new CaroClient.CyberButton();
            this.pnlProfileCard = new CaroClient.RoundedCard();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.lblProfileLevel = new System.Windows.Forms.Label();
            this.lblProfileXp = new System.Windows.Forms.Label();

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

            // View 5: Settings
            this.pnlSettingsView = new System.Windows.Forms.Panel();
            this.cardSettingsBox = new CaroClient.RoundedCard();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.lblServerIp = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.btnConnect = new CaroClient.CyberButton();

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
            this.pnlSidebar.Controls.Add(this.pnlProfileCard);

            // pnlNavList
            this.pnlNavList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavList.Height = 280;
            this.pnlNavList.BackColor = System.Drawing.Color.Transparent;
            this.pnlNavList.Controls.Add(this.btnNavHome);
            this.pnlNavList.Controls.Add(this.btnNavGame);
            this.pnlNavList.Controls.Add(this.btnNavCreate);
            this.pnlNavList.Controls.Add(this.btnNavJoin);
            this.pnlNavList.Controls.Add(this.btnNavSettings);

            // Nav Buttons with CyberIcon: Crisp, modern vector icons that never glitch or wrap
            this.btnNavHome.Location = new System.Drawing.Point(0, 5);
            this.btnNavHome.Size = new System.Drawing.Size(206, 42);
            this.btnNavHome.Text = "Trang chủ";
            this.btnNavHome.NavIcon = CaroClient.CyberIcon.Home;
            this.btnNavHome.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavHome.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavHome.IsPrimaryGradient = true;
            this.btnNavHome.Click += (s, e) => SwitchToView(this.pnlHomeView, this.btnNavHome);

            this.btnNavGame.Location = new System.Drawing.Point(0, 57);
            this.btnNavGame.Size = new System.Drawing.Size(206, 42);
            this.btnNavGame.Text = "Bàn cờ";
            this.btnNavGame.NavIcon = CaroClient.CyberIcon.Gamepad;
            this.btnNavGame.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavGame.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavGame.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavGame.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavGame.Click += new System.EventHandler(this.btnNavGame_Click);

            this.btnNavCreate.Location = new System.Drawing.Point(0, 109);
            this.btnNavCreate.Size = new System.Drawing.Size(206, 42);
            this.btnNavCreate.Text = "Tạo phòng";
            this.btnNavCreate.NavIcon = CaroClient.CyberIcon.Plus;
            this.btnNavCreate.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavCreate.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCreate.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavCreate.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavCreate.Click += (s, e) => SwitchToView(this.pnlCreateRoomView, this.btnNavCreate);

            this.btnNavJoin.Location = new System.Drawing.Point(0, 161);
            this.btnNavJoin.Size = new System.Drawing.Size(206, 42);
            this.btnNavJoin.Text = "Tham gia phòng";
            this.btnNavJoin.NavIcon = CaroClient.CyberIcon.Link;
            this.btnNavJoin.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavJoin.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavJoin.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavJoin.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavJoin.Click += (s, e) => SwitchToView(this.pnlJoinRoomView, this.btnNavJoin);

            this.btnNavSettings.Location = new System.Drawing.Point(0, 213);
            this.btnNavSettings.Size = new System.Drawing.Size(206, 42);
            this.btnNavSettings.Text = "Cài đặt & Kết nối";
            this.btnNavSettings.NavIcon = CaroClient.CyberIcon.Settings;
            this.btnNavSettings.ParentBgColor = System.Drawing.Color.FromArgb(9, 15, 30);
            this.btnNavSettings.CustomTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavSettings.NormalColor = System.Drawing.Color.FromArgb(14, 24, 46);
            this.btnNavSettings.HoverColor = System.Drawing.Color.FromArgb(24, 42, 80);
            this.btnNavSettings.Click += (s, e) => SwitchToView(this.pnlSettingsView, this.btnNavSettings);

            // pnlProfileCard (Bottom Sidebar)
            this.pnlProfileCard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProfileCard.Height = 78;
            this.pnlProfileCard.FillColor = System.Drawing.Color.FromArgb(14, 25, 50);
            this.pnlProfileCard.BorderColor = System.Drawing.Color.FromArgb(24, 44, 84);
            this.pnlProfileCard.Controls.Add(this.lblProfileName);
            this.pnlProfileCard.Controls.Add(this.lblProfileLevel);
            this.pnlProfileCard.Controls.Add(this.lblProfileXp);

            this.lblProfileName.Text = "HuyNguyen";
            this.lblProfileName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblProfileName.ForeColor = System.Drawing.Color.White;
            this.lblProfileName.Location = new System.Drawing.Point(12, 10);
            this.lblProfileName.AutoSize = true;

            this.lblProfileLevel.Text = "Lv. 3";
            this.lblProfileLevel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblProfileLevel.ForeColor = System.Drawing.Color.FromArgb(0, 230, 118);
            this.lblProfileLevel.Location = new System.Drawing.Point(12, 32);
            this.lblProfileLevel.AutoSize = true;

            this.lblProfileXp.Text = "120 / 300 XP";
            this.lblProfileXp.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblProfileXp.ForeColor = System.Drawing.Color.FromArgb(0, 194, 255);
            this.lblProfileXp.Location = new System.Drawing.Point(12, 50);
            this.lblProfileXp.AutoSize = true;

            // -------------------------------------------------------------
            // pnlRightBar (Right Panel - Dedicated strictly to Chat)
            // -------------------------------------------------------------
            this.pnlRightBar.BackColor = System.Drawing.Color.FromArgb(8, 14, 28);
            this.pnlRightBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightBar.Width = 280;
            this.pnlRightBar.Padding = new System.Windows.Forms.Padding(10);
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

            this.cardCreateBox.Size = new System.Drawing.Size(540, 514);
            this.cardCreateBox.Location = new System.Drawing.Point(80, 20);
            this.cardCreateBox.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
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
            this.pnlCreateHeader.Location = new System.Drawing.Point(24, 18);
            this.pnlCreateHeader.Size = new System.Drawing.Size(492, 48);
            this.pnlCreateHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlCreateHeader.Controls.Add(this.badgeCreatePlus);
            this.pnlCreateHeader.Controls.Add(this.lblCreateTitle);
            this.pnlCreateHeader.Controls.Add(this.lblCreateSubtitle);

            this.badgeCreatePlus.Location = new System.Drawing.Point(0, 6);
            this.badgeCreatePlus.Size = new System.Drawing.Size(36, 36);

            this.lblCreateTitle.Text = "Tạo phòng mới";
            this.lblCreateTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblCreateTitle.ForeColor = System.Drawing.Color.White;
            this.lblCreateTitle.Location = new System.Drawing.Point(46, 2);
            this.lblCreateTitle.AutoSize = true;

            this.lblCreateSubtitle.Text = "Thiết lập chế độ chơi và mời bạn bè cùng tham gia!";
            this.lblCreateSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCreateSubtitle.ForeColor = System.Drawing.Color.FromArgb(140, 168, 208);
            this.lblCreateSubtitle.Location = new System.Drawing.Point(47, 26);
            this.lblCreateSubtitle.AutoSize = true;

            // Section 1: Mode
            this.secHeaderMode.Location = new System.Drawing.Point(24, 76);
            this.secHeaderMode.Size = new System.Drawing.Size(492, 24);
            this.secHeaderMode.StepNumber = 1;
            this.secHeaderMode.HeaderText = "Chọn chế độ chơi";

            this.cardModePvP.Location = new System.Drawing.Point(24, 104);
            this.cardModePvP.Size = new System.Drawing.Size(240, 84);
            this.cardModePvP.IconType = CaroClient.CardIconType.TwoPlayers;
            this.cardModePvP.Title = "2 người";
            this.cardModePvP.Subtitle = "Đấu trực tiếp";
            this.cardModePvP.IsSelected = true;

            this.cardModeAI.Location = new System.Drawing.Point(276, 104);
            this.cardModeAI.Size = new System.Drawing.Size(240, 84);
            this.cardModeAI.IconType = CaroClient.CardIconType.RobotAI;
            this.cardModeAI.Title = "Vs AI";
            this.cardModeAI.Subtitle = "Chơi với máy";
            this.cardModeAI.IsSelected = false;

            // Section 2: Time
            this.secHeaderTime.Location = new System.Drawing.Point(24, 200);
            this.secHeaderTime.Size = new System.Drawing.Size(492, 24);
            this.secHeaderTime.StepNumber = 2;
            this.secHeaderTime.HeaderText = "Quy định thời gian";

            this.cardTimeTurn.Location = new System.Drawing.Point(24, 228);
            this.cardTimeTurn.Size = new System.Drawing.Size(240, 68);
            this.cardTimeTurn.IconType = CaroClient.CardIconType.ClockTurn;
            this.cardTimeTurn.Title = "Theo lượt";
            this.cardTimeTurn.Subtitle = "";
            this.cardTimeTurn.IsSelected = true;

            this.cardTimeFree.Location = new System.Drawing.Point(276, 228);
            this.cardTimeFree.Size = new System.Drawing.Size(240, 68);
            this.cardTimeFree.IconType = CaroClient.CardIconType.Stopwatch;
            this.cardTimeFree.Title = "Đếm ngược";
            this.cardTimeFree.Subtitle = "";
            this.cardTimeFree.IsSelected = false;

            // pnlTimeLimitOptions
            this.pnlTimeLimitOptions.Location = new System.Drawing.Point(24, 308);
            this.pnlTimeLimitOptions.Size = new System.Drawing.Size(492, 34);
            this.pnlTimeLimitOptions.BackColor = System.Drawing.Color.Transparent;
            this.pnlTimeLimitOptions.Controls.Add(this.lblTimePillLabel);
            this.pnlTimeLimitOptions.Controls.Add(this.pillTime15);
            this.pnlTimeLimitOptions.Controls.Add(this.pillTime30);
            this.pnlTimeLimitOptions.Controls.Add(this.pillTime60);

            this.lblTimePillLabel.Text = "Thời gian mỗi lượt:";
            this.lblTimePillLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTimePillLabel.ForeColor = System.Drawing.Color.FromArgb(150, 175, 215);
            this.lblTimePillLabel.Location = new System.Drawing.Point(0, 8);
            this.lblTimePillLabel.AutoSize = true;

            this.pillTime15.Location = new System.Drawing.Point(120, 0);
            this.pillTime15.Size = new System.Drawing.Size(85, 34);
            this.pillTime15.PillText = "15 giây";
            this.pillTime15.IsSelected = false;

            this.pillTime30.Location = new System.Drawing.Point(212, 0);
            this.pillTime30.Size = new System.Drawing.Size(85, 34);
            this.pillTime30.PillText = "30 giây";
            this.pillTime30.IsSelected = true;

            this.pillTime60.Location = new System.Drawing.Point(304, 0);
            this.pillTime60.Size = new System.Drawing.Size(85, 34);
            this.pillTime60.PillText = "60 giây";
            this.pillTime60.IsSelected = false;

            // pnlAiDifficultyOptions (Shown when Vs AI is selected)
            this.pnlAiDifficultyOptions.Location = new System.Drawing.Point(24, 348);
            this.pnlAiDifficultyOptions.Size = new System.Drawing.Size(492, 34);
            this.pnlAiDifficultyOptions.BackColor = System.Drawing.Color.Transparent;
            this.pnlAiDifficultyOptions.Visible = false;
            this.pnlAiDifficultyOptions.Controls.Add(this.lblAiDifficultyLabel);
            this.pnlAiDifficultyOptions.Controls.Add(this.pillAiEasy);
            this.pnlAiDifficultyOptions.Controls.Add(this.pillAiMedium);
            this.pnlAiDifficultyOptions.Controls.Add(this.pillAiHard);

            this.lblAiDifficultyLabel.Text = "Mức độ AI:";
            this.lblAiDifficultyLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAiDifficultyLabel.ForeColor = System.Drawing.Color.FromArgb(150, 175, 215);
            this.lblAiDifficultyLabel.Location = new System.Drawing.Point(0, 8);
            this.lblAiDifficultyLabel.AutoSize = true;

            this.pillAiEasy.Location = new System.Drawing.Point(120, 0);
            this.pillAiEasy.Size = new System.Drawing.Size(80, 34);
            this.pillAiEasy.PillText = "Dễ";
            this.pillAiEasy.IsSelected = false;

            this.pillAiMedium.Location = new System.Drawing.Point(208, 0);
            this.pillAiMedium.Size = new System.Drawing.Size(100, 34);
            this.pillAiMedium.PillText = "Trung bình";
            this.pillAiMedium.IsSelected = true;

            this.pillAiHard.Location = new System.Drawing.Point(316, 0);
            this.pillAiHard.Size = new System.Drawing.Size(80, 34);
            this.pillAiHard.PillText = "Khó";
            this.pillAiHard.IsSelected = false;

            // Section 3: Rule
            this.secHeaderRule.Location = new System.Drawing.Point(24, 366);
            this.secHeaderRule.Size = new System.Drawing.Size(492, 24);
            this.secHeaderRule.StepNumber = 3;
            this.secHeaderRule.HeaderText = "Luật thi đấu";

            this.pillRuleStandard.Location = new System.Drawing.Point(24, 394);
            this.pillRuleStandard.Size = new System.Drawing.Size(240, 36);
            this.pillRuleStandard.PillText = "Chặn 2 đầu không thắng";
            this.pillRuleStandard.IsSelected = true;

            this.pillRuleFree.Location = new System.Drawing.Point(276, 394);
            this.pillRuleFree.Size = new System.Drawing.Size(240, 36);
            this.pillRuleFree.PillText = "Luật tự do (5 con liên tiếp)";
            this.pillRuleFree.IsSelected = false;

            // Action Buttons
            this.btnCreateRoom.Location = new System.Drawing.Point(24, 448);
            this.btnCreateRoom.Size = new System.Drawing.Size(340, 48);
            this.btnCreateRoom.Text = "▶  Tạo phòng";
            this.btnCreateRoom.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCreateRoom.IsPrimaryGradient = true;
            this.btnCreateRoom.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);

            this.btnCancelCreate.Location = new System.Drawing.Point(376, 448);
            this.btnCancelCreate.Size = new System.Drawing.Size(140, 48);
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
            // View 5: pnlSettingsView (Cài đặt & Kết nối Server)
            // =============================================================
            this.pnlSettingsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettingsView.Padding = new System.Windows.Forms.Padding(40);
            this.pnlSettingsView.Controls.Add(this.cardSettingsBox);

            this.cardSettingsBox.Size = new System.Drawing.Size(520, 350);
            this.cardSettingsBox.Location = new System.Drawing.Point(120, 70);
            this.cardSettingsBox.Controls.Add(this.lblSettingsTitle);
            this.cardSettingsBox.Controls.Add(this.lblServerIp);
            this.cardSettingsBox.Controls.Add(this.txtIp);
            this.cardSettingsBox.Controls.Add(this.lblPort);
            this.cardSettingsBox.Controls.Add(this.txtPort);
            this.cardSettingsBox.Controls.Add(this.lblPlayerName);
            this.cardSettingsBox.Controls.Add(this.txtName);
            this.cardSettingsBox.Controls.Add(this.lblStatusBadge);
            this.cardSettingsBox.Controls.Add(this.btnConnect);

            this.lblSettingsTitle.Text = "Cài đặt & Kết nối Server";
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.White;
            this.lblSettingsTitle.Location = new System.Drawing.Point(24, 20);
            this.lblSettingsTitle.AutoSize = true;

            this.lblServerIp.Text = "Địa chỉ Server IP:";
            this.lblServerIp.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblServerIp.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblServerIp.Location = new System.Drawing.Point(30, 70);
            this.lblServerIp.AutoSize = true;

            this.txtIp.Location = new System.Drawing.Point(170, 68);
            this.txtIp.Size = new System.Drawing.Size(260, 26);
            this.txtIp.Text = "127.0.0.1";
            this.txtIp.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIp.ForeColor = System.Drawing.Color.White;

            this.lblPort.Text = "Cổng Port:";
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblPort.Location = new System.Drawing.Point(30, 115);
            this.lblPort.AutoSize = true;

            this.txtPort.Location = new System.Drawing.Point(170, 113);
            this.txtPort.Size = new System.Drawing.Size(120, 26);
            this.txtPort.Text = "8888";
            this.txtPort.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.ForeColor = System.Drawing.Color.White;

            this.lblPlayerName.Text = "Tên người chơi:";
            this.lblPlayerName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPlayerName.ForeColor = System.Drawing.Color.FromArgb(170, 190, 220);
            this.lblPlayerName.Location = new System.Drawing.Point(30, 160);
            this.lblPlayerName.AutoSize = true;

            this.txtName.Location = new System.Drawing.Point(170, 158);
            this.txtName.Size = new System.Drawing.Size(260, 26);
            this.txtName.Text = "HuyNguyen";
            this.txtName.BackColor = System.Drawing.Color.FromArgb(14, 25, 48);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.White;

            this.lblStatusBadge.Text = "● Chưa kết nối";
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(255, 75, 75);
            this.lblStatusBadge.Location = new System.Drawing.Point(30, 210);
            this.lblStatusBadge.AutoSize = true;

            this.btnConnect.Location = new System.Drawing.Point(30, 250);
            this.btnConnect.Size = new System.Drawing.Size(220, 44);
            this.btnConnect.Text = "Kết nối Server";
            this.btnConnect.IsPrimaryGradient = true;
            this.btnConnect.CustomTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

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
        private CaroClient.CyberButton btnNavSettings;
        private CaroClient.RoundedCard pnlProfileCard;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Label lblProfileLevel;
        private System.Windows.Forms.Label lblProfileXp;

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

        // View 5: Settings
        private System.Windows.Forms.Panel pnlSettingsView;
        private CaroClient.RoundedCard cardSettingsBox;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblStatusBadge;
        private CaroClient.CyberButton btnConnect;

        // Timer
        private System.Windows.Forms.Timer turnTimer;
    }
}
