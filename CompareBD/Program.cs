using Microsoft.EntityFrameworkCore;
using PersonCompareDashboard.Data;
using PersonCompareDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la aplicaci�n
builder.Services.AddRazorPages();

// Configurar la conexi�n a SQL Server
builder.Services.AddDbContext<SqlServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Configurar la conexi�n a PostgreSQL
builder.Services.AddDbContext<PostgreSqlContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

// Registrar tu servicio para que se pueda inyectar en Index.cshtml.cs
builder.Services.AddScoped<IPersonaCompareStrategy, PersonaComparer>();

var app = builder.Build();

// Configuraci�n del middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
