using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface ITicket
    {
        Task<List<Ticket>> GetAllTickets();
        Task<Ticket?> GetTicketById(int id);
        Task<Ticket?> CreateTicket(Ticket ticket);
        Task<Ticket?> UpdateTicket(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);

        // Valgfrit men nyttigt:
        Task<List<Ticket>> GetTicketsByShowtimeId(int showtimeId);
        Task<List<Ticket>> GetTicketsByMovieId(int movieId);
        Task<List<Ticket>> GetTicketsBySeatId(int seatId);
    }
}
