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
        private readonly DatabaseContext _context;

        public ShowtimeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Showtime>> GetAllShowTimes()
        {
            // Include giver os Movie/Hall data (så DTO kan få titel/nummer)
            return await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .ToListAsync();
        }

        public async Task<Showtime?> GetShowtimeById(int id)
        {
            return await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.ShowtimeId == id);
        }

        public async Task<Showtime?> CreateShowtime(Showtime showtime)
        {
            _context.Showtimes.Add(showtime);
            await _context.SaveChangesAsync();

            return await GetShowtimeById(showtime.ShowtimeId);
        }

        public async Task<Showtime?> UpdateShowtime(Showtime showtime)
        {
            var entity = await _context.Showtimes.FindAsync(showtime.ShowtimeId);
            if (entity == null) return null;

            entity.MovieId = showtime.MovieId;
            entity.HallId = showtime.HallId;
            entity.StartTime = showtime.StartTime;
            entity.Price = showtime.Price;

            await _context.SaveChangesAsync();
            return await GetShowtimeById(entity.ShowtimeId);
        }

        public async Task<bool> DeleteShowtimeAsync(int id)
        {
            var entity = await _context.Showtimes.FindAsync(id);
            if (entity == null) return false;

            _context.Showtimes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Showtime>> GetShowtimesByMovieId(int movieId)
        {
            return await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Where(s => s.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<List<Showtime>> GetShowtimesByHallId(int hallId)
        {
            return await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Where(s => s.HallId == hallId)
                .ToListAsync();
        }
    }
}