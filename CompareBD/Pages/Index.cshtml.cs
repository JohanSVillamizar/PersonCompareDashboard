using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonCompareDashboard.Models;
using PersonCompareDashboard.Services;
using System.Diagnostics.Metrics;

namespace PersonCompareDashboard.Pages;

public class IndexModel : PageModel
{
    private readonly IPersonaCompareStrategy _comparer;

    public IndexModel(IPersonaCompareStrategy comparer)
    {
        _comparer = comparer;
    }

    public (int CountSql, int CountPostgre, List<Persona> MissingInPostgre, List<Persona> MissingInSql)? Resultado { get; set; }


    public async Task<IActionResult> OnPostAsync()
    {
        Resultado = await _comparer.CompareAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostSyncAsync()
    {
        var(countSql, countPg, missingInPostgre, missingInSql) = await _comparer.CompareAsync();


        await _comparer.SyncMissingAsync(missingInSql, missingInPostgre);

        Resultado = await _comparer.CompareAsync();

        return Page();
    }
}

