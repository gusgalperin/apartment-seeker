using ApartmentScrapper.Domain.Data.Repositories;
using ApartmentScrapper.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SharpCompress.Common;

namespace ApartmentScrapper.Infrastructure.Data.Mongo.Repositories
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(DataContext db) : base(db)
        {
        }

        protected override string CollectionName => "users";

        public async Task AddAsync(User user)
        {
            await Collection.InsertOneAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await CollectionQuery.ToListAsync();
        }

        public async Task<User> GetOrDefaultAsync(long chatId)
        {
            return await CollectionQuery.FirstOrDefaultAsync(x => x.ChatId == chatId);
        }

        public async Task UpdateAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq(x => x.ChatId, user.ChatId);
            var update = Builders<User>.Update
                .Set(x => x.IsRunning, user.IsRunning);

            await Collection.UpdateOneAsync(
                filter,
                update);
        }
    }
}