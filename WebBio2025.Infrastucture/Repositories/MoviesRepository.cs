using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class MoviesRepository : IMovies
    {
        public DatabaseContext _context;

        public MoviesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task <List<Movies>> GetAllMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

    }
}
