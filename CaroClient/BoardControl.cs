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

        public BoardControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);

            InitEmptyBoard(15);
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            float cellSize = GetCellSize();
            float padding = GetPadding();

            int col = (int)((e.X - padding) / cellSize);
            int row = (int)((e.Y - padding) / cellSize);

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
            float padding = GetPadding();

            int col = (int)((e.X - padding) / cellSize);
            int row = (int)((e.Y - padding) / cellSize);

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

            float cellSize = GetCellSize();
            float padding = GetPadding();

            // Background Fill (Warm elegant wood/slate background)
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(245, 240, 230)))
            {
                g.FillRectangle(bgBrush, ClientRectangle);
            }

            // Draw Board Grid
            using (Pen gridPen = new Pen(Color.FromArgb(160, 140, 110), 1.5f))
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    float pos = padding + i * cellSize + cellSize / 2f;

                    // Horizontal Line
                    g.DrawLine(gridPen, padding + cellSize / 2f, pos, padding + (BoardSize - 0.5f) * cellSize, pos);

                    // Vertical Line
                    g.DrawLine(gridPen, pos, padding + cellSize / 2f, pos, padding + (BoardSize - 0.5f) * cellSize);
                }
            }

            // Draw Star Points (Tâm bàn cờ và các điểm góc)
            int center = BoardSize / 2;
            DrawStarPoint(g, center, center, cellSize, padding);
            if (BoardSize >= 15)
            {
                DrawStarPoint(g, 3, 3, cellSize, padding);
                DrawStarPoint(g, 3, BoardSize - 4, cellSize, padding);
                DrawStarPoint(g, BoardSize - 4, 3, cellSize, padding);
                DrawStarPoint(g, BoardSize - 4, BoardSize - 4, cellSize, padding);
            }

            // Draw Hover Effect
            if (_hoverRow >= 0 && _hoverCol >= 0 && Status == GameStateStatus.Playing && IsMyTurn)
            {
                if (Board[_hoverRow][_hoverCol] == ' ')
                {
                    float hX = padding + _hoverCol * cellSize;
                    float hY = padding + _hoverRow * cellSize;
                    using (SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(80, 76, 175, 80)))
                    {
                        g.FillRectangle(hoverBrush, hX + 2, hY + 2, cellSize - 4, cellSize - 4);
                    }
                }
            }

            // Draw Last Move Highlight
            if (LastMoveRow >= 0 && LastMoveCol >= 0)
            {
                float lmX = padding + LastMoveCol * cellSize;
                float lmY = padding + LastMoveRow * cellSize;
                using (SolidBrush lastMoveBg = new SolidBrush(Color.FromArgb(90, 255, 235, 59)))
                {
                    g.FillRectangle(lastMoveBg, lmX + 2, lmY + 2, cellSize - 4, cellSize - 4);
                }
            }

            // Draw Pieces (X and O)
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    char piece = Board[r][c];
                    if (piece == 'X')
                    {
                        DrawXMark(g, r, c, cellSize, padding);
                    }
                    else if (piece == 'O')
                    {
                        DrawOMark(g, r, c, cellSize, padding);
                    }
                }
            }

            // Draw Win Line
            if (WinLine != null && WinLine.Count >= 5)
            {
                var start = WinLine[0];
                var end = WinLine[WinLine.Count - 1];

                float sX = padding + start.Col * cellSize + cellSize / 2f;
                float sY = padding + start.Row * cellSize + cellSize / 2f;
                float eX = padding + end.Col * cellSize + cellSize / 2f;
                float eY = padding + end.Row * cellSize + cellSize / 2f;

                using (Pen winPen = new Pen(Color.FromArgb(220, 255, 193, 7), 6f))
                {
                    winPen.StartCap = LineCap.Round;
                    winPen.EndCap = LineCap.Round;
                    g.DrawLine(winPen, sX, sY, eX, eY);
                }
            }
        }

        private void DrawStarPoint(Graphics g, int row, int col, float cellSize, float padding)
        {
            float x = padding + col * cellSize + cellSize / 2f;
            float y = padding + row * cellSize + cellSize / 2f;
            float r = 4f;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(120, 100, 70)))
            {
                g.FillEllipse(brush, x - r, y - r, r * 2, r * 2);
            }
        }

        private void DrawXMark(Graphics g, int row, int col, float cellSize, float padding)
        {
            float margin = cellSize * 0.22f;
            float x1 = padding + col * cellSize + margin;
            float y1 = padding + row * cellSize + margin;
            float x2 = padding + (col + 1) * cellSize - margin;
            float y2 = padding + (row + 1) * cellSize - margin;

            using (Pen xPen = new Pen(Color.FromArgb(229, 57, 53), 3.5f)) // Crimson Red
            {
                xPen.StartCap = LineCap.Round;
                xPen.EndCap = LineCap.Round;
                g.DrawLine(xPen, x1, y1, x2, y2);
                g.DrawLine(xPen, x1, y2, x2, y1);
            }
        }

        private void DrawOMark(Graphics g, int row, int col, float cellSize, float padding)
        {
            float margin = cellSize * 0.22f;
            float x = padding + col * cellSize + margin;
            float y = padding + row * cellSize + margin;
            float diameter = cellSize - margin * 2;

            using (Pen oPen = new Pen(Color.FromArgb(30, 136, 229), 3.5f)) // Royal Blue
            {
                g.DrawEllipse(oPen, x, y, diameter, diameter);
            }
        }

        private float GetCellSize()
        {
            float minDim = Math.Min(Width, Height);
            return (minDim - 20) / BoardSize;
        }

        private float GetPadding()
        {
            float cellSize = GetCellSize();
            float boardPixelSize = cellSize * BoardSize;
            return (Math.Min(Width, Height) - boardPixelSize) / 2f;
        }
    }
}
