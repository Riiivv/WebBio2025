using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface IMovies
    {
        Task<List<Movies>> GetAllMovies();
        Task<Movies?> GetMovieById(int id);
        Task<Movies?> CreateMovie(Movies movie);
        Task<Movies?> UpdateMovie(Movies movie);
        Task<bool> DeleteMovieAsync(int id);
    }
}
