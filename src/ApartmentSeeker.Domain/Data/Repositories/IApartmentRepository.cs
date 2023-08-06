using ApartmentScrapper.Entities;

namespace ApartmentScrapper.Domain.Data.Repositories
{
    public interface IApartmentRepository
    {
        Task AddAsync(Apartment apartment);

        Task<Apartment> GetOneAsync(string id);

        Task<IEnumerable<Apartment>> GetAllAsync();
    }
}