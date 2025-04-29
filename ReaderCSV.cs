namespace CarCSV;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
public class ReaderCSV
{
    private readonly ApplicationDbContext _context;
    public ReaderCSV(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Reader(string filePath)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, csvConfig);

        var records = csv.GetRecords<RecordsCSV>();

        var brandDictionary = _context.Brands.ToDictionary(b => b.Name.ToLower(), b => b);

        foreach (var record in records)
        {
            var (brandName, modelName) = SplitCarName(record.CarName);

            var brandNameKey = brandName.ToLower();

            if (!brandDictionary.TryGetValue(brandNameKey, out var brand))
            {
                brand = new Brand 
                {
                    Name = brandName
                };
                _context.Brands.Add(brand);
                brandDictionary.Add(brandNameKey, brand);
            }

            var car = new CarDetails
            {
                Mpg = record.Mpg,
                Cylinders = record.Cylinders,
                Displacement = record.Displacement,
                HorsePower = record.Horsepower,
                Weight = record.Weight,
                Acceleration = record.Acceleration,
                Year = record.Year,
                Origin = record.Origin,
                Model = modelName,
                BrandId = brand.Id
            };
            _context.Cars.Add(car);
        }

        _context.SaveChanges();
    }

    private (string Brand, string Model) SplitCarName(string carName)
    {
        var parts = carName.Split(new[] {' '}, 2);
        string brand = parts[0].Trim();
        if (brand.Contains('-'))
        {
            return (brand, parts.Length > 1 ? parts[1].Trim() : "");
        }
        string model = parts.Length > 1 ? parts[1].Trim() : "";
        return (brand, model);
    }
}