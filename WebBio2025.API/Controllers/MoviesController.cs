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
    public class MoviesController : ControllerBase
    {
        private readonly IMovies _MovieRepo;

        public MoviesController(IMovies movie)
        {
            _MovieRepo = movie;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            return await _MovieRepo.GetAllMovies();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                var movie = await _MovieRepo.DeleteMovieAsync(id);
                if (movie)
                    return NoContent(); // 204 (deleted)
                else
                    return NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Movie not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "an error has occured: " + ex.Message);
            }
        }

    }
}
