    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WebApplication4.Data;
    using WebApplication4.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
            public async Task<IActionResult> Import([FromBody] List<Historic> historicoList)
            {
                if (historicoList == null || historicoList.Count == 0)
                {
                    return BadRequest("No data to import.");
                }

                var validHistoricos = new List<Historic>();
                var errorList = new List<HisError>();
                var erroresConsolidados = new List<string>();

                foreach (var model in historicoList)
                {
                bool vehiculoExiste = await _context.Vehicles.AnyAsync(v => v.VehicleId == model.VehicleId);
                bool operadorExiste = await _context.Employees.AnyAsync(o => o.EmployeeId == model.EmployeeId);
                bool sitioCargaExiste = await _context.Routes.AnyAsync(s => s.loadPointId == model.loadPointId);
                bool sitioDescargaExiste = await _context.Routes.AnyAsync(s => s.unLoadPointId == model.unLoadPointId);
                bool toneladasCorrectas = model.Weight > 0;
                bool materialExiste = await _context.Materials.AnyAsync(m => m.materialTypeId == model.materialTypeId);



                if (vehiculoExiste && operadorExiste && sitioCargaExiste && sitioDescargaExiste && toneladasCorrectas && materialExiste)
                    {
                        validHistoricos.Add(model);
                    }
                    else
                    {
                        var errores = new List<string>();

                        if (!vehiculoExiste) errores.Add($"Vehiculo '{model.VehicleId}' no existe.");
                        if (!operadorExiste) errores.Add($"Operador '{model.EmployeeId}' no existe.");
                        if (!sitioCargaExiste) errores.Add($"Sitio de carga '{model.loadPointId}' no existe.");
                        if (!sitioDescargaExiste) errores.Add($"Sitio de descarga '{model.unLoadPointId}' no existe.");
                        if (!toneladasCorrectas) errores.Add($"Toneladas '{model.Weight}' no es válido.");
                        if (!materialExiste) errores.Add($"Material '{model.materialTypeId}' no existe.");

                        erroresConsolidados.AddRange(errores);

                        errorList.Add(new HisError
                        {
                            VehicleId = model.VehicleId,
                            EmployeeId = model.EmployeeId,
                            loadPointId = model.loadPointId,
                            unLoadPointId = model.unLoadPointId,
                            Weight = model.Weight,
                            materialTypeId = model.materialTypeId,
                            Dateofcarries = model.Dateofcarries,
                            ErrorMessage = string.Join("; ", errores)
                        });
                    }
                }

                if (validHistoricos.Any())
                {
                    _context.Historics.AddRange(validHistoricos);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { Message = "Data import process completed.", Errors = errorList, ConsolidatedErrors = erroresConsolidados });
            }
        }
    }
