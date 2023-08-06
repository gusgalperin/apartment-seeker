using ApartmentScrapper.Domain.Data.Repositories;
using ApartmentScrapper.Utils.Logger;

namespace ApartmentScrapper.Domain.Services
{
    public interface IRunner
    {
        Task RunAsync();
    }

    public class Runner : IRunner
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<Runner> _logger;
        private readonly IFindNewApartments _findNewApartments;

        public Runner(
            IUserRepository userRepository,
            ILogger<Runner> logger,
            IFindNewApartments findNewApartments)
        {
            _userRepository = userRepository;
            _logger = logger;
            _findNewApartments = findNewApartments;
        }

        public async Task RunAsync()
        {
            //var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            //var timer = new PeriodicTimer(TimeSpan.FromSeconds(15));
            //var user = await _userRepository.GetOrDefaultAsync(chatId);


            //while (await timer.WaitForNextTickAsync() && user.IsRunning)
            //{
            //    _logger.LogDebug("starting new round");

            //    await _findNewApartments.FindAsync();

            //    user = await _userRepository.GetOrDefaultAsync(chatId);
            //}
            while (true)
            {
                var users = await _userRepository.GetAllAsync();

                foreach (var user in users)
                {
                    if (user.IsRunning)
                    {
                        _logger.LogDebug("starting new round");

                        await _findNewApartments.FindAsync(user);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}