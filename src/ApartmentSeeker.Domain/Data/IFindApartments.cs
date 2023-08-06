namespace ApartmentScrapper.Domain.Data
{
    public interface IFindApartments
    {
        Task<IEnumerable<ApartmentSearchResult>> FindAsync();
    }
}