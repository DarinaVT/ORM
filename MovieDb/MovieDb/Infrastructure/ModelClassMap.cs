namespace Infrastructure;
using CsvHelper.Configuration;
public class ModelClassMap : ClassMap<Model>
{
    public ModelClassMap()
    {
        Map(m => m.MovieName).Name("Movie Name");
        Map(m => m.ReleaseYear).Name("Release Year");
        Map(m => m.Duration).Name("Duration");
        Map(m => m.IMDBRating).Name("IMDB Rating");
        Map(m => m.Metascore).Name("Metascore");
        Map(m => m.Votes).Name("Votes");
        Map(m => m.Genre).Name("Genre");
        Map(m => m.Director).Name("Director");
        Map(m => m.Cast).Name("Cast");
        Map(m => m.Gross).Name("Gross");
    }
}