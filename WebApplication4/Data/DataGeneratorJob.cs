using Newtonsoft.Json;
using System.Text;
using WebApplication4.Data;
using WebApplication4.Models;

public class DataSyncJob : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DataSyncJob> _logger;
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;
    private Timer _timer;
    private readonly HttpClient _httpClient;
    private readonly Random _random;

    public DataSyncJob(IServiceProvider serviceProvider, ILogger<DataSyncJob> logger, IConfiguration configuration, TokenService tokenService)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
        _tokenService = tokenService;
        _httpClient = new HttpClient();
        _random = new Random();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DataSyncJob iniciado.");
        _timer = new Timer(SyncData, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }

    private async void SyncData(object state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<dbboot>();

            // Generar datos aleatorios para enviar a la API
            var pesoBruto = 100m; // Peso base
            var variacion = 0.05m + (decimal)(_random.NextDouble() * 0.20); // Variación entre 5% y 25%
            var pesoNeto = pesoBruto * (1 + variacion);

            var acarreo = new
            {
                VehicleId = 165, // Ajusta según los datos reales
                EmployeeId = 9316, // Ajusta según los datos reales
                PathId = 3  , // Ajusta según los datos reales
                Weight = 20.663254,
                Date = DateTime.Now,
                Comments = "Comentario aleatorio",
                MaterialTypeId = 1, // Ajusta según los datos reales
            };

            

            var jsonContent = JsonConvert.SerializeObject(acarreo);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var apiUrl = _configuration["Authentication:ApiUrl"];
            var apiEndpoint = $"{apiUrl}/service/haulages/api/v2/manualhaulages/manual/add";

            //var token = _tokenService.GetToken();
            //if (string.IsNullOrEmpty(token))
            //{
            //    _logger.LogError("Token no disponible. Asegúrate de que el usuario ha iniciado sesión y el token ha sido almacenado correctamente.");
            //    return;
            //}

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkIyODc1RjY3MjA4NzcwQzcwRkUwMTlFNEM4NjdCOTE5MUE4REEyOUMiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJzb2RmWnlDSGNNY1A0Qm5reUdlNUdScU5vcHcifQ.eyJuYmYiOjE3MjI1MjIxNTYsImV4cCI6MTcyMjUyNTc1NiwiaXNzIjoibnVsbCIsImF1ZCI6WyJudWxsL3Jlc291cmNlcyIsIklkZW50aXR5U2VydmVyQXBpIiwic21hcnRmbG93Il0sImNsaWVudF9pZCI6InByaXZhdGUubmV0d29ya2luZy5hcHAiLCJzdWIiOiIxMjQxYjgzMS1jNTA3LTQ3MTgtYTMyMy0yODJhYjBhZjdlYjYiLCJhdXRoX3RpbWUiOjE3MjI1MjIxNTYsImlkcCI6ImxvY2FsIiwicHJlZmVycmVkX3VzZXJuYW1lIjoicGh6IiwibmFtZSI6IlBBQkxPIiwiaWRfdXNlciI6IjEyNDFiODMxLWM1MDctNDcxOC1hMzIzLTI4MmFiMGFmN2ViNiIsImxhc3RfbmFtZSI6IkhFUk5BTkRFWiIsInVzZXJfbmFtZSI6InBoeiIsImVuYWJsZV9leHBpcmF0aW9uIjoiRmFsc2UiLCJleHBpcmF0aW9uX2RhdGUiOiIwMS8wMS8wMDAxIDAwOjAwOjAwIiwibGFuZ3VhZ2VfbW9kZSI6IkVTIiwiZW1wbG95ZWVfaWQiOiIiLCJmaXJzdF9jb25uZWN0aW9uIjoiZmFsc2UiLCJjaGFuZ2VfcGFzc3dvcmQiOiIxNzE4NjQzODczMDY0IiwiZW5hYmxlIjoiVHJ1ZSIsInNvdW5kIjoiVHJ1ZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluX2FjYXJyZW9zIiwieHhJbmZJZCI6ImU5NTkxYzFlLThiMzQtNGI4NC1hYTIxLWQwNjIwY2ZmMDcxYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImpodmE1ODZAZ21haWwuY29tIiwic2NvcGUiOlsiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJzbWFydGZsb3ciLCJvZmZsaW5lX2FjY2VzcyJdLCJhbXIiOlsicHdkIl19.SFJ93EWrmKdWneEm41Dgn3Vh_2OTTHqlTGk7PGUrlDvFE4afWcPLkwMSLN7HTLFath7DDmQz-kCbCXGyPT0lqDx8rJjDFxre8NNEKY_B-xMeIV_2jKKpFqOifr6H400m_eEwCNIp0qpwgr8msXLK0IobDF90WpSXVujRAx1Y0RNsHLSDrUoSH4VUojwGSN7vTv0UCwRXZuDVqHJY8eKSScC0nCm4YC-G6uyGjdNE0J4CypCsB5NSb1mi-VMs-bE5-0ZLILVDVWAuVkufp8oam4Lj6ZvJR4n7on9W3xwlubO4EClB3I8vuDgoVCoU-s9BcXDdEz4MwnD8wQ4O0kaVyg");

            try
            {
                var response = await _httpClient.PostAsync(apiEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Datos enviados correctamente a la API.");

                    // Si la solicitud es exitosa, guarda los datos en la base de datos
                    //context.Acarreos.Add(new Acarreo
                    //{
                    //    VehiculoID = acarreo.VehiculoID,
                    //    OperadorID = acarreo.OperadorID,
                    //    RutaID = acarreo.RutaID,
                    //    Toneladas = acarreo.Toneladas,
                    //    Comentarios = acarreo.Comentarios,
                    //    MaterialID = acarreo.MaterialID,
                    //});

                    //await context.SaveChangesAsync();
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

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DataSyncJob detenido.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _httpClient.Dispose();
    }
}
