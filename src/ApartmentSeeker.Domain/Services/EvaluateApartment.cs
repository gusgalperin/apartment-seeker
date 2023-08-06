using ApartmentScrapper.Domain.Data;
using ApartmentScrapper.Utils.Logger;

namespace ApartmentScrapper.Domain.Services
{
    public interface IEvaluateApartment
    {
        bool Evaluate(ApartmentSearchResult apartment);
    }

    public class EvaluateApartment : IEvaluateApartment
    {
        private readonly ILogger<EvaluateApartment> _logger;

        public EvaluateApartment(ILogger<EvaluateApartment> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private static IEnumerable<string> _acceptedNeighbourhoods => new[]
        {
            "Almagro",
            "Caballito",
            "Recoleta",
            "Palermo",
            "Barrio Norte",
            "Palermo Hollywood",
            "Villa Crespo",
            "Colegiales",
            "Palermo Chico",
            "Palermo Soho",
            "Chacarita",
            "Parque Centenario",
        };

        public bool Evaluate(ApartmentSearchResult apartment)
        {
            _logger.WithField("id", apartment.Id);
            _logger.WithField("from", apartment.Source);

            _logger.LogDebug("started evaluation");

            if (apartment.Currency == "USD")
            {
                _logger.LogInfo("currency is USD");
                return false;
            }

            if (apartment.Price > 350000)
            {
                _logger.LogInfo("price is too high: " + apartment.Price);
                return false;
            }

            if (!_acceptedNeighbourhoods.Contains(apartment.Neighborhood))
            {
                _logger.LogInfo("wrong neighborhood: " + apartment.Neighborhood);
                return false;
            }

            if (apartment.CoveredArea < 60)
            {
                _logger.LogInfo("too small in meters: " + apartment.CoveredArea);
                return false;
            }

            if (apartment.RoomsCount < 3)
            {
                _logger.LogInfo("too small in rooms: " + apartment.RoomsCount);
                return false;
            }

            return true;
        }
    }
}