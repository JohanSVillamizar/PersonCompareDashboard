using Microsoft.EntityFrameworkCore;
using PersonCompareDashboard.Models;

namespace PersonCompareDashboard.Data
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options) { }
        public DbSet<Persona> Personas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().ToTable("personas");
            modelBuilder.Entity<Persona>().HasKey(p => p.Cedula);
        }
    }

}
