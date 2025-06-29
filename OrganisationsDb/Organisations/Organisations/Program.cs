using System.Data;
using Microsoft.EntityFrameworkCore;
using DataContext;
using Infrastructure;
public class Program
{
    public static void Main()
    {

        using (var context = new OrganisationsDbContext())
        {
            CsvFileReader csv = new CsvFileReader();
            csv.GetData();
            var dataSeed = new DataSeed(context, csv);
            dataSeed.LoadData();

            DataService dataService = new DataService(context);
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var firstQuery = dataService.CompaniesByIndustries();
            Console.WriteLine();
            var secondQuery = dataService.GetIndustriesByCountries();
            Console.WriteLine();
            var thirdQuery = dataService.EmployeesOver1000();
            Console.WriteLine();
            RawQueries rawQueries = new RawQueries();
            var raw = rawQueries.RawQuery();
            Console.WriteLine();
            CRUD crud = new CRUD(context);
            var get50 = crud.OrganisationsGet50();
            Console.WriteLine();
        }
    }
}