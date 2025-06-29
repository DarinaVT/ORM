namespace Infrastructure;

public class Model
{
    public string MovieName { get; set; }
    public string ReleaseYear { get; set; }
    public int Duration { get; set; }
    public decimal IMDBRating { get; set; }
    public decimal? Metascore { get; set; }
    public decimal Votes { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Cast { get; set; }
    public string Gross { get; set; }
}
