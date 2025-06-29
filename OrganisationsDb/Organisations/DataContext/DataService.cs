using Microsoft.EntityFrameworkCore;
using Models;

namespace DataContext;
public class DataService
{
    private readonly OrganisationsDbContext _context;
    public DataService(OrganisationsDbContext context)
    {
        _context = context;
    }
    public List<CompaniesByIndustry> CompaniesByIndustries()
    {
       
        return _context.Organisations.Join(_context.Industries, o => o.IndustryId, i => i.Id, (o, i) => new { o, i })
            .GroupBy(x => x.i.Name)
            .Select(g => new CompaniesByIndustry { Companies = g.Count(), Name = g.Key })
            .ToList();
    }
    public List<IndustriesByCountry> GetIndustriesByCountries()
    {
        return _context.Countries.Select(c => new IndustriesByCountry
        {
            Country = c.Name,
            Industries = c.Organisations.Select(o => o.IndustryId).Distinct().Count(),
            Companies = c.Organisations.Count()
        })
        .OrderBy(x => x.Country)
        .ToList();

    }
    public List<Organisation> EmployeesOver1000()
    {
        return _context.Organisations.Where(o => o.EmployeesAmount >= 1000).OrderByDescending(o => o.EmployeesAmount).ToList();
    }

}
public class CompaniesByIndustry
{
    public int Companies { get; set; }
    public string Name { get; set; }
    public override string ToString()
    {
        return $"Companies = {Companies}, Name = {Name}";
    }
}

public class IndustriesByCountry
{
    public string Country { get; set; }
    public int Industries { get; set; }
    public int Companies { get; set; }
    public override string ToString()
    {
        return $"Country = {Country}, Industries = {Industries}, Companies = {Companies}";
    }
}
public class GetCompanies
{
    public int Count { get; set; }
    public string Industry { get; set; }
}