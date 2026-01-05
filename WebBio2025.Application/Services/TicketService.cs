using System;
using System.Collections.Generic;
using System.Text;
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
                MovieId = t.MovieId,
                MovieTitle = t.Movie?.Title
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
                TicketId = request.TicketId,
                TicketPrice = request.TicketPrice,
                SeatId = request.SeatId,
                MovieId = request.MovieId
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
                MovieId = request.MovieId
            };

            var updated = await _ticketRepository.UpdateTicket(entity);
            if (updated == null) return null;

            return MapToResponse(updated);
        }

        public async Task<bool> DeleteTicket(int id)
        {
            return await _ticketRepository.DeleteTicketAsync(id);
        }

        public async Task<IEnumerable<TicketDTOResponse>> GetTicketsByMovieId(int movieId)
        {
            var tickets = await _ticketRepository.GetTicketsByMovieId(movieId);
            return tickets.Select(MapToResponse);
        }

        public async Task<IEnumerable<TicketDTOResponse>> GetTicketsBySeatId(int seatId)
        {
            var tickets = await _ticketRepository.GetTicketsBySeatId(seatId);
            return tickets.Select(MapToResponse);
        }
    }
}