using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Application.DTOs
{
    public class TicketDTOResponse
    {
        public int TicketId { get; set; }
        public decimal TicketPrice { get; set; }

        public int SeatId { get; set; }

        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }   // praktisk til frontend (valgfrit)
    }
}
