namespace ApartmentScrapper.Domain.Data.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.User> GetOrDefaultAsync(long chatId);
        Task AddAsync(Entities.User user);
        Task UpdateAsync(Entities.User user);
        Task<IEnumerable<Entities.User>> GetAllAsync();
    }
}