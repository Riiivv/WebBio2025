using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Infrastucture
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Domain.Person> Persons { get; set; }
        public DbSet<Domain.entities.Movies> Movies { get; set; }
        public DbSet<Domain.entities.Hall> Halls { get; set; }
    }
}
