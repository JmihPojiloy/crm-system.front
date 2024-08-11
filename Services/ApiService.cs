using System.Net.Http.Headers;
using front.Models;

namespace front.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            SetAuthorizationHeader();
        }


        public async Task<List<LeadModel>> GetLeadsAsync()
        {
            var response = await _httpClient.GetAsync("lead");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LeadModel>>();
        }

        public async Task<LeadModel> GetLeadByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"lead/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LeadModel>();
        }

        public async Task UpdateLeadAsync(int id, LeadModel lead)
        {
            var response = await _httpClient.PutAsJsonAsync($"lead/{id}", lead);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLeadAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"lead/{id}");
            response.EnsureSuccessStatusCode();
        }
        private void SetAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}