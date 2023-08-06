using System.Text.Json;

namespace ApartmentScrapper.Utils.RestClient
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url)
            where T : new();
    }

    public class RestClient : IRestClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public RestClient(
            IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<T> GetAsync<T>(string url)
            where T : new()
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(
                HttpMethod.Get, url);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var responseModel = JsonSerializer.Deserialize<T>(responseString);

            return responseModel ?? new T();
        }
    }
}