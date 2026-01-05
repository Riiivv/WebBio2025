using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Application.DTOs
{
    public class ShowtimeDTOResponse
    {
        public int ShowtimeId { get; set; }

        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }

        public int HallId { get; set; }
        public int? HallNumber { get; set; }

        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }
    }
}
