// Services/AuthenticationService.cs
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication4.Models; // Asegúrate de tener el namespace correcto

public class AuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly RestClient _client;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
        var options = new RestClientOptions(_configuration["Authentication:ApiUrl"])
        {
            Timeout = Timeout.InfiniteTimeSpan
        };
        _client = new RestClient(options);
    }

    public async Task<AuthResponse> AuthenticateAsync(string username, string password)
    {
        var request = new RestRequest(_configuration["Authentication:ApiUrl"]+"/api/openid/connect/token", Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("client_id", _configuration["Authentication:ClientId"]);
        request.AddParameter("client_secret", _configuration["Authentication:ClientSecret"]);
        request.AddParameter("grant_type", "password");
        request.AddParameter("username", username);
        request.AddParameter("password", password);
        request.AddParameter("scope", "smartflow IdentityServerApi offline_access");

        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            return null;
        }

        return JsonSerializer.Deserialize<AuthResponse>(response.Content);
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var request = new RestRequest("/api/openid/connect/token", Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("client_id", _configuration["Authentication:ClientId"]);
        request.AddParameter("client_secret", _configuration["Authentication:ClientSecret"]);
        request.AddParameter("grant_type", "refresh_token");
        request.AddParameter("refresh_token", refreshToken);

        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            return null;
        }

        return JsonSerializer.Deserialize<AuthResponse>(response.Content);
    }
}
