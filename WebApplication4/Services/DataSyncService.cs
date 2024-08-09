using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApplication4.Data;
using WebApplication4.Models;

public class DataSyncService : IHostedService, IDisposable
{
    private readonly ILogger<DataSyncService> _logger;
    private Timer _timer;
    private readonly HttpClient _httpClient;        
    private readonly IServiceProvider _serviceProvider;

    public DataSyncService(ILogger<DataSyncService> logger, IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Sync Service started.");
        _timer = new Timer(SyncData, null, TimeSpan.Zero, TimeSpan.FromMinutes(30)); // Ajusta el intervalo según tus necesidades
        return Task.CompletedTask;
    }

    private async void SyncData(object state)
    {
        _logger.LogInformation("Starting data sync...");

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<dbboot>();

            // Sincronizar Vehículos
            var vehiclesResponse = await _httpClient.GetStringAsync("https://demo-acarreos.smartflow.com.mx/Catalog/GetAllVehicles");
            var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(vehiclesResponse);
            dbContext.Vehicles.AddRange(vehicles);
            await dbContext.SaveChangesAsync();

            // Sincronizar Empleados
            var employeesResponse = await _httpClient.GetStringAsync("https://demo-acarreos.smartflow.com.mx/service/catalog/api/v1/employees/all");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesResponse);
            dbContext.Employees.AddRange(employees);
            await dbContext.SaveChangesAsync();

            // Sincronizar otros datos de manera similar...

            _logger.LogInformation("Data sync completed.");
        } //agregar un try/catch 
        //concatenar la url que no sea estatica
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Sync Service stopped.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
