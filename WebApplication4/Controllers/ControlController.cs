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
            // L�gica para iniciar el servicio
            // No es necesario hacer nada, ya que el servicio se inicia autom�ticamente al iniciar la aplicaci�n
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Stop()
        {
            // L�gica para detener el servicio
            _host.StopAsync();
            return RedirectToAction("Index");
        }
    }
}
