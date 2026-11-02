using System.Text.Json;
using System.Text.Json.Serialization;

namespace CaroProtocol
{
    public enum PacketType
    {
        ConnectReq, // yêu cầu kết nối  
        ConnectResp,// Phản hồi kết nối
        CreateRoomReq,// Tạo phòng 
        CreateRoomResp,// Tạo phòng thành công 
        JoinRoomReq,// Yêu cầu vào phòng 
        JoinRoomResp,// Vào phòng thành công 
        MoveReq,// Yêu cầu di chuyển 
        MoveResp,// Di chuyển thành công 
        GameStateUpdate,// Cập nhật trạng thái game 
        SurrenderReq,// Yêu cầu đầu hàng 
        RestartReq,// Yêu cầu chơi lại 
        LeaveRoomReq,// Yêu cầu rời phòng 
        LeaveRoomResp,// Rời phòng thành công 
        ChatMessage,// Nhắn tin 
        OpponentDisconnected,// Đối thủ rời khỏi phòng 
        Error, // Lỗi 
        PlayerList, // Danh sách người chơi trên server
        RoomList, // Danh sách các phòng trên server
        GetRoomsReq, // Yêu cầu danh sách phòng
        SpectateRoomReq, // Yêu cầu vào xem phòng
        SpectateRoomResp, // Phản hồi xem phòng
        LoginReq, // Yêu cầu đăng nhập
        LoginResp, // Phản hồi đăng nhập
        RegisterReq, // Yêu cầu đăng ký
        RegisterResp, // Phản hồi đăng ký
        GetHistoryReq, // Yêu cầu xem lịch sử đấu
        GetHistoryResp, // Phản hồi lịch sử đấu
        GetLeaderboardReq, // Yêu cầu xem bảng xếp hạng
        GetLeaderboardResp, // Phản hồi bảng xếp hạng
        EloUpdate, // Cập nhật điểm Elo sau trận đấu
        OpponentReconnecting, // Đối thủ tạm mất kết nối, đang chờ trong thời gian ân hạn
        OpponentReconnected, // Đối thủ đã kết nối lại ván đấu
        ReconnectResp // Phản hồi kết nối lại ván đấu dở dang
    }

    public enum GameMode
    {
        PvP,
        PvAI
    }

    public enum AIDifficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public enum GameStateStatus
    {
        WaitingForOpponent,
        Playing,
        GameOver
    }

    public class Packet
    {
        public PacketType Type { get; set; }
        public string Payload { get; set; } = string.Empty;

        public static Packet Create<T>(PacketType type, T data)
        {
            return new Packet
            {
                Type = type,
                Payload = JsonSerializer.Serialize(data) 
            };
        }

        public T? DeserializePayload<T>()
        {
            if (string.IsNullOrEmpty(Payload)) return default;
            return JsonSerializer.Deserialize<T>(Payload);
        }
    }

    // --- Payload DTOs ---

    public class ConnectReqData
    {
        public string PlayerName { get; set; } = string.Empty;
    }

    public class ConnectRespData
    {
        public bool Success { get; set; }
        public string PlayerId { get; set; } = string.Empty;
        public string ServerHostName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Elo { get; set; } = 1000;
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }

    public class CreateRoomReqData
    {
        public GameMode Mode { get; set; }
        public AIDifficulty AIDifficulty { get; set; } = AIDifficulty.Medium;
        public int BoardSize { get; set; } = 15;
        public bool BlockBothEndsRule { get; set; } = true;
        public int TimePerTurn { get; set; } = 30; // 0 = Không giới hạn, 15, 30, 60
    }

