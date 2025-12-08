using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public DatabaseContext _context;
        public PersonRepository(DatabaseContext context) {
            _context = context;
        }

        public async Task<Person?> CreatePerson(Person person)   
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return await _context.Persons.FindAsync(person.Id);
        }

        public async Task<List<Person>> GetAll()
        {
            var person = await _context.Persons.ToListAsync();
            return person;
        }

        public async Task<Person?> GetPersonById(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<Person?> UpdatePerson(Person person)
        {
            var UpdatePerson = await _context.Persons.FindAsync(person.Id);
            if (person.Name != null) UpdatePerson.Name = person.Name;
            if (person.Lastname != null) UpdatePerson.Lastname = person.Lastname;
            if (person.Mail != null) UpdatePerson.Mail = person.Mail;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new(e.Message + "update throw");
            }
            return UpdatePerson;

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Persons.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            _context.Persons.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
