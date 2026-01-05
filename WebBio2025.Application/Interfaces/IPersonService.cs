using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;
using WebBio2025.Domain.entities;

namespace WebBio2025.Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTOResponse>> GetAllPersons();
        Task<PersonDTOResponse?> GetPersonById(int id);
        Task<PersonDTOResponse?> CreatePerson(PersonDTORequest person);
        Task<PersonDTOResponse?> UpdatePerson(PersonDTORequest person);
        Task<bool> DeletePerson(int id);

    }
}
