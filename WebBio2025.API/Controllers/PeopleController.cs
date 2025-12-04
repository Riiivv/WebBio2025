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
        // private readonly DatabaseContext _context;
        IPersonService _IpersonService;

        public PeopleController(IPersonService PersonRepo)
        {
            _IpersonService = PersonRepo;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTOResponse>>> GetallPersons()
        {
            return Ok(await _IpersonService.GetAllPersons());
        }


        // DELETE: api/People/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _IpersonService.DeletePerson(id);

            if (person == null)
            {
                return NotFound();
            }
            _IpersonService.DeletePerson(id);
            await _IpersonService.DeletePerson(id);

            return NoContent();
        }
    }
}
