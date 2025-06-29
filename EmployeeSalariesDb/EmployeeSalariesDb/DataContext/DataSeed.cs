namespace DataContext;

using Models;
using Infrastructure;

public class DataSeed
{
    private readonly SalariesDbContext _context;
    private readonly ICsvFileReader _reader;

    public DataSeed(SalariesDbContext context, ICsvFileReader reader)
    {
        _context = context;
        _reader = reader;
    }

    private List<Employee> _employees = new List<Employee>();
    public List<Employee> Employees => _employees;

    public Dictionary<string, DepartmentName> NameDict { get; private set; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, DivisionName> DivisionDict { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

    public void LoadData()
    {
        if (_context.Divisions.Any())
        {
            return;
        }

        var records = _reader.GetData();

        foreach (var record in records)
        {
            var department = GetOrAdd(NameDict, record.DepartmentName, () => new DepartmentName { Department = record.DepartmentName });
            var division = GetOrAdd(DivisionDict, record.Division, () => new DivisionName { Division = record.Division });

            var employee = new Employee
            {
                Gender = record.Gender,
                BaseSalary = double.Parse(record.BaseSalary),
                OvertimePay = double.Parse(record.OvertimePay),
                LongevityPay = double.Parse(record.LongevityPay),
                Grade = record.Grade,
                DepartmentName = department,
                Division = division
            };
            _employees.Add(employee);
        }
        _context.Divisions.AddRange(DivisionDict.Values);
        _context.Departments.AddRange(NameDict.Values);
        _context.Employees.AddRange(_employees);
        _context.SaveChanges();
    }

    private static TValue GetOrAdd<TKey, TValue>(
        Dictionary<TKey, TValue> dict,
        TKey key,
        Func<TValue> valueFactory) where TKey : notnull
    {
        if (!dict.TryGetValue(key, out var value))
        {
            value = valueFactory();
            dict[key] = value;
        }
        return value;
    }
}
