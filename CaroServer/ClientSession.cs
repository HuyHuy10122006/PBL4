using CaroProtocol;
using System.Net.Sockets;

namespace CaroServer
{
    public class ClientSession
    {
        public string PlayerId { get; }
        public string PlayerName { get; set; } = "Player";
        public TcpClient Client { get; }
        public NetworkStream Stream { get; }
        public Room? CurrentRoom { get; set; }

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public ClientSession(TcpClient client)
        {
            Client = client;
            Stream = client.GetStream();
            PlayerId = Guid.NewGuid().ToString("N")[..8];
        }

        public async Task SendPacketAsync(Packet packet)
        {
            try
            {
                await PacketFraming.SendPacketAsync(Stream, packet, _cts.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Session {PlayerId}] Error sending packet {packet.Type}: {ex.Message}");
            }
        }

        public async Task StartListenLoopAsync(Func<ClientSession, Packet, Task> onPacketReceived, Action<ClientSession> onDisconnected)
        {
            try
            {
                while (!_cts.Token.IsCancellationRequested && Client.Connected)
                {
                    Packet? packet = await PacketFraming.ReceivePacketAsync(Stream, _cts.Token);
                    if (packet == null)
                    {
                        // Disconnected
                        break;
                    }
                    await onPacketReceived(this, packet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Session {PlayerId}] Connection reset: {ex.Message}");
            }
            finally
            {
                Disconnect();
                onDisconnected(this);
            }
        }

        public void Disconnect()
        {
            _cts.Cancel();
            try { Stream.Close(); } catch { }
            try { Client.Close(); } catch { }
        }
    }
}
