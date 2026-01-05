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
        private readonly DatabaseContext _context;

        public MoviesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Movies>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movies?> GetMovieById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<Movies?> CreateMovie(Movies movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return await _context.Movies.FindAsync(movie.MoviesId);
        }

        public async Task<Movies?> UpdateMovie(Movies movie)
        {
            var entity = await _context.Movies.FindAsync(movie.MoviesId);
            if (entity == null) return null;

            if (movie.Title != null) entity.Title = movie.Title;
            if (movie.Genre != null) entity.Genre = movie.Genre;
            // DateTime er value-type; hvis du vil “kun opdatere hvis sat”, så brug DateTime?
            entity.ReleaseDate = movie.ReleaseDate;
            if (movie.Director != null) entity.Director = movie.Director;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}