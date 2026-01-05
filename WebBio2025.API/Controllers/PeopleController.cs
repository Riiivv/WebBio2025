using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;
using WebBio2025.Infrastucture;
using WebBio2025.Infrastucture.Repositories;

namespace WebBio2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTOResponse>>> GetAllPersons()
        {
            var persons = await _personService.GetAllPersons();
            return Ok(persons);
        }

        // GET: api/People/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonDTOResponse>> GetPersonById(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<PersonDTOResponse>> CreatePerson([FromBody] PersonDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _personService.CreatePerson(request);
            if (created == null)
                return BadRequest("Failed to create person.");

            return CreatedAtAction(nameof(GetPersonById), new { id = created.Id }, created);
        }

        // PUT: api/People/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PersonDTOResponse>> UpdatePerson(int id, [FromBody] PersonDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            if (id != request.Id)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _personService.UpdatePerson(request);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/People/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var deleted = await _personService.DeletePerson(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}