using System;
using Models;
using Infrastructure;
namespace DataContext;

public class CRUD
{
    private readonly OrganisationsDbContext _context;
    public CRUD(OrganisationsDbContext context)
    {
        _context = context;
    }

    public List<Organisation> OrganisationsGet50()
    {
        return _context.Organisations.Take(50).ToList();
    }

    public void AddCountry(Country country)
    {
        var exists = _context.Countries.Any(m => m.Name == country.Name);

        if (!exists)
        {
            _context.Countries.Add(country);
        }
    }

    public void DeleteIndustry(int id)
    {
        var industry = _context.Industries.Find(id);
        if (industry != null)
        {
            _context.Industries.Remove(industry);
        }
    }

    public Year GetById(int id)
    {
        var year = _context.Years.Find(id);
        return year;
    }

    public void UpdateYear(int id)
    {
        var year = _context.Years.Find(id);
        if (year != null)
        {
            _context.Years.Update(year);
        }
    }
}