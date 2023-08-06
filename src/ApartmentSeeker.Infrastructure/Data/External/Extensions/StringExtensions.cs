using System.Text.RegularExpressions;

namespace ApartmentScrapper.Infrastructure.Data.External
{
    public static class StringExtensions
    {
        public static decimal GetNumericPart(this string str)
        {
            string pattern = @"(\d+(\.\d+)?)";

            Match match = Regex.Match(str, pattern);

            if (match.Success)
            {
                if (decimal.TryParse(match.Value, out var num))
                    return num;
            }

            return 0;
        }
    }
}