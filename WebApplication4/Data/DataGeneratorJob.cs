using System;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.Models;

public class DataGeneratorJob
{
    private readonly dbboot _context;
    private readonly Random _random = new Random();

    public DataGeneratorJob(dbboot context)
    {
        _context = context;
    }

    public async Task GenerateDataAsync()
    {
        // Generar datos aleatorios
        var pesoBruto = 100m; // Peso base
        var variacion = 0.05m + (decimal)(_random.NextDouble() * 0.20); // Variación entre 5% y 25%
        var pesoNeto = pesoBruto * (1 + variacion);

        var acarreo = new Acarreo
        {
            VehiculoID = 1, // Ajusta según los datos reales
            OperadorID = 1, // Ajusta según los datos reales
            RutaID = 1, // Ajusta según los datos reales
            Toneladas = pesoNeto,
            Comentarios = "Comentario aleatorio",
            MaterialID = 1, // Ajusta según los datos reales
        };

        // Guardar en la base de datos
        _context.Acarreos.Add(acarreo);
        await _context.SaveChangesAsync();
    }

}