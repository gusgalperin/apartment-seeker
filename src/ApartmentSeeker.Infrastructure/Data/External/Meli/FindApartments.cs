using ApartmentScrapper.Domain.Data;
using ApartmentScrapper.Utils.RestClient;

namespace ApartmentScrapper.Infrastructure.Data.External.Meli
{
    public class FindApartments : IFindApartments
    {
        private readonly IRestClient _restClient;

        public FindApartments(
            IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task<IEnumerable<ApartmentSearchResult>> FindAsync()
        {
            var result = new List<FindApartmentsResponseResult>();

            var page = 0;
            var response = await FindPageAsync(page);

            while (response != null && response.Paging.Total > 0 && response.Results.Any())
            {
                page++;

                result.AddRange(response.Results);

                response = await FindPageAsync(page);
            }

            return result.Select(x => x.ToDomain()).ToList();
        }

        public async Task<FindApartmentsResponse> FindPageAsync(int pageNumber = 0)
        {
            var limit = 50;
            var offset = pageNumber * limit;

            var url = @$"https://api.mercadolibre.com/sites/MLA/search?category=MLA1459&since=today&OPERATION=242073&state=TUxBUENBUGw3M2E1&offset={offset}&limit={limit}";

            return await _restClient.GetAsync<FindApartmentsResponse>(url);
        }
    }
}