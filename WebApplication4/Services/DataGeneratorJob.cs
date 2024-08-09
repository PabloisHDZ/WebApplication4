using Newtonsoft.Json;
using System.Text;
using WebApplication4.Data;
using WebApplication4.Models;


namespace WebApplication4.Services
{
    public class DataSyncJob : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DataSyncJob> _logger;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private Timer _timer;
        private readonly HttpClient _httpClient;
        private readonly Random _random;

        public DataSyncJob(IServiceProvider serviceProvider, ILogger<DataSyncJob> logger, IConfiguration configuration, TokenService tokenService, IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;
            _tokenService = tokenService;
            _httpClient = httpClientFactory.CreateClient();
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
                    PathId = 3, // Ajusta según los datos reales
                    Weight = pesoNeto,
                    Date = DateTime.Now,
                    Comments = "Comentario aleatorio",
                    MaterialTypeId = 1, // Ajusta según los datos reales
                };

                var jsonContent = JsonConvert.SerializeObject(acarreo);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var apiUrl = _configuration["Authentication:ApiUrl"];
                var apiEndpoint = $"{apiUrl}/service/haulages/api/v2/manualhaulages/manual/add";
                
                //libreria decodificar el token 

                try
                {
                    var token = await _tokenService.GetTokenAsync();

                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

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
                            materialTypeId = acarreo.MaterialTypeId,
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
}
