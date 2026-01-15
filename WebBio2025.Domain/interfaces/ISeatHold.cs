using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface ISeatHold
    {
        Task<List<SeatHold>> GetActiveHoldsByShowtime(int showtimeId, DateTime nowUtc);

        Task<List<SeatHold>> GetActiveHoldsForSeats(int showtimeId, IEnumerable<int> seatIds, DateTime nowUtc);

        Task<int> DeleteExpiredHolds(int showtimeId, DateTime nowUtc);

        Task<int> DeleteHoldsByToken(int showtimeId, string holdToken);

        Task UpsertHolds(int showtimeId, string holdToken, IEnumerable<int> seatIds, DateTime expiresAtUtc);
    }
}
