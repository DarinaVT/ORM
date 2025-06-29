using CsvHelper.Configuration;
using CsvHelper;
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
        using (var reader = new StreamReader(GlobalParams.FilePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<ModelClassMap>();
            var organisations = csv.GetRecords<Model>().Where(o => !string.IsNullOrWhiteSpace(o.OrganizationID)).ToList();
            return organisations;
        }
    }
}