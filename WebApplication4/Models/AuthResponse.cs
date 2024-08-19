namespace WebApplication4.Models
{
    // Clase que representa la respuesta de autenticaci�n
    public class AuthResponse
    {
        // Token de acceso proporcionado tras la autenticaci�n
        public string access_token { get; set; }

        // Tipo de token (por ejemplo, "Bearer")
        public string token_type { get; set; }

        // Token de actualizaci�n utilizado para obtener un nuevo token de acceso cuando el actual expira
        public string refresh_token { get; set; }
    }
}
