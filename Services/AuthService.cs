using front.Models;

namespace front.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient) =>
            _httpClient = httpClient;

        public async Task<string> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", model);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (result != null && result.TryGetValue("token", out var token) && token != null)
            {
                return token;
            }

            throw new InvalidOperationException("Token not found in the response.");
        }
    }
}