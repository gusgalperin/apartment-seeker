namespace ApartmentScrapper.Entities
{
    public class Apartment
    {
        public Apartment(string source, string id, string title, string permalink, string thumbnail, decimal price)
        {
            Source = source;
            Id = id;
            Title = title;
            Permalink = permalink;
            Thumbnail = thumbnail;
            Price = price;
            CreatedOn = DateTime.Now;
        }

        public string Source { get;private set; }
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Permalink { get; private set; }
        public string Thumbnail { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedOn { get; private set; }
    }
}