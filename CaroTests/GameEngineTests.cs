using CaroProtocol;
using CaroServer;
using Xunit;

namespace CaroTests
{
    public class GameEngineTests
    {
        [Fact]
        public void MakeMove_ValidMove_UpdatesBoardAndSwitchesTurn()
        {
            var engine = new GameEngine(15);
            bool success = engine.MakeMove(7, 7, 'X', out string err);

            Assert.True(success);
            Assert.Empty(err);
            Assert.Equal('X', engine.Board[7, 7]);
            Assert.Equal('O', engine.CurrentTurnSymbol);
        }

        [Fact]
        public void MakeMove_WrongTurn_ReturnsError()
        {
            var engine = new GameEngine(15);
            bool success = engine.MakeMove(7, 7, 'O', out string err);

            Assert.False(success);
            Assert.Contains("lượt", err);
        }

        [Fact]
        public void MakeMove_OccupiedCell_ReturnsError()
        {
            var engine = new GameEngine(15);
            engine.MakeMove(7, 7, 'X', out _);
            bool success = engine.MakeMove(7, 7, 'O', out string err);

            Assert.False(success);
            Assert.Contains("đã được đánh", err);
        }

        [Fact]
        public void MakeMove_OutOfBounds_ReturnsError()
        {
            var engine = new GameEngine(15);
            bool success = engine.MakeMove(-1, 5, 'X', out string err);

            Assert.False(success);
            Assert.Contains("ngoài phạm vi", err);
        }

        [Fact]
        public void CheckWin_HorizontalFive_DetectsWinner()
        {
            var engine = new GameEngine(15, blockBothEndsRule: true);
            // X: (7,3), (7,4), (7,5), (7,6), (7,7)
            engine.MakeMove(7, 3, 'X', out _); engine.MakeMove(0, 0, 'O', out _);
            engine.MakeMove(7, 4, 'X', out _); engine.MakeMove(0, 1, 'O', out _);
            engine.MakeMove(7, 5, 'X', out _); engine.MakeMove(0, 2, 'O', out _);
            engine.MakeMove(7, 6, 'X', out _); engine.MakeMove(0, 3, 'O', out _);
            engine.MakeMove(7, 7, 'X', out _);

            Assert.Equal(GameStateStatus.GameOver, engine.Status);
            Assert.Equal('X', engine.WinnerSymbol);
            Assert.Equal(5, engine.WinLine.Count);
        }

        [Fact]
        public void CheckWin_BlockedBothEnds_DoesNotWinWhenRuleEnabled()
        {
            var engine = new GameEngine(15, blockBothEndsRule: true);

            // Block ends with O at (7,2) and (7,8)
            engine.MakeMove(0, 0, 'X', out _); engine.MakeMove(7, 2, 'O', out _);
            engine.MakeMove(0, 1, 'X', out _); engine.MakeMove(7, 8, 'O', out _);

            // X plays (7,3), (7,4), (7,5), (7,6), (7,7)
            engine.MakeMove(7, 3, 'X', out _); engine.MakeMove(1, 0, 'O', out _);
            engine.MakeMove(7, 4, 'X', out _); engine.MakeMove(1, 1, 'O', out _);
            engine.MakeMove(7, 5, 'X', out _); engine.MakeMove(1, 2, 'O', out _);
            engine.MakeMove(7, 6, 'X', out _); engine.MakeMove(1, 3, 'O', out _);
            engine.MakeMove(7, 7, 'X', out _);

            // Because blocked at both 7,2 and 7,8, game should NOT be over!
            Assert.Equal(GameStateStatus.Playing, engine.Status);
        }

        [Fact]
        public void CheckWin_BlockedBothEnds_WinsWhenFreeRuleEnabled()
        {
            var engine = new GameEngine(15, blockBothEndsRule: false);

            // Block ends with O at (7,2) and (7,8)
            engine.MakeMove(0, 0, 'X', out _); engine.MakeMove(7, 2, 'O', out _);
            engine.MakeMove(0, 1, 'X', out _); engine.MakeMove(7, 8, 'O', out _);

            // X plays (7,3), (7,4), (7,5), (7,6), (7,7)
            engine.MakeMove(7, 3, 'X', out _); engine.MakeMove(1, 0, 'O', out _);
            engine.MakeMove(7, 4, 'X', out _); engine.MakeMove(1, 1, 'O', out _);
            engine.MakeMove(7, 5, 'X', out _); engine.MakeMove(1, 2, 'O', out _);
            engine.MakeMove(7, 6, 'X', out _); engine.MakeMove(1, 3, 'O', out _);
            engine.MakeMove(7, 7, 'X', out _);

            // In free rule, 5-in-a-row ALWAYS wins!
            Assert.Equal(GameStateStatus.GameOver, engine.Status);
            Assert.Equal('X', engine.WinnerSymbol);
        }

        [Fact]
        public void MinimaxAI_MustBlockWinningMove()
        {
            var engine = new GameEngine(15, blockBothEndsRule: true);

            // Player X builds 3-in-a-row: (7,3), (7,4), (7,5)
            engine.MakeMove(7, 3, 'X', out _); engine.MakeMove(0, 0, 'O', out _);
            engine.MakeMove(7, 4, 'X', out _); engine.MakeMove(0, 1, 'O', out _);
            engine.MakeMove(7, 5, 'X', out _); engine.MakeMove(0, 2, 'O', out _);

            // Player X now plays (7,6) creating Open 4!
            engine.MakeMove(7, 6, 'X', out _);

            // Now it's O's turn (AI). AI must play (7,2) or (7,7) to block X!
            Point aiMove = MinimaxAI.FindBestMove(engine, 'O', AIDifficulty.Hard);

            Assert.True((aiMove.Row == 7 && aiMove.Col == 2) || (aiMove.Row == 7 && aiMove.Col == 7));
        }

        [Fact]
        public void Surrender_UpdatesStatusToGameOverAndSetsWinner()
        {
            var engine = new GameEngine(15);
            Assert.Equal(GameStateStatus.Playing, engine.Status);

            engine.Surrender('O');

            Assert.Equal(GameStateStatus.GameOver, engine.Status);
            Assert.Equal('O', engine.WinnerSymbol);
        }
    }
}
