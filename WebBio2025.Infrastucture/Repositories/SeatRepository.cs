using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class SeatRepository : ISeat
    {
        private readonly DatabaseContext _context;

        public SeatRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Seat>> GetAllSeats()
        {
            // Hvis du vil hente Hall med, brug Include:
            // return await _context.Seats.Include(s => s.Hall).ToListAsync();
            return await _context.Seats.ToListAsync();
        }

        public async Task<Seat?> GetSeatById(int id)
        {
            return await _context.Seats.FindAsync(id);
        }

        public async Task<Seat?> CreateSeat(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
            return await _context.Seats.FindAsync(seat.SeatId);
        }

        public async Task<Seat?> UpdateSeat(Seat seat)
        {
            var entity = await _context.Seats.FindAsync(seat.SeatId);
            if (entity == null) return null;

            entity.RowNumber = seat.RowNumber;
            entity.SeatNumber = seat.SeatNumber;
            entity.HallId = seat.HallId;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteSeatAsync(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null) return false;

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}