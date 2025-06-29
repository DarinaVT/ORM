namespace DataContext;

public class DataCleaner
{
    public static List<decimal> GetCoordinates(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new List<decimal>();
        }
        var clean = input.Replace("POINT (", "").Replace(")", "").Trim();
        var split = clean.Split(" ");
        var coordinates = new List<decimal>();
        foreach(var s in split)
        {
            var c = Convert.ToDecimal(s);
            coordinates.Add(c);
        }
        return coordinates;
    }
    public static decimal ToDecimal(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0;
        }
        Decimal.TryParse(input, out decimal value);
        return value;
    }
}