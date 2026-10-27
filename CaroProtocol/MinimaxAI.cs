namespace CaroProtocol
{
    public class MinimaxAI
    {
        private const int WIN_SCORE = 10000000;
        private const int FOUR_OPEN = 100000;
        private const int FOUR_HALF = 10000;
        private const int THREE_OPEN = 10000;
        private const int THREE_HALF = 1000;
        private const int TWO_OPEN = 100;
        private const int TWO_HALF = 10;

        /// <summary>
        /// Finds the best move for the AI using multithreaded Minimax with Alpha-Beta pruning.
        /// </summary>
        public static Point FindBestMove(GameEngine game, char aiSymbol, AIDifficulty difficulty)
        {
            int maxDepth = (int)difficulty;
            char humanSymbol = (aiSymbol == 'X') ? 'O' : 'X';

            List<Point> candidates = GetCandidateMoves(game.Board, game.BoardSize);
            if (candidates.Count == 0)
            {
                // Board empty, pick center
                int center = game.BoardSize / 2;
                return new Point(center, center);
            }

            if (candidates.Count == 1)
            {
                return candidates[0];
            }

            // Quick check: If AI can win in 1 move, take it immediately!
            foreach (var cand in candidates)
            {
                game.Board[cand.Row, cand.Col] = aiSymbol;
                bool win = game.CheckWinAt(cand.Row, cand.Col, aiSymbol, out _);
                game.Board[cand.Row, cand.Col] = ' ';
                if (win) return cand;
            }

            // Quick check: If Human can win in next move, block it immediately!
            foreach (var cand in candidates)
            {
                game.Board[cand.Row, cand.Col] = humanSymbol;
                bool win = game.CheckWinAt(cand.Row, cand.Col, humanSymbol, out _);
                game.Board[cand.Row, cand.Col] = ' ';
                if (win) return cand;
            }

            // Multithreaded Root Evaluation using Parallel.ForEach
            Point bestMove = candidates[0];
            int bestScore = int.MinValue;
            object lockObj = new object();

            Parallel.ForEach(candidates, candidate =>
            {
                // Thread-local copy of board matrix
                char[,] boardCopy = (char[,])game.Board.Clone();
                boardCopy[candidate.Row, candidate.Col] = aiSymbol;

                int score = Minimax(boardCopy, game.BoardSize, maxDepth - 1, int.MinValue + 100, int.MaxValue - 100, false, aiSymbol, humanSymbol, game.BlockBothEndsRule);

                lock (lockObj)
                {
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = candidate;
                    }
                }
            });

            return bestMove;
        }

        private static int Minimax(char[,] board, int size, int depth, int alpha, int beta, bool isMaximizing, char aiSymbol, char humanSymbol, bool blockBothEndsRule)
        {
            if (depth == 0)
            {
                return EvaluateBoard(board, size, aiSymbol, humanSymbol, blockBothEndsRule);
            }

            List<Point> candidates = GetCandidateMoves(board, size);
            if (candidates.Count == 0)
            {
                return EvaluateBoard(board, size, aiSymbol, humanSymbol, blockBothEndsRule);
            }

            if (isMaximizing)
            {
                int maxEval = int.MinValue;
                foreach (var move in candidates)
                {
                    board[move.Row, move.Col] = aiSymbol;
                    if (CheckWinQuick(board, size, move.Row, move.Col, aiSymbol, blockBothEndsRule))
                    {
                        board[move.Row, move.Col] = ' ';
                        return WIN_SCORE + depth; // Prefer faster wins
                    }

                    int eval = Minimax(board, size, depth - 1, alpha, beta, false, aiSymbol, humanSymbol, blockBothEndsRule);
                    board[move.Row, move.Col] = ' ';

                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha) break; // Alpha-Beta Cutoff
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var move in candidates)
                {
                    board[move.Row, move.Col] = humanSymbol;
                    if (CheckWinQuick(board, size, move.Row, move.Col, humanSymbol, blockBothEndsRule))
                    {
                        board[move.Row, move.Col] = ' ';
                        return -WIN_SCORE - depth; // Prefer delaying losses
                    }

                    int eval = Minimax(board, size, depth - 1, alpha, beta, true, aiSymbol, humanSymbol, blockBothEndsRule);
                    board[move.Row, move.Col] = ' ';

                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha) break; // Alpha-Beta Cutoff
                }
                return minEval;
            }
        }

        private static List<Point> GetCandidateMoves(char[,] board, int size)
        {
            HashSet<long> candidateSet = new HashSet<long>();
            bool emptyBoard = true;

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (board[r, c] != ' ')
                    {
                        emptyBoard = false;
                        // Add empty neighbors within distance 1 or 2
                        for (int dr = -2; dr <= 2; dr++)
                        {
                            for (int dc = -2; dc <= 2; dc++)
                            {
                                if (dr == 0 && dc == 0) continue;
                                int nr = r + dr;
                                int nc = c + dc;
                                if (nr >= 0 && nr < size && nc >= 0 && nc < size && board[nr, nc] == ' ')
                                {
                                    long key = ((long)nr << 32) | (uint)nc;
                                    candidateSet.Add(key);
                                }
                            }
                        }
                    }
                }
            }

            if (emptyBoard)
            {
                int center = size / 2;
                return new List<Point> { new Point(center, center) };
            }

            List<Point> candidates = new List<Point>();
            foreach (long key in candidateSet)
            {
                int r = (int)(key >> 32);
                int c = (int)(key & 0xFFFFFFFF);
                candidates.Add(new Point(r, c));
            }

            return candidates;
        }

        private static bool CheckWinQuick(char[,] board, int size, int r, int c, char symbol, bool blockBothEndsRule)
        {
            int[,] directions = new int[,] { { 0, 1 }, { 1, 0 }, { 1, 1 }, { 1, -1 } };
            char other = (symbol == 'X') ? 'O' : 'X';

            for (int d = 0; d < 4; d++)
            {
                int dr = directions[d, 0];
                int dc = directions[d, 1];
                int count = 1;

                // Forward
                int step = 1;
                while (true)
                {
                    int nr = r + dr * step;
                    int nc = c + dc * step;
                    if (nr >= 0 && nr < size && nc >= 0 && nc < size && board[nr, nc] == symbol)
                    {
                        count++;
                        step++;
                    }
                    else break;
                }
                int end1R = r + dr * step;
                int end1C = c + dc * step;

                // Backward
                step = 1;
                while (true)
                {
                    int nr = r - dr * step;
                    int nc = c - dc * step;
                    if (nr >= 0 && nr < size && nc >= 0 && nc < size && board[nr, nc] == symbol)
                    {
                        count++;
                        step++;
                    }
                    else break;
                }
                int end2R = r - dr * step;
                int end2C = c - dc * step;

                if (count >= 5)
                {
                    if (blockBothEndsRule && count == 5)
                    {
                        bool end1Blocked = end1R < 0 || end1R >= size || end1C < 0 || end1C >= size || board[end1R, end1C] == other;
                        bool end2Blocked = end2R < 0 || end2R >= size || end2C < 0 || end2C >= size || board[end2R, end2C] == other;
                        if (end1Blocked && end2Blocked) continue;
                    }
                    return true;
                }
            }
            return false;
        }

        public static int EvaluateBoard(char[,] board, int size, char aiSymbol, char humanSymbol, bool blockBothEndsRule)
        {
            int aiScore = EvaluatePlayer(board, size, aiSymbol, humanSymbol, blockBothEndsRule);
            int humanScore = EvaluatePlayer(board, size, humanSymbol, aiSymbol, blockBothEndsRule);
            return aiScore - humanScore;
        }

        private static int EvaluatePlayer(char[,] board, int size, char playerSymbol, char opponentSymbol, bool blockBothEndsRule)
        {
            int score = 0;
            int[,] directions = new int[,] { { 0, 1 }, { 1, 0 }, { 1, 1 }, { 1, -1 } };

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (board[r, c] != playerSymbol) continue;

                    for (int d = 0; d < 4; d++)
                    {
                        int dr = directions[d, 0];
                        int dc = directions[d, 1];

                        // To avoid duplicate line counting, only count line starting at end
                        int prevR = r - dr;
                        int prevC = c - dc;
                        if (prevR >= 0 && prevR < size && prevC >= 0 && prevC < size && board[prevR, prevC] == playerSymbol)
                        {
                            continue;
                        }

                        int length = 1;
                        while (true)
                        {
                            int nr = r + dr * length;
                            int nc = c + dc * length;
                            if (nr >= 0 && nr < size && nc >= 0 && nc < size && board[nr, nc] == playerSymbol)
                            {
                                length++;
                            }
                            else break;
                        }

                        int startR = r - dr;
                        int startC = c - dc;
                        int endR = r + dr * length;
                        int endC = c + dc * length;

                        bool startBlocked = startR < 0 || startR >= size || startC < 0 || startC >= size || board[startR, startC] == opponentSymbol;
                        bool endBlocked = endR < 0 || endR >= size || endC < 0 || endC >= size || board[endR, endC] == opponentSymbol;

                        if (length >= 5)
                        {
                            if (blockBothEndsRule && length == 5 && startBlocked && endBlocked)
                            {
                                // Blocked both ends -> 0 score
                            }
                            else
                            {
                                score += WIN_SCORE;
                            }
                        }
                        else if (length == 4)
                        {
                            if (!startBlocked && !endBlocked) score += FOUR_OPEN;
                            else if (!startBlocked || !endBlocked) score += FOUR_HALF;
                        }
                        else if (length == 3)
                        {
                            if (!startBlocked && !endBlocked) score += THREE_OPEN;
                            else if (!startBlocked || !endBlocked) score += THREE_HALF;
                        }
                        else if (length == 2)
                        {
                            if (!startBlocked && !endBlocked) score += TWO_OPEN;
                            else if (!startBlocked || !endBlocked) score += TWO_HALF;
                        }
                    }
                }
            }

            return score;
        }
    }
}
