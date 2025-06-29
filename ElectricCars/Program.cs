using System.Data;
namespace ElectricCars;
using Infrastructure;
using DataContext;
public class Program
{
    private static void Main(string[] args)
    {

        using (var context = new CarsDbContext())
        {
            CsvFileReader csv = new CsvFileReader();
            csv.GetData();
            var dataSeed = new DataSeed(context, csv);
            dataSeed.LoadData();
        }
    }
}