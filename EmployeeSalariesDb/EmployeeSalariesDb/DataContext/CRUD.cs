using System;

namespace DataContext;

public class MovieCRUD
{
    private readonly SalariesDbContext _context;
    public MovieCRUD(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public List<Movie> GetAllMovies()
    {
        return _context.Movies
            .Include(x => x.ReleaseYear)
            .ToList();
    }

    public void AddMovie(Movie movie)
    {
        var exists = _context.Movies
            .Any(m => m.Title == movie.Title && m.YearId == movie.YearId);

        if (!exists)
        {
            _context.Movies.Add(movie);
        }
    }

    public void DeleteMovie(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
        }
    }

    public Movie GetById(int id)
    {
        var movie = _context.Movies.Find(id);
        return movie;
    }

    public void UpdateMovie(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie != null)
        {
            _context.Movies.Update(movie);
        }
    }
}