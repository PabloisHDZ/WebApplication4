using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    // Se define el controlador como un API Controller y se establece la ruta base "api/[controller]"
    [ApiController]
    [Route("api/[controller]")]
    public class HaulagesController : ControllerBase
    {
        // Contexto de la base de datos para interactuar con la base de datos
        private readonly dbboot _context;

        // Constructor que recibe el contexto de la base de datos a través de inyección de dependencias
        public HaulagesController(dbboot context)
        {
            _context = context;
        }

        // Método para manejar las solicitudes GET en la ruta base "api/haulages"
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Haulage>>> GetHaulages()
        {
            // Obtiene todos los registros de la entidad Haulage desde la base de datos y los devuelve como una lista
            return await _context.Haulages.ToListAsync();
        }
    }
}
