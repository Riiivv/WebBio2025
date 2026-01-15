using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class SeatHoldRepository : ISeatHold
    {
        private readonly DatabaseContext _context;

        public SeatHoldRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<SeatHold>> GetActiveHoldsByShowtime(int showtimeId, DateTime nowUtc)
        {
            return await _context.SeatHolds
                .Where(h => h.ShowtimeId == showtimeId && h.ExpiresAtUtc > nowUtc)
                .ToListAsync();
        }

        public async Task<List<SeatHold>> GetHoldsByToken(int showtimeId, string holdToken)
        {
            return await _context.SeatHolds
                .Where(h => h.ShowtimeId == showtimeId && h.HoldToken == holdToken)
                .ToListAsync();
        }

        public async Task<int> DeleteExpiredHolds(int showtimeId, DateTime nowUtc)
        {
            var expired = await _context.SeatHolds
                .Where(h => h.ShowtimeId == showtimeId && h.ExpiresAtUtc <= nowUtc)
                .ToListAsync();

            if (expired.Count == 0) return 0;

            _context.SeatHolds.RemoveRange(expired);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteHoldsByToken(int showtimeId, string holdToken)
        {
            var holds = await _context.SeatHolds
                .Where(h => h.ShowtimeId == showtimeId && h.HoldToken == holdToken)
                .ToListAsync();

            if (holds.Count == 0) return 0;

            _context.SeatHolds.RemoveRange(holds);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<SeatHold>> GetActiveHoldsForSeats(int showtimeId, IEnumerable<int> seatIds, DateTime nowUtc)
        {
            var ids = seatIds.Distinct().ToList();
            if (ids.Count == 0) return new List<SeatHold>();

            return await _context.SeatHolds
                .Where(h => h.ShowtimeId == showtimeId
                         && ids.Contains(h.SeatId)
                         && h.ExpiresAtUtc > nowUtc)
                .ToListAsync();
        }

        public async Task UpsertHolds(int showtimeId, string holdToken, IEnumerable<int> seatIds, DateTime expiresAtUtc)
        {
            var ids = seatIds.Distinct().ToList();
            if (ids.Count == 0) return;

            // Forudsætter at SeatHold har PK: (ShowtimeId, SeatId)
            // Vi gør det simpelt: Find eksisterende for showtime+seat og opdater/indsæt.
            var existing = await _context.SeatHolds
                .Where(h => h.ShowtimeId == showtimeId && ids.Contains(h.SeatId))
                .ToListAsync();

            var existingMap = existing.ToDictionary(x => x.SeatId, x => x);

            foreach (var seatId in ids)
            {
                if (existingMap.TryGetValue(seatId, out var hold))
                {
                    hold.HoldToken = holdToken;
                    hold.ExpiresAtUtc = expiresAtUtc;
                }
                else
                {
                    _context.SeatHolds.Add(new SeatHold
                    {
                        ShowtimeId = showtimeId,
                        SeatId = seatId,
                        HoldToken = holdToken,
                        ExpiresAtUtc = expiresAtUtc,
                        CreatedAtUtc = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}