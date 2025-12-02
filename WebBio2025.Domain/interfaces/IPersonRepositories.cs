using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.interfaces
{
    public interface IPersonRepositories
    {
        Task<List<Person>> GetAll();
    }
}
