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
        public DatabaseContext _context;
        public HallRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task <List<Hall>> GetAllHalls()
        {
            var hall = await _context.Halls.ToListAsync();
            return hall;
        }
        public async Task <bool> DeleteHallAsync(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null) throw new KeyNotFoundException("Hall not found");
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
            return true;

        }
    }   
}
