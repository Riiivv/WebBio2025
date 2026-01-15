using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMovies _moviesRepository;

        public MoviesService(IMovies moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<MoviesDTOResponse>> GetAllMovies()
        {
            var movies = await _moviesRepository.GetAllMovies();

            return movies.Select(m => new MoviesDTOResponse
            {
                MoviesId = m.MoviesId,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.ReleaseDate,
                Director = m.Director
            });
        }

        public async Task<MoviesDTOResponse?> GetMovieById(int id)
        {
            var movie = await _moviesRepository.GetMovieById(id);
            if (movie == null) return null;

            return new MoviesDTOResponse
            {
                MoviesId = movie.MoviesId,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate,
                Director = movie.Director
            };
        }

        public async Task<MoviesDTOResponse?> CreateMovie(MoviesDTORequest request)
        {
            var entity = new Movies
            {
                Title = request.Title,
                Genre = request.Genre,
                ReleaseDate = request.ReleaseDate,
                Director = request.Director
            };

            var created = await _moviesRepository.CreateMovie(entity);
            if (created == null) return null;

            return new MoviesDTOResponse
            {
                MoviesId = created.MoviesId,
                Title = created.Title,
                Genre = created.Genre,
                ReleaseDate = created.ReleaseDate,
                Director = created.Director
            };
        }

        public async Task<MoviesDTOResponse?> UpdateMovie(MoviesDTORequest request)
        {
            var entity = new Movies
            {
                MoviesId = request.MoviesId,
                Title = request.Title,
                Genre = request.Genre,
                ReleaseDate = request.ReleaseDate,
                Director = request.Director
            };

            var updated = await _moviesRepository.UpdateMovie(entity);
            if (updated == null) return null;

            return new MoviesDTOResponse
            {
                MoviesId = updated.MoviesId,
                Title = updated.Title,
                Genre = updated.Genre,
                ReleaseDate = updated.ReleaseDate,
                Director = updated.Director
            };
        }

        public async Task<bool> DeleteMovie(int id)
        {
            return await _moviesRepository.DeleteMovieAsync(id);
        }
    }
}
