using Microsoft.IdentityModel.Tokens;

namespace DataContext;

public static class DataClean
{
    public static List<decimal> GetCoordinates(string input)
    {
        if (input.IsNullOrEmpty())
        {
            return new List<decimal>() { 0, 0 };
        }
        var clean = input.Replace("POINT (", "").Replace(")", "").Trim();
        var split = clean.Split(" ");
        var coordinates = new List<decimal>();
        foreach (var s in split)
        {
            Decimal.TryParse(s, out decimal coordinate);
            coordinates.Add(coordinate);
        }
        return coordinates;
    }
    public static string ShortenType(string input)
    {
        int start = input.IndexOf("(");
        var end = input.IndexOf(")");
        return input.Substring(start, end - start - 1);
    }
    public static string ShortenEligibility(string input)
    {
        string shortened = input switch
        {
            "Clean Alternative Fuel Vehicle Eligible" => "Eligible",
            "Eligibility unknown as battery range has not been researched" => "Unknown",
            "Not eligible due to low battery range" => "Ineligible"
        };
        return shortened;
    }
    public static List<string> GetUtilities(string input)
    {
        if (!input.Contains('|'))
        {
            return new List<string> { input };

        }
        var split = input.Split('|');
        var cleaned = split.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        return cleaned;
    }
    public static string GetDistrictCode(string input)
    {
        if (input.Length < 3)
        {
            return input;
        }
        return input.Substring(0, 2);
    }
}