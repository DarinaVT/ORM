using DataContext;
using Infrastructure;
using System.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

public class Program
{
    private static void Main(string[] args)
    {

        using (var context = new PropertiesDbContext())
        {
            CsvFileReader csv = new CsvFileReader();
            csv.GetData();
            var dataSeed = new DataSeed(context, csv);
            dataSeed.SeedProperties();
        }
    }
}