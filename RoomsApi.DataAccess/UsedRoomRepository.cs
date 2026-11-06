using Microsoft.EntityFrameworkCore;
using RoomsApi.Entities;

namespace RoomsApi.DataAccess
{
    public class UsedRoomRepository : IUsedRoomRepository
    {
        private readonly RoomDbContext _db;

        public UsedRoomRepository(RoomDbContext db)
        {
            _db = db;
        }

        public async Task<List<UsedRoom>> GetForUser(int userId)
        {
            return await _db.UsedRooms
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<UsedRoom?> GetUsedRoomById(int id)
        {
            return await _db.UsedRooms.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddUsedRoom(UsedRoom usedRoom)
        {
            _db.UsedRooms.Add(usedRoom);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var usedRoom = await _db.UsedRooms.FirstOrDefaultAsync(r => r.Id == id);
            if (usedRoom != null)
            {
                _db.UsedRooms.Remove(usedRoom);
                await _db.SaveChangesAsync();
            }
        }
        public async Task<bool> IsRoomTaken(int roomId, DateTime startDate, DateTime endDate)
        {
            return await _db.UsedRooms.AnyAsync(r =>
                r.RoomId == roomId &&
                r.StartDate < endDate &&
                startDate < r.EndDate);
        }

        public async Task<bool> RoomExists(int roomId)
        {
            return await _db.Rooms.AnyAsync(r => r.Id == roomId);
        }
    }
}