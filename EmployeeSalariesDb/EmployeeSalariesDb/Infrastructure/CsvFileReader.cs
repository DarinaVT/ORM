using Infrastructure;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using CsvHelper.Configuration;
using CsvHelper;
public class CsvFileReader : ICsvFileReader
{
    public List<Model> GetData()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
        };
        using (var reader = new StreamReader(GlobalParam.FilePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<ModelClassMap>();
            var properties = csv.GetRecords<Model>().Where(p => !string.IsNullOrWhiteSpace(p.Division)).ToList();
            return properties;
        }
    }
}