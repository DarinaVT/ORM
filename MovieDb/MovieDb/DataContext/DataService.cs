namespace DataContext;

public class DataService
{
    private readonly MoviesDbContext _context;
    public DataService(MoviesDbContext context)
    {
        _context = context;
    }
}