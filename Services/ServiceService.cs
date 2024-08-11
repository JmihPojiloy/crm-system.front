using front.Models.ContentModels;
using front.Models.CreateModels;
using System.Net.Http.Headers;

namespace front.Services
{
    public class ServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public ServiceService(HttpClient httpClient, IHttpContextAccessor contextAccessor) =>
            (_httpClient, _contextAccessor) = (httpClient, contextAccessor);


        public async Task<List<ServiceModel>> GetAllServicesAsync()
        {
            var response = await _httpClient.GetAsync("service");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ServiceModel>>();
        }

        public async Task<ServiceModel> GetServiceByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"service/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceModel>();
        }

        public async Task AddServiceAsync(ServiceCreateModel post)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("service", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateServiceAsync(ServiceModel post)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"service", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteServiceAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"service/{id}");
            response.EnsureSuccessStatusCode();
        }

        private void SetAuthorizationHeader()
        {
            var token = _contextAccessor.HttpContext?.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
