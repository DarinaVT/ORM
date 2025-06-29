using Infrastructure;
using Models;
namespace DataContext;

public class DataSeed
{
    private readonly MoviesDbContext _context;
    private readonly ICsvFileReader _reader;

    public Dictionary<string, Cast> CastDict { get; set; } = new();
    public Dictionary<string, Director> DirectorDict { get; set; } = new();
    public Dictionary<string, Genre> GenreDict { get; set; } = new();
    public List<Movies> Movies { get; set; } = new();
    public DataSeed(MoviesDbContext context, ICsvFileReader reader)
    {
        _context = context;
        _reader = reader;
    }
    public void LoadData()
    {
        if (_context.Movies.Any())
        {
            return;
        }

        var records = _reader.GetData();
        foreach(var record in records )
        {
            if (!DirectorDict.TryGetValue(record.Director, out var director))
            {
                director = new Director { Name = record.Director };
                DirectorDict[record.Director] = director;
            }

            if (!CastDict.TryGetValue(record.Cast, out var cast))
            {
                cast = new Cast { Name = record.Cast };
                CastDict[record.Cast] = cast;
            }
            var genreNames = DataClean.GetGenres(record.Genre);
            var movieGenres = new List<MovieGenre>();
            foreach(var genreName in genreNames)
            {
                if(!GenreDict.TryGetValue(genreName, out var genre))
                {
                    genre = new Genre { Name = genreName };
                    GenreDict[genreName] = genre;
                }
                movieGenres.Add(new MovieGenre { Genre =  genre });
            }
            var movie = new Movies
            {
                MovieName = record.MovieName,
                ReleaseYear = DataClean.GetYear(record.ReleaseYear),
                Duration = record.Duration,
                IMDBRating = record.IMDBRating,
                Votes = record.Votes,
                Gross = DataClean.GetGross(record.Gross),
                Director = director,
                Cast = cast,
                MovieGenres = movieGenres
            };
            Movies.Add(movie);
        }
        _context.AddRange(Movies);
        _context.SaveChanges();
    }
}