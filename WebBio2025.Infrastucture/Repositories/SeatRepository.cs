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
        public DatabaseContext _context;
        public SeatRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Seat>> GetAllSeats()
        {
            var seat = await _context.Seats.ToListAsync();
            return seat;
        }
    }
}
