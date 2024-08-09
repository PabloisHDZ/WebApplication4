using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using Microsoft.Extensions.Logging;
using WebApplication4.Services; // Asegúrate de que este espacio de nombres exista

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Registrar el servicio de HTTP Client
builder.Services.AddHttpClient();

// Registrar el servicio de autenticación
builder.Services.AddScoped<AuthenticationService>();

// Registrar el servicio de token
builder.Services.AddSingleton<TokenService>();

// Agregar DbContext
builder.Services.AddDbContext<dbboot>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el job de sincronización de datos
builder.Services.AddHostedService<DataSyncJob>();

// Agregar logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "auth",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "import",
    pattern: "{controller=Import}/{action=Import}/{id?}");

app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Ocurrió un error durante el inicio de la aplicación.");
    throw;
}
