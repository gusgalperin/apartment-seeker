using ApartmentScrapper.Domain.Notifier;
using ApartmentScrapper.Entities;

namespace ApartmentScrapper.Domain.Services
{
    public interface INotifyNewApartment
    {
        Task NotifyAsync(User user, Apartment apartment);
    }

    public class NotifyNewApartment : INotifyNewApartment
    {
        private readonly INotifier _notifier;

        public NotifyNewApartment(INotifier notifier)
        {
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        public async Task NotifyAsync(User user, Apartment apartment)
        {
            await _notifier.NotifyAsync(user, apartment);
        }
    }
}