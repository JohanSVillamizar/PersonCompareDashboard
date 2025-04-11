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

        public async Task<CompareResult> CompareAsync()
        {
            var sqlList = await _sql.Personas.ToListAsync();
            var pgList = await _pg.Personas.ToListAsync();

            var missingInPostgre = sqlList
                .Where(s => !pgList.Any(p => p.Cedula == s.Cedula))
                .ToList();

            var missingInSql = pgList
                .Where(p => !sqlList.Any(s => s.Cedula == p.Cedula))
                .ToList();

            // Tabla paralela
            var allCedulas = sqlList.Select(s => s.Cedula)
                .Union(pgList.Select(p => p.Cedula))
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            var comparacionesParalelas = allCedulas.Select(cedula =>
            {
                var sql = sqlList.FirstOrDefault(s => s.Cedula == cedula);
                var pg = pgList.FirstOrDefault(p => p.Cedula == cedula);
                return new PersonaParalelo
                {
                    SqlPersona = sql,
                    PostgrePersona = pg
                };
            }).ToList();

            return new CompareResult
            {
                CountSql = sqlList.Count,
                CountPostgre = pgList.Count,
                MissingInPostgre = missingInPostgre,
                MissingInSql = missingInSql,
                ComparacionesParalelas = comparacionesParalelas
            };
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
