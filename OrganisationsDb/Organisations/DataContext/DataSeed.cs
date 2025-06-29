using Infrastructure;
using Models;
namespace DataContext;

public class DataSeed
{
    private readonly OrganisationsDbContext _context;
    private readonly ICsvFileReader _reader;

    public DataSeed(OrganisationsDbContext context, ICsvFileReader reader)
    {
        _context = context;
        _reader = reader;
    }

    public Dictionary<string, Country> CountryDict { get; set; } = new(StringComparer.OrdinalIgnoreCase); 
    public Dictionary<string, Industry> IndustryDict { get; set; } = new(StringComparer.OrdinalIgnoreCase); 
    public Dictionary<int, Year> YearDict { get; set; } = new(); 
    public Dictionary<string, Organisation> OrganisationDict { get; set; } = new(StringComparer.OrdinalIgnoreCase); 

    public void LoadData()
    {
        if (_context.Organisations.Any())
        {
            return;
        }
        var organisationsFromFile = _reader.GetData();
        foreach(var record in organisationsFromFile)
        {
            if(!CountryDict.TryGetValue(record.Country, out var country))
            {
                country = new Country
                {
                    Name = record.Country,
                };
                CountryDict[record.Country] = country;
            }
            if(!IndustryDict.TryGetValue(record.Industry, out var industry))
            {
                industry = new Industry
                {
                    Name = record.Industry
                };
                IndustryDict[record.Industry] = industry;
            }
            if(!YearDict.TryGetValue(record.Founded, out var year))
            {
                year = new Year
                {
                    Founded = record.Founded
                };
                YearDict[record.Founded] = year;
            }
            if(!OrganisationDict.TryGetValue(record.OrganizationID, out var organisation))
            {
                organisation = new Organisation
                {
                    Name = record.Name,
                    OrganisationID = record.OrganizationID,
                    Website = record.Website,
                    Description = record.Description,
                    EmployeesAmount = record.EmployeesAmount,
                    Country = country,
                    Industry = industry,
                    Year = year
                };
                OrganisationDict[record.OrganizationID] = organisation;
            }
            
        }
        _context.Organisations.AddRange(OrganisationDict.Values);
        _context.SaveChanges();
    }
}