using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface IShowtime
    {
        Task<List<Showtime>> GetAllShowTimes();
        Task<Showtime?> GetShowtimeById(int id);
        Task<Showtime?> CreateShowtime(Showtime showtime);
        Task<Showtime?> UpdateShowtime(Showtime showtime);
        Task<bool> DeleteShowtimeAsync(int id);

        // Ekstra (biograf-relevant): hent showtimes for en film
        Task<List<Showtime>> GetShowtimesByMovieId(int movieId);

        // Ekstra (biograf-relevant): hent showtimes for en sal
        Task<List<Showtime>> GetShowtimesByHallId(int hallId);
    }
}
