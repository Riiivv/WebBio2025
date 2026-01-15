using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class ShowtimeSeatService : IShowtimeSeatService
    {
        private readonly IShowtime _showtimeRepository;
        private readonly ISeat _seatRepository;
        private readonly ITicket _ticketRepository;
        private readonly ISeatHold _seatHoldRepository;

        public ShowtimeSeatService(
            IShowtime showtimeRepository,
            ISeat seatRepository,
            ITicket ticketRepository,
            ISeatHold seatHoldRepository)
        {
            _showtimeRepository = showtimeRepository;
            _seatRepository = seatRepository;
            _ticketRepository = ticketRepository;
            _seatHoldRepository = seatHoldRepository;
        }

        public async Task<IEnumerable<ShowTimeSeatDTOResponse>> GetSeatMap(int showtimeId, string? holdToken)
        {
            Console.WriteLine($"GetSeatMap called: showtimeId={showtimeId}, holdToken={holdToken}");

            var now = DateTime.UtcNow;

            var showtime = await _showtimeRepository.GetShowtimeById(showtimeId);
            if (showtime == null) throw new KeyNotFoundException($"Showtime not found. Id={showtimeId}");


            var seats = (await _seatRepository.GetAllSeats())
                .Where(s => s.HallId == showtime.HallId)
                .ToList();

            var soldSeatIds = (await _ticketRepository.GetAllTickets())
                .Where(t => t.ShowtimeId == showtimeId)
                .Select(t => t.SeatId)
                .ToHashSet();

            var activeHolds = await _seatHoldRepository.GetActiveHoldsByShowtime(showtimeId, now);

            return seats.Select(s =>
            {
                if (soldSeatIds.Contains(s.SeatId))
                    return new ShowTimeSeatDTOResponse
                    {
                        SeatId = s.SeatId,
                        RowNumber = s.RowNumber,
                        SeatNumber = s.SeatNumber,
                        HallId = s.HallId,
                        SeatType = SeatType.Standard,
                        Status = SeatStatus.Sold
                    };

                var hold = activeHolds.FirstOrDefault(h => h.SeatId == s.SeatId);
                if (hold != null)
                {
                    var status = holdToken == hold.HoldToken
                        ? SeatStatus.SelectedByYou
                        : SeatStatus.HeldByOther;

                    return new ShowTimeSeatDTOResponse
                    {
                        SeatId = s.SeatId,
                        RowNumber = s.RowNumber,
                        SeatNumber = s.SeatNumber,
                        HallId = s.HallId,
                        SeatType = SeatType.Standard,
                        Status = status
                    };
                }

                return new ShowTimeSeatDTOResponse
                {
                    SeatId = s.SeatId,
                    RowNumber = s.RowNumber,
                    SeatNumber = s.SeatNumber,
                    HallId = s.HallId,
                    SeatType = SeatType.Standard,
                    Status = SeatStatus.Available
                };
            });
        }

        public async Task<ShowTimeSeatHoldDTOResponse> CreateOrRefreshHolds(int showtimeId, ShowTimeSeatDTORequest request)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(2);

            await _seatHoldRepository.DeleteExpiredHolds(showtimeId, now);

            var sold = (await _ticketRepository.GetAllTickets())
                .Where(t => t.ShowtimeId == showtimeId && request.SeatIds.Contains(t.SeatId))
                .Select(t => t.SeatId)
                .ToList();

            if (sold.Any()) throw new Exception("Seat already sold");

            var active = await _seatHoldRepository.GetActiveHoldsForSeats(showtimeId, request.SeatIds, now);
            if (active.Any(h => h.HoldToken != request.HoldToken))
                throw new Exception("Seat held by another user");

            await _seatHoldRepository.UpsertHolds(showtimeId, request.HoldToken, request.SeatIds, expires);

            return new ShowTimeSeatHoldDTOResponse
            {
                HoldToken = request.HoldToken,
                ExpiresAtUtc = expires
            };
        }

        public async Task ReleaseHolds(int showtimeId, string holdToken)
        {
            await _seatHoldRepository.DeleteHoldsByToken(showtimeId, holdToken);
        }
    }
}
