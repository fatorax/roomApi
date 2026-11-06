using Microsoft.AspNetCore.Mvc;
using RoomsApi.Dtos;
using RoomsApi.Services;

namespace RoomsApi.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Récupère toutes les salles
            var rooms = await _roomService.GetAllRooms();

            // Retourne la liste des salles et un code 200 OK
            return Ok(rooms);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            // Récupère la salle
            var room = await _roomService.GetRoomById(id);

            // Si la salle n'existe pas, retourne un code 404 Not Found
            if (room == null) return NotFound();

            // Retourne la salle et un code 200 OK
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomRequest request)
        {
            // Crée la salle
            var room = await _roomService.CreateRoom(request);

            // Retourne la salle créée, un code 201 Created et son adresse dans l'en-tête Location
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Vérifie que la salle existe bien
            var room = await _roomService.GetRoomById(id);

            // Si la salle n'existe pas, retourne un code 404 Not Found
            if (room == null) return NotFound();

            // Supprime la salle
            await _roomService.DeleteRoom(id);

            // Retourne un code 204 No Content pour indiquer que la suppression a réussi
            return NoContent();
        }
    }
}