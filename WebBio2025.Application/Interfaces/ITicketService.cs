using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;

namespace WebBio2025.Application.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDTOResponse>> GetAllTickets();
        Task<TicketDTOResponse?> GetTicketById(int id);
        Task<TicketDTOResponse?> CreateTicket(TicketDTORequest request);
        Task<TicketDTOResponse?> UpdateTicket(TicketDTORequest request);
        Task<bool> DeleteTicket(int id);

        Task<IEnumerable<TicketDTOResponse>> GetTicketsByMovieId(int movieId);
        Task<IEnumerable<TicketDTOResponse>> GetTicketsBySeatId(int seatId);
    }
}