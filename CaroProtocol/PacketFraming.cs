using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace CaroProtocol
{
    public static class PacketFraming
    {
        /// <summary>
        /// Sends a Packet over a Stream using length-prefixing (4 bytes big-endian / network order length + UTF8 JSON payload).
        /// </summary>
        public static async Task SendPacketAsync(Stream stream, Packet packet, CancellationToken cancellationToken = default)
        {
            string json = JsonSerializer.Serialize(packet);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(json);
            byte[] lengthBytes = BitConverter.GetBytes(payloadBytes.Length);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(lengthBytes); // Convert to Network Byte Order (Big-Endian)
            }

            await stream.WriteAsync(lengthBytes, 0, 4, cancellationToken);
            await stream.WriteAsync(payloadBytes, 0, payloadBytes.Length, cancellationToken);
            await stream.FlushAsync(cancellationToken);
        }

        /// <summary>
        /// Reads a Packet from a Stream using length-prefixing. Returns null if stream ends or closes.
        /// </summary>
        public static async Task<Packet?> ReceivePacketAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            byte[] lengthBytes = new byte[4];
            int totalRead = 0;
            while (totalRead < 4)
            {
                int read = await stream.ReadAsync(lengthBytes, totalRead, 4 - totalRead, cancellationToken);
                if (read == 0) return null; // Stream closed gracefully
                totalRead += read;
            }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(lengthBytes);
            }

            int payloadLength = BitConverter.ToInt32(lengthBytes, 0);
            if (payloadLength <= 0 || payloadLength > 10 * 1024 * 1024) // Sanity check max 10MB
            {
                throw new InvalidDataException($"Invalid packet length received: {payloadLength}");
            }

            byte[] payloadBytes = new byte[payloadLength];
            totalRead = 0;
            while (totalRead < payloadLength)
            {
                int read = await stream.ReadAsync(payloadBytes, totalRead, payloadLength - totalRead, cancellationToken);
                if (read == 0) return null;
                totalRead += read;
            }

            string json = Encoding.UTF8.GetString(payloadBytes);
            return JsonSerializer.Deserialize<Packet>(json);
        }
    }
}
