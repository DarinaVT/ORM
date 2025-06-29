using System.Text.RegularExpressions;

namespace DataContext;

public static class DataClean
{
    public static List<string> GetGenres(string input)
    {
        if (input.Contains(','))
        {
            var split = input.Split(',').ToList();
            return split;
        }
        else
        {
            return new List<string>() { input };
        }
    }
    public static decimal GetGross(string input)
    {
        if(input == "NA")
        {
            return 0;
        }
        var cleaned = input.Replace('$', ' ').Replace('M', ' ').Trim();
        decimal gross = Decimal.Parse(cleaned);
        return gross * 10000000;
    }
    public static int GetYear(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0;
        }

        var match = Regex.Match(input, @"\b(19|20)\d{2}\b");
        if (match.Success)
        {
            return int.Parse(match.Value);
        }

        return 0;
    }
}