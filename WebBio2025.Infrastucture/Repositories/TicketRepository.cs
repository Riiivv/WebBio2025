using Microsoft.EntityFrameworkCore;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class TicketRepository : ITicket
    {
        private readonly DatabaseContext _context;

        public TicketRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Tickets
                .Include(t => t.Seat)
                .ToListAsync();
        }

        public async Task<Ticket?> GetTicketById(int id)
        {
            return await _context.Tickets
                .Include(t => t.Seat)
                .FirstOrDefaultAsync(t => t.TicketId == id);
        }

        public async Task<Ticket?> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return await GetTicketById(ticket.TicketId);
        }

        public async Task<Ticket?> UpdateTicket(Ticket ticket)
        {
            var entity = await _context.Tickets.FindAsync(ticket.TicketId);
            if (entity == null) return null;

            entity.TicketPrice = ticket.TicketPrice;
            entity.SeatId = ticket.SeatId;
            entity.ShowtimeId = ticket.ShowtimeId; // hvis du bruger showtime

            await _context.SaveChangesAsync();
            return await GetTicketById(entity.TicketId);
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var entity = await _context.Tickets.FindAsync(id);
            if (entity == null) return false;

            _context.Tickets.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Ticket>> GetTicketsBySeatId(int seatId)
        {
            return await _context.Tickets
                .Include(t => t.Seat)
                .Where(t => t.SeatId == seatId)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByShowtimeId(int showtimeId)
        {
            return await _context.Tickets
                .Where(t => t.ShowtimeId == showtimeId)
                .ToListAsync();
        }

        // Hvis dit interface stadig kræver denne:
        public async Task<List<Ticket>> GetTicketsByMovieId(int movieId)
        {
            return new List<Ticket>(); // Movie findes ikke længere på Ticket
        }
    }
}
