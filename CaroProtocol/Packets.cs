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
        SpectateRoomResp // Phản hồi xem phòng
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
}
