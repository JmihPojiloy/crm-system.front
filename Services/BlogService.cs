using front.Models.CreateModels;
using front.Models.ContentModels;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;

namespace front.Services
{
    public class BlogService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public BlogService(HttpClient httpClient, IHttpContextAccessor contextAccessor) =>
            (_httpClient, _contextAccessor) = (httpClient, contextAccessor);

        public async Task<List<BlogPostModel>> GetBlogPostsAsync()
        {
            var response = await _httpClient.GetAsync("blog");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<BlogPostModel>>();
        }

        public async Task<BlogPostModel> GetBlogPostByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"blog/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BlogPostModel>();
        }

        public async Task AddBlogPostAsync(BlogPostCreateModel post)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("blog", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateBlogPostAsync(BlogPostModel post)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"blog", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBlogPostAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"blog/{id}");
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
