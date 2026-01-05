using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;

namespace WebBio2025.Application.Interfaces
{
    public interface IMoviesService
    {
        Task<IEnumerable<MoviesDTOResponse>> GetAllMovies();
        Task<MoviesDTOResponse?> GetMovieById(int id);
        Task<MoviesDTOResponse?> CreateMovie(MoviesDTORequest request);
        Task<MoviesDTOResponse?> UpdateMovie(MoviesDTORequest request);
        Task<bool> DeleteMovie(int id);
    }
}
