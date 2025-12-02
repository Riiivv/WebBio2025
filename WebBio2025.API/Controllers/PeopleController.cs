using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBio2025.Domain;
using WebBio2025.Domain.interfaces;
using WebBio2025.Infrastucture;

namespace WebBio2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
       // private readonly DatabaseContext _context;
        IPersonRepositories _context;

        public PeopleController(DatabaseContext context, IPersonRepositories c)
        {
            _context = c;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.GetAll();
        }
    }
}
