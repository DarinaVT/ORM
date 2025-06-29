namespace Models;

public class Movies : BaseEntity
{
    public string MovieName { get; set; }
    public int ReleaseYear { get; set; }
    public int? Duration { get; set; }
    public decimal? IMDBRating { get; set; }
    public decimal? Metascore { get; set; }
    public decimal Votes { get; set; }
    public decimal? Gross { get; set; }
    public int DirectorId { get; set; }
    public Director Director { get; set; }
    public int CastId { get; set; }
    public Cast Cast { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; }
}