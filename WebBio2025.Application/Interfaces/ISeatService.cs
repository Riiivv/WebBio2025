using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;

namespace WebBio2025.Application.Interfaces
{
    public interface ISeatService
    {
        Task<IEnumerable<SeatDTOResponse>> GetAllSeats();
        Task<SeatDTOResponse?> GetSeatById(int id);
        Task<SeatDTOResponse?> CreateSeat(SeatDTORequest request);
        Task<SeatDTOResponse?> UpdateSeat(SeatDTORequest request);
        Task<bool> DeleteSeat(int id);
    }
}