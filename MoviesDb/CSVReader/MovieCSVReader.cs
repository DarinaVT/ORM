using CsvHelper;
using CsvHelper.Configuration;
using moviesDB;
using System.Globalization;
using System.Text;

public class MovieCSVReader
{
    public Dictionary<int, ReleaseYear> YearDict { get; private set; } = new();
    public Dictionary<string, Director> DirectorDict { get; private set; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, Cast> CastDict { get; private set; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, Genre> GenreDict { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

    public List<Movie> LoadMovies(string filePath)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            Encoding = Encoding.UTF8
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, csvConfig);

        csv.Context.RegisterClassMap<Mapping>();
        var movieCsvRecords = csv.GetRecords<MovieCSV>().ToList();

        var movies = new List<Movie>();

        foreach (var record in movieCsvRecords)
        {
            var yearValue = DataCleaner.GetYear(record.ReleaseYear) ?? 0;
            var durationValue = DataCleaner.GetInt(record.Duration.ToString()) ?? 0;
            var imdbValue = (float)(DataCleaner.GetDouble(record.IMDB.ToString()) ?? 0.0);
            var metascoreValue = DataCleaner.GetDouble(record.Metascore) ?? 0.0;
            var voteValue = DataCleaner.GetInt(record.Vote.ToString()) ?? 0;
            var directorName = DataCleaner.CleanText(record.Director);
            var castName = DataCleaner.CleanText(record.Cast);
            var grossValue = DataCleaner.GetGross(record.Gross.ToString());
            var genreNames = DataCleaner.SplitGenres(record.Genre);

            if (!YearDict.TryGetValue(yearValue, out var year))
            {
                year = new ReleaseYear { Year = yearValue };
                YearDict[yearValue] = year;
            }

            if (!DirectorDict.TryGetValue(directorName, out var director))
            {
                director = new Director { Name = directorName };
                DirectorDict[directorName] = director;
            }

            if (!CastDict.TryGetValue(castName, out var cast))
            {
                cast = new Cast { Name = castName };
                CastDict[castName] = cast;
            }

            var movieGenres = new List<MovieGenre>();
            foreach (var genreName in genreNames)
            {
                if (!GenreDict.TryGetValue(genreName, out var genre))
                {
                    genre = new Genre { Name = genreName };
                    GenreDict[genreName] = genre;
                }

                movieGenres.Add(new MovieGenre { Genre = genre });
            }

            var movie = new Movie
            {
                Title = DataCleaner.CleanText(record.MovieName),
                ReleaseYear = year,
                Duration = durationValue,
                IMDB = imdbValue,
                Metascore = metascoreValue,
                Vote = voteValue,
                Gross = grossValue,
                Director = director,
                Cast = cast,
                MovieGenres = movieGenres
            };

            movies.Add(movie);
        }

        return movies;
    }
}