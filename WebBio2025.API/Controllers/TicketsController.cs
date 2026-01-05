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
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDTOResponse>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTickets();
            return Ok(tickets);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TicketDTOResponse>> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketById(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpGet("byMovie/{movieId:int}")]
        public async Task<ActionResult<IEnumerable<TicketDTOResponse>>> GetByMovie(int movieId)
        {
            var tickets = await _ticketService.GetTicketsByMovieId(movieId);
            return Ok(tickets);
        }

        [HttpGet("bySeat/{seatId:int}")]
        public async Task<ActionResult<IEnumerable<TicketDTOResponse>>> GetBySeat(int seatId)
        {
            var tickets = await _ticketService.GetTicketsBySeatId(seatId);
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<TicketDTOResponse>> CreateTicket([FromBody] TicketDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _ticketService.CreateTicket(request);
            if (created == null)
                return BadRequest("Failed to create ticket.");

            return CreatedAtAction(nameof(GetTicketById), new { id = created.TicketId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TicketDTOResponse>> UpdateTicket(int id, [FromBody] TicketDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            if (id != request.TicketId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _ticketService.UpdateTicket(request);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var deleted = await _ticketService.DeleteTicket(id);
            if (!deleted) return NotFound("Ticket not found");
            return NoContent();
        }
    }
}