// Services/AuthenticationService.cs
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication4.Models; // Asegúrate de tener el namespace correcto para el modelo AuthResponse

public class AuthenticationService
{
    private readonly IConfiguration _configuration; // Campo para acceder a las configuraciones de la aplicación
    private readonly RestClient _client; // Cliente RestSharp para realizar solicitudes HTTP

    // Constructor que inyecta las configuraciones de la aplicación
    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;

        // Configura el cliente RestSharp con la URL base y un timeout infinito
        var options = new RestClientOptions(_configuration["Authentication:ApiUrl"])
        {
            Timeout = Timeout.InfiniteTimeSpan // Sin límite de tiempo para las solicitudes
        };
        _client = new RestClient(options); // Inicializa el cliente RestSharp con las opciones configuradas
    }

    // Método asíncrono para autenticar al usuario y obtener un token
    public async Task<AuthResponse> AuthenticateAsync(string username, string password)
    {
        // Configura la solicitud HTTP POST para obtener un token de autenticación
        var request = new RestRequest(_configuration["Authentication:ApiUrl"] + "/api/openid/connect/token", Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded"); // Establece el encabezado de tipo de contenido
        request.AddParameter("client_id", _configuration["Authentication:ClientId"]); // Añade el parámetro client_id
        request.AddParameter("client_secret", _configuration["Authentication:ClientSecret"]); // Añade el parámetro client_secret
        request.AddParameter("grant_type", "password"); // Establece el tipo de grant como "password"
        request.AddParameter("username", username); // Añade el nombre de usuario
        request.AddParameter("password", password); // Añade la contraseña
        request.AddParameter("scope", "smartflow IdentityServerApi offline_access"); // Añade los scopes necesarios

        // Ejecuta la solicitud y espera la respuesta
        var response = await _client.ExecuteAsync(request);

        // Verifica si la respuesta fue exitosa
        if (!response.IsSuccessful)
        {
            return null; // Si la solicitud falló, devuelve null
        }

        // Deserializa y devuelve el contenido de la respuesta como un objeto AuthResponse
        return JsonSerializer.Deserialize<AuthResponse>(response.Content);
    }

    // Método asíncrono para refrescar el token usando un refresh token
    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        // Configura la solicitud HTTP POST para refrescar el token de autenticación
        var request = new RestRequest("/api/openid/connect/token", Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded"); // Establece el encabezado de tipo de contenido
        request.AddParameter("client_id", _configuration["Authentication:ClientId"]); // Añade el parámetro client_id
        request.AddParameter("client_secret", _configuration["Authentication:ClientSecret"]); // Añade el parámetro client_secret
        request.AddParameter("grant_type", "refresh_token"); // Establece el tipo de grant como "refresh_token"
        request.AddParameter("refresh_token", refreshToken); // Añade el refresh token

        // Ejecuta la solicitud y espera la respuesta
        var response = await _client.ExecuteAsync(request);

        // Verifica si la respuesta fue exitosa
        if (!response.IsSuccessful)
        {
            return null; // Si la solicitud falló, devuelve null
        }

        // Deserializa y devuelve el contenido de la respuesta como un objeto AuthResponse
        return JsonSerializer.Deserialize<AuthResponse>(response.Content);
    }
}
