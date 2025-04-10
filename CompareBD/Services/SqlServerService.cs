using Microsoft.EntityFrameworkCore;
using PersonCompareDashboard.Data;
using PersonCompareDashboard.Models;


namespace PersonCompareDashboard.Services
{
    public class SqlServerService
    {
        private readonly SqlServerContext _context;

        public SqlServerService(SqlServerContext context)
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
