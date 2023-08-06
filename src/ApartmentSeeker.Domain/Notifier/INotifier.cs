using ApartmentScrapper.Entities;

namespace ApartmentScrapper.Domain.Notifier
{
    public interface INotifier
    {
        Task NotifyAsync(User user, Apartment apartment);
        Task SendHelpAsync(User user);
    }
}