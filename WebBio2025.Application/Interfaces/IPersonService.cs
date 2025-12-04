using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;

namespace WebBio2025.Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTOResponse>> GetAllPersons();
        Task<IActionResult> DeletePerson(int id);
    }
}
