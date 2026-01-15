using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicket _ticketRepository;

        public TicketService(ITicket ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        private static TicketDTOResponse MapToResponse(Ticket t)
        {
            return new TicketDTOResponse
            {
                TicketId = t.TicketId,
                TicketPrice = t.TicketPrice,
                SeatId = t.SeatId,
                ShowTimeId = t.ShowtimeId
            };
        }

        public async Task<IEnumerable<TicketDTOResponse>> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTickets();
            return tickets.Select(MapToResponse);
        }

        public async Task<TicketDTOResponse?> GetTicketById(int id)
        {
            var ticket = await _ticketRepository.GetTicketById(id);
            if (ticket == null) return null;
            return MapToResponse(ticket);
        }

        public async Task<TicketDTOResponse?> CreateTicket(TicketDTORequest request)
        {
            var entity = new Ticket
            {
                TicketPrice = request.TicketPrice,
                SeatId = request.SeatId,
                ShowtimeId = request.ShowtimeId
            };

            var created = await _ticketRepository.CreateTicket(entity);
            if (created == null) return null;

            return MapToResponse(created);
        }

        public async Task<TicketDTOResponse?> UpdateTicket(TicketDTORequest request)
        {
            var entity = new Ticket
            {
                TicketId = request.TicketId,
                TicketPrice = request.TicketPrice,
                SeatId = request.SeatId,
                ShowtimeId = request.ShowtimeId
            };

            var updated = await _ticketRepository.UpdateTicket(entity);
            if (updated == null) return null;

            return MapToResponse(updated);
        }

        public async Task<bool> DeleteTicket(int id)
        {
            return await _ticketRepository.DeleteTicketAsync(id);
        }

        public async Task<IEnumerable<TicketDTOResponse>> GetTicketsBySeatId(int seatId)
        {
            var tickets = await _ticketRepository.GetTicketsBySeatId(seatId);
            return tickets.Select(MapToResponse);
        }

        public async Task<IEnumerable<TicketDTOResponse>> GetTicketsByShowtimeId(int showtimeId)
        {
            var tickets = await _ticketRepository.GetTicketsByShowtimeId(showtimeId);
            return tickets.Select(MapToResponse);
        }

        public async Task<IEnumerable<TicketDTOResponse>> GetTicketsByMovieId(int movieId)
        {
            return new List<TicketDTOResponse>();
        }
    }
}
