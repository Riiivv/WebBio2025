using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class PersonRepository : IPersonRepositories
    {
        public DatabaseContext _context;
        public PersonRepository(DatabaseContext context) {
            _context = context;
        }
        public async Task<List<Person>> GetAll()
        {
            var person = await _context.Persons.ToListAsync();
            return person;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Persons.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            _context.Persons.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
