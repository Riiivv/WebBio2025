using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtime _showtimeRepository;

        public ShowtimeService(IShowtime showtimeRepository)
        {
            _showtimeRepository = showtimeRepository;
        }

        private static ShowtimeDTOResponse MapToResponse(Showtime s)
        {
            return new ShowtimeDTOResponse
            {
                ShowtimeId = s.ShowtimeId,
                MovieId = s.MovieId,
                MovieTitle = s.Movie?.Title,
                HallId = s.HallId,
                HallNumber = s.Hall?.HallNumber,
                StartTime = s.StartTime,
                Price = s.Price
            };
        }

        public async Task<IEnumerable<ShowtimeDTOResponse>> GetAllShowtimes()
        {
            var showtimes = await _showtimeRepository.GetAllShowTimes();
            return showtimes.Select(MapToResponse);
        }

        public async Task<ShowtimeDTOResponse?> GetShowtimeById(int id)
        {
            var showtime = await _showtimeRepository.GetShowtimeById(id);
            if (showtime == null) return null;
            return MapToResponse(showtime);
        }

        public async Task<ShowtimeDTOResponse?> CreateShowtime(ShowTimeDTORequest request)
        {
            var entity = new Showtime
            {
                MovieId = request.MovieId,
                HallId = request.HallId,
                StartTime = request.StartTime,
                Price = request.Price
            };

            var created = await _showtimeRepository.CreateShowtime(entity);
            if (created == null) return null;

            return MapToResponse(created);
        }

        public async Task<ShowtimeDTOResponse?> UpdateShowtime(ShowTimeDTORequest request)
        {
            var entity = new Showtime
            {
                ShowtimeId = request.ShowtimeId,
                MovieId = request.MovieId,
                HallId = request.HallId,
                StartTime = request.StartTime,
                Price = request.Price
            };

            var updated = await _showtimeRepository.UpdateShowtime(entity);
            if (updated == null) return null;

            return MapToResponse(updated);
        }

        public async Task<bool> DeleteShowtime(int id)
        {
            return await _showtimeRepository.DeleteShowtimeAsync(id);
        }

        public async Task<IEnumerable<ShowtimeDTOResponse>> GetShowtimesByMovieId(int movieId)
        {
            var showtimes = await _showtimeRepository.GetShowtimesByMovieId(movieId);
            return showtimes.Select(MapToResponse);
        }

        public async Task<IEnumerable<ShowtimeDTOResponse>> GetShowtimesByHallId(int hallId)
        {
            var showtimes = await _showtimeRepository.GetShowtimesByHallId(hallId);
            return showtimes.Select(MapToResponse);
        }
    }
}