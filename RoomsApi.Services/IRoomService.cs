using RoomsApi.Dtos;

namespace RoomsApi.Services
{
    public interface IRoomService
    {
        Task<List<RoomResponse>> GetAllRooms();
        Task<RoomResponse?> GetRoomById(int id);
        Task<RoomResponse> CreateRoom(RoomRequest request);
        Task DeleteRoom(int id);
    }
}