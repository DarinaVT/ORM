using Microsoft.Data.SqlClient;
using Infrastructure;
namespace DataContext;

public class DataService
{
    private readonly SalariesDbContext _context;
    public DataService(SalariesDbContext context)
    {
        _context = context;
    }


    public List<GenderCount> GroupedByGenderSelectedByGrade()
    {
        return _context.Employees
            .Where(e => e.Grade != "M2" && e.Grade != "N25" && e.LongevityPay > 0)
            .GroupBy(e => e.Gender)
            .Select(g => new GenderCount { Gender = g.Key, Count = g.Count() })
            .ToList();
    }

    public List<DeparmentInfo> GetMinMaxSalary()
    {
        return _context.Employees.Join(
            _context.Departments, e => e.DepartmentNameId, d => d.Id, (e, d) => new {e , d}).
            GroupBy
            (x => x.d.Department).
            Select
            (g => new DeparmentInfo { DivisionInfo = g.Key, Min = g.Min(x => (decimal)x.e.BaseSalary), Max = g.Max(x => (decimal)x.e.BaseSalary)}).
            ToList();
    }
    public List<OverTimePay> GetOver100()
    {
        return _context.Divisions.GroupJoin(_context.Employees.Where(e => e.OvertimePay > 100), d => d.Id, e => e.DivisionNameId, (d, employeesGroup) => new OverTimePay { Department = d.Division, Count = employeesGroup.Count() }).ToList();
    }

}
public class GenderCount
{
    public string Gender { get; set; }
    public int Count { get; set; }

    public override string ToString()
    {
        return $"Gender = {Gender}, Count = {Count}";
    }
}
public class DeparmentInfo
{
    public string DivisionInfo { get; set; }
    public decimal Min {  get; set; }
    public decimal Max { get; set; }
    public override string ToString()
    {
        return $"DivisionInfo '{DivisionInfo}', MinSalary = {Min}, MaxSalary = {Max}";
    }
}
public class OverTimePay
{
    public string Department { get; set; }
    public int Count { get; set; }
    public override string ToString()
    {
        return $"Department = {Department}, Count = {Count}";
    }
}