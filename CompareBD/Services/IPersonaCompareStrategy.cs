using PersonCompareDashboard.Models;

namespace PersonCompareDashboard.Services
{
    public interface IPersonaCompareStrategy
    {
        Task<(int countSql, int countPostgre, List<Persona> missingInPostgre, List<Persona> missingInSql)> CompareAsync();
        Task SyncMissingAsync(List<Persona> missingInSql, List<Persona> missingInPostgre);
    }
}
