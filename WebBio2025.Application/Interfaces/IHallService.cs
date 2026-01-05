using WebBio2025.Application.DTOs;

namespace WebBio2025.Application.Interfaces
{
    public interface IHallService
    {
        Task<IEnumerable<HallDTOResponse>> GetAllHalls();
        Task<HallDTOResponse?> GetHallById(int id);
        Task<HallDTOResponse?> CreateHall(HallDTORequest request);
        Task<HallDTOResponse?> UpdateHall(HallDTORequest request);
        Task<bool> DeleteHall(int id);
    }
}