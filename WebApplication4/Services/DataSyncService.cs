using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApplication4.Data;
using WebApplication4.Models;
using Hangfire;

public class DataSyncService : IHostedService, IDisposable
{
    private readonly ILogger<DataSyncService> _logger; // Logger para registrar mensajes y errores
    private Timer _timer; // Timer para manejar la sincronización periódica
    private readonly HttpClient _httpClient; // Cliente HTTP para realizar peticiones a la API
    private readonly IServiceProvider _serviceProvider; // Proveedor de servicios para manejar la inyección de dependencias
    private readonly IConfiguration _configuration; // Configuración para acceder a variables como URLs de API

    // Constructor de la clase que inyecta las dependencias necesarias
    public DataSyncService(ILogger<DataSyncService> logger, IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient(); // Crea una instancia de HttpClient usando IHttpClientFactory
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    // Método que se ejecuta cuando el servicio es iniciado
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Sync Service started."); // Registra un mensaje indicando que el servicio se ha iniciado
        RecurringJob.AddOrUpdate(() => SyncData(), Cron.MinuteInterval(15)); // Configura una tarea recurrente que se ejecuta cada 15 minutos usando Hangfire
        return Task.CompletedTask; // Devuelve una tarea completada
    }

    // Método que realiza la sincronización de datos
    private async Task SyncData()
    {
        _logger.LogInformation("Starting data sync..."); // Registra un mensaje indicando que la sincronización ha comenzado

        using (var scope = _serviceProvider.CreateScope()) // Crea un nuevo alcance de servicio para obtener el DbContext
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<dbboot>(); // Obtiene una instancia de dbboot (DbContext)

            try
            {
                // Sincronizar Vehículos
                var vehiclesUrl = $"{_configuration["Authentication:ApiUrl"]}/Catalog/GetAllVehicles"; // Construye la URL para obtener los vehículos
                var vehiclesResponse = await _httpClient.GetStringAsync(vehiclesUrl); // Realiza una petición GET a la URL
                var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(vehiclesResponse); // Deserializa la respuesta en una lista de objetos Vehicle
                dbContext.Vehicles.AddRange(vehicles); // Agrega los vehículos obtenidos al DbContext
                await dbContext.SaveChangesAsync(); // Guarda los cambios en la base de datos
                _logger.LogInformation("Vehicles sync completed."); // Registra un mensaje indicando que la sincronización de vehículos ha sido completada

                // Sincronizar Empleados
                var employeesUrl = $"{_configuration["Authentication:ApiUrl"]}/service/catalog/api/v1/employees/all"; // Construye la URL para obtener los empleados
                var employeesResponse = await _httpClient.GetStringAsync(employeesUrl); // Realiza una petición GET a la URL
                var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesResponse); // Deserializa la respuesta en una lista de objetos Employee
                dbContext.Employees.AddRange(employees); // Agrega los empleados obtenidos al DbContext
                await dbContext.SaveChangesAsync(); // Guarda los cambios en la base de datos
                _logger.LogInformation("Employees sync completed."); // Registra un mensaje indicando que la sincronización de empleados ha sido completada

                // Sincronizar Turnos de Trabajo
                var workShiftsUrl = $"{_configuration["Authentication:ApiUrl"]}/Catalog/GetAllWorkShifts"; // Construye la URL para obtener los turnos de trabajo
                var workShiftsResponse = await _httpClient.GetStringAsync(workShiftsUrl); // Realiza una petición GET a la URL
                var workShifts = JsonConvert.DeserializeObject<List<Shift>>(workShiftsResponse); // Deserializa la respuesta en una lista de objetos Shift
                dbContext.Shifts.AddRange(workShifts); // Agrega los turnos obtenidos al DbContext
                await dbContext.SaveChangesAsync(); // Guarda los cambios en la base de datos
                _logger.LogInformation("Work Shifts sync completed."); // Registra un mensaje indicando que la sincronización de turnos de trabajo ha sido completada

                // Sincronizar Rutas de Acarreo
                var haulagePathsUrl = $"{_configuration["Authentication:ApiUrl"]}/service/haulages/api/v2/haulagepaths/all"; // Construye la URL para obtener las rutas de acarreo
                var haulagePathsResponse = await _httpClient.GetStringAsync(haulagePathsUrl); // Realiza una petición GET a la URL
                var haulagePaths = JsonConvert.DeserializeObject<List<WebApplication4.Models.Route>>(haulagePathsResponse); // Deserializa la respuesta en una lista de objetos Route (especificando el namespace completo)
                dbContext.Routes.AddRange(haulagePaths); // Agrega las rutas obtenidas al DbContext
                await dbContext.SaveChangesAsync(); // Guarda los cambios en la base de datos
                _logger.LogInformation("Haulage Paths sync completed."); // Registra un mensaje indicando que la sincronización de rutas de acarreo ha sido completada

                // Sincronizar Tipos de Material
                var materialTypesUrl = $"{_configuration["Authentication:ApiUrl"]}/service/haulages/api/v2/materialtypes/all"; // Construye la URL para obtener los tipos de material
                var materialTypesResponse = await _httpClient.GetStringAsync(materialTypesUrl); // Realiza una petición GET a la URL
                var materialTypes = JsonConvert.DeserializeObject<List<Material>>(materialTypesResponse); // Deserializa la respuesta en una lista de objetos Material
                dbContext.Materials.AddRange(materialTypes); // Agrega los tipos de material obtenidos al DbContext
                await dbContext.SaveChangesAsync(); // Guarda los cambios en la base de datos
                _logger.LogInformation("Material Types sync completed."); // Registra un mensaje indicando que la sincronización de tipos de material ha sido completada
            }
            catch (HttpRequestException httpEx) // Captura errores relacionados con las peticiones HTTP
            {
                _logger.LogError($"HTTP error occurred while syncing data: {httpEx.Message}"); // Registra el error con el mensaje de excepción
            }
            catch (Exception ex) // Captura cualquier otro tipo de error
            {
                _logger.LogError($"An error occurred while syncing data: {ex.Message}"); // Registra el error con el mensaje de excepción
            }
        }

        _logger.LogInformation("Data sync completed."); // Registra un mensaje indicando que la sincronización de datos ha sido completada
    }

    // Método que se ejecuta cuando el servicio es detenido
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Sync Service stopped."); // Registra un mensaje indicando que el servicio ha sido detenido
        _timer?.Change(Timeout.Infinite, 0); // Detiene el temporizador si está activo
        return Task.CompletedTask; // Devuelve una tarea completada
    }

    // Método Dispose para liberar recursos
    public void Dispose()
    {
        _timer?.Dispose(); // Libera los recursos del temporizador si está activo
    }
}
