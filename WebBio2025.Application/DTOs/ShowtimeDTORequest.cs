using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Application.DTOs
{
    public class ShowTimeDTORequest
    {
        public int ShowtimeId { get; set; }   // ved Create kan den være 0
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }
    }
}
