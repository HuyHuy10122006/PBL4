using CaroProtocol;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CaroServer.Database
{
    public class UserData
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Elo { get; set; } = 1000;
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }

    public static class DatabaseManager
    {
        private static string? _workingConnectionString;
        private static bool _isSqlAvailable = false;

        // In-memory fallback if SQL Server is not reachable
        private static readonly Dictionary<string, (string PasswordHash, int Elo, int Wins, int Losses, int Draws)> _memoryUsers = new();
        private static readonly List<(string P1, string P2, string? Winner, int E1Before, int E1After, int E2Before, int E2After, int Moves, DateTime Time)> _memoryHistory = new();
        private static readonly object _lock = new object();

        private static readonly string[] PossibleMasterStrings = new[]
        {
            "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Encrypt=False;",
            "Server=.\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Encrypt=False;",
            "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Encrypt=False;"
        };

        public static bool IsSqlAvailable => _isSqlAvailable;

        public static async Task InitializeAsync()
        {
            Console.WriteLine("[DB] Đang kết nối đến Microsoft SQL Server nội bộ...");

            foreach (var masterConn in PossibleMasterStrings)
            {
                try
                {
                    using var conn = new SqlConnection(masterConn);
                    await conn.OpenAsync();
                    Console.WriteLine($"[DB] Đã kết nối thành công tới SQL Server qua: {masterConn.Split(';')[0]}");

                    // 1. Tạo database CaroGameDB nếu chưa có
                    string createDbSql = @"
                        IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'CaroGameDB')
                        BEGIN
                            CREATE DATABASE CaroGameDB;
                        END";
                    using (var cmd = new SqlCommand(createDbSql, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // 2. Thiết lập connection string tới CaroGameDB
                    _workingConnectionString = masterConn.Replace("Database=master", "Database=CaroGameDB");

                    // 3. Tạo các bảng nếu chưa có
                    using (var dbConn = new SqlConnection(_workingConnectionString))
                    {
                        await dbConn.OpenAsync();

                        string createTablesSql = @"
                            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
                            BEGIN
                                CREATE TABLE Users (
                                    Id INT IDENTITY(1,1) PRIMARY KEY,
                                    Username NVARCHAR(50) NOT NULL UNIQUE,
                                    PasswordHash NVARCHAR(256) NOT NULL,
                                    Elo INT NOT NULL DEFAULT 1000,
                                    Wins INT NOT NULL DEFAULT 0,
                                    Losses INT NOT NULL DEFAULT 0,
                                    Draws INT NOT NULL DEFAULT 0,
                                    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
                                );
                            END

                            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MatchHistory')
                            BEGIN
                                CREATE TABLE MatchHistory (
                                    Id INT IDENTITY(1,1) PRIMARY KEY,
                                    Player1Username NVARCHAR(50) NOT NULL,
                                    Player2Username NVARCHAR(50) NOT NULL,
                                    WinnerUsername NVARCHAR(50) NULL,
                                    Player1EloBefore INT NOT NULL,
                                    Player1EloAfter INT NOT NULL,
                                    Player2EloBefore INT NOT NULL,
                                    Player2EloAfter INT NOT NULL,
                                    TotalMoves INT NOT NULL DEFAULT 0,
                                    PlayedAt DATETIME NOT NULL DEFAULT GETDATE()
                                );
                            END";

                        using var cmdTables = new SqlCommand(createTablesSql, dbConn);
                        await cmdTables.ExecuteNonQueryAsync();
                    }

                    _isSqlAvailable = true;
                    Console.WriteLine("[DB] Cơ sở dữ liệu CaroGameDB & các bảng Users, MatchHistory đã sẵn sàng!\n");
                    return;
                }
                catch
                {
                    // Thử chuỗi kết nối tiếp theo
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[DB-WARNING] Không thể kết nối tới SQL Server cục bộ (localhost / SQLEXPRESS / localdb).");
            Console.WriteLine("[DB-WARNING] Hệ thống chuyển sang chế độ In-Memory Database dự phòng để Server vẫn chạy mượt mà.");
            Console.WriteLine("[DB-WARNING] (Để lưu trữ vĩnh viễn, hãy bật dịch vụ SQL Server trên máy làm Server).\n");
            Console.ResetColor();
        }

        public static string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        public static async Task<(bool Success, string Message, UserData? User)> LoginAsync(string username, string password)
        {
            username = username.Trim();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Tên đăng nhập và mật khẩu không được để trống.", null);
            }

            string passHash = HashPassword(password);

            if (_isSqlAvailable && _workingConnectionString != null)
            {
                try
                {
                    using var conn = new SqlConnection(_workingConnectionString);
                    await conn.OpenAsync();

                    string sql = "SELECT Id, Username, PasswordHash, Elo, Wins, Losses, Draws FROM Users WHERE Username = @Username";
                    using var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Username", username);

                    using var reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        string dbHash = reader.GetString(2);
                        if (string.Equals(dbHash, passHash, StringComparison.OrdinalIgnoreCase))
                        {
                            var user = new UserData
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Elo = reader.GetInt32(3),
                                Wins = reader.GetInt32(4),
                                Losses = reader.GetInt32(5),
                                Draws = reader.GetInt32(6)
                            };
                            return (true, "Đăng nhập thành công!", user);
                        }
                        else
                        {
                            return (false, "Mật khẩu không chính xác.", null);
                        }
                    }
                    else
                    {
                        return (false, "Tài khoản không tồn tại. Vui lòng bấm Đăng ký trước.", null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DB ERROR] Login: {ex.Message}");
                    return (false, $"Lỗi truy vấn Database: {ex.Message}", null);
                }
            }

            // In-Memory Fallback
            lock (_lock)
            {
                if (_memoryUsers.TryGetValue(username.ToLowerInvariant(), out var info))
                {
                    if (info.PasswordHash == passHash)
                    {
                        var user = new UserData
                        {
                            Id = 1,
                            Username = username,
                            Elo = info.Elo,
                            Wins = info.Wins,
                            Losses = info.Losses,
                            Draws = info.Draws
                        };
                        return (true, "Đăng nhập thành công (In-Memory)!", user);
                    }
                    return (false, "Mật khẩu không chính xác.", null);
                }
                return (false, "Tài khoản chưa tồn tại. Vui lòng bấm Đăng ký trước.", null);
            }
        }

        public static async Task<(bool Success, string Message)> RegisterAsync(string username, string password)
        {
            username = username.Trim();
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
            {
                return (false, "Tên đăng nhập phải có ít nhất 3 ký tự.");
            }
            if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
            {
                return (false, "Mật khẩu phải có ít nhất 4 ký tự.");
            }

            string passHash = HashPassword(password);

            if (_isSqlAvailable && _workingConnectionString != null)
            {
                try
                {
                    using var conn = new SqlConnection(_workingConnectionString);
                    await conn.OpenAsync();

                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    using (var checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        int count = (int)await checkCmd.ExecuteScalarAsync();
                        if (count > 0)
                        {
                            return (false, "Tên đăng nhập này đã được sử dụng. Hãy chọn tên khác!");
                        }
                    }

                    string insertSql = @"
                        INSERT INTO Users (Username, PasswordHash, Elo, Wins, Losses, Draws)
                        VALUES (@Username, @PasswordHash, 1000, 0, 0, 0)";
                    using (var insertCmd = new SqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Username", username);
                        insertCmd.Parameters.AddWithValue("@PasswordHash", passHash);
                        await insertCmd.ExecuteNonQueryAsync();
                    }

                    return (true, "Đăng ký tài khoản thành công! Bạn có thể đăng nhập ngay.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DB ERROR] Register: {ex.Message}");
                    return (false, $"Lỗi tạo tài khoản: {ex.Message}");
                }
            }

            // In-Memory Fallback
            lock (_lock)
            {
                string key = username.ToLowerInvariant();
                if (_memoryUsers.ContainsKey(key))
                {
                    return (false, "Tên đăng nhập này đã được sử dụng.");
                }
                _memoryUsers[key] = (passHash, 1000, 0, 0, 0);
                return (true, "Đăng ký tài khoản thành công (In-Memory)! Hãy đăng nhập.");
            }
        }

        public static async Task<(int newElo1, int newElo2, int change1, int change2)> RecordMatchAsync(
            string player1,
            string player2,
            string? winnerUsername,
            double score1,
            int totalMoves)
        {
            int elo1 = 1000, elo2 = 1000;
            int wins1 = 0, losses1 = 0, draws1 = 0;
            int wins2 = 0, losses2 = 0, draws2 = 0;

            if (_isSqlAvailable && _workingConnectionString != null)
            {
                try
                {
                    using var conn = new SqlConnection(_workingConnectionString);
                    await conn.OpenAsync();

                    // Lấy Elo hiện tại của 2 người chơi
                    string sqlGet = "SELECT Username, Elo, Wins, Losses, Draws FROM Users WHERE Username IN (@P1, @P2)";
                    using (var cmd = new SqlCommand(sqlGet, conn))
                    {
                        cmd.Parameters.AddWithValue("@P1", player1);
                        cmd.Parameters.AddWithValue("@P2", player2);
                        using var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            string uname = reader.GetString(0);
                            if (string.Equals(uname, player1, StringComparison.OrdinalIgnoreCase))
                            {
                                elo1 = reader.GetInt32(1);
                                wins1 = reader.GetInt32(2);
                                losses1 = reader.GetInt32(3);
                                draws1 = reader.GetInt32(4);
                            }
                            else if (string.Equals(uname, player2, StringComparison.OrdinalIgnoreCase))
                            {
                                elo2 = reader.GetInt32(1);
                                wins2 = reader.GetInt32(2);
                                losses2 = reader.GetInt32(3);
                                draws2 = reader.GetInt32(4);
                            }
                        }
                    }

                    var (newElo1, newElo2, change1, change2) = EloCalculator.Calculate(elo1, elo2, score1);

                    // Cập nhật stats
                    if (score1 == 1.0)
                    {
                        wins1++;
                        losses2++;
                    }
                    else if (score1 == 0.0)
                    {
                        losses1++;
                        wins2++;
                    }
                    else
                    {
                        draws1++;
                        draws2++;
                    }

                    // Cập nhật Users
                    string sqlUpdate1 = "UPDATE Users SET Elo = @Elo, Wins = @Wins, Losses = @Losses, Draws = @Draws WHERE Username = @Username";
                    using (var cmd = new SqlCommand(sqlUpdate1, conn))
                    {
                        cmd.Parameters.AddWithValue("@Elo", newElo1);
                        cmd.Parameters.AddWithValue("@Wins", wins1);
                        cmd.Parameters.AddWithValue("@Losses", losses1);
                        cmd.Parameters.AddWithValue("@Draws", draws1);
                        cmd.Parameters.AddWithValue("@Username", player1);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    using (var cmd = new SqlCommand(sqlUpdate1, conn))
                    {
                        cmd.Parameters.AddWithValue("@Elo", newElo2);
                        cmd.Parameters.AddWithValue("@Wins", wins2);
                        cmd.Parameters.AddWithValue("@Losses", losses2);
                        cmd.Parameters.AddWithValue("@Draws", draws2);
                        cmd.Parameters.AddWithValue("@Username", player2);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // Ghi vào MatchHistory
                    string sqlInsertHistory = @"
                        INSERT INTO MatchHistory (Player1Username, Player2Username, WinnerUsername, Player1EloBefore, Player1EloAfter, Player2EloBefore, Player2EloAfter, TotalMoves)
                        VALUES (@P1, @P2, @Winner, @E1Before, @E1After, @E2Before, @E2After, @Moves)";
                    using (var cmd = new SqlCommand(sqlInsertHistory, conn))
                    {
                        cmd.Parameters.AddWithValue("@P1", player1);
                        cmd.Parameters.AddWithValue("@P2", player2);
                        cmd.Parameters.AddWithValue("@Winner", (object?)winnerUsername ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@E1Before", elo1);
                        cmd.Parameters.AddWithValue("@E1After", newElo1);
                        cmd.Parameters.AddWithValue("@E2Before", elo2);
                        cmd.Parameters.AddWithValue("@E2After", newElo2);
                        cmd.Parameters.AddWithValue("@Moves", totalMoves);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    return (newElo1, newElo2, change1, change2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DB ERROR] RecordMatchAsync: {ex.Message}");
                }
            }

            // In-Memory Fallback
            lock (_lock)
            {
                string key1 = player1.ToLowerInvariant();
                string key2 = player2.ToLowerInvariant();
                if (_memoryUsers.TryGetValue(key1, out var u1)) elo1 = u1.Elo;
                if (_memoryUsers.TryGetValue(key2, out var u2)) elo2 = u2.Elo;

                var (newElo1, newElo2, change1, change2) = EloCalculator.Calculate(elo1, elo2, score1);

                if (_memoryUsers.TryGetValue(key1, out var inf1))
                {
                    int w = inf1.Wins + (score1 == 1.0 ? 1 : 0);
                    int l = inf1.Losses + (score1 == 0.0 ? 1 : 0);
                    int d = inf1.Draws + (score1 == 0.5 ? 1 : 0);
                    _memoryUsers[key1] = (inf1.PasswordHash, newElo1, w, l, d);
                }

                if (_memoryUsers.TryGetValue(key2, out var inf2))
                {
                    int w = inf2.Wins + (score1 == 0.0 ? 1 : 0);
                    int l = inf2.Losses + (score1 == 1.0 ? 1 : 0);
                    int d = inf2.Draws + (score1 == 0.5 ? 1 : 0);
                    _memoryUsers[key2] = (inf2.PasswordHash, newElo2, w, l, d);
                }

                _memoryHistory.Add((player1, player2, winnerUsername, elo1, newElo1, elo2, newElo2, totalMoves, DateTime.Now));
                return (newElo1, newElo2, change1, change2);
            }
        }

        public static async Task<List<MatchHistoryItemData>> GetUserMatchHistoryAsync(string username)
        {
            var list = new List<MatchHistoryItemData>();

            if (_isSqlAvailable && _workingConnectionString != null)
            {
                try
                {
                    using var conn = new SqlConnection(_workingConnectionString);
                    await conn.OpenAsync();

                    string sql = @"
                        SELECT TOP 30 Id, Player1Username, Player2Username, WinnerUsername, Player1EloBefore, Player1EloAfter, Player2EloBefore, Player2EloAfter, TotalMoves, PlayedAt
                        FROM MatchHistory
                        WHERE Player1Username = @Username OR Player2Username = @Username
                        ORDER BY PlayedAt DESC";

                    using var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Username", username);

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string p1 = reader.GetString(1);
                        string p2 = reader.GetString(2);
                        string? winner = reader.IsDBNull(3) ? null : reader.GetString(3);
                        int p1Before = reader.GetInt32(4);
                        int p1After = reader.GetInt32(5);
                        int p2Before = reader.GetInt32(6);
                        int p2After = reader.GetInt32(7);
                        int moves = reader.GetInt32(8);
                        DateTime playedAt = reader.GetDateTime(9);

                        bool isP1 = string.Equals(username, p1, StringComparison.OrdinalIgnoreCase);
                        string opponent = isP1 ? p2 : p1;
                        int eloChange = isP1 ? (p1After - p1Before) : (p2After - p2Before);

                        string resultStr;
                        if (string.IsNullOrEmpty(winner))
                        {
                            resultStr = "Hòa";
                        }
                        else if (string.Equals(winner, username, StringComparison.OrdinalIgnoreCase))
                        {
                            resultStr = "Thắng";
                        }
                        else
                        {
                            resultStr = "Thua";
                        }

                        list.Add(new MatchHistoryItemData
                        {
                            Id = id,
                            OpponentName = opponent,
                            Result = resultStr,
                            EloChange = eloChange,
                            TotalMoves = moves,
                            PlayedAt = playedAt.ToString("dd/MM/yyyy HH:mm")
                        });
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DB ERROR] GetUserMatchHistoryAsync: {ex.Message}");
                }
            }

            // In-Memory Fallback
            lock (_lock)
            {
                foreach (var h in _memoryHistory.Where(m =>
                    string.Equals(m.P1, username, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(m.P2, username, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(m => m.Time)
                    .Take(30))
                {
                    bool isP1 = string.Equals(username, h.P1, StringComparison.OrdinalIgnoreCase);
                    string opp = isP1 ? h.P2 : h.P1;
                    int change = isP1 ? (h.E1After - h.E1Before) : (h.E2After - h.E2Before);
                    string res = string.IsNullOrEmpty(h.Winner) ? "Hòa" : (string.Equals(h.Winner, username, StringComparison.OrdinalIgnoreCase) ? "Thắng" : "Thua");

                    list.Add(new MatchHistoryItemData
                    {
                        Id = 1,
                        OpponentName = opp,
                        Result = res,
                        EloChange = change,
                        TotalMoves = h.Moves,
                        PlayedAt = h.Time.ToString("dd/MM/yyyy HH:mm")
                    });
                }
            }

            return list;
        }

        public static async Task<List<LeaderboardItemData>> GetLeaderboardAsync(int top = 20)
        {
            var list = new List<LeaderboardItemData>();

            if (_isSqlAvailable && _workingConnectionString != null)
            {
                try
                {
                    using var conn = new SqlConnection(_workingConnectionString);
                    await conn.OpenAsync();

                    string sql = $@"
                        SELECT TOP {top} Username, Elo, Wins, Losses, Draws
                        FROM Users
                        ORDER BY Elo DESC, Wins DESC";

                    using var cmd = new SqlCommand(sql, conn);
                    using var reader = await cmd.ExecuteReaderAsync();
                    int rank = 1;
                    while (await reader.ReadAsync())
                    {
                        string uname = reader.GetString(0);
                        int elo = reader.GetInt32(1);
                        int wins = reader.GetInt32(2);
                        int losses = reader.GetInt32(3);
                        int draws = reader.GetInt32(4);
                        int total = wins + losses + draws;
                        double winRate = (total > 0) ? Math.Round((double)wins / total * 100, 1) : 0;

                        list.Add(new LeaderboardItemData
                        {
                            Rank = rank++,
                            Username = uname,
                            Elo = elo,
                            Wins = wins,
                            Losses = losses,
                            Draws = draws,
                            WinRate = winRate
                        });
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DB ERROR] GetLeaderboardAsync: {ex.Message}");
                }
            }

            // In-Memory Fallback
            lock (_lock)
            {
                int r = 1;
                foreach (var pair in _memoryUsers.OrderByDescending(u => u.Value.Elo).ThenByDescending(u => u.Value.Wins).Take(top))
                {
                    int total = pair.Value.Wins + pair.Value.Losses + pair.Value.Draws;
                    double winRate = (total > 0) ? Math.Round((double)pair.Value.Wins / total * 100, 1) : 0;
                    list.Add(new LeaderboardItemData
                    {
                        Rank = r++,
                        Username = pair.Key,
                        Elo = pair.Value.Elo,
                        Wins = pair.Value.Wins,
                        Losses = pair.Value.Losses,
                        Draws = pair.Value.Draws,
                        WinRate = winRate
                    });
                }
            }

            return list;
        }
    }
}

