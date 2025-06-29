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
            var movies = csv.GetRecords<Model>().Where(p => !string.IsNullOrWhiteSpace(p.MovieName)).ToList();
            return movies;
        }
    }
}