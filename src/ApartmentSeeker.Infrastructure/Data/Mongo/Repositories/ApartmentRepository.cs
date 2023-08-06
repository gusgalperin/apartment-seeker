using Amazon.Runtime.Internal;
using ApartmentScrapper.Domain.Data.Repositories;
using ApartmentScrapper.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ApartmentScrapper.Infrastructure.Data.Mongo.Repositories
{
    public class ApartmentRepository : MongoRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(DataContext db)
            : base(db)
        { }

        protected override string CollectionName => "apartments";

        public async Task AddAsync(Apartment apartment)
        {
            await Collection.InsertOneAsync(apartment);
        }

        public async Task<IEnumerable<Apartment>> GetAllAsync()
        {
            return await CollectionQuery.ToListAsync();
        }

        public async Task<Apartment> GetOneAsync(string id)
        {
            return await CollectionQuery.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}