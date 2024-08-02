// Services/AuthenticationService.cs
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication4.Models; // Asegúrate de tener el namespace correcto

public class TokenService
{
    private string _token;

    public void SetToken(string token)
    {
        _token = token;
    }

    public string GetToken()
    {
        return _token;
    }
}
