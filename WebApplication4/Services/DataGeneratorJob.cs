// Importación de namespaces necesarios
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using WebApplication4.Controllers;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.Services;

// Clase DataSyncJob que implementa IHostedService y IDisposable para ejecutar un trabajo en segundo plano
public class DataSyncJob : IHostedService, IDisposable
{
    // Dependencias inyectadas
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DataSyncJob> _logger;
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;
    private Timer _timer;
    private readonly HttpClient _httpClient;
    private readonly Random _random;

    // Constructor que inicializa las dependencias y el cliente HTTP
    public DataSyncJob(IServiceProvider serviceProvider, ILogger<DataSyncJob> logger, IConfiguration configuration, TokenService tokenService, IHttpClientFactory httpClientFactory)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
        _tokenService = tokenService;
        _httpClient = httpClientFactory.CreateClient();
        _random = new Random();
    }

    // Método que se ejecuta al iniciar el servicio
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DataSyncJob iniciado.");
        _timer = new Timer(SyncData, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Inicia un temporizador que ejecuta el método SyncData cada minuto
        return Task.CompletedTask;
    }

    // Método principal de sincronización de datos que se ejecuta periódicamente
    private async void SyncData(object state)
    {
        // Verifica si la sincronización está habilitada
        if (!DataSyncController.IsSyncEnabled())
        {
            _logger.LogInformation("Sincronización deshabilitada. No se realizó ninguna acción.");
            return;
        }

        using (var scope = _serviceProvider.CreateScope()) // Crea un scope para resolver servicios con un ciclo de vida limitado
        {
            var context = scope.ServiceProvider.GetRequiredService<dbboot>(); // Obtiene el contexto de la base de datos

            try
            {
                // Obtener el token de acceso mediante el servicio TokenService
                var token = await _tokenService.GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Agrega el token en la cabecera de la solicitud

                // Obtener datos de varias APIs
                var vehicles = await GetDataFromApi<List<Vehicle>>("https://demo-acarreos.smartflow.com.mx/service/catalog/api/v1/Vehicles/all");
                var employees = await GetDataFromApi<List<Employee>>("https://demo-acarreos.smartflow.com.mx/service/haulages/api/v1/GeneralSettings/employees/all");
                var workShifts = await GetDataFromApi<List<Shift>>("https://demo-acarreos.smartflow.com.mx/service/catalog/api/v1/Workshifts/all");
                var haulagePaths = await GetDataFromApi<List<Haulage>>("https://demo-acarreos.smartflow.com.mx/service/haulages/api/v2/haulagepaths/all");

                // Validar datos obtenidos
                if (vehicles == null || !vehicles.Any())
                {
                    _logger.LogError("No se encontraron vehículos en la respuesta de la API.");
                    return;
                }

                if (employees == null || !employees.Any())
                {
                    _logger.LogError("No se encontraron empleados en la respuesta de la API.");
                    return;
                }

                if (haulagePaths == null || !haulagePaths.Any())
                {
                    _logger.LogError("No se encontraron rutas en la respuesta de la API.");
                    return;
                }

                // Seleccionar datos basados en VehicleType.Name
                var selectedVehicle = vehicles.FirstOrDefault(v => v.VehicleType?.Name.Contains("CAMIONES DE VOLTEO (ACARREO)", StringComparison.OrdinalIgnoreCase) == true);
                var selectedEmployee = employees.FirstOrDefault();
                var selectedPath = haulagePaths.FirstOrDefault();

                // Validar selección de datos
                if (selectedVehicle == null)
                {
                    _logger.LogError("No se encontró un vehículo que coincida con el criterio.");
                    return;
                }

                if (selectedEmployee == null)
                {
                    _logger.LogError("No se encontró un empleado.");
                    return;
                }

                if (selectedPath == null)
                {
                    _logger.LogError("No se encontró una ruta.");
                    return;
                }

                // Preparar el objeto para enviar
                var acarreo = new
                {
                    VehicleId = selectedVehicle.VehicleId,
                    EmployeeId = selectedEmployee.EmployeeId,
                    PathId = selectedPath.PathId,
                    Weight = CalculateRandomWeight(), // Calcula un peso aleatorio
                    Date = DateTime.Now,
                    Comments = "Comentario generado a partir de los datos.",
                    materialTypeId = 1,
                };

                // Serializar el objeto en JSON
                var jsonContent = JsonConvert.SerializeObject(acarreo);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Construir la URL de la API para enviar los datos
                var apiUrl = _configuration["Authentication:ApiUrl"];
                var apiEndpoint = $"{apiUrl}/service/haulages/api/v2/manualhaulages/manual/add";

                // Enviar los datos a la API
                var response = await _httpClient.PostAsync(apiEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Datos enviados correctamente a la API.");

                    // Si la solicitud es exitosa, guarda los datos en la base de datos
                    context.Haulages.Add(new Haulage
                    {
                        VehicleId = acarreo.VehicleId,
                        EmployeeId = acarreo.EmployeeId,
                        PathId = acarreo.PathId,
                        Weight = acarreo.Weight,
                        Comments = acarreo.Comments,
                        materialTypeId = acarreo.materialTypeId,
                    });

                    await context.SaveChangesAsync();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error al enviar datos a la API: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Excepción al intentar enviar datos a la API: {ex.Message}");
            }
        }
    }

    // Método para obtener datos de la API
    private async Task<T> GetDataFromApi<T>(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync(url); // Hacer la solicitud HTTP GET
            var responseString = await response.Content.ReadAsStringAsync(); // Leer la respuesta como string
            _logger.LogInformation($"Respuesta de la API para {url}: {responseString}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al obtener datos de la API {url}: {response.StatusCode} - {responseString}");
                throw new Exception($"Error al obtener datos de la API: {response.StatusCode} - {responseString}");
            }

            // Verificar si la respuesta es un array JSON o un objeto JSON
            JToken token = JToken.Parse(responseString);

            if (token.Type == JTokenType.Array)
            {
                // Si el JSON es un array, deserializar en una lista
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            else if (token.Type == JTokenType.Object)
            {
                // Si el JSON es un objeto, deserializar directamente
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            else
            {
                throw new Exception("Formato de JSON inesperado.");
            }
        }
        catch (JsonException jsonEx)
        {
            _logger.LogError($"Error al deserializar JSON: {jsonEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener datos de la API: {ex.Message}");
            throw;
        }
    }

    // Método para calcular peso aleatorio basado en la configuración
    private decimal CalculateRandomWeight()
    {
        var pesoBruto = 35m; // Peso base
        var variacionMin = 0.05m; // 5% de variación mínima
        var variacionMax = 0.25m; // 25% de variación máxima
        var variacion = variacionMin + (decimal)(_random.NextDouble() * (double)(variacionMax - variacionMin)); // Calcula una variación aleatoria
        return pesoBruto * (1 + variacion); // Retorna el peso con la variación aplicada
    }

    // Método para decodificar el token JWT y obtener la fecha de expiración
    private DateTime? DecodeTokenAndGetExpirationDate(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token); // Lee el token JWT
            return jwtToken.ValidTo; // Retorna la fecha de expiración del token
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al decodificar el token: {ex.Message}");
            return null;
        }
    }

    // Método que se ejecuta al detener el servicio
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DataSyncJob detenido.");
        _timer?.Change(Timeout.Infinite, 0); // Detiene el temporizador
        return Task.CompletedTask;
    }

    // Método Dispose para liberar recursos
    public void Dispose()
    {
        _timer?.Dispose(); // Libera el temporizador
        _httpClient?.Dispose(); // Libera el cliente HTTP
    }
}
