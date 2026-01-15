using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonDTOResponse>> GetAllPersons()
        {
            var persons = await _personRepository.GetAll();

            return persons.Select(p => new PersonDTOResponse
            {
                Id = p.Id,
                Name = p.Name,
                Mail = p.Mail
            });
        }

        public async Task<PersonDTOResponse?> GetPersonById(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null) return null;

            return new PersonDTOResponse
            {
                Id = person.Id,
                Name = person.Name,
                Mail = person.Mail
            };
        }

        public async Task<PersonDTOResponse?> CreatePerson(PersonDTORequest request)
        {
            var entity = new Person
            {
                Name = request.Name,
                Lastname = request.Lastname,
                Mail = request.Mail
            };

            var created = await _personRepository.CreatePerson(entity);
            if (created == null) return null;

            return new PersonDTOResponse
            {
                Id = created.Id,
                Name = created.Name,
                Mail = created.Mail
            };
        }

        public async Task<PersonDTOResponse?> UpdatePerson(PersonDTORequest request)
        {
            var entity = new Person
            {
                Id = request.Id,
                Name = request.Name,
                Lastname = request.Lastname,
                Mail = request.Mail
            };

            var updated = await _personRepository.UpdatePerson(entity);
            if (updated == null) return null;

            return new PersonDTOResponse
            {
                Id = updated.Id,
                Name = updated.Name,
                Mail = updated.Mail
            };
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                return await _personRepository.DeleteUserAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}