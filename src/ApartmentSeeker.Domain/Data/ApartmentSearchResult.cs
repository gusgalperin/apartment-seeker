namespace ApartmentScrapper.Domain.Data
{
    public class ApartmentSearchResult
    {
        public string Source { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Permalink { get; set; }

        public string Thumbnail { get; set; }

        public string Currency { get; set; }

        public decimal Price { get; set; }

        public decimal CoveredArea { get; set; }

        public int RoomsCount { get; set; }

        public string Neighborhood { get; set; }
    }
}