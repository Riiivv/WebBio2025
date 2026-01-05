using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.entities
{
    public class Showtime
    {
        public int ShowtimeId { get; set; }

        public int MovieId { get; set; }
        public Movies Movie { get; set; }

        public int HallId { get; set; }
        public Hall Hall { get; set; }

        public DateTime StartTime { get; set; }

        // Optional: pris pr. billet
        public decimal Price { get; set; } = 150;

        // Navigation
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
