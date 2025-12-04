using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface IPersonRepositories
    {
        Task<List<Person>> GetAll();
        Task<bool> DeleteUserAsync(int id);
    }
}
