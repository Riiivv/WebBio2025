using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface ISeat
    {
        Task<List<Seat>> GetAllSeats();
        Task<Seat?> GetSeatById(int id);
        Task<Seat?> CreateSeat(Seat seat);
        Task<Seat?> UpdateSeat(Seat seat);
        Task<bool> DeleteSeatAsync(int id);

        Task CreateSeatsAsync(IEnumerable<Seat> seats);
    }
}