    public class CreateRoomRespData
    {
        public bool Success { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public char PlayerSymbol { get; set; } // 'X' or 'O'
        public bool IsYourTurn { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class JoinRoomReqData
    {
        public string RoomId { get; set; } = string.Empty;
    }

    public class JoinRoomRespData
    {
        public bool Success { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public char PlayerSymbol { get; set; }
        public bool IsYourTurn { get; set; }
        public string OpponentName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class MoveReqData 
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }

    public class MoveRespData 
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class Point
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Point() { }
        public Point(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    public class GameStateUpdateData
    {
        public int BoardSize { get; set; } = 15;
        public char[][] Board { get; set; } = Array.Empty<char[]>();
        public char CurrentTurnSymbol { get; set; } // 'X' or 'O'
        public string CurrentTurnPlayerName { get; set; } = string.Empty;
        public int LastMoveRow { get; set; } = -1;
        public int LastMoveCol { get; set; } = -1;
        public GameStateStatus Status { get; set; }
        public char WinnerSymbol { get; set; } = ' '; // 'X', 'O', 'D' (Draw), ' ' (None)
        public List<Point> WinLine { get; set; } = new List<Point>();
        public string StatusMessage { get; set; } = string.Empty;
        public bool BlockBothEndsRule { get; set; } = true;
        public int TimePerTurn { get; set; } = 30;
    }

    public class ChatMessageData
    {
        public string SenderName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class LeaveRoomRespData
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class PlayerInfoData
    {
        public string PlayerId { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public string Status { get; set; } = "Đang chờ"; // "Đang chờ" or "Trong phòng"
        public int Elo { get; set; } = 1000;
        public int Wins { get; set; }
        public int Losses { get; set; }
    }

    public class PlayerListData
    {
        public List<PlayerInfoData> Players { get; set; } = new List<PlayerInfoData>();
    }

    public class RoomInfoData
    {
        public string RoomId { get; set; } = string.Empty;
        public GameMode Mode { get; set; }
        public string Player1Name { get; set; } = string.Empty;
        public string Player2Name { get; set; } = string.Empty;
        public int SpectatorCount { get; set; }
        public GameStateStatus Status { get; set; }
        public bool IsFull { get; set; }
        public bool CanJoinAsPlayer { get; set; }
        public bool BlockBothEndsRule { get; set; } = true;
        public int TimePerTurn { get; set; } = 30;
    }

    public class RoomListData
    {
        public List<RoomInfoData> Rooms { get; set; } = new List<RoomInfoData>();
    }

    public class SpectateRoomReqData
    {
        public string RoomId { get; set; } = string.Empty;
    }

    public class SpectateRoomRespData
    {
        public bool Success { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public string Player1Name { get; set; } = string.Empty;
        public string Player2Name { get; set; } = string.Empty;
        public GameMode Mode { get; set; }
        public bool BlockBothEndsRule { get; set; } = true;
        public int TimePerTurn { get; set; } = 30;
        public GameStateUpdateData CurrentState { get; set; } = new GameStateUpdateData();
        public string Message { get; set; } = string.Empty;
    }

    public class LoginReqData
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRespData
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string PlayerId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int Elo { get; set; } = 1000;
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }

    public class RegisterReqData
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRespData
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class MatchHistoryItemData
    {
        public int Id { get; set; }
        public string OpponentName { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // "Thắng", "Thua", "Hòa"
        public int EloChange { get; set; }
        public int TotalMoves { get; set; }
        public string PlayedAt { get; set; } = string.Empty;
    }

    public class MatchHistoryRespData
    {
        public List<MatchHistoryItemData> History { get; set; } = new List<MatchHistoryItemData>();
    }

    public class LeaderboardItemData
    {
        public int Rank { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Elo { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public double WinRate { get; set; }
    }

    public class LeaderboardRespData
    {
        public List<LeaderboardItemData> TopPlayers { get; set; } = new List<LeaderboardItemData>();
    }

    public class EloUpdateData
    {
        public int NewElo { get; set; }
        public int EloChange { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }

    public class OpponentReconnectingData
    {
        public string DisconnectedPlayerName { get; set; } = string.Empty;
        public int GracePeriodSeconds { get; set; } = 30;
        public string Message { get; set; } = string.Empty;
    }

    public class OpponentReconnectedData
    {
        public string ReconnectedPlayerName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class ReconnectRespData
    {
        public bool Success { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public char PlayerSymbol { get; set; } // 'X' or 'O'
        public bool IsYourTurn { get; set; }
        public string OpponentName { get; set; } = string.Empty;
        public GameMode Mode { get; set; }
        public bool BlockBothEndsRule { get; set; } = true;
        public int TimePerTurn { get; set; } = 30;
        public GameStateUpdateData CurrentState { get; set; } = new GameStateUpdateData();
        public string Message { get; set; } = string.Empty;
    }

    public static class EloCalculator
    {
        private const int K = 32;

        public static (int newElo1, int newElo2, int change1, int change2) Calculate(int elo1, int elo2, double score1)
        {
            // score1: 1.0 (Player 1 wins), 0.5 (Draw), 0.0 (Player 2 wins)
            double score2 = 1.0 - score1;

            double expected1 = 1.0 / (1.0 + Math.Pow(10, (elo2 - elo1) / 400.0));
            double expected2 = 1.0 - expected1;

            int change1 = (int)Math.Round(K * (score1 - expected1));
            int change2 = (int)Math.Round(K * (score2 - expected2));

            int newElo1 = Math.Max(100, elo1 + change1);
            int newElo2 = Math.Max(100, elo2 + change2);

            return (newElo1, newElo2, change1, change2);
        }
    }
}
