using moviesDB;

public class Movie : BaseEntity
{
    public string Title { get; set; }

    public int YearId { get; set; }
    public ReleaseYear ReleaseYear { get; set; }

    public int Duration { get; set; }

    public float IMDB { get; set; }

    public double Metascore { get; set; }

    public int Vote { get; set; } 

    public double Gross { get; set; }

    public int DirectorId { get; set; }
    public Director Director { get; set; }

    public int CastId { get; set; }
    public Cast Cast { get; set; }

    public ICollection<MovieGenre> MovieGenres { get; set; }
}
