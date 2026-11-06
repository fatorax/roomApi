using RoomsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace RoomsApi.DataAccess
{
    public class RoomDbContext : DbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<UsedRoom> UsedRooms { get; set; }
    }
}