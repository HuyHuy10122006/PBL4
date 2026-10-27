using CaroProtocol;
using CaroServer;
using Xunit;

namespace CaroTests
{
    public class RoomSpectatorTests
    {
        [Fact]
        public async Task RoomManager_ActiveRooms_ReflectsCreatedRoomsAndPlayerSlots()
        {
            var manager = new RoomManager();

            // Client 1 creates room
            var c1 = new ClientSession("c1", "Player1");
            Room room = await manager.CreateRoomAsync(c1, GameMode.PvP, AIDifficulty.Medium, 15);

            var activeRooms = manager.GetActiveRooms();
            Assert.Single(activeRooms);

            var info = activeRooms[0];
            Assert.Equal(room.RoomId, info.RoomId);
            Assert.Equal("Player1", info.Player1Name);
            Assert.Empty(info.Player2Name);
            Assert.True(info.CanJoinAsPlayer);
            Assert.Equal(0, info.SpectatorCount);
        }

        [Fact]
        public async Task RoomManager_WhenFull_CanJoinAsPlayerBecomesFalse()
        {
            var manager = new RoomManager();
            var c1 = new ClientSession("c1", "Player1");
            Room room = await manager.CreateRoomAsync(c1, GameMode.PvP, AIDifficulty.Medium, 15);

            var c2 = new ClientSession("c2", "Player2");
            bool joined = await manager.JoinRoomAsync(c2, room.RoomId);
            Assert.True(joined);

            var activeRooms = manager.GetActiveRooms();
            Assert.Single(activeRooms);

            var info = activeRooms[0];
            Assert.Equal("Player1", info.Player1Name);
            Assert.Equal("Player2", info.Player2Name);
            Assert.False(info.CanJoinAsPlayer); // Room is full, can only spectate
        }

        [Fact]
        public async Task RoomManager_Spectator_CanJoinAndLeaveWithoutDisruptingGame()
        {
            var manager = new RoomManager();
            var c1 = new ClientSession("c1", "Player1");
            Room room = await manager.CreateRoomAsync(c1, GameMode.PvP, AIDifficulty.Medium, 15);

            var spec1 = new ClientSession("spec1", "Viewer1");
            bool spectated = await manager.SpectateRoomAsync(spec1, room.RoomId);
            Assert.True(spectated);
            Assert.True(spec1.IsSpectator);
            Assert.Equal(1, room.SpectatorCount);

            var activeRooms = manager.GetActiveRooms();
            Assert.Equal(1, activeRooms[0].SpectatorCount);

            // Spectator leaves room
            await manager.LeaveRoomAsync(spec1);
            Assert.Equal(0, room.SpectatorCount);
            // Room is still intact for players
            Assert.NotNull(c1.CurrentRoom);
        }
    }
}
