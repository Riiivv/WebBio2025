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
    public class HallsController : ControllerBase
    {
        private readonly IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallDTOResponse>>> GetAllHalls()
        {
            var halls = await _hallService.GetAllHalls();
            return Ok(halls);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HallDTOResponse>> GetHallById(int id)
        {
            var hall = await _hallService.GetHallById(id);
            if (hall == null) return NotFound();
            return Ok(hall);
        }

        [HttpPost]
        public async Task<ActionResult<HallDTOResponse>> CreateHall([FromBody] HallDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _hallService.CreateHall(request);
            if (created == null)
                return BadRequest("Failed to create hall.");

            return CreatedAtAction(nameof(GetHallById), new { id = created.HallId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HallDTOResponse>> UpdateHall(int id, [FromBody] HallDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            if (id != request.HallId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _hallService.UpdateHall(request);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var deleted = await _hallService.DeleteHall(id);
            if (!deleted) return NotFound("Hall not found");
            return NoContent();
        }
    }
}