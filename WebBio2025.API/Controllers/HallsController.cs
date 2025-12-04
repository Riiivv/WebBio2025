using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;
using WebBio2025.Infrastucture;

namespace WebBio2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController : ControllerBase
    {
        private readonly IHall _hallRepo;

        public HallsController(IHall hallrepo)
        {
            _hallRepo = hallrepo;
        }

        // GET: api/Halls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hall>>> GetHalls()
        {
            return await _hallRepo.GetAllHalls();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            try
            {
                var hall = await _hallRepo.DeleteHallAsync(id);
                if (hall)
                    return NoContent(); // 204 (deleted)
                else
                    return NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Hall not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "an error has occured: " + ex.Message);
            }

        }

    }
}
