using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WebApplication4.Services
{
    // La clase TokenService se encarga de manejar la obtención y renovación de tokens de autenticación.
    public class TokenService
    {
        private readonly IHttpClientFactory _httpClientFactory; // Fábrica para crear instancias de HttpClient.
        private readonly IConfiguration _configuration; // Proporciona acceso a la configuración de la aplicación.
        private string _token; // Almacena el token actual.
        private DateTime _tokenExpiry; // Almacena la fecha y hora en que expira el token.

        // Método para establecer manualmente un token y su tiempo de expiración.
        public void SetToken(string token)
        {
            _token = token;
            _tokenExpiry = DateTime.Now.AddMinutes(60); // Establece un tiempo de expiración por defecto de 60 minutos desde el momento actual.
        }

        // Constructor de la clase TokenService que inyecta las dependencias necesarias.
        public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // Método para obtener el token actual. Si el token es nulo o ha expirado, se renueva.
        public async Task<string> GetTokenAsync()
        {
            if (string.IsNullOrEmpty(_token) || _tokenExpiry <= DateTime.Now)
            {
                // Si no hay un token o si el token ha expirado, se llama a RefreshTokenAsync para obtener un nuevo token.
                await RefreshTokenAsync();
            }

            return _token; // Devuelve el token actual.
        }

        // Método privado que se encarga de solicitar un nuevo token a la API.
        private async Task RefreshTokenAsync()
        {
            var client = _httpClientFactory.CreateClient(); // Crea una instancia de HttpClient.
            var apiUrl = _configuration["Authentication:ApiUrl"]; // Obtiene la URL base de la API desde la configuración.
            var tokenEndpoint = $"{apiUrl}/path/to/token/endpoint"; // Construye la URL completa para obtener el token.

            var response = await client.PostAsync(tokenEndpoint, null); // Realiza una solicitud POST para obtener el token.
            response.EnsureSuccessStatusCode(); // Lanza una excepción si la solicitud no fue exitosa.

            // Deserializa la respuesta en un objeto TokenResponse.
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _token = tokenResponse.AccessToken; // Almacena el nuevo token.
            _tokenExpiry = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn - 60); // Ajusta el tiempo de expiración del token (restando 60 segundos como margen).
        }

        // Clase interna que representa la respuesta de la API que contiene el token y su tiempo de expiración.
        private class TokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; } // El token de acceso.

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; } // El tiempo en segundos antes de que expire el token.
        }
    }
}
