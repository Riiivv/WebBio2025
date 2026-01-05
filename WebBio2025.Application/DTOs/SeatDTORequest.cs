using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Application.DTOs
{
    public class SeatDTORequest
    {
        public int SeatId { get; set; }       // ved Create kan den være 0
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
    }
}
