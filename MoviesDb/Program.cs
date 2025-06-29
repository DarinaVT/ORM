using moviesDB;
using moviesDB.CrudOperations;

public class Program
{
    static void Main(string[] args)
    {
        var reader = new MovieCSVReader();
        reader.LoadMovies(@"D:\movies.csv");

        var dbContext = new AppDbContext();
        var movieCrud = new MovieCRUD(dbContext);

        List<Movie> allMovies = movieCrud.GetAllMovies();
        foreach (var movie in allMovies)
        {
            Console.WriteLine("Title: {0}, Year: {1}", movie.Title, movie.ReleaseYear.Year);
        }    
    }
}