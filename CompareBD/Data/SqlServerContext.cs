using Microsoft.EntityFrameworkCore;
using PersonCompareDashboard.Models;

namespace PersonCompareDashboard.Data
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }
        public DbSet<Persona> Personas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().ToTable("Personas");
            modelBuilder.Entity<Persona>().HasKey(p => p.Cedula);
        }
    }

}
