namespace WebApplication4.Models
{
    // Clase que representa la respuesta de autenticación
    public class AuthResponse
    {
        // Token de acceso proporcionado tras la autenticación
        public string access_token { get; set; }

        // Tipo de token (por ejemplo, "Bearer")
        public string token_type { get; set; }

        // Token de actualización utilizado para obtener un nuevo token de acceso cuando el actual expira
        public string refresh_token { get; set; }
    }
}
