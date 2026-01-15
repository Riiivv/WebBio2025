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
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<SeatHold> SeatHolds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Undgå cascade-multiple-paths
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.showtime)
                .WithMany()
                .HasForeignKey(t => t.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite key for SeatHold
            modelBuilder.Entity<SeatHold>()
                .HasKey(h => new { h.ShowtimeId, h.SeatId });

            modelBuilder.Entity<Seat>()
                .Property(s => s.SeatType)
                .HasConversion<int>()
                .HasDefaultValue(SeatType.Standard);

        }
    }
}