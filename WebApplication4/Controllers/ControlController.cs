using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Historicos.Controllers
{
    public class ControlController : Controller
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IHost _host;

        public ControlController(IHostApplicationLifetime lifetime, IHost host)
        {
            _lifetime = lifetime;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Start()
        {
            // Lógica para iniciar el servicio
            // No es necesario hacer nada, ya que el servicio se inicia automáticamente al iniciar la aplicación
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Stop()
        {
            // Lógica para detener el servicio
            _host.StopAsync();
            return RedirectToAction("Index");
        }
    }
}
