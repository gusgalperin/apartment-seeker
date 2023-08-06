using MongoDB.Driver;

namespace ApartmentScrapper.Infrastructure.Data.Mongo
{
    public class DataContext
    {
        private readonly IMongoDatabase _db;

        public DataContext(MongoClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            _db = client.GetDatabase("ApartmentsDb");
        }

        public IMongoCollection<T> Collection<T>(string name)
            => _db.GetCollection<T>(name);
    }
}