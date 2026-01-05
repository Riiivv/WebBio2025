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
using WebBio2025.Infrastucture;

namespace WebBio2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatDTOResponse>>> GetAllSeats()
        {
            var seats = await _seatService.GetAllSeats();
            return Ok(seats);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SeatDTOResponse>> GetSeatById(int id)
        {
            var seat = await _seatService.GetSeatById(id);
            if (seat == null) return NotFound();
            return Ok(seat);
        }

        [HttpPost]
        public async Task<ActionResult<SeatDTOResponse>> CreateSeat([FromBody] SeatDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _seatService.CreateSeat(request);
            if (created == null)
                return BadRequest("Failed to create seat.");

            return CreatedAtAction(nameof(GetSeatById), new { id = created.SeatId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SeatDTOResponse>> UpdateSeat(int id, [FromBody] SeatDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            if (id != request.SeatId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _seatService.UpdateSeat(request);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var deleted = await _seatService.DeleteSeat(id);
            if (!deleted) return NotFound("Seat not found");
            return NoContent();
        }
    }
}