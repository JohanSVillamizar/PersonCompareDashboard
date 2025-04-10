using Microsoft.EntityFrameworkCore;
using PersonCompareDashboard.Data;
using PersonCompareDashboard.Models;

namespace PersonCompareDashboard.Services
{
    public class PersonaComparer : IPersonaCompareStrategy
    {
        private readonly SqlServerContext _sql;
        private readonly PostgreSqlContext _pg;

        public PersonaComparer(SqlServerContext sql, PostgreSqlContext pg)
        {
            _sql = sql;
            _pg = pg;
        }

        public async Task<(int countSql, int countPostgre, List<Persona> missingInPostgre, List<Persona> missingInSql)> CompareAsync()
        {
            var sqlList = await _sql.Personas.ToListAsync();
            var pgList = await _pg.Personas.ToListAsync();

            var missingInPostgre = sqlList
                .Where(s => !pgList.Any(p => p.Cedula == s.Cedula))
                .ToList();

            var missingInSql = pgList
                .Where(p => !sqlList.Any(s => s.Cedula == p.Cedula))
                .ToList();

            return (sqlList.Count, pgList.Count, missingInPostgre, missingInSql);
        }


        public async Task SyncMissingAsync(List<Persona> missingInSql, List<Persona> missingInPostgre)
        {
            if (missingInSql.Any())
            {
                await _sql.Personas.AddRangeAsync(missingInSql);
                await _sql.SaveChangesAsync();
            }

            if (missingInPostgre.Any())
            {
                await _pg.Personas.AddRangeAsync(missingInPostgre);
                await _pg.SaveChangesAsync();
            }
        }
    }

}
