using CaroProtocol;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CaroClient
{
    public class BoardControl : Control
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BoardSize { get; private set; } = 15;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public char[][] Board { get; private set; } = Array.Empty<char[]>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LastMoveRow { get; set; } = -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LastMoveCol { get; set; } = -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CaroProtocol.Point> WinLine { get; set; } = new List<CaroProtocol.Point>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public char MySymbol { get; set; } = 'X';

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMyTurn { get; set; } = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GameStateStatus Status { get; set; } = GameStateStatus.WaitingForOpponent;

        public event Action<int, int>? OnCellClicked;

        private int _hoverRow = -1;
        private int _hoverCol = -1;
        private Image? _bgImage;

        public BoardControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);

            LoadBackground();
            InitEmptyBoard(15);
        }

        private Image? SafeLoadImage(string fileName)
        {
            try
            {
                string assetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", fileName);
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", fileName);
                if (!File.Exists(assetPath)) assetPath = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets", fileName);
                if (!File.Exists(assetPath)) assetPath = @"d:\PBL4\CaroClient\Assets\" + fileName;
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

        private void LoadBackground()
        {
            _bgImage = SafeLoadImage("game_board_bg.jpg") ?? SafeLoadImage("caro_clean_bg.jpg");
        }

        public void UpdateBoardState(GameStateUpdateData data, char mySymbol)
        {
            BoardSize = data.BoardSize;
            Board = data.Board;
            LastMoveRow = data.LastMoveRow;
            LastMoveCol = data.LastMoveCol;
            WinLine = data.WinLine ?? new List<CaroProtocol.Point>();
            MySymbol = mySymbol;
            IsMyTurn = (data.CurrentTurnSymbol == mySymbol) && (data.Status == GameStateStatus.Playing);
            Status = data.Status;

            Invalidate();
        }

        public void InitEmptyBoard(int size)
        {
            BoardSize = size;
            Board = new char[size][];
            for (int r = 0; r < size; r++)
            {
                Board[r] = new char[size];
                for (int c = 0; c < size; c++)
                {
                    Board[r][c] = ' ';
                }
            }
            LastMoveRow = -1;
            LastMoveCol = -1;
            WinLine.Clear();
            Invalidate();
        }

        public float GetCellSize()
        {
            float minDim = Math.Min(Width - 20, Height - 20);
            float framePadding = 26f;
            float maxGrid = Math.Max(100f, minDim - framePadding * 2);
            return (float)Math.Floor(maxGrid / BoardSize);
        }

        public float GetFrameSize()
        {
            float cellSize = GetCellSize();
            float boardPixelSize = cellSize * BoardSize;
            float framePadding = 26f;
            return boardPixelSize + framePadding * 2;
        }

        public float GetGridStartX()
        {
            float finalFrameSize = GetFrameSize();
            float framePadding = 26f;
            return (Width - finalFrameSize) / 2f + framePadding;
        }

        public float GetGridStartY()
        {
            float finalFrameSize = GetFrameSize();
            float framePadding = 26f;
            return (Height - finalFrameSize) / 2f + framePadding;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            float cellSize = GetCellSize();
            float startX = GetGridStartX();
            float startY = GetGridStartY();

            int col = (int)((e.X - startX) / cellSize);
            int row = (int)((e.Y - startY) / cellSize);

            if (row >= 0 && row < BoardSize && col >= 0 && col < BoardSize)
            {
                if (_hoverRow != row || _hoverCol != col)
                {
                    _hoverRow = row;
                    _hoverCol = col;
                    Invalidate();
                }
            }
            else
            {
                if (_hoverRow != -1 || _hoverCol != -1)
                {
                    _hoverRow = -1;
                    _hoverCol = -1;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hoverRow = -1;
            _hoverCol = -1;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left) return;
            if (Status != GameStateStatus.Playing || !IsMyTurn) return;

            float cellSize = GetCellSize();
            float startX = GetGridStartX();
            float startY = GetGridStartY();

            int col = (int)((e.X - startX) / cellSize);
            int row = (int)((e.Y - startY) / cellSize);

            if (row >= 0 && row < BoardSize && col >= 0 && col < BoardSize)
            {
                if (Board[row][col] == ' ')
                {
                    OnCellClicked?.Invoke(row, col);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 1. Draw Space Nebula Background (Aligned seamlessly with Parent arena panel)
            if (Parent != null && _bgImage != null)
            {
                float imgAspect = (float)_bgImage.Width / _bgImage.Height;
                float parentAspect = (float)Parent.Width / Math.Max(1, Parent.Height);

                Rectangle parentDestRect;
                if (parentAspect > imgAspect)
                {
                    int h = (int)(Parent.Width / imgAspect);
                    parentDestRect = new Rectangle(0, (Parent.Height - h) / 2, Parent.Width, h);
                }
                else
                {
                    int w = (int)(Parent.Height * imgAspect);
                    parentDestRect = new Rectangle((Parent.Width - w) / 2, 0, w, Parent.Height);
                }

                Rectangle localDestRect = new Rectangle(parentDestRect.X - Left, parentDestRect.Y - Top, parentDestRect.Width, parentDestRect.Height);
                g.DrawImage(_bgImage, localDestRect);

                // Subtle dark vignette overlay for optimal contrast
                using (SolidBrush darkOverlay = new SolidBrush(Color.FromArgb(35, 6, 10, 22)))
                {
                    g.FillRectangle(darkOverlay, ClientRectangle);
                }
            }
            else
            {
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(10, 16, 34)))
                {
                    g.FillRectangle(bgBrush, ClientRectangle);
                }
            }

            float cellSize = GetCellSize();
            float boardPixelSize = cellSize * BoardSize;
            float framePadding = 26f;
            float finalFrameSize = boardPixelSize + framePadding * 2;
            float frameX = (Width - finalFrameSize) / 2f;
            float frameY = (Height - finalFrameSize) / 2f;
            float gridStartX = frameX + framePadding;
            float gridStartY = frameY + framePadding;

            Rectangle frameRect = new Rectangle((int)frameX, (int)frameY, (int)finalFrameSize, (int)finalFrameSize);

            // 2. Soft Outer Drop Shadow around frame
            using (GraphicsPath outerShadowPath = UITheme.CreateRoundedRectanglePath(new Rectangle(frameRect.X - 3, frameRect.Y - 2, frameRect.Width + 6, frameRect.Height + 6), 18))
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(90, 0, 0, 0), 6f))
                {
                    g.DrawPath(shadowPen, outerShadowPath);
                }
            }

            // 3. Outer Cyberpunk Metallic Frame Body
            using (GraphicsPath framePath = UITheme.CreateRoundedRectanglePath(frameRect, 16))
            {
                using (SolidBrush frameBrush = new SolidBrush(Color.FromArgb(18, 28, 48)))
                {
                    g.FillPath(frameBrush, framePath);
                }
                using (Pen frameBorder = new Pen(Color.FromArgb(36, 52, 86), 2.5f))
                {
                    g.DrawPath(frameBorder, framePath);
                }
            }

            // 4. Inner Dark Tech Grid Container
            Rectangle innerGridRect = new Rectangle((int)gridStartX - 4, (int)gridStartY - 4, (int)boardPixelSize + 8, (int)boardPixelSize + 8);
            using (GraphicsPath innerPath = UITheme.CreateRoundedRectanglePath(innerGridRect, 10))
            {
                using (SolidBrush innerBg = new SolidBrush(Color.FromArgb(242, 10, 18, 38)))
                {
                    g.FillPath(innerBg, innerPath);
                }
                using (Pen innerBorder = new Pen(Color.FromArgb(42, 72, 120), 1.2f))
                {
                    g.DrawPath(innerBorder, innerPath);
                }
            }

            // 5. Dual Glowing Neon Tubes on all 4 Edges
            float cornerSize = 36f;
            float railStart = frameX + cornerSize - 2;
            float railEnd = frameX + finalFrameSize - cornerSize + 2;
            float railVStart = frameY + cornerSize - 2;
            float railVEnd = frameY + finalFrameSize - cornerSize + 2;

            Color neonCyan = Color.FromArgb(0, 229, 255);
            Color neonPink = Color.FromArgb(255, 46, 126);

            // Top rails (Magenta upper, Cyan lower)
            DrawNeonTube(g, new PointF(railStart, frameY + 7), new PointF(railEnd, frameY + 7), neonPink);
            DrawNeonTube(g, new PointF(railStart, frameY + 13), new PointF(railEnd, frameY + 13), neonCyan);

            // Bottom rails (Cyan upper, Magenta lower)
            DrawNeonTube(g, new PointF(railStart, frameY + finalFrameSize - 13), new PointF(railEnd, frameY + finalFrameSize - 13), neonCyan);
            DrawNeonTube(g, new PointF(railStart, frameY + finalFrameSize - 7), new PointF(railEnd, frameY + finalFrameSize - 7), neonPink);

            // Left rails (Magenta outer, Cyan inner)
            DrawNeonTube(g, new PointF(frameX + 7, railVStart), new PointF(frameX + 7, railVEnd), neonPink);
            DrawNeonTube(g, new PointF(frameX + 13, railVStart), new PointF(frameX + 13, railVEnd), neonCyan);

            // Right rails (Cyan inner, Magenta outer)
            DrawNeonTube(g, new PointF(frameX + finalFrameSize - 13, railVStart), new PointF(frameX + finalFrameSize - 13, railVEnd), neonCyan);
            DrawNeonTube(g, new PointF(frameX + finalFrameSize - 7, railVStart), new PointF(frameX + finalFrameSize - 7, railVEnd), neonPink);

            // 6. Four Sci-Fi Metallic Corner Brackets with Bolts and Cross Rivets
            PointF[] cornerLocs = new PointF[]
            {
                new PointF(frameX, frameY),
                new PointF(frameX + finalFrameSize - cornerSize, frameY),
                new PointF(frameX, frameY + finalFrameSize - cornerSize),
                new PointF(frameX + finalFrameSize - cornerSize, frameY + finalFrameSize - cornerSize)
            };

            for (int ci = 0; ci < 4; ci++)
            {
                PointF cl = cornerLocs[ci];
                Rectangle cornerRect = new Rectangle((int)cl.X, (int)cl.Y, (int)cornerSize, (int)cornerSize);
                using (GraphicsPath cPath = UITheme.CreateRoundedRectanglePath(cornerRect, 10))
                {
                    using (LinearGradientBrush cb = new LinearGradientBrush(cornerRect, Color.FromArgb(48, 68, 108), Color.FromArgb(24, 34, 58), 45f))
                    {
                        g.FillPath(cb, cPath);
                    }
                    using (Pen cp = new Pen(Color.FromArgb(90, 128, 185), 1.5f))
                    {
                        g.DrawPath(cp, cPath);
                    }
                }

                // Bolt circle with cross '+'
                float bSize = 10f;
                float bX = cl.X + (cornerSize - bSize) / 2f;
                float bY = cl.Y + (cornerSize - bSize) / 2f;
                using (Pen boltPen = new Pen(Color.FromArgb(100, 145, 205), 1.2f))
                using (SolidBrush boltBg = new SolidBrush(Color.FromArgb(16, 24, 42)))
                {
                    g.FillEllipse(boltBg, bX, bY, bSize, bSize);
                    g.DrawEllipse(boltPen, bX, bY, bSize, bSize);
                }
                using (Pen crossPen = new Pen(Color.FromArgb(170, 215, 255), 1.4f))
                {
                    crossPen.StartCap = LineCap.Round;
                    crossPen.EndCap = LineCap.Round;
                    g.DrawLine(crossPen, bX + 2.5f, bY + bSize / 2f, bX + bSize - 2.5f, bY + bSize / 2f);
                    g.DrawLine(crossPen, bX + bSize / 2f, bY + 2.5f, bX + bSize / 2f, bY + bSize - 2.5f);
                }

                // Corner rivet dot
                float rDotX = (ci == 0 || ci == 2) ? cl.X + 6 : cl.X + cornerSize - 8;
                float rDotY = (ci == 0 || ci == 1) ? cl.Y + 6 : cl.Y + cornerSize - 8;
                using (SolidBrush rDot = new SolidBrush(Color.FromArgb(130, 175, 230)))
                {
                    g.FillEllipse(rDot, rDotX - 1.5f, rDotY - 1.5f, 3f, 3f);
                }
            }

            // 7. Draw Board Grid Lines
            using (Pen gridPen = new Pen(Color.FromArgb(38, 64, 110), 1.2f))
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    float pos = gridStartY + i * cellSize + cellSize / 2f;

                    // Horizontal Line
                    g.DrawLine(gridPen, gridStartX + cellSize / 2f, pos, gridStartX + (BoardSize - 0.5f) * cellSize, pos);

                    // Vertical Line
                    pos = gridStartX + i * cellSize + cellSize / 2f;
                    g.DrawLine(gridPen, pos, gridStartY + cellSize / 2f, pos, gridStartY + (BoardSize - 0.5f) * cellSize);
                }
            }

            // 8. Draw Star Points (Tâm bàn cờ và các điểm góc với hiệu ứng phát sáng Neon)
            int center = BoardSize / 2;
            DrawStarPoint(g, center, center, cellSize, gridStartX, gridStartY);
            if (BoardSize >= 15)
            {
                DrawStarPoint(g, 3, 3, cellSize, gridStartX, gridStartY);
                DrawStarPoint(g, 3, BoardSize - 4, cellSize, gridStartX, gridStartY);
                DrawStarPoint(g, BoardSize - 4, 3, cellSize, gridStartX, gridStartY);
                DrawStarPoint(g, BoardSize - 4, BoardSize - 4, cellSize, gridStartX, gridStartY);
            }

            // 9. Draw Hover Effect (Neon Cyan Glow)
            if (_hoverRow >= 0 && _hoverCol >= 0 && Status == GameStateStatus.Playing && IsMyTurn)
            {
                if (Board[_hoverRow][_hoverCol] == ' ')
                {
                    float hX = gridStartX + _hoverCol * cellSize;
                    float hY = gridStartY + _hoverRow * cellSize;
                    using (SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(40, 0, 229, 255)))
                    using (Pen hoverPen = new Pen(Color.FromArgb(160, 0, 229, 255), 1.5f))
                    {
                        g.FillRectangle(hoverBrush, hX + 2, hY + 2, cellSize - 4, cellSize - 4);
                        g.DrawRectangle(hoverPen, hX + 2, hY + 2, cellSize - 4, cellSize - 4);
                    }
                }
            }

            // 10. Draw Last Move Highlight (Glowing Amber Box)
            if (LastMoveRow >= 0 && LastMoveCol >= 0)
            {
                float lmX = gridStartX + LastMoveCol * cellSize;
                float lmY = gridStartY + LastMoveRow * cellSize;
                using (SolidBrush lastMoveBg = new SolidBrush(Color.FromArgb(60, 255, 215, 0)))
                using (Pen lastMoveBorder = new Pen(Color.FromArgb(200, 255, 215, 0), 1.5f))
                {
                    g.FillRectangle(lastMoveBg, lmX + 3, lmY + 3, cellSize - 6, cellSize - 6);
                    g.DrawRectangle(lastMoveBorder, lmX + 3, lmY + 3, cellSize - 6, cellSize - 6);
                }
            }

            // 11. Draw Pieces (X and O)
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    char piece = Board[r][c];
                    if (piece == 'X')
                    {
                        DrawXMark(g, r, c, cellSize, gridStartX, gridStartY);
                    }
                    else if (piece == 'O')
                    {
                        DrawOMark(g, r, c, cellSize, gridStartX, gridStartY);
                    }
                }
            }

            // 12. Draw Win Line (Laser Beam Effect)
            if (WinLine != null && WinLine.Count >= 5)
            {
                var start = WinLine[0];
                var end = WinLine[WinLine.Count - 1];

                float sX = gridStartX + start.Col * cellSize + cellSize / 2f;
                float sY = gridStartY + start.Row * cellSize + cellSize / 2f;
                float eX = gridStartX + end.Col * cellSize + cellSize / 2f;
                float eY = gridStartY + end.Row * cellSize + cellSize / 2f;

                // Outer Laser Glow
                using (Pen glowPen = new Pen(Color.FromArgb(120, 255, 215, 0), 10f))
                {
                    glowPen.StartCap = LineCap.Round;
                    glowPen.EndCap = LineCap.Round;
                    g.DrawLine(glowPen, sX, sY, eX, eY);
                }

                // Inner Bright Beam
                using (Pen winPen = new Pen(Color.FromArgb(255, 255, 255), 4f))
                {
                    winPen.StartCap = LineCap.Round;
                    winPen.EndCap = LineCap.Round;
                    g.DrawLine(winPen, sX, sY, eX, eY);
                }
            }
        }

        private void DrawNeonTube(Graphics g, PointF p1, PointF p2, Color neonColor)
        {
            // Outer wide glow
            using (Pen glowPen = new Pen(Color.FromArgb(45, neonColor), 6f))
            {
                glowPen.StartCap = LineCap.Round;
                glowPen.EndCap = LineCap.Round;
                g.DrawLine(glowPen, p1, p2);
            }
            // Mid glow
            using (Pen midPen = new Pen(Color.FromArgb(140, neonColor), 2.8f))
            {
                midPen.StartCap = LineCap.Round;
                midPen.EndCap = LineCap.Round;
                g.DrawLine(midPen, p1, p2);
            }
            // Core white/neon line
            using (Pen corePen = new Pen(Color.FromArgb(235, 255, 255, 255), 1.2f))
            {
                corePen.StartCap = LineCap.Round;
                corePen.EndCap = LineCap.Round;
                g.DrawLine(corePen, p1, p2);
            }
        }

        private void DrawStarPoint(Graphics g, int row, int col, float cellSize, float startX, float startY)
        {
            float x = startX + col * cellSize + cellSize / 2f;
            float y = startY + row * cellSize + cellSize / 2f;
            float r = 3.5f;

            // Halo glow
            using (SolidBrush glow = new SolidBrush(Color.FromArgb(50, 0, 229, 255)))
            {
                g.FillEllipse(glow, x - r * 2, y - r * 2, r * 4, r * 4);
            }
            // Center point
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 229, 255)))
            {
                g.FillEllipse(brush, x - r, y - r, r * 2, r * 2);
            }
        }

        private void DrawXMark(Graphics g, int row, int col, float cellSize, float startX, float startY)
        {
            float margin = cellSize * 0.22f;
            float x1 = startX + col * cellSize + margin;
            float y1 = startY + row * cellSize + margin;
            float x2 = startX + (col + 1) * cellSize - margin;
            float y2 = startY + (row + 1) * cellSize - margin;

            // Outer Crimson Glow
            using (Pen glowPen = new Pen(Color.FromArgb(70, 255, 46, 99), 7f))
            {
                glowPen.StartCap = LineCap.Round;
                glowPen.EndCap = LineCap.Round;
                g.DrawLine(glowPen, x1, y1, x2, y2);
                g.DrawLine(glowPen, x1, y2, x2, y1);
            }

            // Core Neon Red Stroke
            using (Pen xPen = new Pen(Color.FromArgb(255, 46, 99), 3.5f))
            {
                xPen.StartCap = LineCap.Round;
                xPen.EndCap = LineCap.Round;
                g.DrawLine(xPen, x1, y1, x2, y2);
                g.DrawLine(xPen, x1, y2, x2, y1);
            }
        }

        private void DrawOMark(Graphics g, int row, int col, float cellSize, float startX, float startY)
        {
            float margin = cellSize * 0.22f;
            float x = startX + col * cellSize + margin;
            float y = startY + row * cellSize + margin;
            float diameter = cellSize - margin * 2;

            // Outer Cyan Glow
            using (Pen glowPen = new Pen(Color.FromArgb(70, 0, 229, 255), 7f))
            {
                g.DrawEllipse(glowPen, x, y, diameter, diameter);
            }

            // Core Electric Cyan Stroke
            using (Pen oPen = new Pen(Color.FromArgb(0, 229, 255), 3.5f))
            {
                g.DrawEllipse(oPen, x, y, diameter, diameter);
            }
        }
    }
}
