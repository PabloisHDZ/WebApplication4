using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa un registro de token de autenticaci�n
    public class TokenRegistry
    {
        // Propiedad que act�a como la clave primaria de la entidad TokenRegistry
        [Key]
        public int TokenRegistryId { get; set; }

        // Token de acceso utilizado para autenticar las solicitudes a la API
        public string access_token { get; set; }

        // Tipo del token (por ejemplo, "Bearer")
        public string token_type { get; set; }

        // Token de actualizaci�n utilizado para obtener un nuevo token de acceso
        public string refresh_token { get; set; }
    }
}
