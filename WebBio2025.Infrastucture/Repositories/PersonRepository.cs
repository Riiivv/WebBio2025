using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain;
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
            var t = await _context.Persons.ToListAsync();
            return t;
        }
    }
}
