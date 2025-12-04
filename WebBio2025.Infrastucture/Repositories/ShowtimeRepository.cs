using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class ShowtimeRepository : IShowtime
    {
        public DatabaseContext _context;
        public ShowtimeRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Showtime>> GetAllShowTimes()
        {
            var showtimes = await _context.Showtimes.ToListAsync();
            return showtimes;
        }
    }
}
