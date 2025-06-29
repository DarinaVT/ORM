namespace Models;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Organisation> Organisations { get; set; } = new List<Organisation>();
}