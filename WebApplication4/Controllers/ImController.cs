//using Newtonsoft.Json;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
//using WebApplication4.Controllers;
//using WebApplication4.Data;
//using WebApplication4.Models;

//namespace WebApplication4.Services
//{
//    public class DataSyncJob : IHostedService, IDisposable
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private readonly ILogger<DataSyncJob> _logger;
//        private readonly IConfiguration _configuration;
//        private readonly TokenService _tokenService;
//        private Timer _timer;
//        private readonly HttpClient _httpClient;
//        private readonly Random _random;

//        public DataSyncJob(IServiceProvider serviceProvider, ILogger<DataSyncJob> logger, IConfiguration configuration, TokenService tokenService, IHttpClientFactory httpClientFactory)
//        {
//            _serviceProvider = serviceProvider;
//            _logger = logger;
//            _configuration = configuration;
//            _tokenService = tokenService;
//            _httpClient = httpClientFactory.CreateClient();
//            _random = new Random();
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("DataSyncJob iniciado.");
//            _timer = new Timer(SyncData, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
//            return Task.CompletedTask;
//        }

//        private async void SyncData(object state)
//        {
//            // Verificar si la sincronización está habilitada
//            if (!DataSyncController.IsSyncEnabled())
//            {
//                _logger.LogInformation("Sincronización deshabilitada. No se realizó ninguna acción.");
//                return;
//            }

//            using (var scope = _serviceProvider.CreateScope())
//            {
//                var context = scope.ServiceProvider.GetRequiredService<dbboot>();

//                // Obtener la variación de peso y el tiempo desde la configuración
//                string variacionConfig = "5-25"; // Reemplaza por el valor leído de la UI
//                string tiempoConfig = "5-25 min."; // Reemplaza por el valor leído de la UI

//                // Parsear la configuración de variación de peso
//                var variacionParts = variacionConfig.Split('-');
//                decimal variacionMin = decimal.Parse(variacionParts[0]) / 100;
//                decimal variacionMax = decimal.Parse(variacionParts[1]) / 100;

//                // Generar un peso neto aleatorio basado en la configuración
//                var pesoBruto = 35m; // Peso base
//                var variacion = variacionMin + (decimal)(_random.NextDouble() * (double)(variacionMax - variacionMin));
//                var pesoNeto = pesoBruto * (1 + variacion);

//                // Preparar el objeto para enviar
//                var acarreo = new
//                {
//                    VehicleId = 165, // Ajusta según los datos reales
//                    EmployeeId = 9316, // Ajusta según los datos reales
//                    PathId = 3, // Ajusta según los datos reales
//                    Weight = pesoNeto,
//                    Date = DateTime.Now,
//                    Comments = "Comentario aleatorio",
//                    materialTypeId = 1, // Ajusta según los datos reales
//                };

//                var jsonContent = JsonConvert.SerializeObject(acarreo);
//                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

//                var apiUrl = _configuration["Authentication:ApiUrl"];
//                var apiEndpoint = $"{apiUrl}/service/haulages/api/v2/manualhaulages/manual/add";

//                try
//                {
//                    var token = await _tokenService.GetTokenAsync();

//                    // Decodificar el token para obtener la fecha de expiración
//                    var expirationDate = DecodeTokenAndGetExpirationDate(token);
//                    _logger.LogInformation($"Fecha de expiración del token: {expirationDate}");

//                    _httpClient.DefaultRequestHeaders.Clear();
//                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

//                    var response = await _httpClient.PostAsync(apiEndpoint, content);

//                    if (response.IsSuccessStatusCode)
//                    {
//                        _logger.LogInformation("Datos enviados correctamente a la API.");

//                        // Si la solicitud es exitosa, guarda los datos en la base de datos
//                        context.Haulages.Add(new Haulage
//                        {
//                            VehicleId = acarreo.VehicleId,
//                            EmployeeId = acarreo.EmployeeId,
//                            PathId = acarreo.PathId,
//                            Weight = acarreo.Weight,
//                            Comments = acarreo.Comments,
//                            materialTypeId = acarreo.materialTypeId,
//                        });

//                        await context.SaveChangesAsync();
//                    }
//                    else
//                    {
//                        var errorMessage = await response.Content.ReadAsStringAsync();
//                        _logger.LogError($"Error al enviar datos a la API: {response.StatusCode} - {errorMessage}");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError($"Excepción al intentar enviar datos a la API: {ex.Message}");
//                }
//            }
//        }


//        private DateTime? DecodeTokenAndGetExpirationDate(string token)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var jwtToken = tokenHandler.ReadJwtToken(token);
//                return jwtToken.ValidTo;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error al decodificar el token: {ex.Message}");
//                return null;
//            }
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("DataSyncJob detenido.");
//            _timer?.Change(Timeout.Infinite, 0);
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            _timer?.Dispose();
//            _httpClient.Dispose();
//        }
//    }
//}
