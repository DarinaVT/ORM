namespace DataContext;

using Estate.Models;
using Infrastructure;
public class DataSeed
{
    private readonly PropertiesDbContext _context;
    private readonly ICsvFileReader _reader;
    public DataSeed(PropertiesDbContext context, ICsvFileReader reader)
    {
        _context = context;
        _reader = reader;
    }
    public void SeedProperties()
    {
        if (_context.PropertiesInfo.Any())
        {
            return;
        }
      
        var propertiesFromCsv = _reader.GetData();
        foreach(var record in propertiesFromCsv)
        {
            var coordinates = DataCleaner.GetCoordinates(record.Location); 
            var location = new Location
            {
                TownName = record.Town,
                Address = record.Address,
                Latitude = coordinates[1],
                Longitude = coordinates[0]
            };

            var assessment = new Assessment
            {
                AssessedValue = DataCleaner.ToDecimal(record.AssessedValue),
                SaleAmount = DataCleaner.ToDecimal(record.SaleAmount),
                SalesRatio = DataCleaner.ToDecimal(record.SalesRatio)
            };

            var remark = new Remark
            {
                AssessorRemarks = record.AssessorRemarks,
                OPM = record.OPMremarks
            };

            var property = new PropertyInfo
            {
                SerialNumber = record.SerialNumber,
                ListYear = record.ListYear,
                DateRecorded = record.DateRecorded,
                PropertyType = record.PropertyType,
                ResidentialType = record.ResidentialType,
                NonUseCode = record.NonUseCode,
                Location = location,
                Assessment = assessment,
                Remark = remark
            };
            _context.PropertiesInfo.Add(property);
        }
        _context.SaveChanges(); 
    }
    //for dictionaries
    private static TValue GetOrAdd<TKey, TValue>(
    Dictionary<TKey, TValue> dict,
    TKey key,
    Func<TValue> valueFactory)
    {
        if (!dict.TryGetValue(key, out var value))
        {
            value = valueFactory();
            dict[key] = value;
        }
        return value;
    }
}