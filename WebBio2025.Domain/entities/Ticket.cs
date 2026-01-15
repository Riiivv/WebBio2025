using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public decimal TicketPrice { get; set; }

        public int SeatId { get; set; }
        public Seat? Seat { get; set; }

        public int ShowtimeId { get; set; }
        public Showtime? showtime { get; set; }
    }
}
