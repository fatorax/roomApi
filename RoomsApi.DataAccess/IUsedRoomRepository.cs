using RoomsApi.Entities;

namespace RoomsApi.DataAccess
{
    public interface IUsedRoomRepository
    {
        Task<List<UsedRoom>> GetForUser(int userId);
        Task<UsedRoom?> GetUsedRoomById(int id);
        Task AddUsedRoom(UsedRoom usedRoom);
        Task Delete(int id);
        Task<bool> IsRoomTaken(int roomId, DateTime startDate, DateTime endDate);
        Task<bool> RoomExists(int roomId);
    }
}