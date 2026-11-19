using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CaroClient
{
    public enum GameResultType
    {
        Win,
        Loss,
        Draw
    }

    /// <summary>
    /// Popup thông báo kết quả ván đấu (Thắng / Thua / Hòa) phong cách Game hiện đại.
    /// </summary>
    public class GameOverModal : Control
    {
        private GameResultType _result = GameResultType.Win;
        private string _title = "Bạn Thắng!";
        private string _subtitle = "Chúc mừng bạn đã giành chiến thắng!";

        private bool _isHoverHome = false;
        private bool _isHoverPlayAgain = false;
        private bool _isHoverClose = false;

        public event EventHandler? PlayAgainClicked;
        public event EventHandler? HomeClicked;
        public event EventHandler? CloseClicked;

        public GameOverModal()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.FromArgb(170, 5, 10, 22);
            Visible = false;
        }

        public void ShowResult(GameResultType result, string title, string subtitle)
        {
            _result = result;
            _title = title;
            _subtitle = subtitle;
            _isHoverHome = false;
            _isHoverPlayAgain = false;
            _isHoverClose = false;
            Visible = true;
            BringToFront();
            Invalidate();
        }

        private Rectangle GetCardBounds()
        {
            int cardW = 440;
            int cardH = 370;
            int cardX = Math.Max(10, (Width - cardW) / 2);
            int cardY = Math.Max(10, (Height - cardH) / 2);
            return new Rectangle(cardX, cardY, cardW, cardH);
        }

        private Rectangle GetCloseBounds(Rectangle card)
        {
            return new Rectangle(card.Right - 38, card.Top + 14, 26, 26);
        }

        private Rectangle GetHomeButtonBounds(Rectangle card)
        {
            int btnW = 165;
            int btnH = 46;
            int btnY = card.Bottom - 64;
            int btnX = card.Left + 35;
            return new Rectangle(btnX, btnY, btnW, btnH);
        }

        private Rectangle GetPlayAgainButtonBounds(Rectangle card)
        {
            int btnW = 165;
            int btnH = 46;
            int btnY = card.Bottom - 64;
            int btnX = card.Right - 35 - btnW;
            return new Rectangle(btnX, btnY, btnW, btnH);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Rectangle card = GetCardBounds();
            Rectangle btnClose = GetCloseBounds(card);
            Rectangle btnHome = GetHomeButtonBounds(card);
            Rectangle btnPlayAgain = GetPlayAgainButtonBounds(card);

            bool hoverClose = btnClose.Contains(e.Location);
            bool hoverHome = btnHome.Contains(e.Location);
            bool hoverPlayAgain = btnPlayAgain.Contains(e.Location);

            if (hoverClose != _isHoverClose || hoverHome != _isHoverHome || hoverPlayAgain != _isHoverPlayAgain)
            {
                _isHoverClose = hoverClose;
                _isHoverHome = hoverHome;
                _isHoverPlayAgain = hoverPlayAgain;
                Cursor = (hoverClose || hoverHome || hoverPlayAgain) ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Rectangle card = GetCardBounds();
            Rectangle btnClose = GetCloseBounds(card);
            Rectangle btnHome = GetHomeButtonBounds(card);
            Rectangle btnPlayAgain = GetPlayAgainButtonBounds(card);

            if (btnClose.Contains(e.Location))
            {
                CloseClicked?.Invoke(this, EventArgs.Empty);
            }
            else if (btnHome.Contains(e.Location))
            {
                HomeClicked?.Invoke(this, EventArgs.Empty);
            }
            else if (btnPlayAgain.Contains(e.Location))
            {
                PlayAgainClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. Phủ mờ toàn bộ nền đằng sau
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(170, 5, 10, 22)))
            {
                g.FillRectangle(bgBrush, ClientRectangle);
            }

            Rectangle card = GetCardBounds();
            if (card.Width <= 0 || card.Height <= 0) return;

            // 2. Vẽ Hộp Thoại (Card)
            Color cardBg;
            Color cardBorder;
            switch (_result)
            {
                case GameResultType.Win:
                    cardBg = Color.FromArgb(13, 26, 56);
                    cardBorder = Color.FromArgb(40, 85, 175);
                    break;
                case GameResultType.Loss:
                    cardBg = Color.FromArgb(20, 22, 38);
                    cardBorder = Color.FromArgb(60, 50, 78);
                    break;
                default:
                    cardBg = Color.FromArgb(16, 28, 52);
                    cardBorder = Color.FromArgb(0, 160, 210);
                    break;
            }

            using (GraphicsPath cardPath = UITheme.CreateRoundedRectanglePath(card, 22))
            {
                using (SolidBrush brush = new SolidBrush(cardBg))
                {
                    g.FillPath(brush, cardPath);
                }
                using (Pen pen = new Pen(cardBorder, 2f))
                {
                    g.DrawPath(pen, cardPath);
                }
            }

            // 3. Nút Đóng (X) ở góc trên phải
            Rectangle btnClose = GetCloseBounds(card);
            using (GraphicsPath closePath = new GraphicsPath())
            {
                closePath.AddEllipse(btnClose);
                Color closeBg = _isHoverClose ? Color.FromArgb(70, 90, 130) : Color.FromArgb(35, 48, 78);
                using (SolidBrush closeBrush = new SolidBrush(closeBg))
                {
                    g.FillPath(closeBrush, closePath);
                }
                using (Pen closePen = new Pen(Color.White, 2f))
                {
                    int pad = 7;
                    g.DrawLine(closePen, btnClose.Left + pad, btnClose.Top + pad, btnClose.Right - pad, btnClose.Bottom - pad);
                    g.DrawLine(closePen, btnClose.Right - pad, btnClose.Top + pad, btnClose.Left + pad, btnClose.Bottom - pad);
                }
            }

            // 4. Vẽ Nhân vật / Biểu tượng trên đầu Card
            int mascotCx = card.Left + card.Width / 2;
            int mascotCy = card.Top + 82;

            if (_result == GameResultType.Win)
            {
                DrawWinMascot(g, mascotCx, mascotCy);
            }
            else if (_result == GameResultType.Loss)
            {
                DrawLossMascot(g, mascotCx, mascotCy);
            }
            else
            {
                DrawDrawMascot(g, mascotCx, mascotCy);
            }

            // 5. Vẽ Ruy-băng (Ribbon Banner) 3D ở giữa
            Rectangle ribbonRect = new Rectangle(card.Left + 25, card.Top + 138, card.Width - 50, 56);
            DrawRibbonBanner(g, ribbonRect, _title, _result);

            // 6. Vẽ Subtitle và Ngôi sao
            using (Font subFont = new Font("Segoe UI", 10.5f, FontStyle.Regular))
            using (SolidBrush subBrush = new SolidBrush(Color.FromArgb(220, 235, 255)))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                Rectangle textRect = new Rectangle(card.Left + 20, card.Top + 202, card.Width - 40, 24);
                g.DrawString(_subtitle, subFont, subBrush, textRect, sf);

                // Dải sao trang trí
                int starY = card.Top + 234;
                using (Pen starLine = new Pen(Color.FromArgb(60, 90, 140), 1f))
                {
                    g.DrawLine(starLine, card.Left + 80, starY, card.Left + 180, starY);
                    g.DrawLine(starLine, card.Right - 180, starY, card.Right - 80, starY);
                }
                using (Font starFont = new Font("Segoe UI", 11f, FontStyle.Bold))
                using (SolidBrush starBrush = new SolidBrush(_result == GameResultType.Win ? Color.FromArgb(255, 200, 30) : Color.FromArgb(170, 185, 215)))
                {
                    g.DrawString("★", starFont, starBrush, new Rectangle(card.Left, starY - 10, card.Width, 20), sf);
                }
            }

            // 7. Vẽ 2 Nút Hành động ở đáy
            DrawActionButtons(g, card);
        }

        private void DrawWinMascot(Graphics g, int cx, int cy)
        {
            // Hào quang vàng
            Rectangle glowRect = new Rectangle(cx - 75, cy - 65, 150, 130);
            using (GraphicsPath glowPath = new GraphicsPath())
            {
                glowPath.AddEllipse(glowRect);
                using (PathGradientBrush pgb = new PathGradientBrush(glowPath))
                {
                    pgb.CenterColor = Color.FromArgb(120, 255, 215, 0);
                    pgb.SurroundColors = new[] { Color.Transparent };
                    g.FillEllipse(pgb, glowRect);
                }
            }

            // Cành nguyệt quế vàng hai bên
            using (Pen laurelPen = new Pen(Color.FromArgb(255, 195, 0), 2.5f))
            {
                g.DrawArc(laurelPen, cx - 72, cy - 40, 45, 75, 90, 160);
                g.DrawArc(laurelPen, cx + 27, cy - 40, 45, 75, -90, -160);
            }

            // Vẽ lá nguyệt quế & ngôi sao nhỏ
            using (SolidBrush leafBrush = new SolidBrush(Color.FromArgb(255, 200, 20)))
            {
                g.FillEllipse(leafBrush, cx - 68, cy - 10, 10, 6);
                g.FillEllipse(leafBrush, cx - 64, cy - 25, 10, 6);
                g.FillEllipse(leafBrush, cx + 58, cy - 10, 10, 6);
                g.FillEllipse(leafBrush, cx + 54, cy - 25, 10, 6);
            }

            // Thân nhân vật (Quân O tròn vui mừng)
            int bodyR = 34;
            Rectangle bodyRect = new Rectangle(cx - bodyR, cy - bodyR, bodyR * 2, bodyR * 2);
            using (LinearGradientBrush lgb = new LinearGradientBrush(bodyRect, Color.White, Color.FromArgb(225, 240, 255), LinearGradientMode.Vertical))
            {
                g.FillEllipse(lgb, bodyRect);
            }
            using (Pen bodyBorder = new Pen(Color.FromArgb(180, 215, 255), 2f))
            {
                g.DrawEllipse(bodyBorder, bodyRect);
            }

            // Biểu tượng vòng tròn O màu xanh neon giữa bụng
            Rectangle oRect = new Rectangle(cx - 15, cy - 12, 30, 30);
            using (Pen oPen = new Pen(Color.FromArgb(13, 110, 253), 5.5f))
            {
                g.DrawEllipse(oPen, oRect);
            }

            // Đôi mắt cười vui sướng ^ ^
            using (Pen eyePen = new Pen(Color.FromArgb(10, 25, 60), 2.4f))
            {
                eyePen.StartCap = LineCap.Round;
                eyePen.EndCap = LineCap.Round;
                g.DrawArc(eyePen, cx - 18, cy - 20, 10, 8, 180, 180);
                g.DrawArc(eyePen, cx + 8, cy - 20, 10, 8, 180, 180);
            }

            // Hai tay giơ lên ăn mừng
            using (SolidBrush handBrush = new SolidBrush(Color.White))
            using (Pen handBorder = new Pen(Color.FromArgb(180, 215, 255), 1.8f))
            {
                g.FillEllipse(handBrush, cx - 44, cy - 22, 16, 14);
                g.DrawEllipse(handBorder, cx - 44, cy - 22, 16, 14);

                g.FillEllipse(handBrush, cx + 28, cy - 22, 16, 14);
                g.DrawEllipse(handBorder, cx + 28, cy - 22, 16, 14);
            }

            // Vương miện hoàng gia vàng trên đầu 👑
            Point[] crownPoints = new Point[]
            {
                new Point(cx - 20, cy - 30),
                new Point(cx - 24, cy - 50),
                new Point(cx - 10, cy - 40),
                new Point(cx, cy - 55),
                new Point(cx + 10, cy - 40),
                new Point(cx + 24, cy - 50),
                new Point(cx + 20, cy - 30)
            };
            using (LinearGradientBrush crownBrush = new LinearGradientBrush(new Rectangle(cx - 25, cy - 55, 50, 30), Color.FromArgb(255, 230, 50), Color.FromArgb(255, 170, 0), LinearGradientMode.Vertical))
            {
                g.FillPolygon(crownBrush, crownPoints);
            }
            using (Pen crownPen = new Pen(Color.FromArgb(220, 140, 0), 1.5f))
            {
                g.DrawPolygon(crownPen, crownPoints);
            }

            // Ngọc ruby đính trên đỉnh vương miện
            using (SolidBrush gemBrush = new SolidBrush(Color.FromArgb(255, 45, 85)))
            {
                g.FillEllipse(gemBrush, cx - 2, cy - 56, 5, 5);
                g.FillEllipse(gemBrush, cx - 25, cy - 51, 4, 4);
                g.FillEllipse(gemBrush, cx + 22, cy - 51, 4, 4);
            }
        }

        private void DrawLossMascot(Graphics g, int cx, int cy)
        {
            // Đám mây u ám xám tím
            using (SolidBrush cloudBrush = new SolidBrush(Color.FromArgb(60, 50, 75)))
            {
                g.FillEllipse(cloudBrush, cx - 60, cy - 65, 55, 45);
                g.FillEllipse(cloudBrush, cx - 25, cy - 78, 65, 55);
                g.FillEllipse(cloudBrush, cx + 15, cy - 62, 50, 42);
            }

            // Vệt mưa rơi buồn
            using (Pen rainPen = new Pen(Color.FromArgb(100, 130, 180), 1.5f))
            {
                g.DrawLine(rainPen, cx - 45, cy - 35, cx - 45, cy - 15);
                g.DrawLine(rainPen, cx + 45, cy - 35, cx + 45, cy - 15);
                g.DrawLine(rainPen, cx - 35, cy - 40, cx - 35, cy - 20);
                g.DrawLine(rainPen, cx + 35, cy - 40, cx + 35, cy - 20);
            }

            // Nhân vật quân X đen xám
            int xSize = 28;
            Rectangle xBounds = new Rectangle(cx - xSize, cy - xSize, xSize * 2, xSize * 2);
            using (GraphicsPath xPath = new GraphicsPath())
            {
                int armW = 14;
                xPath.AddPolygon(new Point[]
                {
                    new Point(cx - armW, cy - xSize),
                    new Point(cx, cy - armW),
                    new Point(cx + armW, cy - xSize),
                    new Point(cx + xSize, cy - armW),
                    new Point(cx + armW, cy),
                    new Point(cx + xSize, cy + armW),
                    new Point(cx + armW, cy + xSize),
                    new Point(cx, cy + armW),
                    new Point(cx - armW, cy + xSize),
                    new Point(cx - xSize, cy + armW),
                    new Point(cx - armW, cy),
                    new Point(cx - xSize, cy - armW),
                });

                using (LinearGradientBrush xBrush = new LinearGradientBrush(xBounds, Color.FromArgb(50, 52, 70), Color.FromArgb(30, 32, 45), LinearGradientMode.Vertical))
                {
                    g.FillPath(xBrush, xPath);
                }
                using (Pen xPen = new Pen(Color.FromArgb(80, 85, 115), 1.8f))
                {
                    g.DrawPath(xPen, xPath);
                }
            }

            // Đôi mắt buồn nhắm chặt > <
            using (Pen eyePen = new Pen(Color.FromArgb(235, 240, 255), 2.4f))
            {
                eyePen.StartCap = LineCap.Round;
                eyePen.EndCap = LineCap.Round;
                // Mắt trái >
                g.DrawLine(eyePen, cx - 18, cy - 8, cx - 10, cy - 4);
                g.DrawLine(eyePen, cx - 18, cy, cx - 10, cy - 4);
                // Mắt phải <
                g.DrawLine(eyePen, cx + 18, cy - 8, cx + 10, cy - 4);
                g.DrawLine(eyePen, cx + 18, cy, cx + 10, cy - 4);
            }

            // Vòng xoáy chóng mặt trên đầu @
            using (Pen swirlPen = new Pen(Color.FromArgb(170, 180, 210), 1.8f))
            {
                g.DrawArc(swirlPen, cx - 12, cy - 48, 24, 20, 0, 320);
                g.DrawArc(swirlPen, cx - 8, cy - 44, 16, 14, 0, 280);
            }
        }

        private void DrawDrawMascot(Graphics g, int cx, int cy)
        {
            // Hào quang xanh ngọc
            Rectangle glowRect = new Rectangle(cx - 70, cy - 60, 140, 120);
            using (GraphicsPath glowPath = new GraphicsPath())
            {
                glowPath.AddEllipse(glowRect);
                using (PathGradientBrush pgb = new PathGradientBrush(glowPath))
                {
                    pgb.CenterColor = Color.FromArgb(100, 0, 194, 255);
                    pgb.SurroundColors = new[] { Color.Transparent };
                    g.FillEllipse(pgb, glowRect);
                }
            }

            // 2 quân X và O đứng cạnh nhau hòa nhã
            // Quân O
            Rectangle oRect = new Rectangle(cx - 38, cy - 24, 32, 32);
            using (Pen oPen = new Pen(Color.FromArgb(0, 194, 255), 5f))
            {
                g.DrawEllipse(oPen, oRect);
            }
            // Quân X
            using (Pen xPen = new Pen(Color.FromArgb(255, 46, 99), 5f))
            {
                xPen.StartCap = LineCap.Round;
                xPen.EndCap = LineCap.Round;
                g.DrawLine(xPen, cx + 8, cy - 20, cx + 36, cy + 8);
                g.DrawLine(xPen, cx + 36, cy - 20, cx + 8, cy + 8);
            }

            // Biểu tượng bắt tay / hòa bình trên đầu
            using (Font peaceFont = new Font("Segoe UI", 16f, FontStyle.Bold))
            using (SolidBrush peaceBrush = new SolidBrush(Color.FromArgb(255, 215, 0)))
            {
                g.DrawString("🤝", peaceFont, peaceBrush, new PointF(cx - 16, cy - 54));
            }
        }

        private void DrawRibbonBanner(Graphics g, Rectangle r, string title, GameResultType res)
        {
            Color col1, col2, shadowCol;
            switch (res)
            {
                case GameResultType.Win:
                    col1 = Color.FromArgb(255, 210, 40);
                    col2 = Color.FromArgb(255, 150, 0);
                    shadowCol = Color.FromArgb(160, 85, 0);
                    break;
                case GameResultType.Loss:
                    col1 = Color.FromArgb(255, 95, 120);
                    col2 = Color.FromArgb(235, 45, 75);
                    shadowCol = Color.FromArgb(140, 20, 40);
                    break;
                default:
                    col1 = Color.FromArgb(0, 210, 255);
                    col2 = Color.FromArgb(13, 110, 253);
                    shadowCol = Color.FromArgb(0, 60, 150);
                    break;
            }

            int tailW = 24;
            int tailH = r.Height - 12;

            // Đuôi ribbon bên trái
            Point[] leftTail = new Point[]
            {
                new Point(r.Left - tailW + 8, r.Top + 6),
                new Point(r.Left + 10, r.Top + 6),
                new Point(r.Left + 10, r.Top + 6 + tailH),
                new Point(r.Left - tailW + 8, r.Top + 6 + tailH),
                new Point(r.Left - tailW + 18, r.Top + 6 + tailH / 2)
            };
            using (SolidBrush shadowBrush = new SolidBrush(shadowCol))
            {
                g.FillPolygon(shadowBrush, leftTail);
            }

            // Đuôi ribbon bên phải
            Point[] rightTail = new Point[]
            {
                new Point(r.Right - 10, r.Top + 6),
                new Point(r.Right + tailW - 8, r.Top + 6),
                new Point(r.Right + tailW - 18, r.Top + 6 + tailH / 2),
                new Point(r.Right + tailW - 8, r.Top + 6 + tailH),
                new Point(r.Right - 10, r.Top + 6 + tailH)
            };
            using (SolidBrush shadowBrush = new SolidBrush(shadowCol))
            {
                g.FillPolygon(shadowBrush, rightTail);
            }

            // Thân ribbon chính
            Rectangle mainRibbon = new Rectangle(r.Left, r.Top, r.Width, r.Height);
            using (GraphicsPath ribPath = UITheme.CreateRoundedRectanglePath(mainRibbon, 16))
            {
                using (LinearGradientBrush ribBrush = new LinearGradientBrush(mainRibbon, col1, col2, LinearGradientMode.Vertical))
                {
                    g.FillPath(ribBrush, ribPath);
                }
                using (Pen ribPen = new Pen(Color.FromArgb(255, 255, 255), 1.6f))
                {
                    g.DrawPath(ribPen, ribPath);
                }
            }

            // Vẽ Chữ Tiêu đề to 3D trên ruy-băng
            using (Font titleFont = new Font("Segoe UI", 21f, FontStyle.Bold))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                // Bóng chữ 3D
                Rectangle shadowRect = new Rectangle(r.Left, r.Top + 2, r.Width, r.Height);
                using (SolidBrush textShadow = new SolidBrush(shadowCol))
                {
                    g.DrawString(title, titleFont, textShadow, shadowRect, sf);
                }

                // Chữ trắng nổi bật
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    g.DrawString(title, titleFont, textBrush, r, sf);
                }
            }
        }

        private void DrawActionButtons(Graphics g, Rectangle card)
        {
            Rectangle btnHome = GetHomeButtonBounds(card);
            Rectangle btnPlayAgain = GetPlayAgainButtonBounds(card);

            using (Font btnFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            {
                // 1. Nút Về Trang Chủ
                Color home1, home2;
                if (_result == GameResultType.Win)
                {
                    home1 = _isHoverHome ? Color.FromArgb(25, 130, 255) : Color.FromArgb(13, 110, 253);
                    home2 = _isHoverHome ? Color.FromArgb(0, 180, 255) : Color.FromArgb(0, 150, 240);
                }
                else
                {
                    home1 = _isHoverHome ? Color.FromArgb(60, 75, 110) : Color.FromArgb(42, 54, 80);
                    home2 = _isHoverHome ? Color.FromArgb(75, 95, 135) : Color.FromArgb(52, 66, 96);
                }

                using (GraphicsPath homePath = UITheme.CreateRoundedRectanglePath(btnHome, btnHome.Height / 2))
                {
                    using (LinearGradientBrush hBrush = new LinearGradientBrush(btnHome, home1, home2, LinearGradientMode.Vertical))
                    {
                        g.FillPath(hBrush, homePath);
                    }
                    using (Pen hPen = new Pen(Color.FromArgb(120, 255, 255, 255), 1.2f))
                    {
                        g.DrawPath(hPen, homePath);
                    }
                }

                // Căn giữa icon ngôi nhà và chữ "Về trang chủ"
                string homeText = "Về trang chủ";
                SizeF homeTextSz = g.MeasureString(homeText, btnFont);
                float homeIconW = 16f;
                float homeGap = 7f;
                float homeTotalW = homeIconW + homeGap + homeTextSz.Width;
                float homeStartX = btnHome.Left + (btnHome.Width - homeTotalW) / 2f;
                float homeCenterY = btnHome.Top + btnHome.Height / 2f;

                DrawHomeIcon(g, homeStartX, homeCenterY - 8f, 16f, Color.White);
                using (SolidBrush hTextBrush = new SolidBrush(Color.White))
                {
                    g.DrawString(homeText, btnFont, hTextBrush, homeStartX + homeIconW + homeGap, homeCenterY - homeTextSz.Height / 2f);
                }

                // 2. Nút Chơi Lại
                Color pa1, pa2;
                if (_result == GameResultType.Win)
                {
                    pa1 = _isHoverPlayAgain ? Color.FromArgb(255, 190, 20) : Color.FromArgb(255, 165, 0);
                    pa2 = _isHoverPlayAgain ? Color.FromArgb(255, 150, 0) : Color.FromArgb(235, 130, 0);
                }
                else if (_result == GameResultType.Loss)
                {
                    pa1 = _isHoverPlayAgain ? Color.FromArgb(255, 80, 110) : Color.FromArgb(255, 55, 90);
                    pa2 = _isHoverPlayAgain ? Color.FromArgb(235, 45, 75) : Color.FromArgb(215, 30, 60);
                }
                else
                {
                    pa1 = _isHoverPlayAgain ? Color.FromArgb(0, 210, 255) : Color.FromArgb(0, 180, 235);
                    pa2 = _isHoverPlayAgain ? Color.FromArgb(13, 110, 253) : Color.FromArgb(10, 90, 215);
                }

                using (GraphicsPath paPath = UITheme.CreateRoundedRectanglePath(btnPlayAgain, btnPlayAgain.Height / 2))
                {
                    using (LinearGradientBrush paBrush = new LinearGradientBrush(btnPlayAgain, pa1, pa2, LinearGradientMode.Vertical))
                    {
                        g.FillPath(paBrush, paPath);
                    }
                    using (Pen paPen = new Pen(Color.FromArgb(140, 255, 255, 255), 1.2f))
                    {
                        g.DrawPath(paPen, paPath);
                    }
                }

                // Căn giữa icon xoay lại và chữ "Chơi lại"
                string paText = "Chơi lại";
                SizeF paTextSz = g.MeasureString(paText, btnFont);
                float paIconW = 16f;
                float paGap = 7f;
                float paTotalW = paIconW + paGap + paTextSz.Width;
                float paStartX = btnPlayAgain.Left + (btnPlayAgain.Width - paTotalW) / 2f;
                float paCenterY = btnPlayAgain.Top + btnPlayAgain.Height / 2f;

                DrawPlayAgainIcon(g, paStartX, paCenterY - 8f, 16f, Color.White);
                using (SolidBrush paTextBrush = new SolidBrush(Color.White))
                {
                    g.DrawString(paText, btnFont, paTextBrush, paStartX + paIconW + paGap, paCenterY - paTextSz.Height / 2f);
                }
            }
        }

        private void DrawHomeIcon(Graphics g, float x, float y, float size, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            using (Pen pen = new Pen(color, 1.8f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                // Mái nhà tam giác
                PointF[] roof = new PointF[]
                {
                    new PointF(x + size / 2f, y + 1f),
                    new PointF(x + 1f, y + size * 0.48f),
                    new PointF(x + size - 1f, y + size * 0.48f)
                };
                g.FillPolygon(brush, roof);

                // Thân nhà chữ nhật
                RectangleF body = new RectangleF(x + size * 0.20f, y + size * 0.45f, size * 0.60f, size * 0.50f);
                g.FillRectangle(brush, body);

                // Ống khói nhỏ
                g.FillRectangle(brush, x + size * 0.68f, y + 2f, size * 0.16f, size * 0.28f);

                // Cửa nhà (khoét màu sẫm tương phản)
                Color doorColor = (_result == GameResultType.Win) ? Color.FromArgb(10, 80, 190) : Color.FromArgb(30, 40, 60);
                using (SolidBrush doorBrush = new SolidBrush(doorColor))
                {
                    RectangleF door = new RectangleF(x + size * 0.38f, y + size * 0.62f, size * 0.24f, size * 0.34f);
                    g.FillRectangle(doorBrush, door);
                }
            }
        }

        private void DrawPlayAgainIcon(Graphics g, float x, float y, float size, Color color)
        {
            using (Pen arcPen = new Pen(color, 2.2f))
            using (SolidBrush arrowBrush = new SolidBrush(color))
            {
                arcPen.StartCap = LineCap.Round;
                arcPen.EndCap = LineCap.Round;

                // Cung tròn xoay ~270 độ
                RectangleF arcRect = new RectangleF(x + 1f, y + 1f, size - 2.5f, size - 2.5f);
                g.DrawArc(arcPen, arcRect, 45, 270);

                // Mũi tên nhọn chỉ chiều xoay kim đồng hồ ở đầu cung
                float arrowX = x + size - 2.5f;
                float arrowY = y + size * 0.28f;
                PointF[] arrow = new PointF[]
                {
                    new PointF(arrowX + 2.5f, arrowY - 4.5f),
                    new PointF(arrowX + 4.5f, arrowY + 2.5f),
                    new PointF(arrowX - 2.5f, arrowY + 0.5f)
                };
                g.FillPolygon(arrowBrush, arrow);
            }
        }
    }
}

