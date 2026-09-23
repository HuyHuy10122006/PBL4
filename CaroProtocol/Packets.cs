using System.Text.Json;
using System.Text.Json.Serialization;

namespace CaroProtocol
{
    public enum PacketType
    {
        ConnectReq,
        ConnectResp,
        CreateRoomReq,
        CreateRoomResp,
        JoinRoomReq,
        JoinRoomResp,
        MoveReq,
        MoveResp,
        GameStateUpdate,
        SurrenderReq,
        RestartReq,
        LeaveRoomReq,
        LeaveRoomResp,
        ChatMessage,
        OpponentDisconnected,
        Error
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
        public string Message { get; set; } = string.Empty;
    }

    public class CreateRoomReqData
    {
        public GameMode Mode { get; set; }
        public AIDifficulty AIDifficulty { get; set; } = AIDifficulty.Medium;
        public int BoardSize { get; set; } = 15;
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
}
