namespace DataContext;
using Infrastructure;
public class DataService
{
    private readonly CarsDbContext _context;
    public DataService(CarsDbContext context)
    {
        _context = context;
    }
}