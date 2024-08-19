using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Historicos.Controllers
{
    public class ControlController : Controller
    {
        // Inyecci�n de dependencias para el ciclo de vida de la aplicaci�n y el host
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IHost _host;

        // Constructor que recibe el ciclo de vida de la aplicaci�n y el host a trav�s de inyecci�n de dependencias
        public ControlController(IHostApplicationLifetime lifetime, IHost host)
        {
            _lifetime = lifetime;
            _host = host;
        }

        // M�todo para manejar solicitudes GET a la ruta "Index"
        public IActionResult Index()
        {
            // Devuelve la vista de �ndice
            return View();
        }

        // M�todo para manejar solicitudes GET a la ruta "Start"
        public async Task<IActionResult> Start()
        {
            // L�gica para iniciar el servicio
            // No es necesario hacer nada, ya que el servicio se inicia autom�ticamente al iniciar la aplicaci�n
            // El comentario indica que el servicio se inicia autom�ticamente con la aplicaci�n
            return RedirectToAction("Index");
        }

        // M�todo para manejar solicitudes GET a la ruta "Stop"
        public async Task<IActionResult> Stop()
        {
            // L�gica para detener el servicio
            // Llama a StopAsync en el host para detener la aplicaci�n
            await _host.StopAsync();

            // Redirige de vuelta a la p�gina de �ndice
            return RedirectToAction("Index");
        }
    }
}
