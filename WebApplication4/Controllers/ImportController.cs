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
                bool vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Numero_economico == model.Vehiculo);
                bool operadorExiste = await _context.Operadores.AnyAsync(o => o.Fullname.Contains(model.Operador));
                bool sitioCargaExiste = await _context.Rutas.AnyAsync(s => s.Sito_De_Carga == model.Sitio_de_carga);
                bool sitioDescargaExiste = await _context.Rutas.AnyAsync(s => s.Sito_De_Descarga == model.Sitio_de_descarga);
                bool toneladasExiste = await _context.Historicos.AnyAsync(s => s.Toneladas == model.Toneladas);
                bool materialExiste = await _context.Materiales.AnyAsync(m => m.Descripcion == model.Material);

                if (vehiculoExiste && operadorExiste && sitioCargaExiste && sitioDescargaExiste && materialExiste)
                {
                    validHistoricos.Add(model);
                }
                else
                {
                    var errores = new List<string>();

                    if (!vehiculoExiste) errores.Add($"Vehiculo '{model.Vehiculo}' no existe.");
                    if (!operadorExiste) errores.Add($"Operador '{model.Operador}' no existe.");
                    if (!sitioCargaExiste) errores.Add($"Sitio de carga '{model.Sitio_de_carga}' no existe.");
                    if (!sitioDescargaExiste) errores.Add($"Sitio de descarga '{model.Sitio_de_descarga}' no existe.");
                    if (!toneladasExiste) errores.Add($"Toneladas '{model.Toneladas}' no existe.");
                    if (!materialExiste) errores.Add($"Material '{model.Material}' no existe.");

                    erroresConsolidados.AddRange(errores); 

                    errorList.Add(new HisError
                    {
                        Vehiculo = model.Vehiculo,
                        Operador = model.Operador,
                        Sitio_de_carga = model.Sitio_de_carga,
                        Sitio_de_descarga = model.Sitio_de_descarga,
                        Toneladas = model.Toneladas,
                        Material = model.Material,
                        Fecha_de_acarreos = model.Fecha_de_acarreos,
                        Comentarios = model.Comentarios,
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
