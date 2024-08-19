using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using Microsoft.Extensions.Logging;
using WebApplication4.Services; 

var builder = WebApplication.CreateBuilder(args); // Crea el builder para configurar la aplicación.

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews(); // Agrega el servicio de controladores con vistas al contenedor de servicios.

// Registrar el servicio de HTTP Client
builder.Services.AddHttpClient(); // Registra HttpClient en el contenedor de servicios para hacer solicitudes HTTP.

// Registrar el servicio de autenticación
builder.Services.AddScoped<AuthenticationService>(); // Registra el servicio de autenticación con un ciclo de vida scoped (una instancia por solicitud).

// Registrar el servicio de token
builder.Services.AddSingleton<TokenService>(); // Registra el servicio de tokens con un ciclo de vida singleton (una instancia para toda la aplicación).

// Agregar DbContext
builder.Services.AddDbContext<dbboot>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Configura y registra el contexto de base de datos para usar SQL Server, obteniendo la cadena de conexión desde el archivo de configuración.

// Registrar el job de sincronización de datos
builder.Services.AddHostedService<DataSyncJob>(); // Registra el servicio en segundo plano (hosted service) para la sincronización de datos.

// Agregar logging
builder.Logging.ClearProviders(); // Limpia los proveedores de logging predeterminados.
builder.Logging.AddConsole(); // Agrega un proveedor de logging para la consola.
builder.Logging.AddDebug(); // Agrega un proveedor de logging para el debug.

var app = builder.Build(); // Construye la aplicación con la configuración proporcionada.

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment()) // Si la aplicación no está en el entorno de desarrollo:
{
    app.UseExceptionHandler("/Home/Error"); // Usa el controlador de excepciones para manejar errores globales.
    app.UseHsts(); // Habilita HSTS (HTTP Strict Transport Security) para mejorar la seguridad en las conexiones HTTPS.
}

app.UseHttpsRedirection(); // Redirige las solicitudes HTTP a HTTPS.
app.UseStaticFiles(); // Habilita el uso de archivos estáticos como imágenes, CSS y JavaScript.

app.UseRouting(); // Habilita el enrutamiento para manejar las solicitudes entrantes.

app.UseAuthentication(); // Habilita la autenticación en la aplicación.
app.UseAuthorization(); // Habilita la autorización para restringir el acceso según las políticas definidas.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Define la ruta predeterminada para los controladores y acciones.

app.MapControllerRoute(
    name: "auth",
    pattern: "{controller=Authentication}/{action=Login}/{id?}"); // Define una ruta específica para el controlador de autenticación.

app.MapControllerRoute(
    name: "import",
    pattern: "{controller=Import}/{action=Import}/{id?}"); // Define una ruta específica para el controlador de importación.

app.MapControllers(); // Mapea todos los controladores definidos en la aplicación.

try
{
    app.Run(); // Inicia la aplicación y comienza a escuchar las solicitudes entrantes.
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>(); // Obtiene el servicio de logging.
    logger.LogError(ex, "Ocurrió un error durante el inicio de la aplicación."); // Registra un error en caso de que falle el inicio de la aplicación.
    throw; // Relanza la excepción para que sea manejada por el sistema.
}
