using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> CreatePerson(Person person);

        Task<Person?> GetPersonById(int id);
        Task<List<Person>> GetAll();
        Task<Person?> UpdatePerson(Person person);
        Task<bool> DeleteUserAsync(int id);
    }
}
