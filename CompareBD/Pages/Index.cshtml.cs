using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonCompareDashboard.Models;
using PersonCompareDashboard.Services;

namespace PersonCompareDashboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonaCompareStrategy _comparer;

        public IndexModel(IPersonaCompareStrategy comparer)
        {
            _comparer = comparer;
        }

        [BindProperty]
        public CompareResult Resultado { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Resultado = await _comparer.CompareAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSyncAsync()
        {
            // Vuelve a comparar para obtener los registros actuales a sincronizar
            var resultadoActual = await _comparer.CompareAsync();

            // Sincroniza los registros que hacen falta
            await _comparer.SyncMissingAsync(resultadoActual.MissingInSql, resultadoActual.MissingInPostgre);

            // Vuelve a obtener el resultado para mostrar la vista actualizada
            Resultado = await _comparer.CompareAsync();

            return Page();
        }
    }
}
