using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int TicketPrice { get; set; }
        public int SeatId { get; set; }
        public Movies Movies { get; set; } = new Movies();
    }
}
