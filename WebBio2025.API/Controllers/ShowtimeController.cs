using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;

namespace WebBio2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimesController : ControllerBase
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimesController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowtimeDTOResponse>>> GetAllShowtimes()
        {
            var showtimes = await _showtimeService.GetAllShowtimes();
            return Ok(showtimes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShowtimeDTOResponse>> GetShowtimeById(int id)
        {
            var showtime = await _showtimeService.GetShowtimeById(id);
            if (showtime == null) return NotFound();
            return Ok(showtime);
        }

        [HttpGet("byMovie/{movieId:int}")]
        public async Task<ActionResult<IEnumerable<ShowtimeDTOResponse>>> GetByMovie(int movieId)
        {
            var showtimes = await _showtimeService.GetShowtimesByMovieId(movieId);
            return Ok(showtimes);
        }

        [HttpGet("byHall/{hallId:int}")]
        public async Task<ActionResult<IEnumerable<ShowtimeDTOResponse>>> GetByHall(int hallId)
        {
            var showtimes = await _showtimeService.GetShowtimesByHallId(hallId);
            return Ok(showtimes);
        }

        [HttpPost]
        public async Task<ActionResult<ShowtimeDTOResponse>> CreateShowtime([FromBody] ShowTimeDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _showtimeService.CreateShowtime(request);
            if (created == null)
                return BadRequest("Failed to create showtime.");

            return CreatedAtAction(nameof(GetShowtimeById), new { id = created.ShowtimeId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ShowtimeDTOResponse>> UpdateShowtime(int id, [FromBody] ShowTimeDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            if (id != request.ShowtimeId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _showtimeService.UpdateShowtime(request);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShowtime(int id)
        {
            var deleted = await _showtimeService.DeleteShowtime(id);
            if (!deleted) return NotFound("Showtime not found");
            return NoContent();
        }
    }
}