using System;
using System.Collections.Generic;
using System.Text;

namespace RoomsApi.Dtos
{
    public class UsedRoomResponce
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
