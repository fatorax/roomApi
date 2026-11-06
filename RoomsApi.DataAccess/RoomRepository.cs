using Microsoft.EntityFrameworkCore;
using RoomsApi.Entities;

namespace RoomsApi.DataAccess
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDbContext _db;

        public RoomRepository(RoomDbContext db)
        {
            _db = db;
        }

        public async Task<List<Room>> GetAll()
        {
            return await _db.Rooms.ToListAsync();
        }

        public async Task<Room?> GetById(int id)
        {
            return await _db.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Add(Room room)
        {
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Room room)
        {
            _db.Rooms.Remove(room);
            await _db.SaveChangesAsync();
        }
    }
}