using front.Models.ContentModels;
using front.Models.CreateModels;
using System.Net.Http.Headers;

namespace front.Services
{
    public class ProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProjectService(HttpClient httpClient, IHttpContextAccessor contextAccessor) =>
            (_httpClient, _contextAccessor) = (httpClient, contextAccessor);


        public async Task<List<ProjectModel>> GetAllProjectsAsync()
        {
            var response = await _httpClient.GetAsync("project");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProjectModel>>();
        }

        public async Task<ProjectModel> GetProjectByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"project/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProjectModel>();
        }

        public async Task AddProjectAsync(ProjectCreateModel post)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("project", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProjectAsync(ProjectModel post)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"project", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProjectAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"project/{id}");
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
