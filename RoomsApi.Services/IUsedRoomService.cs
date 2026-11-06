using RoomsApi.Dtos;

namespace RoomsApi.Services
{
    public interface IUsedRoomService
    {
        Task<List<UsedRoomResponce>> GetForUser(int userId);
        Task<UsedRoomResponce?> GetUsedRoomsById(int id);
        Task<bool> RoomExists(int roomId);
        Task<UsedRoomResponce?> CreateUsedRoom(UsedRoomRequest request);
        Task DeleteUsedRoom(int id);
    }
}