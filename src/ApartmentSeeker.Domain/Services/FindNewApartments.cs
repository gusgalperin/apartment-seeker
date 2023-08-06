using ApartmentScrapper.Domain.Data;
using ApartmentScrapper.Domain.Data.Repositories;
using ApartmentScrapper.Entities;

namespace ApartmentScrapper.Domain.Services
{
    public interface IFindNewApartments
    {
        Task FindAsync(User user);
    }

    public class FindNewApartments : IFindNewApartments
    {
        private readonly IEnumerable<IFindApartments> _apartmentFinders;
        private readonly IEvaluateApartment _evaluateApartment;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly INotifyNewApartment _notifyNewApartment;

        public FindNewApartments(
            IEnumerable<IFindApartments> apartmentFinders,
            IEvaluateApartment evaluateApartment,
            IApartmentRepository apartmentRepository,
            INotifyNewApartment notifyNewApartment)
        {
            _apartmentFinders = apartmentFinders ?? throw new ArgumentNullException(nameof(apartmentFinders));
            _evaluateApartment = evaluateApartment ?? throw new ArgumentNullException(nameof(evaluateApartment));
            _apartmentRepository = apartmentRepository ?? throw new ArgumentNullException(nameof(apartmentRepository));
            _notifyNewApartment = notifyNewApartment ?? throw new ArgumentNullException(nameof(notifyNewApartment));
        }

        public async Task FindAsync(User user)
        {
            var apartments = new List<ApartmentSearchResult>();

            var tasks = new List<Task<IEnumerable<ApartmentSearchResult>>>();

            foreach (var aptFinder in _apartmentFinders)
            {
                tasks.Add(aptFinder.FindAsync());
            }

            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                apartments.AddRange(task.Result);
            }

            if (!apartments.Any())
                return;

            foreach (var apt in apartments)
            {
                var isGood = _evaluateApartment.Evaluate(apt);

                if (!isGood)
                    continue;

                var e = new Apartment(apt.Source, apt.Id, apt.Title, apt.Permalink, apt.Thumbnail, apt.Price);

                try
                {
                    await _apartmentRepository.AddAsync(e);
                    await _notifyNewApartment.NotifyAsync(user, e);
                }
                catch (Exception)
                { }
            }
        }
    }
}