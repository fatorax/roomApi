using RoomsApi.Entities;

namespace RoomsApi.DataAccess
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAll();
        Task<Room?> GetById(int id);
        Task Add(Room room);
        Task Delete(Room room);
    }
}