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

        public HallService(IHall hallRepository)
        {
            _hallRepository = hallRepository;
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
                HallId = request.HallId,
                HallNumber = request.HallNumber,
                Capacity = request.Capacity
            };

            var created = await _hallRepository.CreateHall(entity);
            if (created == null) return null;

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
    }
}