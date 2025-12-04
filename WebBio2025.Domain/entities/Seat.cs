using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; } = new Hall();
    }
}
