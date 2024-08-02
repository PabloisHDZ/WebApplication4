using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApplication4.Models; // Asegúrate de tener el namespace correcto

public class AuthenticationController : Controller
{
    private readonly AuthenticationService _authenticationService;
    private readonly TokenService _tokenService;

    public AuthenticationController(AuthenticationService authenticationService, TokenService tokenService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
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

        if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.access_token))
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return RedirectToAction("Index", "Home"); // Redirigir a una vista de error
        }

        // Guardar el token en el TokenService
        _tokenService.SetToken(tokenResponse.access_token);

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
