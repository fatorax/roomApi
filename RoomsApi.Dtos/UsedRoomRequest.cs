using System.ComponentModel.DataAnnotations;

namespace RoomsApi.Dtos
{
    public class UsedRoomRequest
    {
        [Required] public int? RoomId { get; set; }
        [Required] public int? UserId { get; set; }
        [Required] public DateTime? StartDate { get; set; }
        [Required] public DateTime? EndDate { get; set; }
    }
}