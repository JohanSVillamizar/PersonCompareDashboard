using PersonCompareDashboard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonCompareDashboard.Services
{
    public interface IPersonaCompareStrategy
    {
        Task<CompareResult> CompareAsync();
        Task SyncMissingAsync(List<Persona> missingInSql, List<Persona> missingInPostgre);
    }
}
