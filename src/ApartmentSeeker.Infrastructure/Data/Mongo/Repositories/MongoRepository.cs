using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ApartmentScrapper.Infrastructure.Data.Mongo.Repositories
{
    public abstract class MongoRepository<T>
    {
        protected readonly DataContext _db;
        protected abstract string CollectionName { get; }

        protected MongoRepository(DataContext db)
        {
            _db = db;
        }

        protected IMongoCollection<T> Collection => _db.Collection<T>(CollectionName);
        protected IMongoQueryable<T> CollectionQuery => Collection.AsQueryable();
    }
}