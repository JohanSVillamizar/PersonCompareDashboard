using Microsoft.EntityFrameworkCore;
using PersonCompareDashboard.Data;
using PersonCompareDashboard.Models;

namespace PersonCompareDashboard.Services
{
    public class PostgreSqlService
    {
        private readonly PostgreSqlContext _context;

        public PostgreSqlService(PostgreSqlContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> GetPersonasAsync()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task InsertPersonaAsync(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
        }
    }

}
