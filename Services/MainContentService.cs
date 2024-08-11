using front.Models.ContentModels;
using front.Models.CreateModels;
using System.Net.Http.Headers;

namespace front.Services
{
    public class MainContentService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public MainContentService(HttpClient httpClient, IHttpContextAccessor contextAccessor) =>
            (_httpClient, _contextAccessor) = (httpClient, contextAccessor);

        public async Task<List<MainContentModel>> GetAllMainContentAsync()
        {
            var response = await _httpClient.GetAsync("content");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<MainContentModel>>();
        }

        public async Task<MainContentModel> GetMainContentByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"content/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MainContentModel>();
        }

        public async Task AddMainContentAsync(MainContentCreateModel contactInfo)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("content", contactInfo);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMainContentAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"content/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateMainContentAsync(MainContentModel contactInfo)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"content/{contactInfo.Id}", contactInfo);
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
