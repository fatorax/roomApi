using RoomsApi.DataAccess;
using RoomsApi.Dtos;
using RoomsApi.Entities;

namespace RoomsApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoomResponse>> GetAllRooms()
        {
            return (await _repository.GetAll())
                .OrderBy(r => r.Name)
                .Select(ToResponse)
                .ToList();
        }

        public async Task<RoomResponse?> GetRoomById(int id)
        {
            var room = await _repository.GetById(id);
            if (room == null) return null;

            return ToResponse(room);
        }

        public async Task<RoomResponse> CreateRoom(RoomRequest request)
        {
            var room = new Room
            {
                Name = request.Name,
                Capacity = request.Capacity
            };

            await _repository.Add(room);
            return ToResponse(room);
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _repository.GetById(id);
            if (room != null)
                await _repository.Delete(room);
        }

        private static RoomResponse ToResponse(Room room)
        {
            return new RoomResponse
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity
            };
        }
    }
}