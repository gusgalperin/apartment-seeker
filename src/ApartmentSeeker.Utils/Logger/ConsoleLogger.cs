namespace ApartmentScrapper.Utils.Logger
{
    public class ConsoleLogger<T> : ILogger<T>
    {
        private readonly IDictionary<string, string> _fields = new Dictionary<string, string>();

        public void LogDebug(string message)
        {
            Log(message, ConsoleColor.White, ConsoleColor.Blue);
        }

        public void LogError(string message)
        {
            Log(message, ConsoleColor.White, ConsoleColor.Red);
        }

        public void LogInfo(string message)
        {
            Log(message, ConsoleColor.White, ConsoleColor.Green);
        }

        public void LogWarning(string message)
        {
            Log(message, ConsoleColor.White, ConsoleColor.Yellow);
        }

        public void WithField(string name, string value)
        {
            _fields[name] = value;
        }

        private void Log(string message, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = fontColor;
            Console.BackgroundColor = backgroundColor;

            var fields = string.Join(",", _fields.Select(x => $"{x.Key}: {x.Value}").ToList());
            if (!string.IsNullOrEmpty(fields))
            {
                fields = $"| {fields}";
            }

            Console.WriteLine($"{typeof(T).Name} {fields} | {message}");
        }
    }
}