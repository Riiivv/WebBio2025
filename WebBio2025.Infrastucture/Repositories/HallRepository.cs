using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class HallRepository : IHall
    {
        private readonly DatabaseContext _context;

        public HallRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Hall>> GetAllHalls()
        {
            return await _context.Halls.ToListAsync();
        }

        public async Task<Hall?> GetHallById(int id)
        {
            return await _context.Halls.FindAsync(id);
        }

        public async Task<Hall?> CreateHall(Hall hall)
        {
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();
            return await _context.Halls.FindAsync(hall.HallId);
        }

        public async Task<Hall?> UpdateHall(Hall hall)
        {
            var entity = await _context.Halls.FindAsync(hall.HallId);
            if (entity == null) return null;

            entity.HallNumber = hall.HallNumber;
            entity.Capacity = hall.Capacity;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteHallAsync(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null) return false;

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}