using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class PersonService : IPersonService
    {
        public IPersonRepository _personRepository;
        public PersonService(IPersonRepository context) { context = _personRepository; }

        public async Task<IEnumerable<PersonDTOResponse>> GetAllPersons()
        {
            return await _personRepository.GetAll().ContinueWith(p => p.Result.Select(r => new PersonDTOResponse
            {
                Id = r.Id,
                Name = r.Name,
                Mail = r.Mail

            }));
        }

        public async Task<IActionResult> DeletePerson(int id)
        {

            var PersonDelete = await _personRepository.DeleteUserAsync(id);
            if (PersonDelete == null)
            {
                return new NotFoundResult();
            }
            await _personRepository.DeleteUserAsync(id);
            return new OkResult();
        }
    }
}
