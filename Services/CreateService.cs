using front.Models.CreateModels;

namespace front.Services
{
    public class CreateService
    {
        private readonly HttpClient _httpclient;
        public CreateService(HttpClient httpClient) =>
            _httpclient = httpClient;

        public async Task CreateLeadAsync(LeadCreateModel model)
        {
            var response = await _httpclient.PostAsJsonAsync("lead", model);
            response.EnsureSuccessStatusCode();
        }
    }
}