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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.lblServerIp = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.grpChat = new System.Windows.Forms.GroupBox();
            this.btnSendChat = new System.Windows.Forms.Button();
            this.txtChatInput = new System.Windows.Forms.TextBox();
            this.rtbChatLog = new System.Windows.Forms.RichTextBox();
            this.grpGameStatus = new System.Windows.Forms.GroupBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnSurrender = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblTurnInfo = new System.Windows.Forms.Label();
            this.lblPlayersInfo = new System.Windows.Forms.Label();
            this.lblRoomIdInfo = new System.Windows.Forms.Label();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.txtRoomId = new System.Windows.Forms.TextBox();
            this.lblRoomId = new System.Windows.Forms.Label();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.radPvAI = new System.Windows.Forms.RadioButton();
            this.radPvP = new System.Windows.Forms.RadioButton();
            this.panelBoardContainer = new System.Windows.Forms.Panel();
            this.boardControl1 = new CaroClient.BoardControl();
            this.btnLeaveRoom = new System.Windows.Forms.Button();
            this.turnTimer = new System.Windows.Forms.Timer(this.components);

            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(42)))));
            this.panelTop.Controls.Add(this.lblStatusBadge);
            this.panelTop.Controls.Add(this.btnConnect);
            this.panelTop.Controls.Add(this.txtName);
            this.panelTop.Controls.Add(this.lblPlayerName);
            this.panelTop.Controls.Add(this.txtPort);
            this.panelTop.Controls.Add(this.lblPort);
            this.panelTop.Controls.Add(this.txtIp);
            this.panelTop.Controls.Add(this.lblServerIp);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1084, 55);
            this.panelTop.TabIndex = 0;
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.AutoSize = true;
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.lblStatusBadge.Location = new System.Drawing.Point(740, 18);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(109, 17);
            this.lblStatusBadge.TabIndex = 7;
            this.lblStatusBadge.Text = "🔴 Chưa kết nối";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(600, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 28);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Kết nối Server";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(440, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 23);
            this.txtName.TabIndex = 5;
            this.txtName.Text = "Player1";
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPlayerName.Location = new System.Drawing.Point(365, 18);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(69, 15);
            this.lblPlayerName.TabIndex = 4;
            this.lblPlayerName.Text = "Tên người chơi:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(280, 15);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(65, 23);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "8888";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.ForeColor = System.Drawing.Color.White;
            this.lblPort.Location = new System.Drawing.Point(240, 18);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(32, 15);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(85, 15);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(135, 23);
            this.txtIp.TabIndex = 1;
            this.txtIp.Text = "127.0.0.1";
            // 
            // lblServerIp
            // 
            this.lblServerIp.AutoSize = true;
            this.lblServerIp.ForeColor = System.Drawing.Color.White;
            this.lblServerIp.Location = new System.Drawing.Point(15, 18);
            this.lblServerIp.Name = "lblServerIp";
            this.lblServerIp.Size = new System.Drawing.Size(65, 15);
            this.lblServerIp.TabIndex = 0;
            this.lblServerIp.Text = "Server IP:";
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelRight.Controls.Add(this.grpChat);
            this.panelRight.Controls.Add(this.grpGameStatus);
            this.panelRight.Controls.Add(this.grpMode);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(684, 55);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(10);
            this.panelRight.Size = new System.Drawing.Size(400, 626);
            this.panelRight.TabIndex = 1;
            // 
            // grpChat
            // 
            this.grpChat.Controls.Add(this.btnSendChat);
            this.grpChat.Controls.Add(this.txtChatInput);
            this.grpChat.Controls.Add(this.rtbChatLog);
            this.grpChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpChat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpChat.Location = new System.Drawing.Point(10, 360);
            this.grpChat.Name = "grpChat";
            this.grpChat.Size = new System.Drawing.Size(380, 256);
            this.grpChat.TabIndex = 2;
            this.grpChat.TabStop = false;
            this.grpChat.Text = "💬 Trò chuyện & Nhật ký Trận đấu";
            // 
            // btnSendChat
            // 
            this.btnSendChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSendChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendChat.ForeColor = System.Drawing.Color.White;
            this.btnSendChat.Location = new System.Drawing.Point(300, 222);
            this.btnSendChat.Name = "btnSendChat";
            this.btnSendChat.Size = new System.Drawing.Size(70, 25);
            this.btnSendChat.TabIndex = 2;
            this.btnSendChat.Text = "Gửi";
            this.btnSendChat.UseVisualStyleBackColor = false;
            this.btnSendChat.Click += new System.EventHandler(this.btnSendChat_Click);
            // 
            // txtChatInput
            // 
            this.txtChatInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChatInput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtChatInput.Location = new System.Drawing.Point(10, 223);
            this.txtChatInput.Name = "txtChatInput";
            this.txtChatInput.Size = new System.Drawing.Size(284, 23);
            this.txtChatInput.TabIndex = 1;
            this.txtChatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChatInput_KeyDown);
            // 
            // rtbChatLog
            // 
            this.rtbChatLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbChatLog.BackColor = System.Drawing.Color.White;
            this.rtbChatLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbChatLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbChatLog.Location = new System.Drawing.Point(10, 22);
            this.rtbChatLog.Name = "rtbChatLog";
            this.rtbChatLog.ReadOnly = true;
            this.rtbChatLog.Size = new System.Drawing.Size(360, 195);
            this.rtbChatLog.TabIndex = 0;
            this.rtbChatLog.Text = "";
            // 
            // grpGameStatus
            // 
            this.grpGameStatus.Controls.Add(this.btnLeaveRoom);
            this.grpGameStatus.Controls.Add(this.btnRestart);
            this.grpGameStatus.Controls.Add(this.btnSurrender);
            this.grpGameStatus.Controls.Add(this.lblTimer);
            this.grpGameStatus.Controls.Add(this.lblTurnInfo);
            this.grpGameStatus.Controls.Add(this.lblPlayersInfo);
            this.grpGameStatus.Controls.Add(this.lblRoomIdInfo);
            this.grpGameStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpGameStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpGameStatus.Location = new System.Drawing.Point(10, 185);
            this.grpGameStatus.Name = "grpGameStatus";
            this.grpGameStatus.Size = new System.Drawing.Size(380, 210);
            this.grpGameStatus.TabIndex = 1;
            this.grpGameStatus.TabStop = false;
            this.grpGameStatus.Text = "📊 Trạng thái Trận đấu";
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.Location = new System.Drawing.Point(195, 132);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(130, 30);
            this.btnRestart.TabIndex = 5;
            this.btnRestart.Text = "🔄 Chơi lại";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnSurrender
            // 
            this.btnSurrender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnSurrender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSurrender.ForeColor = System.Drawing.Color.White;
            this.btnSurrender.Location = new System.Drawing.Point(50, 132);
            this.btnSurrender.Name = "btnSurrender";
            this.btnSurrender.Size = new System.Drawing.Size(130, 30);
            this.btnSurrender.TabIndex = 4;
            this.btnSurrender.Text = "🚩 Đầu hàng";
            this.btnSurrender.UseVisualStyleBackColor = false;
            this.btnSurrender.Click += new System.EventHandler(this.btnSurrender_Click);
            // 
            // btnLeaveRoom
            // 
            this.btnLeaveRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnLeaveRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeaveRoom.ForeColor = System.Drawing.Color.White;
            this.btnLeaveRoom.Location = new System.Drawing.Point(50, 170);
            this.btnLeaveRoom.Name = "btnLeaveRoom";
            this.btnLeaveRoom.Size = new System.Drawing.Size(275, 30);
            this.btnLeaveRoom.TabIndex = 6;
            this.btnLeaveRoom.Text = "🚪 Thoát phòng";
            this.btnLeaveRoom.UseVisualStyleBackColor = false;
            this.btnLeaveRoom.Visible = false;
            this.btnLeaveRoom.Click += new System.EventHandler(this.btnLeaveRoom_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTimer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblTimer.Location = new System.Drawing.Point(15, 100);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(150, 20);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "⏱️ Thời gian: 30 Giây";
            // 
            // lblTurnInfo
            // 
            this.lblTurnInfo.AutoSize = true;
            this.lblTurnInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTurnInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblTurnInfo.Location = new System.Drawing.Point(15, 75);
            this.lblTurnInfo.Name = "lblTurnInfo";
            this.lblTurnInfo.Size = new System.Drawing.Size(81, 17);
            this.lblTurnInfo.TabIndex = 2;
            this.lblTurnInfo.Text = "👉 Lượt: ---";
            // 
            // lblPlayersInfo
            // 
            this.lblPlayersInfo.AutoSize = true;
            this.lblPlayersInfo.Location = new System.Drawing.Point(15, 50);
            this.lblPlayersInfo.Name = "lblPlayersInfo";
            this.lblPlayersInfo.Size = new System.Drawing.Size(136, 15);
            this.lblPlayersInfo.TabIndex = 1;
            this.lblPlayersInfo.Text = "⚔️ X: ---  VS  O: ---";
            // 
            // lblRoomIdInfo
            // 
            this.lblRoomIdInfo.AutoSize = true;
            this.lblRoomIdInfo.Location = new System.Drawing.Point(15, 25);
            this.lblRoomIdInfo.Name = "lblRoomIdInfo";
            this.lblRoomIdInfo.Size = new System.Drawing.Size(84, 15);
            this.lblRoomIdInfo.TabIndex = 0;
            this.lblRoomIdInfo.Text = "🏠 Phòng: ---";
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.btnJoinRoom);
            this.grpMode.Controls.Add(this.txtRoomId);
            this.grpMode.Controls.Add(this.lblRoomId);
            this.grpMode.Controls.Add(this.btnCreateRoom);
            this.grpMode.Controls.Add(this.cmbDifficulty);
            this.grpMode.Controls.Add(this.lblDifficulty);
            this.grpMode.Controls.Add(this.radPvAI);
            this.grpMode.Controls.Add(this.radPvP);
            this.grpMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMode.Location = new System.Drawing.Point(10, 10);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(380, 175);
            this.grpMode.TabIndex = 0;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "🎮 Tùy chọn Chế độ Chơi";
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnJoinRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinRoom.ForeColor = System.Drawing.Color.White;
            this.btnJoinRoom.Location = new System.Drawing.Point(260, 130);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(105, 26);
            this.btnJoinRoom.TabIndex = 7;
            this.btnJoinRoom.Text = "Vào phòng";
            this.btnJoinRoom.UseVisualStyleBackColor = false;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // txtRoomId
            // 
            this.txtRoomId.Location = new System.Drawing.Point(165, 131);
            this.txtRoomId.Name = "txtRoomId";
            this.txtRoomId.Size = new System.Drawing.Size(85, 23);
            this.txtRoomId.TabIndex = 6;
            // 
            // lblRoomId
            // 
            this.lblRoomId.AutoSize = true;
            this.lblRoomId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRoomId.Location = new System.Drawing.Point(95, 134);
            this.lblRoomId.Name = "lblRoomId";
            this.lblRoomId.Size = new System.Drawing.Size(64, 15);
            this.lblRoomId.TabIndex = 5;
            this.lblRoomId.Text = "Mã phòng:";
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.btnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoom.ForeColor = System.Drawing.Color.White;
            this.btnCreateRoom.Location = new System.Drawing.Point(15, 90);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(350, 32);
            this.btnCreateRoom.TabIndex = 4;
            this.btnCreateRoom.Text = "➕ Tạo phòng mới";
            this.btnCreateRoom.UseVisualStyleBackColor = false;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "Dễ (Minimax Depth 2)",
            "Trung bình (Minimax Depth 3)",
            "Khó (Multithread Minimax Depth 4)"});
            this.cmbDifficulty.Location = new System.Drawing.Point(180, 52);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(185, 23);
            this.cmbDifficulty.TabIndex = 3;
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDifficulty.Location = new System.Drawing.Point(100, 56);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(74, 15);
            this.lblDifficulty.TabIndex = 2;
            this.lblDifficulty.Text = "Độ khó AI:";
            // 
            // radPvAI
            // 
            this.radPvAI.AutoSize = true;
            this.radPvAI.Location = new System.Drawing.Point(15, 54);
            this.radPvAI.Name = "radPvAI";
            this.radPvAI.Size = new System.Drawing.Size(76, 19);
            this.radPvAI.TabIndex = 1;
            this.radPvAI.Text = "🤖 Đấu AI";
            this.radPvAI.UseVisualStyleBackColor = true;
            this.radPvAI.CheckedChanged += new System.EventHandler(this.radPvAI_CheckedChanged);
            // 
            // radPvP
            // 
            this.radPvP.AutoSize = true;
            this.radPvP.Checked = true;
            this.radPvP.Location = new System.Drawing.Point(15, 25);
            this.radPvP.Name = "radPvP";
            this.radPvP.Size = new System.Drawing.Size(126, 19);
            this.radPvP.TabIndex = 0;
            this.radPvP.TabStop = true;
            this.radPvP.Text = "⚔️ Đấu Người (PvP)";
            this.radPvP.UseVisualStyleBackColor = true;
            this.radPvP.CheckedChanged += new System.EventHandler(this.radPvP_CheckedChanged);
            // 
            // panelBoardContainer
            // 
            this.panelBoardContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(225)))), ((int)(((byte)(215)))));
            this.panelBoardContainer.Controls.Add(this.boardControl1);
            this.panelBoardContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBoardContainer.Location = new System.Drawing.Point(0, 55);
            this.panelBoardContainer.Name = "panelBoardContainer";
            this.panelBoardContainer.Padding = new System.Windows.Forms.Padding(10);
            this.panelBoardContainer.Size = new System.Drawing.Size(684, 626);
            this.panelBoardContainer.TabIndex = 2;
            // 
            // boardControl1
            // 
            this.boardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardControl1.IsMyTurn = false;
            this.boardControl1.LastMoveCol = -1;
            this.boardControl1.LastMoveRow = -1;
            this.boardControl1.Location = new System.Drawing.Point(10, 10);
            this.boardControl1.MySymbol = 'X';
            this.boardControl1.Name = "boardControl1";
            this.boardControl1.Size = new System.Drawing.Size(664, 606);
            this.boardControl1.Status = CaroProtocol.GameStateStatus.WaitingForOpponent;
            this.boardControl1.TabIndex = 0;

            // 
            // turnTimer
            // 
            this.turnTimer.Interval = 1000;
            this.turnTimer.Tick += new System.EventHandler(this.turnTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 681);
            this.Controls.Add(this.panelBoardContainer);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Caro (Gomoku) Client - PBL4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.grpChat.ResumeLayout(false);
            this.grpChat.PerformLayout();
            this.grpGameStatus.ResumeLayout(false);
            this.grpGameStatus.PerformLayout();
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.panelBoardContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.RadioButton radPvP;
        private System.Windows.Forms.RadioButton radPvAI;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Label lblRoomId;
        private System.Windows.Forms.TextBox txtRoomId;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.GroupBox grpGameStatus;
        private System.Windows.Forms.Label lblRoomIdInfo;
        private System.Windows.Forms.Label lblPlayersInfo;
        private System.Windows.Forms.Label lblTurnInfo;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnSurrender;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.GroupBox grpChat;
        private System.Windows.Forms.RichTextBox rtbChatLog;
        private System.Windows.Forms.TextBox txtChatInput;
        private System.Windows.Forms.Button btnSendChat;
        private System.Windows.Forms.Panel panelBoardContainer;
        private BoardControl boardControl1;
        private System.Windows.Forms.Timer turnTimer;
        private System.Windows.Forms.Button btnLeaveRoom;
    }
}
