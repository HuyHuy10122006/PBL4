using CaroProtocol;
using CaroServer.Database;
using Xunit;

namespace CaroTests
{
    public class EloAndAuthTests
    {
        [Fact]
        public void EloCalculator_EqualElo_WinnerGainsPoints()
        {
            int elo1 = 1000;
            int elo2 = 1000;

            // Player 1 wins
            var (newElo1, newElo2, change1, change2) = EloCalculator.Calculate(elo1, elo2, 1.0);

            Assert.Equal(1016, newElo1);
            Assert.Equal(984, newElo2);
            Assert.Equal(16, change1);
            Assert.Equal(-16, change2);
        }

        [Fact]
        public void EloCalculator_EqualElo_Draw_NoChange()
        {
            int elo1 = 1000;
            int elo2 = 1000;

            // Draw
            var (newElo1, newElo2, change1, change2) = EloCalculator.Calculate(elo1, elo2, 0.5);

            Assert.Equal(1000, newElo1);
            Assert.Equal(1000, newElo2);
            Assert.Equal(0, change1);
            Assert.Equal(0, change2);
        }

        [Fact]
        public void EloCalculator_UnderdogWins_GainsMorePoints()
        {
            int underdogElo = 1000;
            int masterElo = 1400;

            // Underdog wins against master
            var (newUnderdog, newMaster, changeUnderdog, changeMaster) = EloCalculator.Calculate(underdogElo, masterElo, 1.0);

            Assert.True(changeUnderdog > 25);
            Assert.True(newUnderdog > 1025);
            Assert.True(newMaster < 1375);
        }

        [Fact]
        public async Task DatabaseManager_RegisterAndLogin_Succeeds()
        {
            string testUser = "User_" + Guid.NewGuid().ToString("N")[..6];
            string testPass = "Pass1234";

            var (regOk, regMsg) = await DatabaseManager.RegisterAsync(testUser, testPass);
            Assert.True(regOk, regMsg);

            // Duplicate register should fail
            var (dupOk, _) = await DatabaseManager.RegisterAsync(testUser, testPass);
            Assert.False(dupOk);

            // Correct login
            var (loginOk, loginMsg, user) = await DatabaseManager.LoginAsync(testUser, testPass);
            Assert.True(loginOk, loginMsg);
            Assert.NotNull(user);
            Assert.Equal(1000, user.Elo);

            // Wrong password login should fail
            var (wrongOk, wrongMsg, _) = await DatabaseManager.LoginAsync(testUser, "WrongPassword");
            Assert.False(wrongOk);
        }

        [Fact]
        public async Task DatabaseManager_RecordMatch_UpdatesEloAndHistory()
        {
            string p1 = "P1_" + Guid.NewGuid().ToString("N")[..6];
            string p2 = "P2_" + Guid.NewGuid().ToString("N")[..6];

            await DatabaseManager.RegisterAsync(p1, "pass123");
            await DatabaseManager.RegisterAsync(p2, "pass123");

            // P1 wins against P2
            var (newE1, newE2, ch1, ch2) = await DatabaseManager.RecordMatchAsync(p1, p2, p1, 1.0, 15);
            Assert.True(newE1 > 1000);
            Assert.True(newE2 < 1000);
            Assert.True(ch1 > 0);
            Assert.True(ch2 < 0);

            var history = await DatabaseManager.GetUserMatchHistoryAsync(p1);
            Assert.NotEmpty(history);
            Assert.Equal("Thắng", history[0].Result);
            Assert.Equal(p2, history[0].OpponentName);
            Assert.Equal(15, history[0].TotalMoves);
        }

        [Fact]
        public void GameEngine_TracksTotalMovesCount()
        {
            var engine = new GameEngine(15);
            Assert.Equal(0, engine.TotalMovesCount);

            engine.MakeMove(7, 7, 'X', out _);
            Assert.Equal(1, engine.TotalMovesCount);

            engine.MakeMove(7, 8, 'O', out _);
            Assert.Equal(2, engine.TotalMovesCount);

            engine.Reset();
            Assert.Equal(0, engine.TotalMovesCount);
        }
    }
}

