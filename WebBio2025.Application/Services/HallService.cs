using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class HallService : IHallService
    {
        private readonly IHall _hallRepository;
        private readonly ISeat _seatRepository;

        public HallService(IHall hallRepository, ISeat seatRepository)
        {
            _hallRepository = hallRepository;
            _seatRepository = seatRepository;
        }

        public async Task<IEnumerable<HallDTOResponse>> GetAllHalls()
        {
            var halls = await _hallRepository.GetAllHalls();

            return halls.Select(h => new HallDTOResponse
            {
                HallId = h.HallId,
                HallNumber = h.HallNumber,
                Capacity = h.Capacity
            });
        }

        public async Task<HallDTOResponse?> GetHallById(int id)
        {
            var hall = await _hallRepository.GetHallById(id);
            if (hall == null) return null;

            return new HallDTOResponse
            {
                HallId = hall.HallId,
                HallNumber = hall.HallNumber,
                Capacity = hall.Capacity
            };
        }

        public async Task<HallDTOResponse?> CreateHall(HallDTORequest request)
        {
            var entity = new Hall
            {
                HallNumber = request.HallNumber,
                Capacity = request.Capacity
            };

            var created = await _hallRepository.CreateHall(entity);
            if (created == null) return null;

            // AUTO-GENERER SEATS BASERET PÅ CAPACITY
            // simpel layout: 10 sæder pr række, rækker = ceil(capacity/10)
            // wheelchair + companion: vi sætter 2% wheelchair, og companion lige ved siden af (hvis muligt)
            var seats = GenerateSeats(created.HallId, created.Capacity);
            await _seatRepository.CreateSeatsAsync(seats);

            return new HallDTOResponse
            {
                HallId = created.HallId,
                HallNumber = created.HallNumber,
                Capacity = created.Capacity
            };
        }

        public async Task<HallDTOResponse?> UpdateHall(HallDTORequest request)
        {
            var entity = new Hall
            {
                HallId = request.HallId,
                HallNumber = request.HallNumber,
                Capacity = request.Capacity
            };

            var updated = await _hallRepository.UpdateHall(entity);
            if (updated == null) return null;

            return new HallDTOResponse
            {
                HallId = updated.HallId,
                HallNumber = updated.HallNumber,
                Capacity = updated.Capacity
            };
        }

        public async Task<bool> DeleteHall(int id)
        {
            return await _hallRepository.DeleteHallAsync(id);
        }

        private static List<Seat> GenerateSeats(int hallId, int capacity)
        {
            const int seatsPerRow = 10;

            var list = new List<Seat>(capacity);


            var wheelchairPlaced = 0;

            for (int i = 1; i <= capacity; i++)
            {
                var row = (int)Math.Ceiling(i / (double)seatsPerRow);
                var seatNo = ((i - 1) % seatsPerRow) + 1;

                var seat = new Seat
                {
                    HallId = hallId,
                    RowNumber = row,
                    SeatNumber = seatNo,
                    SeatType = SeatType.Standard
                };


                if (seatNo == 1 && wheelchairPlaced < 2)
                {
                    seat.SeatType = SeatType.Wheelchair;
                    wheelchairPlaced++;
                }


                if (seatNo == 2)
                {
                    var prev = list.LastOrDefault(x => x.RowNumber == row && x.SeatNumber == 1);
                    if (prev != null && prev.SeatType == SeatType.Wheelchair)
                    {
                        seat.SeatType = SeatType.Companion;
                    }
                }

                list.Add(seat);
            }

            return list;
        }
    }
}