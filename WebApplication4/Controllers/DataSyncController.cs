using Microsoft.AspNetCore.Mvc;

// Información adicional sobre habilitar Web API para proyectos vacíos está disponible en:
// https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    // Define la ruta base del controlador como "api/sync"
    [Route("api/sync")]
    [ApiController]
    public class DataSyncController : ControllerBase
    {
        // Campo estático que indica si la sincronización está habilitada o no
        private static bool _isSyncEnabled = false;

        // Método para manejar solicitudes POST en la ruta "api/sync/toggle"
        [HttpPost("toggle")]
        public IActionResult ToggleSync([FromBody] bool enable)
        {
            // Actualiza el estado de la sincronización basado en el valor recibido en el cuerpo de la solicitud
            _isSyncEnabled = enable;

            // Devuelve una respuesta HTTP 200 OK con el estado actual de la sincronización
            return Ok(new { SyncEnabled = _isSyncEnabled });
        }

        // Método estático para obtener el estado de la sincronización
        public static bool IsSyncEnabled() => _isSyncEnabled;
    }
}
    