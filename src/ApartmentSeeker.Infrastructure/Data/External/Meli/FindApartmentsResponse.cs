using ApartmentScrapper.Domain.Data;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ApartmentScrapper.Infrastructure.Data.External.Meli
{
    public class FindApartmentsResponse
    {
        [JsonPropertyName("paging")]
        public FindApartmentsResponsePaging Paging { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<FindApartmentsResponseResult> Results { get; set; }

        public FindApartmentsResponse()
        {
            Paging = new FindApartmentsResponsePaging
            {
                Total = 0
            };

            Results = new List<FindApartmentsResponseResult>();
        }
    }

    public class FindApartmentsResponsePaging
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class FindApartmentsResponseResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("permalink")]
        public string Permalink { get; set; }

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonPropertyName("currency_id")]
        public string Currency { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("attributes")]
        public IEnumerable<FindApartmentsResponseAttribute> Attributes { get; set; }

        [JsonPropertyName("location")]
        public FindApartmentsResponseLocation Location { get; set; }

        public ApartmentSearchResult ToDomain()
        {
            var coveredAreaAttr = Attributes.FirstOrDefault(x => x.Id == "COVERED_AREA");
            var roomsAttr = Attributes.FirstOrDefault(x => x.Id == "ROOMS");

            return new ApartmentSearchResult
            {
                Source = "Meli",
                Id = Id,
                Title = Title,
                Currency = Currency,
                Price = Price,
                Permalink = Permalink,
                Thumbnail = Thumbnail,
                Neighborhood = Location.Neighborhood.Name,
                CoveredArea = coveredAreaAttr?.ValueName?.GetNumericPart() ?? 0,
                RoomsCount = (int)(roomsAttr?.ValueName.GetNumericPart() ?? 0)
            };
        }
    }

    public class FindApartmentsResponseAttribute
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("value_name")]
        public string ValueName { get; set; }
    }

    public class FindApartmentsResponseLocation
    {
        [JsonPropertyName("address_line")]
        public string AddressLine { get; set; }

        [JsonPropertyName("neighborhood")]
        public FindApartmentsResponseLocationNeighborhood Neighborhood { get; set; }
    }

    public class FindApartmentsResponseLocationNeighborhood
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}