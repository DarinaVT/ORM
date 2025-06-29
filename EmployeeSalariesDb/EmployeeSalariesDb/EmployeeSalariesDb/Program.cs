using System.Data;
using DataContext;
namespace EmployeeSalariesDb;

public class Program
{
    private static void Main(string[] args)
    {

        using (var context = new SalariesDbContext())
        {
            CsvFileReader csv = new CsvFileReader();
            csv.GetData();
            var dataSeed = new DataSeed(context, csv);
            dataSeed.LoadData();

            DataService service = new DataService(context);
            var queryFirst = service.GroupedByGenderSelectedByGrade();
            var querySecond = service.GetMinMaxSalary();
            var queryThird = service.GetOver100();

            RawQuery rawQuery = new RawQuery();
            rawQuery.RawQueryCreateTable();
            rawQuery.RawQueryInsertInto();
            Console.WriteLine();
        }
        
    }

}