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

namespace WebBio2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesDTOResponse>>> GetAllMovies()
        {
            var movies = await _moviesService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MoviesDTOResponse>> GetMovieById(int id)
        {
            var movie = await _moviesService.GetMovieById(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MoviesDTOResponse>> CreateMovie([FromBody] MoviesDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _moviesService.CreateMovie(request);
            if (created == null)
                return BadRequest("Failed to create movie.");

            return CreatedAtAction(nameof(GetMovieById), new { id = created.MoviesId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MoviesDTOResponse>> UpdateMovie(int id, [FromBody] MoviesDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            if (id != request.MoviesId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _moviesService.UpdateMovie(request);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deleted = await _moviesService.DeleteMovie(id);
            if (!deleted) return NotFound("Movie not found");
            return NoContent();
        }
    }
}