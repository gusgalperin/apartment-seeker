namespace ApartmentScrapper.Utils.Logger
{
    public interface ILogger<T>
    {
        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(string message);

        void LogDebug(string message);
        void WithField(string name, string value);
    }
}