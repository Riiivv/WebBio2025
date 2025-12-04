using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Infrastucture
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Domain.entities.Movies> Movies { get; set; }
        public DbSet<Domain.entities.Hall> Halls { get; set; }
        public DbSet<Domain.entities.Seat> Seats { get; set; }
        public DbSet<Domain.entities.Ticket> Tickets { get; set; }
        public DbSet<Domain.entities.Showtime> Showtimes { get; set; }
    }
}
