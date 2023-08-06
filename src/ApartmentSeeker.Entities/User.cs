namespace ApartmentScrapper.Entities
{
    public class User
    {
        public User(long chatId, SearchCriteria searchCriteria)
        {
            ChatId = chatId;
            SearchCriteria = searchCriteria;
            IsRunning = false;
        }

        public string Id { get; private set; }
        public long ChatId { get; private set; }
        public bool IsRunning { get; private set; }
        public SearchCriteria SearchCriteria { get; private set; }

        public void Run()
        {
            IsRunning = true;
        }

        public void NoRun()
        {
            IsRunning = false;
        }
    }

    public class SearchCriteria
    {
        public SearchCriteria(IEnumerable<string> acceptedNeighbourhoods, string currency, decimal price, decimal coveredArea, int roomsCount)
        {
            AcceptedNeighbourhoods = acceptedNeighbourhoods;
            Currency = currency;
            Price = price;
            CoveredArea = coveredArea;
            RoomsCount = roomsCount;
        }

        public IEnumerable<string> AcceptedNeighbourhoods { get; private set; }
        public string Currency { get; private set; }
        public decimal Price { get; private set; }
        public decimal CoveredArea { get; private set; }
        public int RoomsCount { get; private set; }

        public static SearchCriteria Default()
        {
            return new SearchCriteria(
                acceptedNeighbourhoods: new[]
                {
                    "Almagro",
                    "Caballito",
                    "Recoleta",
                    "Palermo",
                    "Barrio Norte",
                    "Palermo Hollywood",
                    "Villa Crespo",
                    "Colegiales",
                    "Palermo Chico",
                    "Palermo Soho",
                    "Chacarita",
                    "Parque Centenario",
                },
                currency: "ARS",
                price: 400000,
                coveredArea: 65,
                roomsCount: 3);
        }
    }
}