using System.ComponentModel;
using System.Drawing.Drawing2D;
using Point = System.Drawing.Point;
using CaroProtocol;

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

        public static PointF[] CalculateStarPoints(PointF center, float outerRadius, float innerRadius)
        {
            PointF[] points = new PointF[10];
            double rx = Math.PI / 2;
            double step = Math.PI / 5;
            for (int i = 0; i < 10; i++)
            {
                float r = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = i * step - rx;
                points[i] = new PointF((float)(center.X + r * Math.Cos(angle)), (float)(center.Y + r * Math.Sin(angle)));
            }
            return points;
        }

        public static Color ResolveParentBgColor(Control control, Color fallback)
        {
            Control? parent = control.Parent;
            while (parent != null)
            {
                if (parent is RoundedCard rc) return rc.FillColor;
                if (parent is CyberLoginCard) return Color.FromArgb(11, 20, 36);
                if (parent.BackColor != Color.Transparent && parent.BackColor.A == 255) return parent.BackColor;
                parent = parent.Parent;
            }
            return fallback;
        }
    }

    public class RoundedCard : Panel
    {
        public int CornerRadius { get; set; } = 14;

        public Color BorderColor { get; set; } = UITheme.BorderCard;

        public float BorderWidth { get; set; } = 1.2f;

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
        Settings,
        History,
        Trophy,
        User,
        Login,
        Key,
        Edit,
        Lightning,
        Refresh,
        Eye,
        Door,
        Lock,
        Star,
        Search,
        LayerStack,
        Power
    }

    public class CyberButton : Button
    {
        public int CornerRadius { get; set; } = 10;

        public Color ParentBgColor { get; set; } = Color.FromArgb(9, 15, 30);

        public Color NormalColor { get; set; } = Color.FromArgb(14, 24, 46);

        public Color HoverColor { get; set; } = Color.FromArgb(24, 42, 80);

        public Color ClickColor { get; set; } = Color.FromArgb(10, 18, 36);

        public Color BorderLineColor { get; set; } = Color.Transparent;

        public bool IsPrimaryGradient { get; set; } = false;

        public bool IsPurpleGradient { get; set; } = false;

        public ContentAlignment CustomTextAlign { get; set; } = ContentAlignment.MiddleLeft;

        public int TextLeftPadding { get; set; } = 16;

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
            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush parentBrush = new SolidBrush(effParentBg))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            Color currentBg = _isPressed ? ClickColor : (_isHovered ? HoverColor : NormalColor);
            Color textColor = ForeColor;

            if (!Enabled)
            {
                currentBg = Color.FromArgb(20, 32, 54);
                textColor = Color.FromArgb(145, 170, 205);
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
                else if (IsPurpleGradient)
                {
                    Color startCol = _isPressed ? Color.FromArgb(0, 100, 220) : (_isHovered ? Color.FromArgb(0, 160, 255) : Color.FromArgb(0, 132, 255));
                    Color endCol = _isPressed ? Color.FromArgb(160, 20, 180) : (_isHovered ? Color.FromArgb(220, 50, 240) : Color.FromArgb(192, 38, 211));
                    using (LinearGradientBrush lgb = new LinearGradientBrush(rect, startCol, endCol, LinearGradientMode.Horizontal))
                    {
                        g.FillPath(lgb, path);
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

                if (!Enabled)
                {
                    using (Pen pen = new Pen(Color.FromArgb(35, 55, 90), 1.2f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
                else if (BorderLineColor != Color.Transparent)
                {
                    using (Pen pen = new Pen(BorderLineColor, 1.2f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            // Vẽ Icon vector nếu có (chuẩn cyberpunk, không bao giờ bị lỗi font tofu)
            int effectiveLeftPad = TextLeftPadding;
            Rectangle textRect = rect;
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix;
            if (Text.Contains('\n'))
            {
                flags |= TextFormatFlags.WordBreak;
            }
            else
            {
                flags |= TextFormatFlags.SingleLine;
            }

            if (NavIcon != CyberIcon.None)
            {
                if (CustomTextAlign == ContentAlignment.MiddleCenter)
                {
                    Size textSize = TextRenderer.MeasureText(Text, Font);
                    int totalContentW = 18 + 8 + textSize.Width;
                    int startX = Math.Max(6, (Width - totalContentW) / 2);
                    DrawNavIcon(g, NavIcon, startX, (Height - 18) / 2, 18);
                    textRect = new Rectangle(startX + 24, 0, Width - (startX + 24), Height);
                    flags |= TextFormatFlags.Left;
                }
                else
                {
                    effectiveLeftPad = 46;
                    DrawNavIcon(g, NavIcon, 15, (Height - 18) / 2, 18);
                    flags |= TextFormatFlags.Left;
                    textRect = new Rectangle(effectiveLeftPad, 0, Width - effectiveLeftPad - 4, Height);
                }
            }
            else
            {
                if (CustomTextAlign == ContentAlignment.MiddleLeft)
                {
                    flags |= TextFormatFlags.Left;
                    textRect = new Rectangle(effectiveLeftPad, 0, Width - effectiveLeftPad - 4, Height);
                }
                else
                {
                    flags |= TextFormatFlags.HorizontalCenter;
                }
            }

            TextRenderer.DrawText(g, Text, Font, textRect, textColor, flags);
        }

        private void DrawNavIcon(Graphics g, CyberIcon icon, int x, int y, int size)
        {
            Color iconColor = !Enabled ? Color.FromArgb(90, 110, 140) : (IsPrimaryGradient || IsPurpleGradient ? Color.White : (_isHovered ? UITheme.AccentCyan : Color.FromArgb(180, 205, 240)));

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

                    case CyberIcon.History:
                        // Đồng hồ lịch sử
                        g.DrawEllipse(pen, x + 1, y + 1, size - 2, size - 2);
                        g.DrawLine(pen, x + size / 2, y + 4, x + size / 2, y + size / 2);
                        g.DrawLine(pen, x + size / 2, y + size / 2, x + size / 2 + 4, y + size / 2);
                        break;

                    case CyberIcon.Trophy:
                        // Chiếc cúp vô địch
                        g.DrawArc(pen, x + 3, y + 2, size - 6, 8, 0, 180);
                        g.DrawLine(pen, x + 3, y + 2, x + size - 3, y + 2);
                        g.DrawLine(pen, x + size / 2, y + 10, x + size / 2, y + size - 4);
                        g.DrawLine(pen, x + 4, y + size - 3, x + size - 4, y + size - 3);
                        break;

                    case CyberIcon.User:
                        // Biểu tượng người dùng (Đầu + Vai)
                        g.DrawEllipse(pen, x + size / 2 - 3, y + 2, 6, 6);
                        g.DrawArc(pen, x + 2, y + 9, size - 4, 10, 180, 180);
                        break;

                    case CyberIcon.Login:
                    case CyberIcon.Key:
                        // Biểu tượng Chìa khóa
                        g.DrawEllipse(pen, x + 2, y + 2, 7, 7);
                        g.DrawLine(pen, x + 9, y + 6, x + size - 2, y + size - 3);
                        g.DrawLine(pen, x + size - 6, y + size - 7, x + size - 4, y + size - 5);
                        g.DrawLine(pen, x + size - 3, y + size - 4, x + size - 1, y + size - 2);
                        break;

                    case CyberIcon.Edit:
                        // Cây bút
                        g.DrawLine(pen, x + 2, y + size - 2, x + 5, y + size - 6);
                        g.DrawLine(pen, x + 5, y + size - 6, x + size - 3, y + 4);
                        g.DrawLine(pen, x + size - 3, y + 4, x + size - 1, y + 6);
                        g.DrawLine(pen, x + size - 1, y + 6, x + 7, y + size - 4);
                        g.DrawLine(pen, x + 7, y + size - 4, x + 2, y + size - 2);
                        break;

                    case CyberIcon.Lightning:
                        // Tia chớp
                        Point[] bolt = new Point[] {
                            new Point(x + size / 2 + 1, y + 1),
                            new Point(x + 3, y + size / 2 + 1),
                            new Point(x + size / 2 + 1, y + size / 2 + 1),
                            new Point(x + size / 2 - 2, y + size - 1),
                            new Point(x + size - 1, y + size / 2 - 1),
                            new Point(x + size / 2 + 1, y + size / 2 - 1)
                        };
                        g.FillPolygon(brush, bolt);
                        break;

                    case CyberIcon.Refresh:
                        // Mũi tên tròn làm mới
                        g.DrawArc(pen, x + 2, y + 2, size - 4, size - 4, 30, 290);
                        Point[] arr = new Point[] {
                            new Point(x + size - 1, y + 2),
                            new Point(x + size - 6, y + 2),
                            new Point(x + size - 2, y + 7)
                        };
                        g.FillPolygon(brush, arr);
                        break;

                    case CyberIcon.Eye:
                        // Con mắt
                        g.DrawArc(pen, x + 1, y + 2, size - 2, size - 4, 0, 180);
                        g.DrawArc(pen, x + 1, y - 2, size - 2, size + 4, 180, 180);
                        g.FillEllipse(brush, x + size / 2 - 2, y + size / 2 - 2, 5, 5);
                        break;

                    case CyberIcon.Door:
                        // Cửa vào phòng
                        g.DrawRectangle(pen, x + 2, y + 1, size - 9, size - 2);
                        g.DrawLine(pen, x + size / 2 - 1, y + size / 2, x + size - 1, y + size / 2);
                        g.DrawLine(pen, x + size - 5, y + size / 2 - 3, x + size - 1, y + size / 2);
                        g.DrawLine(pen, x + size - 5, y + size / 2 + 3, x + size - 1, y + size / 2);
                        break;

                    case CyberIcon.Lock:
                        // Ổ khóa
                        g.DrawArc(pen, x + 4, y + 1, size - 8, 8, 180, 180);
                        g.DrawLine(pen, x + 4, y + 5, x + 4, y + 8);
                        g.DrawLine(pen, x + size - 4, y + 5, x + size - 4, y + 8);
                        g.FillRectangle(brush, x + 2, y + 8, size - 4, size - 9);
                        break;

                    case CyberIcon.Star:
                        // Ngôi sao 5 cánh
                        PointF[] starPts = UITheme.CalculateStarPoints(new PointF(x + size / 2f, y + size / 2f), size / 2f - 1f, size / 4f);
                        g.FillPolygon(brush, starPts);
                        break;

                    case CyberIcon.Search:
                        g.DrawEllipse(pen, x + 2, y + 2, size - 7, size - 7);
                        g.DrawLine(pen, x + size - 5, y + size - 5, x + size - 1, y + size - 1);
                        break;

                    case CyberIcon.LayerStack:
                        Point[] ls1 = new Point[] { new Point(x + size / 2, y + 1), new Point(x + size - 2, y + 4), new Point(x + size / 2, y + 7), new Point(x + 2, y + 4) };
                        g.DrawPolygon(pen, ls1);
                        Point[] ls2 = new Point[] { new Point(x + 2, y + 7), new Point(x + size / 2, y + 10), new Point(x + size - 2, y + 7) };
                        g.DrawLines(pen, ls2);
                        Point[] ls3 = new Point[] { new Point(x + 2, y + 10), new Point(x + size / 2, y + 13), new Point(x + size - 2, y + 10) };
                        g.DrawLines(pen, ls3);
                        break;

                    case CyberIcon.Power:
                        // Biểu tượng Đăng xuất (Nút nguồn)
                        g.DrawArc(pen, x + 2, y + 3, size - 4, size - 4, 135, 270);
                        g.DrawLine(pen, x + size / 2, y + 1, x + size / 2, y + size / 2 - 1);
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
                    byte[] bytes = File.ReadAllBytes(assetPath);
                    using var ms = new MemoryStream(bytes);
                    using var temp = Image.FromStream(ms);
                    _bannerImage = new Bitmap(temp);
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

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; Invalidate(); }
        }

        public string Title { get; set; } = "Title";

        public string Subtitle { get; set; } = "Subtitle";

        public CardIconType IconType { get; set; } = CardIconType.None;

        public Color ParentBgColor { get; set; } = Color.FromArgb(13, 24, 48);

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
            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush parentBrush = new SolidBrush(effParentBg))
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
                // Content height: Icon(20) + Title(18) + Subtitle(14) ~ 58px
                float startY = Math.Max(8f, (Height - 58f) / 2f);
                iconCenterY = startY + 11f;
                titleY = startY + 26f;
                subY = startY + 47f;
            }
            else
            {
                // Content height: Icon(20) + Title(18) ~ 40px
                float startY = Math.Max(8f, (Height - 40f) / 2f);
                iconCenterY = startY + 10f;
                titleY = startY + 25f;
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

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; Invalidate(); }
        }

        public string PillText { get; set; } = "Option";

        public string OptionText
        {
            get => PillText;
            set => PillText = value;
        }

        public Color ParentBgColor { get; set; } = Color.FromArgb(13, 24, 48);

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

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush parentBrush = new SolidBrush(effParentBg))
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
        public int StepNumber { get; set; } = 1;

        public string HeaderText { get; set; } = "Section Title";

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

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush parentBrush = new SolidBrush(effParentBg))
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

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush parentBrush = new SolidBrush(effParentBg))
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

    /// <summary>
    /// Đồng hồ đếm ngược thời gian lượt đi phong cách Cyberpunk với thanh tiến trình phát sáng.
    /// </summary>
    public class CyberTimerWidget : Panel
    {
        public int CornerRadius { get; set; } = 12;

        public bool IsCountdownMode { get; set; } = false;

        private int _remainingSeconds = 30;
        private int _totalSeconds = 30;
        private bool _isInfinite = false;
        private bool _isGameOver = false;

        public int RemainingSeconds => _remainingSeconds;
        public int TotalSeconds => _totalSeconds;

        public CyberTimerWidget()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(150, 68);
        }

        public void UpdateTime(int remaining, int total)
        {
            _remainingSeconds = Math.Max(0, remaining);
            _totalSeconds = Math.Max(1, total);
            _isInfinite = (total <= 0);
            _isGameOver = false;
            Invalidate();
        }

        public void SetGameOver()
        {
            _isGameOver = true;
            Invalidate();
        }

        public void SetInfinite()
        {
            _isInfinite = true;
            _isGameOver = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            // Xác định trạng thái màu sắc theo thời gian còn lại
            Color activeColor;
            Color cardBg = Color.FromArgb(14, 25, 48);
            Color borderColor = UITheme.BorderCard;

            if (_isGameOver)
            {
                activeColor = Color.FromArgb(130, 150, 185);
            }
            else if (_isInfinite)
            {
                activeColor = UITheme.AccentGreen; // Neon Green
            }
            else if (_remainingSeconds > 10)
            {
                activeColor = UITheme.AccentCyan; // Cyber Cyan (#00C2FF)
            }
            else if (_remainingSeconds > 5)
            {
                activeColor = UITheme.AccentOrange; // Cảnh báo vàng cam (#FF9100)
                borderColor = Color.FromArgb(180, 120, 0);
            }
            else
            {
                activeColor = UITheme.AccentRed; // Báo động đỏ neon (#FF2E63)
                borderColor = Color.FromArgb(220, 40, 80);
                cardBg = Color.FromArgb(32, 18, 30); // Ánh đỏ nhẹ khi gấp rút
            }

            // 1. Vẽ nền thẻ bo góc
            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, CornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(cardBg))
                {
                    g.FillPath(brush, path);
                }
                using (Pen pen = new Pen(borderColor, 1.2f))
                {
                    g.DrawPath(pen, path);
                }
            }

            // 2. Tiêu đề nhỏ: THỜI GIAN LƯỢT hoặc THỜI GIAN CÒN LẠI (Sleek bullet, không dùng emoji tránh lỗi cắt đỉnh glyph)
            using (SolidBrush dotBrush = new SolidBrush(Color.FromArgb(0, 210, 255)))
            {
                g.FillEllipse(dotBrush, 12, 11, 5, 5);
            }
            using (Font fontHeader = new Font("Segoe UI", 7.5f, FontStyle.Bold))
            using (SolidBrush brushHeader = new SolidBrush(UITheme.TextMuted))
            {
                string headerTitle = IsCountdownMode ? "THỜI GIAN CÒN LẠI" : "THỜI GIAN LƯỢT";
                g.DrawString(headerTitle, fontHeader, brushHeader, new PointF(21, 7));
            }

            // 3. Số đếm ngược điện tử to bản, rõ nét
            string mainText;
            Font fontMain;
            if (_isGameOver)
            {
                mainText = "KẾT THÚC";
                fontMain = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            }
            else if (_isInfinite)
            {
                mainText = "∞ VÔ HẠN";
                fontMain = new Font("Segoe UI", 12.5f, FontStyle.Bold);
            }
            else if (_remainingSeconds >= 60)
            {
                int min = _remainingSeconds / 60;
                int sec = _remainingSeconds % 60;
                mainText = $"{min:D2}:{sec:D2}";
                fontMain = new Font("Segoe UI", 16f, FontStyle.Bold);
            }
            else
            {
                mainText = $"{_remainingSeconds:D2}s";
                fontMain = new Font("Segoe UI", 16f, FontStyle.Bold);
            }

            using (fontMain)
            using (SolidBrush brushMain = new SolidBrush(activeColor))
            {
                g.DrawString(mainText, fontMain, brushMain, new PointF(10, 24));
            }

            // 4. Thanh tiến trình thời gian co dần dưới đáy thẻ
            if (!_isInfinite && !_isGameOver)
            {
                int barX = 12;
                int barY = Height - 10;
                int barWidth = Width - 24;
                int barHeight = 4;

                Rectangle barTrack = new Rectangle(barX, barY, barWidth, barHeight);
                using (GraphicsPath trackPath = UITheme.CreateRoundedRectanglePath(barTrack, 2))
                using (SolidBrush trackBrush = new SolidBrush(Color.FromArgb(25, 45, 80)))
                {
                    g.FillPath(trackBrush, trackPath);
                }

                float pct = Math.Clamp((float)_remainingSeconds / Math.Max(1, _totalSeconds), 0f, 1f);
                int fillWidth = Math.Max(3, (int)(barWidth * pct));
                Rectangle barFill = new Rectangle(barX, barY, fillWidth, barHeight);
                using (GraphicsPath fillPath = UITheme.CreateRoundedRectanglePath(barFill, 2))
                using (SolidBrush fillBrush = new SolidBrush(activeColor))
                {
                    g.FillPath(fillBrush, fillPath);
                }
            }
        }
    }

    public class CircularAvatarControl : Control
    {
        private Image? _avatarImage;
        private bool _isOnline = true;
        private bool _isHovered = false;
        private bool _isEditable = false;

        public Image? AvatarImage
        {
            get => _avatarImage;
            set { _avatarImage = value; Invalidate(); }
        }

        public bool IsOnline
        {
            get => _isOnline;
            set { _isOnline = value; Invalidate(); }
        }

        public bool IsEditable
        {
            get => _isEditable;
            set { _isEditable = value; Invalidate(); }
        }

        public bool ShowStatusDot { get; set; } = false;

        public Color BorderRingColor { get; set; } = Color.FromArgb(0, 194, 255);

        public Color ParentBgColor { get; set; } = Color.FromArgb(12, 24, 52);

        public event EventHandler? AvatarClicked;

        public CircularAvatarControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Size = new Size(86, 86);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (_isEditable)
            {
                _isHovered = true;
                Cursor = Cursors.Hand;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_isEditable)
            {
                _isHovered = false;
                Cursor = Cursors.Default;
                Invalidate();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (_isEditable)
            {
                AvatarClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        private static void DrawVectorCamera(Graphics g, Rectangle bounds, Color color)
        {
            int pad = Math.Max(2, bounds.Width / 5);
            int w = bounds.Width - pad * 2;
            int h = Math.Max(6, (int)(w * 0.72f));
            int x = bounds.X + pad;
            int y = bounds.Y + (bounds.Height - h) / 2 + 1;

            using (Pen pen = new Pen(color, 1.4f))
            using (SolidBrush brush = new SolidBrush(color))
            {
                // Top knob/flash
                int knobW = Math.Max(3, w / 3);
                g.FillRectangle(brush, x + (w - knobW) / 2, y - 2, knobW, 3);

                // Body
                using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(new Rectangle(x, y, w, h), 2))
                {
                    g.DrawPath(pen, path);
                }

                // Lens
                int lensD = Math.Max(4, h * 3 / 5);
                int lx = x + (w - lensD) / 2;
                int ly = y + (h - lensD) / 2;
                g.DrawEllipse(pen, lx, ly, lensD, lensD);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush bg = new SolidBrush(effParentBg))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            int margin = 4;
            int diameter = Math.Min(Width, Height) - margin * 2;
            int x = (Width - diameter) / 2;
            int y = (Height - diameter) / 2;
            Rectangle circleRect = new Rectangle(x, y, diameter, diameter);

            if (_avatarImage != null)
            {
                using (GraphicsPath clipPath = new GraphicsPath())
                {
                    clipPath.AddEllipse(circleRect);
                    g.SetClip(clipPath);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(_avatarImage, circleRect);
                    g.ResetClip();
                }
            }
            else
            {
                using (LinearGradientBrush lgb = new LinearGradientBrush(circleRect, Color.FromArgb(20, 48, 100), Color.FromArgb(10, 24, 52), LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(lgb, circleRect);
                }
                using (Font f = new Font("Segoe UI", 24f, FontStyle.Bold))
                using (SolidBrush b = new SolidBrush(Color.White))
                {
                    string initial = "👤";
                    SizeF sz = g.MeasureString(initial, f);
                    g.DrawString(initial, f, b, x + (diameter - sz.Width) / 2f, y + (diameter - sz.Height) / 2f);
                }
            }

            // Glowing cyan border ring
            using (Pen pen = new Pen(BorderRingColor, 2.5f))
            {
                g.DrawEllipse(pen, circleRect);
            }

            // Editable hover overlay
            if (_isEditable && _isHovered)
            {
                using (GraphicsPath clipPath = new GraphicsPath())
                {
                    clipPath.AddEllipse(circleRect);
                    g.SetClip(clipPath);
                    using (SolidBrush darkOverlay = new SolidBrush(Color.FromArgb(160, 5, 12, 28)))
                    {
                        g.FillRectangle(darkOverlay, circleRect);
                    }
                    Rectangle camBox = new Rectangle(x + (diameter - 24) / 2, y + (diameter - 34) / 2, 24, 20);
                    DrawVectorCamera(g, camBox, Color.FromArgb(0, 230, 255));

                    using (Font camFont = new Font("Segoe UI", 8.5f, FontStyle.Bold))
                    using (SolidBrush whiteBrush = new SolidBrush(Color.FromArgb(0, 230, 255)))
                    {
                        string camText = "ĐỔI ẢNH";
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString(camText, camFont, whiteBrush, new RectangleF(x, camBox.Bottom + 1, diameter, 16), sf);
                    }
                    g.ResetClip();
                }
            }

            // When editable, draw camera badge at bottom right
            if (_isEditable)
            {
                int badgeD = Math.Max(24, diameter / 4);
                // Position on rim at angle 45 deg
                float rad = 45f * (float)Math.PI / 180f;
                float cx = x + diameter / 2f;
                float cy = y + diameter / 2f;
                float r = diameter / 2f;
                float badgeCx = cx + r * (float)Math.Cos(rad);
                float badgeCy = cy + r * (float)Math.Sin(rad);
                float badgeX = badgeCx - badgeD / 2f;
                float badgeY = badgeCy - badgeD / 2f;

                // Outer cutout ring matching background
                using (SolidBrush cutBrush = new SolidBrush(effParentBg))
                {
                    g.FillEllipse(cutBrush, badgeX - 2, badgeY - 2, badgeD + 4, badgeD + 4);
                }

                // Badge body
                Color badgeBg = _isHovered ? Color.FromArgb(0, 194, 255) : Color.FromArgb(16, 28, 54);
                using (SolidBrush bBg = new SolidBrush(badgeBg))
                {
                    g.FillEllipse(bBg, badgeX, badgeY, badgeD, badgeD);
                }
                using (Pen bPen = new Pen(_isHovered ? Color.White : Color.FromArgb(0, 200, 255), 1.5f))
                {
                    g.DrawEllipse(bPen, badgeX, badgeY, badgeD, badgeD);
                }

                // Camera glyph vector
                Rectangle camRect = new Rectangle((int)badgeX + 3, (int)badgeY + 3, badgeD - 6, badgeD - 6);
                DrawVectorCamera(g, camRect, _isHovered ? Color.Black : Color.FromArgb(0, 220, 255));
            }
            else if (ShowStatusDot && _isOnline)
            {
                // Normal online status dot on rim at 45 deg
                int dotD = Math.Max(14, diameter / 5);
                float rad = 45f * (float)Math.PI / 180f;
                float cx = x + diameter / 2f;
                float cy = y + diameter / 2f;
                float r = diameter / 2f;
                float dotCx = cx + r * (float)Math.Cos(rad);
                float dotCy = cy + r * (float)Math.Sin(rad);
                float dotX = dotCx - dotD / 2f;
                float dotY = dotCy - dotD / 2f;

                using (SolidBrush darkBrush = new SolidBrush(effParentBg))
                {
                    g.FillEllipse(darkBrush, dotX - 2, dotY - 2, dotD + 4, dotD + 4);
                }
                using (SolidBrush greenBrush = new SolidBrush(Color.FromArgb(0, 230, 118)))
                {
                    g.FillEllipse(greenBrush, dotX, dotY, dotD, dotD);
                }
            }
        }
    }

    public class WinRateDonutGauge : Control
    {
        private int _winRate = 0;

        public int WinRate
        {
            get => _winRate;
            set { _winRate = Math.Clamp(value, 0, 100); Invalidate(); }
        }

        public string Title { get; set; } = "Tỉ lệ thắng";

        public Color ParentBgColor { get; set; } = Color.FromArgb(12, 24, 52);

        public Color GaugeColor { get; set; } = Color.FromArgb(0, 210, 255);

        public WinRateDonutGauge()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Size = new Size(86, 96);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush bg = new SolidBrush(effParentBg))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            int ringSize = 64;
            int ringX = (Width - ringSize) / 2;
            int ringY = 2;
            Rectangle ringRect = new Rectangle(ringX + 4, ringY + 4, ringSize - 8, ringSize - 8);

            float strokeW = 6.5f;

            // Background track
            using (Pen trackPen = new Pen(Color.FromArgb(20, 38, 76), strokeW))
            {
                g.DrawEllipse(trackPen, ringRect);
            }

            // Progress Arc
            if (_winRate > 0)
            {
                float sweepAngle = (_winRate / 100f) * 360f;
                using (Pen arcPen = new Pen(GaugeColor, strokeW))
                {
                    arcPen.StartCap = LineCap.Round;
                    arcPen.EndCap = LineCap.Round;
                    g.DrawArc(arcPen, ringRect, -90f, sweepAngle);
                }
            }

            // Percentage Text in center
            using (Font f = new Font("Segoe UI", 12.5f, FontStyle.Bold))
            using (SolidBrush b = new SolidBrush(Color.White))
            {
                string text = $"{_winRate}%";
                SizeF sz = g.MeasureString(text, f);
                g.DrawString(text, f, b, ringX + (ringSize - sz.Width) / 2f, ringY + (ringSize - sz.Height) / 2f);
            }

            // Subtitle below circle
            using (Font subF = new Font("Segoe UI", 8.25f, FontStyle.Regular))
            using (SolidBrush subB = new SolidBrush(Color.FromArgb(145, 175, 215)))
            {
                SizeF sz = g.MeasureString(Title, subF);
                g.DrawString(Title, subF, subB, (Width - sz.Width) / 2f, ringY + ringSize + 6);
            }
        }
    }

    public enum KpiIconType { Trophy, Checkmark, Cross }

    public class KpiStatBox : Control
    {
        public KpiIconType IconType { get; set; } = KpiIconType.Trophy;

        public string ValueText { get; set; } = "0";

        public string LabelText { get; set; } = "Tổng trận";

        public Color ParentBgColor { get; set; } = Color.FromArgb(12, 24, 52);

        public KpiStatBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Size = new Size(68, 86);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush bg = new SolidBrush(effParentBg))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            Rectangle boxRect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(boxRect, 8))
            {
                using (SolidBrush b = new SolidBrush(Color.FromArgb(10, 20, 44)))
                {
                    g.FillPath(b, path);
                }
                using (Pen p = new Pen(Color.FromArgb(24, 46, 88), 1f))
                {
                    g.DrawPath(p, path);
                }
            }

            int iconSize = 20;
            int iconX = (Width - iconSize) / 2;
            int iconY = 8;

            switch (IconType)
            {
                case KpiIconType.Trophy:
                    Rectangle trophyCircle = new Rectangle(iconX, iconY, iconSize, iconSize);
                    using (SolidBrush tb = new SolidBrush(Color.FromArgb(245, 158, 11)))
                    {
                        g.FillEllipse(tb, trophyCircle);
                    }
                    using (Pen tp = new Pen(Color.White, 1.6f))
                    {
                        tp.StartCap = LineCap.Round;
                        tp.EndCap = LineCap.Round;
                        // Cup body
                        g.DrawArc(tp, iconX + 5, iconY + 4, iconSize - 10, 8, 0, 180);
                        g.DrawLine(tp, iconX + 5, iconY + 4, iconX + iconSize - 5, iconY + 4);
                        // Stem & base
                        g.DrawLine(tp, iconX + iconSize / 2, iconY + 12, iconX + iconSize / 2, iconY + 15);
                        g.DrawLine(tp, iconX + 6, iconY + 15, iconX + iconSize - 6, iconY + 15);
                        // Handles
                        g.DrawArc(tp, iconX + 3, iconY + 5, 4, 4, 90, 180);
                        g.DrawArc(tp, iconX + iconSize - 7, iconY + 5, 4, 4, 270, 180);
                    }
                    break;

                case KpiIconType.Checkmark:
                    Rectangle checkCircle = new Rectangle(iconX, iconY, iconSize, iconSize);
                    using (SolidBrush cb = new SolidBrush(Color.FromArgb(0, 160, 80)))
                    {
                        g.FillEllipse(cb, checkCircle);
                    }
                    using (Pen cp = new Pen(Color.White, 2f))
                    {
                        cp.StartCap = LineCap.Round;
                        cp.EndCap = LineCap.Round;
                        g.DrawLine(cp, iconX + 5, iconY + 10, iconX + 8, iconY + 14);
                        g.DrawLine(cp, iconX + 8, iconY + 14, iconX + 15, iconY + 6);
                    }
                    break;

                case KpiIconType.Cross:
                    Rectangle crossCircle = new Rectangle(iconX, iconY, iconSize, iconSize);
                    using (SolidBrush rb = new SolidBrush(Color.FromArgb(210, 40, 65)))
                    {
                        g.FillEllipse(rb, crossCircle);
                    }
                    using (Pen xp = new Pen(Color.White, 2f))
                    {
                        xp.StartCap = LineCap.Round;
                        xp.EndCap = LineCap.Round;
                        g.DrawLine(xp, iconX + 6, iconY + 6, iconX + 14, iconY + 14);
                        g.DrawLine(xp, iconX + 14, iconY + 6, iconX + 6, iconY + 14);
                    }
                    break;
            }

            // Value Text
            using (Font vf = new Font("Segoe UI", 15f, FontStyle.Bold))
            using (SolidBrush vb = new SolidBrush(Color.White))
            {
                SizeF sz = g.MeasureString(ValueText, vf);
                g.DrawString(ValueText, vf, vb, (Width - sz.Width) / 2f, 31);
            }

            // Label Text
            using (Font lf = new Font("Segoe UI", 8f))
            using (SolidBrush lb = new SolidBrush(Color.FromArgb(145, 175, 215)))
            {
                SizeF sz = g.MeasureString(LabelText, lf);
                g.DrawString(LabelText, lf, lb, (Width - sz.Width) / 2f, 55);
            }
        }
    }

    public class QuickStatItem : Control
    {
        public enum QuickStatIcon { Swords, Trophy, Star }

        public QuickStatIcon IconType { get; set; } = QuickStatIcon.Swords;

        public string ValueText { get; set; } = "0";

        public string LabelText { get; set; } = "Tổng trận";

        public Color ParentBgColor { get; set; } = Color.FromArgb(12, 24, 52);

        public QuickStatItem()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Size = new Size(110, 80);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush bg = new SolidBrush(effParentBg))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            int cx = Width / 2;

            switch (IconType)
            {
                case QuickStatIcon.Swords:
                    using (Pen sp = new Pen(Color.FromArgb(0, 194, 255), 2f))
                    {
                        sp.StartCap = LineCap.Round;
                        sp.EndCap = LineCap.Round;
                        g.DrawLine(sp, cx - 10, 5, cx + 10, 23);
                        g.DrawLine(sp, cx + 10, 5, cx - 10, 23);
                        g.DrawLine(sp, cx - 8, 17, cx - 3, 22);
                        g.DrawLine(sp, cx + 8, 17, cx + 3, 22);
                    }
                    break;

                case QuickStatIcon.Trophy:
                    using (Pen tp = new Pen(Color.FromArgb(255, 200, 50), 2f))
                    {
                        tp.StartCap = LineCap.Round;
                        tp.EndCap = LineCap.Round;
                        g.DrawArc(tp, cx - 9, 3, 18, 12, 0, 180);
                        g.DrawLine(tp, cx - 9, 3, cx + 9, 3);
                        g.DrawLine(tp, cx, 15, cx, 21);
                        g.DrawLine(tp, cx - 6, 21, cx + 6, 21);
                    }
                    break;

                case QuickStatIcon.Star:
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(255, 195, 45)))
                    {
                        PointF[] starPts = UITheme.CalculateStarPoints(new PointF(cx, 13), 10, 4.5f);
                        g.FillPolygon(sb, starPts);
                    }
                    break;
            }

            // Value text
            using (Font vf = new Font("Segoe UI", 15f, FontStyle.Bold))
            using (SolidBrush vb = new SolidBrush(Color.White))
            {
                SizeF sz = g.MeasureString(ValueText, vf);
                g.DrawString(ValueText, vf, vb, (Width - sz.Width) / 2f, 29);
            }

            // Label text
            using (Font lf = new Font("Segoe UI", 8.5f))
            using (SolidBrush lb = new SolidBrush(Color.FromArgb(145, 175, 215)))
            {
                SizeF sz = g.MeasureString(LabelText, lf);
                g.DrawString(LabelText, lf, lb, (Width - sz.Width) / 2f, 57);
            }
        }
    }

    public class AchievementBadgeControl : Control
    {
        private Image? _badgeImage;

        public Image? BadgeImage
        {
            get => _badgeImage;
            set { _badgeImage = value; Invalidate(); }
        }

        public string BadgeTitle { get; set; } = "Tân Thủ";

        public Color ParentBgColor { get; set; } = Color.FromArgb(12, 24, 52);

        public AchievementBadgeControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            Size = new Size(80, 80);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color effParentBg = UITheme.ResolveParentBgColor(this, ParentBgColor);
            using (SolidBrush bg = new SolidBrush(effParentBg))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            int iconSize = 48;
            int iconX = (Width - iconSize) / 2;
            int iconY = 2;

            if (_badgeImage != null)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(_badgeImage, iconX, iconY, iconSize, iconSize);
            }
            else
            {
                Rectangle r = new Rectangle(iconX, iconY, iconSize, iconSize);
                using (GraphicsPath p = UITheme.CreateRoundedRectanglePath(r, 8))
                using (SolidBrush b = new SolidBrush(Color.FromArgb(20, 36, 70)))
                {
                    g.FillPath(b, p);
                }
            }

            // Title text below
            using (Font f = new Font("Segoe UI", 8.25f, FontStyle.Bold))
            using (SolidBrush b = new SolidBrush(Color.FromArgb(220, 235, 255)))
            {
                SizeF sz = g.MeasureString(BadgeTitle, f);
                g.DrawString(BadgeTitle, f, b, (Width - sz.Width) / 2f, 56);
            }
        }
    }

    public class VectorIconLabel : Control
    {
        private CyberIcon _icon = CyberIcon.None;
        private Color _iconColor = Color.FromArgb(140, 165, 200);

        public CyberIcon Icon
        {
            get => _icon;
            set { _icon = value; Invalidate(); }
        }

        public Color IconColor
        {
            get => _iconColor;
            set { _iconColor = value; Invalidate(); }
        }

        public override Color ForeColor
        {
            get => _iconColor;
            set { _iconColor = value; Invalidate(); }
        }

        public VectorIconLabel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(18, 18);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color effBg = UITheme.ResolveParentBgColor(this, Color.FromArgb(12, 24, 52));
            using (SolidBrush bgBrush = new SolidBrush(effBg))
            {
                g.FillRectangle(bgBrush, ClientRectangle);
            }

            int size = Math.Min(Width, Height);
            int x = (Width - size) / 2;
            int y = (Height - size) / 2;

            using (Pen pen = new Pen(_iconColor, 1.8f))
            using (SolidBrush brush = new SolidBrush(_iconColor))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                switch (_icon)
                {
                    case CyberIcon.Edit:
                        // Modern pencil icon
                        pen.Width = 1.6f;
                        g.DrawLine(pen, x + 3, y + size - 5, x + size - 5, y + 3);
                        g.DrawLine(pen, x + 5, y + size - 3, x + size - 3, y + 5);
                        g.DrawLine(pen, x + size - 5, y + 3, x + size - 3, y + 5);
                        PointF[] editTip = new PointF[] {
                            new PointF(x + 3, y + size - 5),
                            new PointF(x + 5, y + size - 3),
                            new PointF(x + 1, y + size - 1)
                        };
                        g.FillPolygon(brush, editTip);
                        break;
                    case CyberIcon.Search:
                        g.DrawEllipse(pen, x + 2, y + 2, size - 7, size - 7);
                        g.DrawLine(pen, x + size - 5, y + size - 5, x + size - 1, y + size - 1);
                        break;

                    case CyberIcon.User:
                        g.FillEllipse(brush, x + size / 2 - 3, y + 2, 6, 6);
                        g.FillPie(brush, x + 1, y + 9, size - 2, 10, 180, 180);
                        break;

                    case CyberIcon.Lock:
                        pen.Width = 1.5f;
                        g.DrawArc(pen, x + 3, y + 1, size - 6, 7, 180, 180);
                        g.DrawLine(pen, x + 3, y + 4, x + 3, y + 7);
                        g.DrawLine(pen, x + size - 3, y + 4, x + size - 3, y + 7);
                        g.FillRectangle(brush, x + 1, y + 7, size - 2, size - 8);
                        break;

                    case CyberIcon.Eye:
                        g.DrawArc(pen, x + 1, y + 2, size - 2, size - 4, 0, 180);
                        g.DrawArc(pen, x + 1, y - 2, size - 2, size + 4, 180, 180);
                        g.FillEllipse(brush, x + size / 2 - 2, y + size / 2 - 2, 4, 4);
                        break;

                    case CyberIcon.LayerStack:
                        Point[] ls1 = new Point[] { new Point(x + size / 2, y + 1), new Point(x + size - 2, y + 4), new Point(x + size / 2, y + 7), new Point(x + 2, y + 4) };
                        g.DrawPolygon(pen, ls1);
                        Point[] ls2 = new Point[] { new Point(x + 2, y + 7), new Point(x + size / 2, y + 10), new Point(x + size - 2, y + 7) };
                        g.DrawLines(pen, ls2);
                        Point[] ls3 = new Point[] { new Point(x + 2, y + 10), new Point(x + size / 2, y + 13), new Point(x + size - 2, y + 10) };
                        g.DrawLines(pen, ls3);
                        break;

                    case CyberIcon.Power:
                        // Biểu tượng Đăng xuất (Nút nguồn)
                        g.DrawArc(pen, x + 2, y + 3, size - 4, size - 4, 135, 270);
                        g.DrawLine(pen, x + size / 2, y + 1, x + size / 2, y + size / 2 - 1);
                        break;
                }
            }
        }
    }

    public class LobbyHeaderIconBox : Control
    {
        public LobbyHeaderIconBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(46, 46);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(r, 12))
            {
                using (SolidBrush bg = new SolidBrush(Color.FromArgb(18, 30, 56)))
                    g.FillPath(bg, path);
                using (Pen border = new Pen(Color.FromArgb(32, 54, 98), 1.2f))
                    g.DrawPath(border, path);
            }

            int totalW = 30;
            int totalH = 22;
            int startX = (Width - totalW) / 2;
            int startY = (Height - totalH) / 2;

            using (SolidBrush ib = new SolidBrush(Color.FromArgb(0, 194, 255)))
            {
                // Person 1 (left)
                int headSize = 9;
                int bodyW = 18;
                int bodyH = 13;
                g.FillEllipse(ib, startX + 2, startY, headSize, headSize);
                g.FillPie(ib, startX, startY + headSize + 1, bodyW, bodyH, 180, 180);

                // Person 2 (right)
                g.FillEllipse(ib, startX + 17, startY, headSize, headSize);
                g.FillPie(ib, startX + 12, startY + headSize + 1, bodyW, bodyH, 180, 180);
            }
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class LobbyBannerPanel : Panel
    {
        private Image? _bannerImage;

        public LobbyBannerPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            try
            {
                string assetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "lobby_header_3d.jpg");
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "lobby_header_3d.jpg");
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets", "lobby_header_3d.jpg");
                if (!File.Exists(assetPath)) assetPath = @"d:\PBL4\CaroClient\Assets\lobby_header_3d.jpg";
                if (File.Exists(assetPath))
                {
                    byte[] bytes = File.ReadAllBytes(assetPath);
                    using var ms = new MemoryStream(bytes);
                    using var temp = Image.FromStream(ms);
                    _bannerImage = new Bitmap(temp);
                }
            }
            catch { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color effParentBg = UITheme.ResolveParentBgColor(this, Color.FromArgb(8, 14, 26));
            using (SolidBrush parentBrush = new SolidBrush(effParentBg))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, 14))
            {
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(10, 18, 36)))
                {
                    g.FillPath(bgBrush, path);
                }

                if (_bannerImage != null)
                {
                    g.SetClip(path);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Draw image covering the entire banner
                    g.DrawImage(_bannerImage, new Rectangle(0, 0, Width, Height));

                    // Smooth dark overlay on the left so that title and subtitle have crystal-clear contrast
                    using (LinearGradientBrush darkFade = new LinearGradientBrush(
                        rect,
                        Color.FromArgb(235, 8, 15, 30),
                        Color.FromArgb(20, 8, 15, 30),
                        LinearGradientMode.Horizontal))
                    {
                        ColorBlend blend = new ColorBlend();
                        blend.Colors = new Color[] {
                            Color.FromArgb(245, 8, 15, 30),
                            Color.FromArgb(210, 8, 15, 30),
                            Color.FromArgb(120, 8, 15, 30),
                            Color.FromArgb(20, 8, 15, 30)
                        };
                        blend.Positions = new float[] { 0.0f, 0.40f, 0.70f, 1.0f };
                        darkFade.InterpolationColors = blend;
                        g.FillRectangle(darkFade, rect);
                    }

                    g.ResetClip();
                }

                // Subtle cyber border
                using (Pen borderPen = new Pen(Color.FromArgb(32, 64, 115), 1.5f))
                {
                    g.DrawPath(borderPen, path);
                }

                // Top cyan accent glow line
                using (Pen glowPen = new Pen(Color.FromArgb(0, 194, 255), 1.2f))
                {
                    g.DrawLine(glowPen, rect.Left + 18, rect.Top, rect.Left + 160, rect.Top);
                }
            }
        }
    }

    public class LobbyHeaderArtwork : Control
    {
        private Image? _header3dImage;

        public LobbyHeaderArtwork()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(340, 75);

            try
            {
                string assetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "lobby_header_3d.jpg");
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "lobby_header_3d.jpg");
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets", "lobby_header_3d.jpg");
                if (File.Exists(assetPath))
                {
                    byte[] bytes = File.ReadAllBytes(assetPath);
                    using var ms = new MemoryStream(bytes);
                    using var temp = Image.FromStream(ms);
                    _header3dImage = new Bitmap(temp);
                }
            }
            catch { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (_header3dImage != null)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(_header3dImage, new Rectangle(0, 0, Width, Height));
            }
        }
    }

    public class LobbyFilterPill : Control
    {
        private bool _isSelected;
        private bool _isHovered;
        private string _filterKey = "all";

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; Invalidate(); }
        }

        public string FilterKey
        {
            get => _filterKey;
            set => _filterKey = value;
        }

        public LobbyFilterPill()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(95, 32);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Do not paint default grey control background
        }

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color parentBg = Color.FromArgb(8, 14, 26);
            Control? parentCtrl = Parent;
            while (parentCtrl != null)
            {
                if (parentCtrl.BackColor != Color.Transparent && parentCtrl.BackColor.A > 0)
                {
                    parentBg = parentCtrl.BackColor;
                    break;
                }
                parentCtrl = parentCtrl.Parent;
            }
            using (SolidBrush pb = new SolidBrush(parentBg))
            {
                g.FillRectangle(pb, ClientRectangle);
            }

            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = Height / 2;

            Color bg = _isSelected
                ? Color.FromArgb(0, 132, 255)
                : (_isHovered ? Color.FromArgb(22, 36, 64) : Color.FromArgb(14, 24, 46));
            Color border = _isSelected
                ? Color.FromArgb(0, 194, 255)
                : (_isHovered ? Color.FromArgb(48, 76, 120) : Color.FromArgb(28, 48, 80));
            Color textCol = _isSelected
                ? Color.White
                : (_isHovered ? Color.FromArgb(220, 235, 255) : Color.FromArgb(140, 165, 200));

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(r, radius))
            {
                using (SolidBrush b = new SolidBrush(bg)) g.FillPath(b, path);
                using (Pen p = new Pen(border, 1.2f)) g.DrawPath(p, path);
            }

            int textLeft = 14;

            // Draw crisp vector icon based on FilterKey
            if (_filterKey == "all")
            {
                int sz = 3;
                int gap = 2;
                int ix = 9;
                int iy = (Height - (sz * 2 + gap)) / 2;
                Color ic = _isSelected ? Color.White : Color.FromArgb(0, 194, 255);
                using (SolidBrush ib = new SolidBrush(ic))
                {
                    g.FillRectangle(ib, ix, iy, sz, sz);
                    g.FillRectangle(ib, ix + sz + gap, iy, sz, sz);
                    g.FillRectangle(ib, ix, iy + sz + gap, sz, sz);
                    g.FillRectangle(ib, ix + sz + gap, iy + sz + gap, sz, sz);
                }
                textLeft = ix + sz * 2 + gap + 6;
            }
            else if (_filterKey == "2p" || _filterKey == "4p")
            {
                int ix = 9;
                int iy = Height / 2;
                Color ic = _isSelected ? Color.White : Color.FromArgb(140, 170, 210);
                using (SolidBrush ib = new SolidBrush(ic))
                {
                    g.FillEllipse(ib, ix, iy - 6, 5, 5);
                    g.FillPie(ib, ix - 2, iy + 1, 9, 7, 180, 180);
                    g.FillEllipse(ib, ix + 5, iy - 6, 5, 5);
                    g.FillPie(ib, ix + 3, iy + 1, 9, 7, 180, 180);
                }
                textLeft = ix + 16;
            }
            else if (_filterKey == "waiting")
            {
                int ix = 9;
                int iy = Height / 2;
                using (SolidBrush gb = new SolidBrush(Color.FromArgb(0, 230, 118)))
                {
                    g.FillEllipse(gb, ix, iy - 4, 7, 7);
                }
                textLeft = ix + 13;
            }

            Rectangle textRect = new Rectangle(textLeft, 0, Width - textLeft - 4, Height);
            using (Font f = new Font("Segoe UI", 9f, _isSelected ? FontStyle.Bold : FontStyle.Regular))
            using (SolidBrush tb = new SolidBrush(textCol))
            using (StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.EllipsisCharacter
            })
            {
                g.DrawString(Text, f, tb, textRect, sf);
            }
        }
    }

    public class MiniBoardPreviewControl : Control
    {
        private Image? _boardImg;
        private Image? _customImg;

        public void SetCustomImage(Image? img)
        {
            _customImg = img;
            Invalidate();
        }

        public MiniBoardPreviewControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Size = new Size(95, 95);

            try
            {
                string assetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "mini_board_preview.jpg");
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "mini_board_preview.jpg");
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets", "mini_board_preview.jpg");
                if (File.Exists(assetPath))
                {
                    byte[] bytes = File.ReadAllBytes(assetPath);
                    using var ms = new MemoryStream(bytes);
                    using var temp = Image.FromStream(ms);
                    _boardImg = new Bitmap(temp);
                }
            }
            catch { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(r, 12))
            {
                using (SolidBrush b = new SolidBrush(Color.FromArgb(12, 22, 44))) g.FillRectangle(b, ClientRectangle);

                g.SetClip(path);
                Image? toDraw = _customImg ?? _boardImg;
                if (toDraw != null)
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(toDraw, new Rectangle(0, 0, Width, Height));
                }
                g.ResetClip();

                using (Pen p = new Pen(Color.FromArgb(0, 194, 255), 1.3f)) g.DrawPath(p, path);
            }
        }
    }

    public class RoomListItemControl : Control
    {
        private RoomInfoData _roomData;
        private Image? _hostAvatar;
        private bool _isHovered;
        private Rectangle _btnRect;

        public RoomInfoData RoomData => _roomData;

        public Image? HostAvatar
        {
            get => _hostAvatar;
            set { _hostAvatar = value; Invalidate(); }
        }

        public event Action<RoomInfoData>? JoinClicked;
        public event Action<RoomInfoData>? SpectateClicked;

        public RoomListItemControl(RoomInfoData roomData, Image? hostAvatar = null)
        {
            _roomData = roomData;
            _hostAvatar = hostAvatar;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Height = 66;
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = _btnRect.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (_roomData.CanJoinAsPlayer)
            {
                JoinClicked?.Invoke(_roomData);
            }
            else
            {
                SpectateClicked?.Invoke(_roomData);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Xóa sạch nền bằng màu cha để bo góc mềm mại không bị vệt trắng
            Color effParentBg = UITheme.ResolveParentBgColor(this, Color.FromArgb(13, 22, 40));
            using (SolidBrush parentBrush = new SolidBrush(effParentBg))
            {
                g.FillRectangle(parentBrush, ClientRectangle);
            }

            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            Color bg = _isHovered ? Color.FromArgb(21, 33, 56) : Color.FromArgb(16, 25, 45);
            Color border = _isHovered ? Color.FromArgb(42, 70, 118) : Color.FromArgb(28, 43, 75);

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(r, 12))
            {
                using (SolidBrush b = new SolidBrush(bg)) g.FillPath(b, path);
                using (Pen p = new Pen(border, 1.2f)) g.DrawPath(p, path);
            }

            // 1. Host Avatar (Circular avatar image - User constraint!)
            int avSize = 44;
            int avX = 12;
            int avY = (Height - avSize) / 2;
            Rectangle avRect = new Rectangle(avX, avY, avSize, avSize);

            if (_hostAvatar != null)
            {
                using (GraphicsPath clipPath = new GraphicsPath())
                {
                    clipPath.AddEllipse(avRect);
                    g.SetClip(clipPath);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(_hostAvatar, avRect);
                    g.ResetClip();
                }
            }
            else
            {
                using (LinearGradientBrush lgb = new LinearGradientBrush(avRect, Color.FromArgb(25, 55, 110), Color.FromArgb(14, 30, 65), LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(lgb, avRect);
                }
                using (Font f = new Font("Segoe UI", 12f, FontStyle.Bold))
                using (SolidBrush b = new SolidBrush(Color.FromArgb(0, 194, 255)))
                {
                    string initial = !string.IsNullOrWhiteSpace(_roomData.Player1Name) ? _roomData.Player1Name.Substring(0, 1).ToUpper() : "C";
                    SizeF sz = g.MeasureString(initial, f);
                    g.DrawString(initial, f, b, avX + (avSize - sz.Width) / 2f, avY + (avSize - sz.Height) / 2f);
                }
            }

            // Avatar ring border
            using (Pen ringPen = new Pen(Color.FromArgb(0, 194, 255), 1.5f))
            {
                g.DrawEllipse(ringPen, avRect);
            }

            // Columns layout calculation (Responsive: khong bao gio bi de chu hay de cot len nhau)
            bool isWaiting = _roomData.CanJoinAsPlayer;
            int btnW = isWaiting ? 90 : 78;
            int btnH = 32;
            int btnX = Width - btnW - 10;
            int btnY = (Height - btnH) / 2;
            _btnRect = new Rectangle(btnX, btnY, btnW, btnH);

            int statusW = 82;
            int statusX = btnX - statusW - 8;

            bool showTime = Width >= 560;
            int timeW = showTime ? 74 : 0;
            int timeX = showTime ? (statusX - timeW - 10) : statusX;

            bool showMode = Width >= 400;
            int modeW = showMode ? 66 : 0;
            int modeX = showMode ? (timeX - modeW - 10) : timeX;

            int titleX = avX + avSize + 10;
            int titleW = Math.Max(110, modeX - titleX - 8);

            // 2. Room Name & Host
            string roomTitle = !string.IsNullOrWhiteSpace(_roomData.Player1Name) ? $"Phòng của {_roomData.Player1Name}" : $"Phòng #{_roomData.RoomId}";

            using (Font fTitle = new Font("Segoe UI", 9.5f, FontStyle.Bold))
            using (SolidBrush bTitle = new SolidBrush(Color.White))
            {
                Rectangle titleRect = new Rectangle(titleX, 13, titleW, 20);
                StringFormat sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
                g.DrawString(roomTitle, fTitle, bTitle, titleRect, sf);

                // Draw gold crown for rooms with long / ranked time
                if (_roomData.TimePerTurn >= 900)
                {
                    SizeF titleSz = g.MeasureString(roomTitle, fTitle);
                    int cx = titleX + Math.Min((int)titleSz.Width + 4, titleW - 14);
                    using (SolidBrush crownBrush = new SolidBrush(Color.FromArgb(255, 200, 0)))
                    {
                        Point[] crown = new Point[] {
                            new Point(cx, 23),
                            new Point(cx, 16),
                            new Point(cx + 3, 19),
                            new Point(cx + 6, 14),
                            new Point(cx + 9, 19),
                            new Point(cx + 12, 16),
                            new Point(cx + 12, 23)
                        };
                        g.FillPolygon(crownBrush, crown);
                    }
                }
            }

            // Vector user icon + host name (safe from tofu)
            int hx = titleX;
            int hy = 37;
            using (SolidBrush bHostIcon = new SolidBrush(Color.FromArgb(120, 145, 180)))
            {
                g.FillEllipse(bHostIcon, hx + 1, hy, 5, 5);
                g.FillPie(bHostIcon, hx - 1, hy + 5, 9, 7, 180, 180);
            }
            using (Font fHost = new Font("Segoe UI", 8.25f))
            using (SolidBrush bHost = new SolidBrush(Color.FromArgb(142, 160, 192)))
            {
                Rectangle hostRect = new Rectangle(titleX + 13, 35, titleW - 13, 18);
                StringFormat sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
                g.DrawString(_roomData.Player1Name, fHost, bHost, hostRect, sf);
            }

            // 3. Mode & Capacity
            if (showMode)
            {
                string modeStr = _roomData.Mode == GameMode.PvAI ? "Vs Máy" : "2 người";
                string capStr = _roomData.CanJoinAsPlayer ? "1/2" : "2/2";
                int mx = modeX;
                using (SolidBrush bModeIcon = new SolidBrush(Color.FromArgb(140, 170, 210)))
                {
                    g.FillEllipse(bModeIcon, mx + 1, 16, 5, 5);
                    g.FillPie(bModeIcon, mx - 1, 21, 9, 7, 180, 180);
                    g.FillEllipse(bModeIcon, mx + 6, 16, 5, 5);
                    g.FillPie(bModeIcon, mx + 4, 21, 9, 7, 180, 180);
                }
                using (Font fMode = new Font("Segoe UI", 8.5f))
                using (SolidBrush bMode = new SolidBrush(Color.FromArgb(200, 215, 240)))
                {
                    g.DrawString(modeStr, fMode, bMode, modeX + 17, 14);
                }
                using (Font fCap = new Font("Segoe UI", 8f))
                using (SolidBrush bCap = new SolidBrush(Color.FromArgb(107, 122, 153)))
                {
                    g.DrawString(capStr, fCap, bCap, modeX + 17, 35);
                }
            }

            // 4. Time & Rule
            if (showTime)
            {
                string timeStr = _roomData.TimePerTurn > 0
                    ? (_roomData.TimePerTurn >= 60 ? $"{_roomData.TimePerTurn / 60} phút" : $"{_roomData.TimePerTurn}s")
                    : "Không giới hạn";
                string subTimeStr = _roomData.TimePerTurn > 0 ? "Bắt đầu sau" : "";
                int tx = timeX;
                using (Pen pClock = new Pen(Color.FromArgb(140, 170, 210), 1.2f))
                {
                    g.DrawEllipse(pClock, tx, 16, 10, 10);
                    g.DrawLine(pClock, tx + 5, 18, tx + 5, 21);
                    g.DrawLine(pClock, tx + 5, 21, tx + 8, 21);
                }
                using (Font fTime = new Font("Segoe UI", 8.5f))
                using (SolidBrush bTime = new SolidBrush(Color.FromArgb(200, 215, 240)))
                {
                    g.DrawString(timeStr, fTime, bTime, timeX + 14, 14);
                }
                using (Font fRule = new Font("Segoe UI", 8f))
                using (SolidBrush bRule = new SolidBrush(Color.FromArgb(107, 122, 153)))
                {
                    g.DrawString(subTimeStr, fRule, bRule, timeX + 14, 35);
                }
            }

            // 5. Status Badge
            string statusText = isWaiting ? "Đang chờ" : "Đang thi đấu";
            Color statusColor = isWaiting ? Color.FromArgb(0, 210, 106) : Color.FromArgb(0, 194, 255);

            using (SolidBrush dotBrush = new SolidBrush(statusColor))
            {
                g.FillEllipse(dotBrush, statusX, 26, 7, 7);
            }
            using (Font fStat = new Font("Segoe UI", 8.25f, FontStyle.Bold))
            using (SolidBrush bStat = new SolidBrush(statusColor))
            {
                g.DrawString(statusText, fStat, bStat, statusX + 11, 22);
            }

            // 6. Action Button
            using (GraphicsPath btnPath = UITheme.CreateRoundedRectanglePath(_btnRect, 10))
            {
                if (isWaiting)
                {
                    using (LinearGradientBrush lgb = new LinearGradientBrush(_btnRect, Color.FromArgb(0, 132, 255), Color.FromArgb(0, 194, 255), LinearGradientMode.Horizontal))
                    {
                        g.FillPath(lgb, btnPath);
                    }

                    // Vector paper-airplane / play arrow
                    int ax = btnX + 12;
                    int ay = btnY + (btnH - 12) / 2;
                    using (SolidBrush bArrow = new SolidBrush(Color.White))
                    {
                        Point[] arrow = new Point[] {
                            new Point(ax, ay),
                            new Point(ax + 9, ay + 6),
                            new Point(ax, ay + 12)
                        };
                        g.FillPolygon(bArrow, arrow);
                    }

                    using (Font fBtn = new Font("Segoe UI", 8.5f, FontStyle.Bold))
                    using (SolidBrush bBtn = new SolidBrush(Color.White))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        Rectangle textR = new Rectangle(btnX + 12, btnY, btnW - 12, btnH);
                        g.DrawString("Tham gia", fBtn, bBtn, textR, sf);
                    }
                }
                else
                {
                    using (SolidBrush bBtnBg = new SolidBrush(Color.FromArgb(20, 34, 58)))
                    {
                        g.FillPath(bBtnBg, btnPath);
                    }
                    using (Pen pBtn = new Pen(Color.FromArgb(38, 64, 105), 1.2f))
                    {
                        g.DrawPath(pBtn, btnPath);
                    }
                    int eX = btnX + 14;
                    int eY = btnY + (btnH - 12) / 2;
                    using (Pen pEye = new Pen(Color.FromArgb(180, 210, 255), 1.2f))
                    using (SolidBrush bPupil = new SolidBrush(Color.FromArgb(180, 210, 255)))
                    {
                        g.DrawArc(pEye, eX, eY, 13, 9, 0, 180);
                        g.DrawArc(pEye, eX, eY - 2, 13, 11, 180, 180);
                        g.FillEllipse(bPupil, eX + 5, eY + 2, 3, 3);
                    }
                    using (Font fBtn = new Font("Segoe UI", 8.5f, FontStyle.Bold))
                    using (SolidBrush bBtn = new SolidBrush(Color.FromArgb(180, 210, 255)))
                    {
                        Rectangle labelR = new Rectangle(btnX + 30, btnY, btnW - 32, btnH);
                        using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                        {
                            g.DrawString("Xem", fBtn, bBtn, labelR, sf);
                        }
                    }
                }
            }
        }
    }

    public class SettingsBackgroundPanel : Panel
    {
        private Image? _bannerImage;
        private Image? _imgXIdle;
        private Image? _imgXCovered;
        private Image? _imgOIdle;
        private Image? _imgOCovered;
        private Image? _imgOPeek;
        private Image? _imgLogo;
        private MascotReactionState _mascotState = MascotReactionState.Idle;
        private Control? _cardBox;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MascotReactionState MascotState
        {
            get => _mascotState;
            set
            {
                if (_mascotState != value)
                {
                    _mascotState = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control? CardBox
        {
            get => _cardBox;
            set
            {
                _cardBox = value;
                Invalidate();
            }
        }

        public SettingsBackgroundPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.FromArgb(8, 13, 26);
            AutoScroll = false;

            LoadAssets();
        }

        private Image? SafeLoadImage(string fileName)
        {
            try
            {
                string assetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", fileName);
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", fileName);
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets", fileName);
                if (File.Exists(assetPath))
                {
                    byte[] bytes = File.ReadAllBytes(assetPath);
                    using var ms = new MemoryStream(bytes);
                    using var temp = Image.FromStream(ms);
                    return new Bitmap(temp);
                }
            }
            catch { }
            return null;
        }

        private void LoadAssets()
        {
            _bannerImage = SafeLoadImage("caro_clean_bg.jpg") ?? SafeLoadImage("caro_hero_banner.jpg");
            _imgXIdle = SafeLoadImage("monster_x_idle.png");
            _imgXCovered = SafeLoadImage("monster_x_covered.png");
            _imgOIdle = SafeLoadImage("monster_o_idle.png");
            _imgOCovered = SafeLoadImage("monster_o_covered.png");
            _imgOPeek = SafeLoadImage("monster_o_peek.png");
            _imgLogo = SafeLoadImage("carokan_3d_logo.png");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. Draw pure galaxy background covering the entire view
            if (_bannerImage != null)
            {
                g.DrawImage(_bannerImage, new Rectangle(0, 0, Width, Height));
            }
            else
            {
                using (SolidBrush b = new SolidBrush(Color.FromArgb(8, 13, 26)))
                {
                    g.FillRectangle(b, ClientRectangle);
                }
            }

            // 2. Draw Mascots, 3D Logo & Reaction Badge directly on space background
            Control? card = _cardBox;
            if (card == null)
            {
                foreach (Control c in Controls)
                {
                    if (c is CyberLoginCard) { card = c; break; }
                }
            }

            if (card != null && card.Visible)
            {
                int cardX = card.Left;
                int cardY = card.Top;
                int cardW = card.Width;

                // 2.1 Logo 3D CaroKan in the center
                if (_imgLogo != null)
                {
                    int logoW = 196;
                    int logoH = (int)(logoW * ((float)_imgLogo.Height / _imgLogo.Width));
                    int logoX = cardX + (cardW - logoW) / 2;
                    int logoY = cardY - logoH + 4;
                    g.DrawImage(_imgLogo, new Rectangle(logoX, logoY, logoW, logoH));
                }

                // 2.2 Monster X (Left) climbing on top edge
                Image? imgX = (_mascotState == MascotReactionState.PasswordMasked) ? (_imgXCovered ?? _imgXIdle) : _imgXIdle;
                if (imgX != null)
                {
                    int xW = 104;
                    int xH = (int)(xW * ((float)imgX.Height / imgX.Width));
                    int xX = cardX + 16;
                    int xY = cardY - xH + 10;
                    g.DrawImage(imgX, new Rectangle(xX, xY, xW, xH));
                }

                // 2.3 Monster O (Right) sitting on top edge
                Image? imgO = (_mascotState == MascotReactionState.PasswordPeek)
                    ? (_imgOPeek ?? _imgOIdle)
                    : ((_mascotState == MascotReactionState.PasswordMasked) ? (_imgOCovered ?? _imgOIdle) : _imgOIdle);
                if (imgO != null)
                {
                    int oW = 114;
                    int oH = (int)(oW * ((float)imgO.Height / imgO.Width));
                    int oX = cardX + cardW - oW - 16;
                    int oY = cardY - oH + 10;
                    g.DrawImage(imgO, new Rectangle(oX, oY, oW, oH));
                }

                // 2.4 Reaction Badge
                if (_mascotState == MascotReactionState.PasswordMasked)
                {
                    int badgeW = 175;
                    int badgeH = 24;
                    int badgeX = cardX + (cardW - badgeW) / 2;
                    int badgeY = cardY - 76;
                    Rectangle badgeRect = new Rectangle(badgeX, badgeY, badgeW, badgeH);

                    using (GraphicsPath bPath = UITheme.CreateRoundedRectanglePath(badgeRect, 12))
                    {
                        using (LinearGradientBrush lgb = new LinearGradientBrush(badgeRect, Color.FromArgb(240, 255, 64, 128), Color.FromArgb(240, 180, 20, 220), LinearGradientMode.Horizontal))
                        {
                            g.FillPath(lgb, bPath);
                        }
                        using (Pen bp = new Pen(Color.FromArgb(255, 255, 180, 210), 1.2f))
                        {
                            g.DrawPath(bp, bPath);
                        }
                    }

                    using (Font fb = new Font("Segoe UI", 8.25f, FontStyle.Bold))
                    using (SolidBrush tb = new SolidBrush(Color.White))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString("Shh... Không nhìn đâu!", fb, tb, badgeRect, sf);
                    }
                }
                else if (_mascotState == MascotReactionState.PasswordPeek)
                {
                    int badgeW = 175;
                    int badgeH = 24;
                    int badgeX = cardX + (cardW - badgeW) / 2;
                    int badgeY = cardY - 76;
                    Rectangle badgeRect = new Rectangle(badgeX, badgeY, badgeW, badgeH);

                    using (GraphicsPath bPath = UITheme.CreateRoundedRectanglePath(badgeRect, 12))
                    {
                        using (LinearGradientBrush lgb = new LinearGradientBrush(badgeRect, Color.FromArgb(240, 0, 194, 255), Color.FromArgb(240, 0, 230, 118), LinearGradientMode.Horizontal))
                        {
                            g.FillPath(lgb, bPath);
                        }
                        using (Pen bp = new Pen(Color.FromArgb(255, 180, 255, 230), 1.2f))
                        {
                            g.DrawPath(bp, bPath);
                        }
                    }

                    using (Font fb = new Font("Segoe UI", 8.25f, FontStyle.Bold))
                    using (SolidBrush tb = new SolidBrush(Color.White))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString("Đang nhìn trộm nè!", fb, tb, badgeRect, sf);
                    }
                }
            }
        }
    }

    public enum MascotReactionState
    {
        Idle,
        PasswordMasked,
        PasswordPeek
    }

    public class CyberLoginCard : Panel
    {
        private MascotReactionState _mascotState = MascotReactionState.Idle;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MascotReactionState MascotState
        {
            get => _mascotState;
            set
            {
                if (_mascotState != value)
                {
                    _mascotState = value;
                    if (Parent is SettingsBackgroundPanel sbp)
                    {
                        sbp.MascotState = value;
                    }
                    Invalidate();
                }
            }
        }

        public CyberLoginCard()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.FromArgb(11, 20, 36);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (Width > 10 && Height > 10)
            {
                using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(new Rectangle(0, 0, Width, Height), 16))
                {
                    Region = new Region(path);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = 16;

            using (GraphicsPath cardPath = UITheme.CreateRoundedRectanglePath(rect, radius))
            {
                // Nen xanh navy dam chuan cyberpunk
                using (SolidBrush bBg = new SolidBrush(Color.FromArgb(246, 11, 20, 36)))
                {
                    g.FillPath(bBg, cardPath);
                }

                // Vien phat sang Cyan (#00C2FF)
                using (Pen pBorder = new Pen(Color.FromArgb(220, 0, 194, 255), 2.0f))
                {
                    g.DrawPath(pBorder, cardPath);
                }

                // Diem nhan duong vien cam vang ben phai tao hieu ung 3D nhu anh mau
                using (Pen pOrange = new Pen(Color.FromArgb(160, 240, 120, 40), 2.0f))
                {
                    g.DrawLine(pOrange, Width - 2, radius, Width - 2, Height - radius);
                }
            }

            // Goi vien kim loai phan tach tren cung
            using (Pen pRim = new Pen(Color.FromArgb(255, 60, 130, 200), 2.2f))
            {
                g.DrawLine(pRim, 20, 1, Width - 20, 1);
            }

            base.OnPaint(e);
        }
    }
}
