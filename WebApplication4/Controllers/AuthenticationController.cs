using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApplication4.Services;
using WebApplication4.Models; // Asegúrate de tener el namespace correcto

public class AuthenticationController : Controller
{
    // Servicios necesarios para la autenticación y el manejo de tokens
    private readonly AuthenticationService _authenticationService;
    private readonly TokenService _tokenService;

    // Constructor que recibe las dependencias necesarias a través de inyección de dependencias
    public AuthenticationController(AuthenticationService authenticationService, TokenService tokenService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
    }

    // Método para manejar solicitudes GET a la ruta "Login"
    [HttpGet]
    public IActionResult Login()
    {
        // Devuelve la vista de inicio de sesión
        return View();
    }

    // Método para manejar solicitudes POST a la ruta "Login"
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Llama al servicio de autenticación para obtener un token de acceso
        var tokenResponse = await _authenticationService.AuthenticateAsync(username, password);

        // Verifica si la respuesta del token es nula o si el token de acceso está vacío
        if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.access_token))
        {
            // Almacena un mensaje de error temporalmente y redirige de vuelta a la página de inicio de sesión
            TempData["ErrorMessage"] = "Invalid login attempt.";
            return RedirectToAction("Login");
        }

        // Guarda el token en el TokenService y en las cookies
        _tokenService.SetToken(tokenResponse.access_token);

        // Configura una cookie para el token de acceso
        HttpContext.Response.Cookies.Append("AuthToken", tokenResponse.access_token, new CookieOptions
        {
            HttpOnly = true,  // La cookie no es accesible a través de JavaScript
            Secure = true,    // La cookie solo se envía a través de conexiones HTTPS
            SameSite = SameSiteMode.Strict  // La cookie solo se envía en solicitudes del mismo sitio
        });

        // Configura una cookie para el tipo de token
        HttpContext.Response.Cookies.Append("TokenType", tokenResponse.token_type, new CookieOptions
        {
            HttpOnly = true,  // La cookie no es accesible a través de JavaScript
            Secure = true,    // La cookie solo se envía a través de conexiones HTTPS
            SameSite = SameSiteMode.Strict  // La cookie solo se envía en solicitudes del mismo sitio
        });

        // Imprime los datos del token en la consola para depuración
        Console.WriteLine($"AccessToken: {tokenResponse.access_token}");
        Console.WriteLine($"TokenType: {tokenResponse.token_type}");
        Console.WriteLine($"RefreshToken: {tokenResponse.refresh_token}");

        // Redirige a la página de inicio o a otra acción después de la autenticación exitosa
        return RedirectToAction("gene", "Home");
    }
}
