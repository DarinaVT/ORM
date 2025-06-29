namespace DataContext;
public class DataService
{
    private readonly PropertiesDbContext _context;
    public DataService(PropertiesDbContext context)
    {
        _context = context;
    }
    //queries
}