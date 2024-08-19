using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models.ViewModels.Historics;
using WebApplication4.Models.ViewModels;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly dbboot _context;

        public ImportController(dbboot context)
        {
            _context = context;
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import([FromBody] List<CustomHistoric> historicoList)
        {
            // Verificar si la lista de historicos está vacía o nula
            if (historicoList == null || historicoList.Count == 0)
            {
                return BadRequest("No data to import.");
            }

            // Listas para almacenar datos válidos y errores encontrados
            var validHistoricos = new List<Historic>();
            var errorList = new List<HisError>();
            var erroresConsolidados = new List<string>();

            // Iterar sobre cada modelo de historico en la lista
            foreach (var model in historicoList)
            {
                // Consultar datos necesarios en la base de datos
                var vehiculoExiste = await _context.Vehicles.Where(v => v.EconomicNumber == model.vehicle).FirstOrDefaultAsync();
                var operadorExiste = await _context.Employees.Where(o => o.FullName == model.FullName).FirstOrDefaultAsync();
                var sitioCargaExiste = await _context.Routes.Where(s => s.loadPointName == model.loadPointName).FirstOrDefaultAsync();
                var sitioDescargaExiste = await _context.Routes.Where(s => s.unLoadPointName == model.unLoadPointName).FirstOrDefaultAsync();
                var toneladasCorrectas = model.Weight > 0;
                var materialExiste = await _context.Materials.Where(m => m.materialTypeId == model.materialTypeId).FirstOrDefaultAsync();

                // Verificar si todos los datos necesarios son válidos
                if (vehiculoExiste != null && operadorExiste != null && sitioCargaExiste != null && sitioDescargaExiste != null && toneladasCorrectas && materialExiste != null)
                {
                    validHistoricos.Add(new Historic()
                    {
                        TokenRegistryId = model.TokenRegistryId,
                        vehicle = vehiculoExiste.EconomicNumber,
                        VehicleNavigation = vehiculoExiste, // Instancia completa de Vehicle
                        FullName = operadorExiste.FullName,
                        Employee = operadorExiste, // Instancia completa de Employee
                        loadPointName = sitioCargaExiste.loadPointName,
                        unLoadPointName = sitioDescargaExiste.unLoadPointName,
                        HaulagePath = sitioDescargaExiste, // Instancia completa de Route
                        Weight = model.Weight,
                        materialTypeId = materialExiste.materialTypeId,
                        MaterialType = materialExiste, // Instancia completa de Material
                        WorkShiftId = model.WorkShiftId,
                        WorkShift = await _context.Shifts.FirstOrDefaultAsync(s => s.WorkShiftId == model.WorkShiftId), // Instancia completa de Shift
                        Dateofcarries = model.Dateofcarries,
                        TokenRegistry = await _context.TokenRegistries.FirstOrDefaultAsync(t => t.TokenRegistryId == model.TokenRegistryId) // Instancia completa de TokenRegistry
                    });
                }
                else
                {
                    var errores = new List<string>();

                    // Agregar mensajes de error para cada dato inválido
                    if (vehiculoExiste == null) errores.Add($"Vehiculo '{model.vehicle}' no existe.");
                    if (operadorExiste == null) errores.Add($"Operador '{model.FullName}' no existe.");
                    if (sitioCargaExiste == null) errores.Add($"Sitio de carga '{model.loadPointName}' no existe.");
                    if (sitioDescargaExiste == null) errores.Add($"Sitio de descarga '{model.unLoadPointName}' no existe.");
                    if (toneladasCorrectas == false) errores.Add($"Toneladas '{model.Weight}' no es válido."); // Se cambió a `false` en la validación
                    if (materialExiste == null) errores.Add($"Material '{model.materialTypeId}' no existe.");

                    // Consolidar errores y agregarlos a la lista de errores
                    erroresConsolidados.AddRange(errores);

                    errorList.Add(new HisError
                    {
                        vehicle = model.vehicle,
                        FullName = model.FullName,
                        loadPointName = model.loadPointName,
                        unLoadPointName = model.unLoadPointName,
                        Weight = model.Weight,
                        materialTypeId = model.materialTypeId,
                        Dateofcarries = model.Dateofcarries,
                        ErrorMessage = string.Join("; ", errores)
                    });
                }
            }

            // Si hay historicos válidos, agregarlos a la base de datos
            if (validHistoricos.Any())
            {
                _context.Historics.AddRange(validHistoricos);
                await _context.SaveChangesAsync();
            }

            // Devolver una respuesta con el mensaje y los errores encontrados
            return Ok(new { Message = "Data import process completed.", Errors = errorList, ConsolidatedErrors = erroresConsolidados });
        }
    }
}
