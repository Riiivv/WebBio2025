using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;

namespace WebBio2025.Application.Interfaces
{
    public interface IShowtimeSeatService
    {
        Task<IEnumerable<ShowTimeSeatDTOResponse>> GetSeatMap(int showtimeId, string? holdToken);

        Task<ShowTimeSeatHoldDTOResponse> CreateOrRefreshHolds(int showtimeId, ShowTimeSeatDTORequest request);

        Task ReleaseHolds(int showtimeId, string holdToken);
    }
}
