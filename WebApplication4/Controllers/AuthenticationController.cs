// Controllers/AuthenticationController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApplication4.Models; // Asegúrate de tener el namespace correcto

public class AuthenticationController : Controller
{
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var tokenResponse = await _authenticationService.AuthenticateAsync(username, password);
        Console.WriteLine($"usernane:");
        if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.access_token))
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        // Guardar el token y el tipo de token en cookies
        HttpContext.Response.Cookies.Append("AuthToken", tokenResponse.access_token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        HttpContext.Response.Cookies.Append("TokenType", tokenResponse.token_type, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        // Imprimir los datos en la consola
        Console.WriteLine($"AccessToken: {tokenResponse.access_token}");
        Console.WriteLine($"TokenType: {tokenResponse.token_type}");
        Console.WriteLine($"RefreshToken: {tokenResponse.refresh_token}");

        // Redirigir a la página de inicio
        return RedirectToAction("gene", "Home");
    }
}
