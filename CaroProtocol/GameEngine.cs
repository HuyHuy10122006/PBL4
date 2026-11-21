namespace CaroProtocol
{
    public class GameEngine
    {
        public int BoardSize { get; private set; }
        public char[,] Board { get; private set; }
        public char CurrentTurnSymbol { get; private set; }
        public GameStateStatus Status { get; private set; }
        public char WinnerSymbol { get; private set; } = ' ';
        public List<Point> WinLine { get; private set; } = new List<Point>();
        public bool BlockBothEndsRule { get; set; } = true;
        public Point? LastMove { get; private set; }
        public int TotalMovesCount { get; private set; } = 0;

        public GameEngine(int boardSize = 15, bool blockBothEndsRule = true)
        {
            BoardSize = boardSize;
            Board = new char[boardSize, boardSize];
            BlockBothEndsRule = blockBothEndsRule;
            Reset();
        }

        public void Reset()
        {
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    Board[r, c] = ' ';
                }
            }
            CurrentTurnSymbol = 'X'; // X plays first
            Status = GameStateStatus.Playing;
            WinnerSymbol = ' ';
            WinLine.Clear();
            LastMove = null;
            TotalMovesCount = 0;
        }

        public void Surrender(char winnerSymbol)
        {
            Status = GameStateStatus.GameOver;
            WinnerSymbol = winnerSymbol;
        }

        public GameEngine Clone()
        {
            var copy = new GameEngine(BoardSize, BlockBothEndsRule)
            {
                CurrentTurnSymbol = this.CurrentTurnSymbol,
                Status = this.Status,
                WinnerSymbol = this.WinnerSymbol,
                WinLine = new List<Point>(this.WinLine),
                LastMove = this.LastMove,
                TotalMovesCount = this.TotalMovesCount
            };
            Array.Copy(this.Board, copy.Board, this.Board.Length);
            return copy;
        }

        public bool MakeMove(int row, int col, char playerSymbol, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (Status != GameStateStatus.Playing)
            {
                errorMessage = "Trận đấu đã kết thúc.";
                return false;
            }

            if (playerSymbol != CurrentTurnSymbol)
            {
                errorMessage = "Chưa đến lượt của bạn.";
                return false;
            }

            if (row < 0 || row >= BoardSize || col < 0 || col >= BoardSize)
            {
                errorMessage = "Nước đi nằm ngoài phạm vi bàn cờ.";
                return false;
            }

            if (Board[row, col] != ' ')
            {
                errorMessage = "Ô này đã được đánh.";
                return false;
            }

            // Place symbol
            Board[row, col] = playerSymbol;
            LastMove = new Point(row, col);
            TotalMovesCount++;

            // Check Win Condition
            if (CheckWinAt(row, col, playerSymbol, out List<Point> winningPoints))
            {
                Status = GameStateStatus.GameOver;
                WinnerSymbol = playerSymbol;
                WinLine = winningPoints;
                return true;
            }

            // Check Draw (Board Full)
            if (IsBoardFull())
            {
                Status = GameStateStatus.GameOver;
                WinnerSymbol = 'D'; // Draw
                return true;
            }

            // Switch Turn
            CurrentTurnSymbol = (CurrentTurnSymbol == 'X') ? 'O' : 'X';
            return true;
        }

        public bool IsBoardFull()
        {
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if (Board[r, c] == ' ') return false;
                }
            }
            return true;
        }

        public bool CheckWinAt(int r, int c, char symbol, out List<Point> winLine)
        {
            winLine = new List<Point>();
            int[,] directions = new int[,]
            {
                { 0, 1 },  // Horizontal
                { 1, 0 },  // Vertical
                { 1, 1 },  // Main Diagonal
                { 1, -1 }  // Anti Diagonal
            };

            for (int d = 0; d < 4; d++)
            {
                int dr = directions[d, 0];
                int dc = directions[d, 1];

                List<Point> line = new List<Point> { new Point(r, c) };

                // Count forward
                int step = 1;
                while (true)
                {
                    int nr = r + dr * step;
                    int nc = c + dc * step;
                    if (nr >= 0 && nr < BoardSize && nc >= 0 && nc < BoardSize && Board[nr, nc] == symbol)
                    {
                        line.Add(new Point(nr, nc));
                        step++;
                    }
                    else break;
                }
                int end1R = r + dr * step;
                int end1C = c + dc * step;

                // Count backward
                step = 1;
                while (true)
                {
                    int nr = r - dr * step;
                    int nc = c - dc * step;
                    if (nr >= 0 && nr < BoardSize && nc >= 0 && nc < BoardSize && Board[nr, nc] == symbol)
                    {
                        line.Add(new Point(nr, nc));
                        step++;
                    }
                    else break;
                }
                int end2R = r - dr * step;
                int end2C = c - dc * step;

                if (line.Count >= 5)
                {
                    // Check blocked 2 ends rule if enabled
                    if (BlockBothEndsRule && line.Count == 5)
                    {
                        bool end1Blocked = IsBlocked(end1R, end1C, symbol);
                        bool end2Blocked = IsBlocked(end2R, end2C, symbol);

                        if (end1Blocked && end2Blocked)
                        {
                            // Blocked at both ends -> not a win!
                            continue;
                        }
                    }

                    winLine = line;
                    return true;
                }
            }

            return false;
        }

        private bool IsBlocked(int r, int c, char playerSymbol)
        {
            if (r < 0 || r >= BoardSize || c < 0 || c >= BoardSize) return true; // Edge of board counts as blocked
            char other = (playerSymbol == 'X') ? 'O' : 'X';
            return Board[r, c] == other;
        }

        public char[][] GetBoardMatrix()
        {
            char[][] matrix = new char[BoardSize][];
            for (int r = 0; r < BoardSize; r++)
            {
                matrix[r] = new char[BoardSize];
                for (int c = 0; c < BoardSize; c++)
                {
                    matrix[r][c] = Board[r, c];
                }
            }
            return matrix;
        }
    }
}
