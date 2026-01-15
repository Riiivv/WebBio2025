using Microsoft.AspNetCore.Mvc;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;

namespace WebBio2025.API.Controllers
{
    [Route("api/showtimes")]
    [ApiController]
    public class ShowtimeSeatsController : ControllerBase
    {
        private readonly IShowtimeSeatService _service;

        public ShowtimeSeatsController(IShowtimeSeatService service)
        {
            _service = service;
        }

        [HttpGet("{showtimeId:int}/seats")]
        public async Task<ActionResult<IEnumerable<ShowTimeSeatDTOResponse>>> GetSeatMap(
            int showtimeId,
            [FromQuery] string? holdToken)
        {
            try
            {
                var seats = await _service.GetSeatMap(showtimeId, holdToken);
                return Ok(seats);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{showtimeId:int}/holds")]
        public async Task<ActionResult<ShowTimeSeatHoldDTOResponse>> CreateOrRefreshHolds(
            int showtimeId,
            [FromBody] ShowTimeSeatDTORequest request)
        {
            try
            {
                var res = await _service.CreateOrRefreshHolds(showtimeId, request);
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // conflict (sold / held by other)
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{showtimeId:int}/holds/{holdToken}")]
        public async Task<IActionResult> ReleaseHolds(int showtimeId, string holdToken)
        {
            await _service.ReleaseHolds(showtimeId, holdToken);
            return NoContent();
        }
    }
}