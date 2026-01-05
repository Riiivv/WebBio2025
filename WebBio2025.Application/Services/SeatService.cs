using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeat _seatRepository;

        public SeatService(ISeat seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<IEnumerable<SeatDTOResponse>> GetAllSeats()
        {
            var seats = await _seatRepository.GetAllSeats();

            return seats.Select(s => new SeatDTOResponse
            {
                SeatId = s.SeatId,
                RowNumber = s.RowNumber,
                SeatNumber = s.SeatNumber,
                HallId = s.HallId
            });
        }

        public async Task<SeatDTOResponse?> GetSeatById(int id)
        {
            var seat = await _seatRepository.GetSeatById(id);
            if (seat == null) return null;

            return new SeatDTOResponse
            {
                SeatId = seat.SeatId,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                HallId = seat.HallId
            };
        }

        public async Task<SeatDTOResponse?> CreateSeat(SeatDTORequest request)
        {
            var entity = new Seat
            {
                SeatId = request.SeatId,
                RowNumber = request.RowNumber,
                SeatNumber = request.SeatNumber,
                HallId = request.HallId
            };

            var created = await _seatRepository.CreateSeat(entity);
            if (created == null) return null;

            return new SeatDTOResponse
            {
                SeatId = created.SeatId,
                RowNumber = created.RowNumber,
                SeatNumber = created.SeatNumber,
                HallId = created.HallId
            };
        }

        public async Task<SeatDTOResponse?> UpdateSeat(SeatDTORequest request)
        {
            var entity = new Seat
            {
                SeatId = request.SeatId,
                RowNumber = request.RowNumber,
                SeatNumber = request.SeatNumber,
                HallId = request.HallId
            };

            var updated = await _seatRepository.UpdateSeat(entity);
            if (updated == null) return null;

            return new SeatDTOResponse
            {
                SeatId = updated.SeatId,
                RowNumber = updated.RowNumber,
                SeatNumber = updated.SeatNumber,
                HallId = updated.HallId
            };
        }

        public async Task<bool> DeleteSeat(int id)
        {
            return await _seatRepository.DeleteSeatAsync(id);
        }
    }
}