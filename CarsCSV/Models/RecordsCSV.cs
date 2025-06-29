using CsvHelper.Configuration.Attributes;

namespace CarCSV;

public class RecordsCSV
{
    [Name("mpg")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public decimal? Mpg { get; set; }

    [Name("cylinders")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public int? Cylinders { get; set; }

    [Name("displacement")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public decimal? Displacement { get; set; }

    [Name("horsepower")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public int? Horsepower { get; set; }

    [Name("weight")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public int? Weight { get; set; }

    [Name("acceleration")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public decimal? Acceleration { get; set; }

    [Name("model year")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public int? Year { get; set; }

    [Name("origin")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public int? Origin { get; set; }

    [Name("car name")]
    [TypeConverter(typeof(StrictTypeConverter))]
    public string CarName { get; set; }
}
