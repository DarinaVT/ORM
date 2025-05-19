using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class DataCleaner
{
    public static int? GetYear(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        var match = Regex.Match(input, @"\b(19|20)\d{2}\b");
        if (match.Success)
        {
            return int.Parse(match.Value);
        }

        return null;
    }

    public static int? GetInt(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Trim().Equals("NA", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var cleaned = input.Replace(",", "").Trim();

        if (int.TryParse(cleaned, out var result))
        {
            return result;
        }

        return null;
    }


    public static double? GetDouble(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0.0;
        }

        if (double.TryParse(input, out var result))
        {
            return result;
        }

        return null;
    }

    public static double GetGross(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0.0;
        }

        var trimmed = input.Trim();
        if (trimmed == "NA")
        {
            return 0.0;
        }

        var cleaned = trimmed.Replace("$", "").Replace("M", "");

        if (double.TryParse(cleaned, out var result))
        {
            return result * 1000000;
        }

        return 0.0;
    }

    public static string CleanText(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var fixedInput = FixEncoding(input);
        return fixedInput.Trim();
    }

    public static string[] SplitGenres(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Array.Empty<string>();
        }

        var genres = input
            .Split(',')
            .Select(g => g.Trim())
            .Where(g => !string.IsNullOrEmpty(g))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return genres;
    }

    public static string FixEncoding(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;

        }
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        byte[] bytes = Encoding.GetEncoding("windows-1252").GetBytes(input);
        return Encoding.UTF8.GetString(bytes);
    }

}
