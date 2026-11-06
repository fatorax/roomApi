using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoomsApi.Dtos
{
    public class RoomRequest
    {
        [Required] public string Name { get; set; } = "";
        [Required] public int Capacity { get; set; }
    }
}
