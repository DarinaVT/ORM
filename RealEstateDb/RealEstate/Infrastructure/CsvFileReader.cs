using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace Infrastructure;

public class CsvFileReader : ICsvFileReader
{
    public List<Model> GetData()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
        };
        using(var reader = new StreamReader(GlobalParam.filePath))
        using(var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<ModelClassMap>();
            var properties = csv.GetRecords<Model>().Where(p => !string.IsNullOrWhiteSpace(p.SerialNumber)).ToList();
            return properties;
        }
    }
}
