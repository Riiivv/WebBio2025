using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Domain.interfaces;

namespace WebBio2025.Infrastucture.Repositories
{
    public class TicketRepository : ITicket
    {
        public DatabaseContext _context;
        public TicketRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Ticket>> GetAllTickets()
        {
            var ticket = await _context.Tickets.ToListAsync();
            return ticket;
        }
    }
}
