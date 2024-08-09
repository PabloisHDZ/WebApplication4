using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HaulagesController : ControllerBase
    {
        private readonly dbboot _context;

        public HaulagesController(dbboot context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Haulage>>> GetHaulages()
        {
            return await _context.Haulages.ToListAsync();
        }
    }
}
