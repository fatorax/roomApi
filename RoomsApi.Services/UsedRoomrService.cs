using RoomsApi.DataAccess;
using RoomsApi.Dtos;
using RoomsApi.Entities;

namespace RoomsApi.Services
{
    public class UsedRoomService : IUsedRoomService
    {
        private readonly IUsedRoomRepository _repository;

        public UsedRoomService(IUsedRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UsedRoomResponce>> GetForUser(int userId)
        {
            return (await _repository.GetForUser(userId))
                .OrderBy(r => r.StartDate)
                .Select(ToResponse)
                .ToList();
        }

        public async Task<UsedRoomResponce?> GetUsedRoomsById(int id)
        {
            var usedRoom = await _repository.GetUsedRoomById(id);
            if (usedRoom == null) return null;

            return ToResponse(usedRoom);
        }

        public async Task<bool> RoomExists(int roomId)
        {
            return await _repository.RoomExists(roomId);
        }

        // null = la salle est déjà prise sur ce créneau
        public async Task<UsedRoomResponce?> CreateUsedRoom(UsedRoomRequest request)
        {
            var roomId = request.RoomId!.Value;
            var userId = request.UserId!.Value;
            var startDate = request.StartDate!.Value;
            var endDate = request.EndDate!.Value;

            if (await _repository.IsRoomTaken(roomId, startDate, endDate))
                return null;

            var usedRoom = new UsedRoom
            {
                RoomId = roomId,
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate
            };

            await _repository.AddUsedRoom(usedRoom);
            return ToResponse(usedRoom);
        }

        public async Task DeleteUsedRoom(int id)
        {
            await _repository.Delete(id);
        }

        private static UsedRoomResponce ToResponse(UsedRoom usedRoom)
        {
            return new UsedRoomResponce
            {
                Id = usedRoom.Id,
                RoomId = usedRoom.RoomId,
                UserId = usedRoom.UserId,
                StartDate = usedRoom.StartDate,
                EndDate = usedRoom.EndDate
            };
        }
    }
}