using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApplication4.Data;
using WebApplication4.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public class DataSyncJob : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer;
    private readonly HttpClient _httpClient;
    private readonly Random _random;

    public DataSyncJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _httpClient = new HttpClient();
        _random = new Random();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SyncData, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }

    private async void SyncData(object state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<dbboot>();

            // Lógica para consultar la API y obtener datos
            var response = await _httpClient.GetAsync("https://demo-acarreos.smartflow.com.mx/service/haulages/api/v2/manualhaulages/manual/add"); // API de registro de acarreos
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var jsonData = JArray.Parse(responseData);

            foreach (var item in jsonData)
            {
                var pesoBruto = 100m; // Peso base
                var variacion = 0.05m + (decimal)(_random.NextDouble() * 0.20); // Variación entre 5% y 25%
                var pesoNeto = pesoBruto * (1 + variacion);

                var acarreo = new Acarreo
                {
                    VehiculoID = item["VehiculoID"].Value<int>(), // Ajusta según los datos reales
                    OperadorID = item["OperadorID"].Value<int>(), // Ajusta según los datos reales
                    RutaID = item["RutaID"].Value<int>(), // Ajusta según los datos reales
                    Toneladas = pesoNeto,
                    Comentarios = item["Comentarios"].Value<string>(),
                    MaterialID = item["MaterialID"].Value<int>(), // Ajusta según los datos reales
                };

                context.Acarreos.Add(acarreo);
            }

            await context.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
