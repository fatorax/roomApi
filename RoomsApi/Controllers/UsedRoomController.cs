using Microsoft.AspNetCore.Mvc;
using RoomsApi.Dtos;
using RoomsApi.Services;

namespace RoomsApi.Controllers
{
    [ApiController]
    [Route("api/rooms/reservation")]
    public class UsedRoomController : ControllerBase
    {
        private readonly IUsedRoomService _usedRoomService;

        public UsedRoomController(IUsedRoomService usedRoomService)
        {
            _usedRoomService = usedRoomService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUsedRoom(int id)
        {
            // Récupère la réservation
            var usedRoom = await _usedRoomService.GetUsedRoomsById(id);

            // Si la réservation n'existe pas, retourne un code 404 Not Found
            if (usedRoom == null) return NotFound();

            // Retourne la réservation
            return Ok(usedRoom);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetForUser(int userId)
        {
            // Retourne les réservations d'un utilisateur, de la plus proche à la plus lointaine
            return Ok(await _usedRoomService.GetForUser(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsedRoomRequest request)
        {
            // Vérifie que la date de début est bien avant la date de fin
            if (request.EndDate!.Value <= request.StartDate!.Value)
                return Problem(statusCode: 400, detail: "La date de fin doit être après la date de début.");

            // Vérifie que la salle existe bien
            if (!await _usedRoomService.RoomExists(request.RoomId!.Value))
                return Problem(statusCode: 404, detail: "Cette salle n'existe pas.");

            // Crée la réservation, sauf si la salle est déjà prise sur ce créneau
            var usedRoom = await _usedRoomService.CreateUsedRoom(request);
            if (usedRoom == null)
                return Problem(statusCode: 409, detail: "La salle est déjà réservée sur ce créneau.");

            // Retourne la réservation créée, un code 201 Created et son adresse
            return CreatedAtAction(nameof(GetUsedRoom), new { id = usedRoom.Id }, usedRoom);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Vérifie que la réservation existe bien
            if (await _usedRoomService.GetUsedRoomsById(id) == null) return NotFound();

            // Supprime la réservation
            await _usedRoomService.DeleteUsedRoom(id);

            // Retourne un code 204 No Content pour indiquer que la suppression a réussi
            return NoContent();
        }
    }
}