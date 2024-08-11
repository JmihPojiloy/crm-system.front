using front.Models;
using front.Models.ContentModels;
using front.Models.CreateModels;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

namespace front.Services
{
    public class ContactService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public ContactService(HttpClient httpClient, IHttpContextAccessor contextAccessor) =>
            (_httpClient, _contextAccessor) = (httpClient, contextAccessor);

        public async Task<List<ContactInfoModel>> GetAllContactInfoAsync()
        {
            var response = await _httpClient.GetAsync("contact");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ContactInfoModel>>();
        }

        public async Task<ContactInfoModel> GetContactInfoByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"contact/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ContactInfoModel>();
        }

        public async Task AddContactInfoAsync(ContactInfoCreateModel contactInfo)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("contact", contactInfo);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteContactInfoAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"contact/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateContactInfoAsync(ContactInfoModel contactInfo)
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"contact/{contactInfo.Id}", contactInfo);
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
