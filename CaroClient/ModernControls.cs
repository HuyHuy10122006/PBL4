using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CaroClient
{
    public static class UITheme
    {
        public static readonly Color BgDark = Color.FromArgb(8, 13, 26);         // #080D1A
        public static readonly Color BgSidebar = Color.FromArgb(10, 17, 34);      // #0A1122
        public static readonly Color BgCard = Color.FromArgb(13, 24, 48);         // #0D1830
        public static readonly Color BgCardLight = Color.FromArgb(18, 32, 64);    // #122040
        public static readonly Color BorderCard = Color.FromArgb(25, 45, 85);     // #192D55
        public static readonly Color AccentBlue = Color.FromArgb(13, 110, 253);   // Electric Blue
        public static readonly Color AccentCyan = Color.FromArgb(0, 194, 255);    // Cyber Cyan
        public static readonly Color AccentRed = Color.FromArgb(255, 46, 99);     // Neon Red
        public static readonly Color AccentGreen = Color.FromArgb(0, 230, 118);   // Neon Green
        public static readonly Color AccentOrange = Color.FromArgb(255, 145, 0);  // Amber Orange
        public static readonly Color TextWhite = Color.FromArgb(245, 248, 255);
        public static readonly Color TextMuted = Color.FromArgb(125, 145, 180);
        public static readonly Color TextDim = Color.FromArgb(85, 105, 138);

        public static GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

            // Top left
            path.AddArc(arc, 180, 90);

            // Top right
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }

    public class RoundedCard : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 14;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = UITheme.BorderCard;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float BorderWidth { get; set; } = 1.2f;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color FillColor { get; set; } = UITheme.BgCard;

        public RoundedCard()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            Padding = new Padding(12);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, CornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(FillColor))
                {
                    g.FillPath(brush, path);
                }

                if (BorderWidth > 0 && BorderColor != Color.Transparent)
                {
                    using (Pen pen = new Pen(BorderColor, BorderWidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }
    }

    public enum CyberIcon
    {
        None,
        Home,
        Gamepad,
        Plus,
        Link,
        Settings
    }

    public class CyberButton : Button
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 10;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ParentBgColor { get; set; } = Color.FromArgb(9, 15, 30);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color NormalColor { get; set; } = Color.FromArgb(14, 24, 46);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color HoverColor { get; set; } = Color.FromArgb(24, 42, 80);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ClickColor { get; set; } = Color.FromArgb(10, 18, 36);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderLineColor { get; set; } = Color.Transparent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrimaryGradient { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ContentAlignment CustomTextAlign { get; set; } = ContentAlignment.MiddleLeft;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TextLeftPadding { get; set; } = 16;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CyberIcon NavIcon { get; set; } = CyberIcon.None;

        private bool _isHovered = false;
        private bool _isPressed = false;

        public CyberButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            ForeColor = UITheme.TextWhite;
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            Cursor = Cursors.Hand;
            BackColor = Color.FromArgb(9, 15, 30);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _isPressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Xóa sạch nền bằng màu của container cha để chống lưu vết text cũ
            using (SolidBrush parentBrush = new SolidBrush(ParentBgColor))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            Color currentBg = _isPressed ? ClickColor : (_isHovered ? HoverColor : NormalColor);
            Color textColor = ForeColor;

            if (!Enabled)
            {
                currentBg = Color.FromArgb(16, 24, 42);
                textColor = Color.FromArgb(90, 110, 140);
            }

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, CornerRadius))
            {
                if (!Enabled)
                {
                    using (SolidBrush brush = new SolidBrush(currentBg))
                    {
                        g.FillPath(brush, path);
                    }
                }
                else if (IsPrimaryGradient)
                {
                    Color startCol = _isPressed ? Color.FromArgb(0, 80, 200) : (_isHovered ? Color.FromArgb(0, 150, 255) : Color.FromArgb(13, 110, 253));
                    Color endCol = _isPressed ? Color.FromArgb(0, 120, 240) : (_isHovered ? Color.FromArgb(0, 200, 255) : Color.FromArgb(0, 194, 255));
                    using (LinearGradientBrush lgb = new LinearGradientBrush(rect, startCol, endCol, LinearGradientMode.Horizontal))
                    {
                        g.FillPath(lgb, path);
                    }
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(currentBg))
                    {
                        g.FillPath(brush, path);
                    }
                }

                if (Enabled && BorderLineColor != Color.Transparent)
                {
                    using (Pen pen = new Pen(BorderLineColor, 1.2f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            // Vẽ Icon vector nếu có (chuẩn cyberpunk, không bao giờ bị lỗi font tofu)
            int effectiveLeftPad = TextLeftPadding;
            if (NavIcon != CyberIcon.None)
            {
                effectiveLeftPad = 46;
                DrawNavIcon(g, NavIcon, 15, (Height - 18) / 2, 18);
            }

            // Vẽ Text với căn lề sạch sẽ - NoPrefix chống biến '&' thành gạch chân, SingleLine chống rớt dòng
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix;
            if (Text.Contains('\n'))
            {
                flags |= TextFormatFlags.WordBreak;
            }
            else
            {
                flags |= TextFormatFlags.SingleLine;
            }

            Rectangle textRect = rect;
            if (CustomTextAlign == ContentAlignment.MiddleLeft)
            {
                flags |= TextFormatFlags.Left;
                textRect = new Rectangle(effectiveLeftPad, 0, Width - effectiveLeftPad - 4, Height);
            }
            else
            {
                flags |= TextFormatFlags.HorizontalCenter;
            }

            TextRenderer.DrawText(g, Text, Font, textRect, textColor, flags);
        }

        private void DrawNavIcon(Graphics g, CyberIcon icon, int x, int y, int size)
        {
            Color iconColor = !Enabled ? Color.FromArgb(90, 110, 140) : (IsPrimaryGradient ? Color.White : (_isHovered ? UITheme.AccentCyan : Color.FromArgb(180, 205, 240)));

            using (Pen pen = new Pen(iconColor, 1.8f))
            using (SolidBrush brush = new SolidBrush(iconColor))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                switch (icon)
                {
                    case CyberIcon.Home:
                        // Mái nhà
                        Point[] roof = new Point[] {
                            new Point(x + size / 2, y),
                            new Point(x + 1, y + size / 2),
                            new Point(x + size - 1, y + size / 2)
                        };
                        g.DrawPolygon(pen, roof);
                        // Thân nhà
                        g.DrawRectangle(pen, x + 3, y + size / 2, size - 6, size / 2 - 1);
                        // Cửa
                        g.FillRectangle(brush, x + size / 2 - 2, y + size / 2 + 3, 4, size / 2 - 4);
                        break;

                    case CyberIcon.Gamepad:
                        // Thân máy chơi game
                        Rectangle padRect = new Rectangle(x, y + 2, size, size - 4);
                        using (GraphicsPath padPath = UITheme.CreateRoundedRectanglePath(padRect, 4))
                        {
                            g.DrawPath(pen, padPath);
                        }
                        // D-pad dấu cộng bên trái
                        g.DrawLine(pen, x + 4, y + size / 2, x + 8, y + size / 2);
                        g.DrawLine(pen, x + 6, y + size / 2 - 2, x + 6, y + size / 2 + 2);
                        // Nút bấm bên phải
                        g.FillEllipse(brush, x + size - 7, y + size / 2 - 3, 3, 3);
                        g.FillEllipse(brush, x + size - 4, y + size / 2, 3, 3);
                        break;

                    case CyberIcon.Plus:
                        // Dấu cộng nét đậm
                        g.DrawLine(pen, x + size / 2, y + 2, x + size / 2, y + size - 2);
                        g.DrawLine(pen, x + 2, y + size / 2, x + size - 2, y + size / 2);
                        break;

                    case CyberIcon.Link:
                        // Mắt xích kết nối
                        g.DrawEllipse(pen, x + 1, y + size / 2 - 4, 8, 8);
                        g.DrawEllipse(pen, x + size - 9, y + size / 2 - 4, 8, 8);
                        g.DrawLine(pen, x + 6, y + size / 2, x + size - 6, y + size / 2);
                        break;

                    case CyberIcon.Settings:
                        // Bánh răng cài đặt
                        int cx = x + size / 2;
                        int cy = y + size / 2;
                        int r = 5;
                        g.DrawEllipse(pen, cx - r, cy - r, r * 2, r * 2);
                        for (int i = 0; i < 4; i++)
                        {
                            double angle = i * Math.PI / 2.0;
                            int x1 = cx + (int)((r + 1) * Math.Cos(angle));
                            int y1 = cy + (int)((r + 1) * Math.Sin(angle));
                            int x2 = cx + (int)((r + 3) * Math.Cos(angle));
                            int y2 = cy + (int)((r + 3) * Math.Sin(angle));
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                        break;
                }
            }
        }
    }

    public class HeroBannerPanel : Panel
    {
        private Image? _bannerImage;

        public HeroBannerPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;

            // Attempt to load generated banner asset
            try
            {
                string assetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "caro_hero_banner.jpg");
                if (File.Exists(assetPath))
                {
                    _bannerImage = Image.FromFile(assetPath);
                }
            }
            catch { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, 16))
            {
                // Gradient Background
                using (LinearGradientBrush bgBrush = new LinearGradientBrush(rect, Color.FromArgb(14, 28, 58), Color.FromArgb(8, 15, 32), LinearGradientMode.Horizontal))
                {
                    g.FillPath(bgBrush, path);
                }

                // Draw banner image on the right if available
                if (_bannerImage != null)
                {
                    int imgWidth = (int)(Height * 1.55f);
                    int imgX = Width - imgWidth;
                    Rectangle imgRect = new Rectangle(imgX, 0, imgWidth, Height);

                    g.SetClip(path);
                    g.DrawImage(_bannerImage, imgRect);

                    // Fade gradient over the image transition
                    Rectangle fadeRect = new Rectangle(imgX - 40, 0, 100, Height);
                    using (LinearGradientBrush fade = new LinearGradientBrush(fadeRect, Color.FromArgb(255, 12, 24, 50), Color.FromArgb(0, 12, 24, 50), LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(fade, fadeRect);
                    }
                    g.ResetClip();
                }

                // Border
                using (Pen pen = new Pen(Color.FromArgb(28, 52, 98), 1.5f))
                {
                    g.DrawPath(pen, path);
                }
            }
        }
    }

    public enum CardIconType
    {
        None,
        TwoPlayers,
        RobotAI,
        ClockTurn,
        Stopwatch
    }

    public class ModeSelectionCard : Control
    {
        private bool _isSelected = false;
        private bool _isHovered = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title { get; set; } = "Title";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Subtitle { get; set; } = "Subtitle";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CardIconType IconType { get; set; } = CardIconType.None;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ParentBgColor { get; set; } = Color.FromArgb(13, 24, 48);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 12;

        public event EventHandler? SelectionChanged;

        public ModeSelectionCard()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Cursor = Cursors.Hand;
            Size = new Size(230, 95);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Clear parent background
            using (SolidBrush parentBrush = new SolidBrush(ParentBgColor))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, CornerRadius))
            {
                // Background
                if (_isSelected)
                {
                    using (LinearGradientBrush lgb = new LinearGradientBrush(rect, Color.FromArgb(14, 38, 76), Color.FromArgb(10, 26, 56), LinearGradientMode.Vertical))
                    {
                        g.FillPath(lgb, path);
                    }
                    using (Pen pen = new Pen(UITheme.AccentCyan, 2f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
                else
                {
                    Color bg = _isHovered ? Color.FromArgb(18, 32, 60) : Color.FromArgb(12, 22, 42);
                    using (SolidBrush brush = new SolidBrush(bg))
                    {
                        g.FillPath(brush, path);
                    }
                    Color borderColor = _isHovered ? Color.FromArgb(40, 70, 120) : Color.FromArgb(24, 42, 80);
                    using (Pen pen = new Pen(borderColor, 1.2f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            // Top-right checkmark badge if selected
            if (_isSelected)
            {
                int badgeSize = 18;
                int badgeX = Width - badgeSize - 8;
                int badgeY = 8;
                Rectangle badgeRect = new Rectangle(badgeX, badgeY, badgeSize, badgeSize);
                using (SolidBrush badgeBrush = new SolidBrush(UITheme.AccentCyan))
                {
                    g.FillEllipse(badgeBrush, badgeRect);
                }
                using (Pen checkPen = new Pen(Color.White, 2f))
                {
                    checkPen.StartCap = LineCap.Round;
                    checkPen.EndCap = LineCap.Round;
                    g.DrawLine(checkPen, badgeX + 4, badgeY + 9, badgeX + 7, badgeY + 13);
                    g.DrawLine(checkPen, badgeX + 7, badgeY + 13, badgeX + 14, badgeY + 5);
                }
            }

            bool hasSubtitle = !string.IsNullOrWhiteSpace(Subtitle);
            float iconCenterY;
            float titleY;
            float subY;

            if (hasSubtitle)
            {
                // Content height: Icon(20) + Title(18) + Subtitle(14) ~ 56px
                float startY = Math.Max(8f, (Height - 56f) / 2f);
                iconCenterY = startY + 10f;
                titleY = startY + 23f;
                subY = startY + 41f;
            }
            else
            {
                // Content height: Icon(20) + Title(18) ~ 40px
                float startY = Math.Max(8f, (Height - 40f) / 2f);
                iconCenterY = startY + 10f;
                titleY = startY + 24f;
                subY = 0f;
            }

            // Draw Icon
            int iconCenterX = Width / 2;
            DrawCardIcon(g, IconType, iconCenterX, (int)iconCenterY, _isSelected ? UITheme.AccentCyan : (_isHovered ? Color.FromArgb(180, 205, 240) : Color.FromArgb(120, 145, 185)));

            // Draw Title
            using (Font titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            using (SolidBrush titleBrush = new SolidBrush(_isSelected ? Color.White : (_isHovered ? Color.FromArgb(235, 245, 255) : Color.FromArgb(210, 225, 245))))
            {
                SizeF size = g.MeasureString(Title, titleFont);
                float titleX = (Width - size.Width) / 2f;
                g.DrawString(Title, titleFont, titleBrush, titleX, titleY);
            }

            // Draw Subtitle (if present)
            if (hasSubtitle)
            {
                using (Font subFont = new Font("Segoe UI", 8.25f, FontStyle.Regular))
                using (SolidBrush subBrush = new SolidBrush(_isSelected ? UITheme.AccentCyan : Color.FromArgb(120, 145, 185)))
                {
                    SizeF size = g.MeasureString(Subtitle, subFont);
                    float subX = (Width - size.Width) / 2f;
                    g.DrawString(Subtitle, subFont, subBrush, subX, subY);
                }
            }
        }

        private void DrawCardIcon(Graphics g, CardIconType icon, int cx, int cy, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            using (Pen pen = new Pen(color, 1.8f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                switch (icon)
                {
                    case CardIconType.TwoPlayers:
                        // Left Player
                        g.FillEllipse(brush, cx - 11, cy - 10, 9, 9);
                        g.FillPie(brush, cx - 15, cy - 1, 17, 14, 180, 180);
                        // Right Player
                        g.FillEllipse(brush, cx + 2, cy - 8, 8, 8);
                        g.FillPie(brush, cx - 1, cy, 14, 12, 180, 180);
                        break;

                    case CardIconType.RobotAI:
                        // Antenna
                        g.DrawLine(pen, cx, cy - 10, cx, cy - 5);
                        g.FillEllipse(brush, cx - 2, cy - 12, 4, 4);
                        // Head
                        Rectangle headRect = new Rectangle(cx - 9, cy - 5, 18, 14);
                        using (GraphicsPath hp = UITheme.CreateRoundedRectanglePath(headRect, 3))
                        {
                            g.FillPath(brush, hp);
                        }
                        // Ears
                        g.FillRectangle(brush, cx - 11, cy - 1, 2, 6);
                        g.FillRectangle(brush, cx + 9, cy - 1, 2, 6);
                        // Eyes (dark)
                        using (SolidBrush darkBrush = new SolidBrush(Color.FromArgb(12, 22, 42)))
                        {
                            g.FillEllipse(darkBrush, cx - 6, cy - 2, 3, 3);
                            g.FillEllipse(darkBrush, cx + 3, cy - 2, 3, 3);
                            // Mouth
                            using (Pen darkPen = new Pen(Color.FromArgb(12, 22, 42), 1.5f))
                            {
                                g.DrawLine(darkPen, cx - 4, cy + 5, cx + 4, cy + 5);
                            }
                        }
                        break;

                    case CardIconType.ClockTurn:
                        // Circle
                        g.DrawEllipse(pen, cx - 8, cy - 8, 16, 16);
                        // Center dot
                        g.FillEllipse(brush, cx - 1, cy - 1, 3, 3);
                        // Hands
                        g.DrawLine(pen, cx, cy, cx, cy - 5);
                        g.DrawLine(pen, cx, cy, cx + 4, cy);
                        break;

                    case CardIconType.Stopwatch:
                        // Top stem & button
                        g.DrawLine(pen, cx, cy - 10, cx, cy - 7);
                        g.DrawLine(pen, cx - 3, cy - 10, cx + 3, cy - 10);
                        // Main circle
                        g.DrawEllipse(pen, cx - 8, cy - 6, 16, 16);
                        // Hand angled
                        g.DrawLine(pen, cx, cy + 2, cx + 4, cy - 2);
                        break;
                }
            }
        }
    }

    public class OptionPill : Control
    {
        private bool _isSelected = false;
        private bool _isHovered = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PillText { get; set; } = "Option";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ParentBgColor { get; set; } = Color.FromArgb(13, 24, 48);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 8;

        public event EventHandler? SelectionChanged;

        public OptionPill()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Cursor = Cursors.Hand;
            Size = new Size(110, 36);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush parentBrush = new SolidBrush(ParentBgColor))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, CornerRadius))
            {
                if (_isSelected)
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(12, 44, 92)))
                    {
                        g.FillPath(brush, path);
                    }
                    using (Pen pen = new Pen(UITheme.AccentCyan, 1.8f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
                else
                {
                    Color bg = _isHovered ? Color.FromArgb(18, 34, 64) : Color.FromArgb(11, 20, 38);
                    using (SolidBrush brush = new SolidBrush(bg))
                    {
                        g.FillPath(brush, path);
                    }
                    Color borderColor = _isHovered ? Color.FromArgb(36, 62, 108) : Color.FromArgb(24, 44, 80);
                    using (Pen pen = new Pen(borderColor, 1.2f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            using (Font font = new Font("Segoe UI", 9f, _isSelected ? FontStyle.Bold : FontStyle.Regular))
            using (SolidBrush textBrush = new SolidBrush(_isSelected ? Color.White : (_isHovered ? Color.FromArgb(220, 235, 255) : Color.FromArgb(150, 175, 215))))
            {
                SizeF size = g.MeasureString(PillText, font);
                float textX = (Width - size.Width) / 2f;
                float textY = (Height - size.Height) / 2f;
                g.DrawString(PillText, font, textBrush, textX, textY);
            }
        }
    }

    public class SectionHeaderLabel : Control
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StepNumber { get; set; } = 1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HeaderText { get; set; } = "Section Title";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ParentBgColor { get; set; } = Color.FromArgb(13, 24, 48);

        public SectionHeaderLabel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Size = new Size(300, 28);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush parentBrush = new SolidBrush(ParentBgColor))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            // Circle badge for step number
            int circleDiameter = 22;
            int circleX = 0;
            int circleY = (Height - circleDiameter) / 2;
            Rectangle circleRect = new Rectangle(circleX, circleY, circleDiameter, circleDiameter);

            using (SolidBrush circleBrush = new SolidBrush(UITheme.AccentBlue))
            {
                g.FillEllipse(circleBrush, circleRect);
            }

            using (Font numFont = new Font("Segoe UI", 8.5f, FontStyle.Bold))
            using (SolidBrush numBrush = new SolidBrush(Color.White))
            {
                string numStr = StepNumber.ToString();
                SizeF numSize = g.MeasureString(numStr, numFont);
                g.DrawString(numStr, numFont, numBrush, circleX + (circleDiameter - numSize.Width) / 2f, circleY + (circleDiameter - numSize.Height) / 2f);
            }

            // Text next to circle
            using (Font textFont = new Font("Segoe UI", 9.5f, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                int textX = circleX + circleDiameter + 8;
                int textY = (Height - 18) / 2;
                g.DrawString(HeaderText, textFont, textBrush, textX, textY);
            }
        }
    }

    public class CircleBadge : Control
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ParentBgColor { get; set; } = Color.FromArgb(13, 24, 48);

        public CircleBadge()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Size = new Size(36, 36);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush parentBrush = new SolidBrush(ParentBgColor))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (LinearGradientBrush lgb = new LinearGradientBrush(rect, Color.FromArgb(13, 110, 253), Color.FromArgb(0, 194, 255), LinearGradientMode.ForwardDiagonal))
            {
                g.FillEllipse(lgb, rect);
            }

            // Draw white plus sign
            int cx = Width / 2;
            int cy = Height / 2;
            int arm = 6;
            using (Pen pen = new Pen(Color.White, 2.4f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, cx - arm, cy, cx + arm, cy);
                g.DrawLine(pen, cx, cy - arm, cx, cy + arm);
            }
        }
    }
}
