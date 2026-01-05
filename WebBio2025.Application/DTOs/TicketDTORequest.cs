using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Application.DTOs
{
    public class TicketDTORequest
    {
        public int TicketId { get; set; }     // ved Create kan den være 0
        public decimal TicketPrice { get; set; }

        public int SeatId { get; set; }
        public int MovieId { get; set; }
    }
}
