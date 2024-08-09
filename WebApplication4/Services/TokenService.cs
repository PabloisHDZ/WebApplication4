using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;



namespace WebApplication4.Services
{
    public class TokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private string _token;
        private DateTime _tokenExpiry;


        public void SetToken(string token)
        {
            _token = token;
            _tokenExpiry = DateTime.Now.AddMinutes(60); // Establece un tiempo de expiración por defecto
        }


        public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetTokenAsync()
        {
            if (string.IsNullOrEmpty(_token) || _tokenExpiry <= DateTime.Now)
            {
                await RefreshTokenAsync();
            }

            return _token;
        }

        private async Task RefreshTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _configuration["Authentication:ApiUrl"];
            var tokenEndpoint = $"{apiUrl}/path/to/token/endpoint";

            var response = await client.PostAsync(tokenEndpoint, null);
            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _token = tokenResponse.AccessToken;
            _tokenExpiry = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn - 60); // Ajusta según el tiempo de expiración del token
        }

        private class TokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }
        }
    }
}
