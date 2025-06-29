namespace moviesDB;

public class Genre : BaseEntity
{
    public string Name  { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; }
}