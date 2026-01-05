using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;

namespace WebBio2025.Application.Interfaces
{
    public interface IShowtimeService
    {
        Task<IEnumerable<ShowtimeDTOResponse>> GetAllShowtimes();
        Task<ShowtimeDTOResponse?> GetShowtimeById(int id);
        Task<ShowtimeDTOResponse?> CreateShowtime(ShowTimeDTORequest request);
        Task<ShowtimeDTOResponse?> UpdateShowtime(ShowTimeDTORequest request);
        Task<bool> DeleteShowtime(int id);

        Task<IEnumerable<ShowtimeDTOResponse>> GetShowtimesByMovieId(int movieId);
        Task<IEnumerable<ShowtimeDTOResponse>> GetShowtimesByHallId(int hallId);
    }
}