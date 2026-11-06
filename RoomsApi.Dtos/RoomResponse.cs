using System;
using System.Collections.Generic;
using System.Text;

namespace RoomsApi.Dtos
{
    public class RoomResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Capacity { get; set; }
    }
}
