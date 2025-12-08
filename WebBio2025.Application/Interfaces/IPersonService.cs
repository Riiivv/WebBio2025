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

        Task<ActionResult> UpdatePerson(int id, PersonDTORequest person);
        Task<IActionResult> DeletePerson(int id);

    }
}
