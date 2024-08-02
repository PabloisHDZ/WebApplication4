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
        public async Task<IActionResult> Import([FromBody] List<Historico> historicoList)
        {
            if (historicoList == null || historicoList.Count == 0)
            {
                return BadRequest("No data to import.");
            }

            var validHistoricos = new List<Historico>();
            var errorList = new List<HisError>();
            var erroresConsolidados = new List<string>();

            foreach (var model in historicoList)
            {
                bool vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.EconomicNumber == model.Vehicle);
                bool operadorExiste = await _context.Operadores.AnyAsync(o => o.FullName == model.FullName);
                bool sitioCargaExiste = await _context.Sitios.AnyAsync(s => s.Name == model.LoadPointName);
                bool sitioDescargaExiste = await _context.Sitios.AnyAsync(s => s.Name == model.UnLoadPointName);
                bool toneladasCorrectas = model.Weight > 0;
                bool materialExiste = await _context.Materiales.AnyAsync(m => m.Name == model.Name);

                if (vehiculoExiste && operadorExiste && sitioCargaExiste && sitioDescargaExiste && toneladasCorrectas && materialExiste)
                {
                    validHistoricos.Add(model);
                }
                else
                {
                    var errores = new List<string>();

                    if (!vehiculoExiste) errores.Add($"Vehiculo '{model.Vehicle}' no existe.");
                    if (!operadorExiste) errores.Add($"Operador '{model.FullName}' no existe.");
                    if (!sitioCargaExiste) errores.Add($"Sitio de carga '{model.LoadPointName}' no existe.");
                    if (!sitioDescargaExiste) errores.Add($"Sitio de descarga '{model.UnLoadPointName}' no existe.");
                    if (!toneladasCorrectas) errores.Add($"Toneladas '{model.Weight}' no es válido.");
                    if (!materialExiste) errores.Add($"Material '{model.Name}' no existe.");

                    erroresConsolidados.AddRange(errores);

                    errorList.Add(new HisError
                    {
                        Vehicle = model.Vehicle,
                        FullName = model.FullName,
                        LoadPointName = model.LoadPointName,
                        UnLoadPointName = model.UnLoadPointName,
                        Weight = model.Weight,
                        Name = model.Name,
                        DateOfCarries = model.DateOfCarries,
                        Comments = model.Comments,
                        ErrorMessage = string.Join("; ", errores)
                    });
                }
            }

            if (validHistoricos.Any())
            {
                _context.Historicos.AddRange(validHistoricos);
                await _context.SaveChangesAsync();
            }

            return Ok(new { Message = "Data import process completed.", Errors = errorList, ConsolidatedErrors = erroresConsolidados });
        }
    }
}
