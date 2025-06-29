namespace Models;

public class Year : BaseEntity
{
    public int Founded { get; set; }
    public ICollection<Organisation> Organisations { get; set; } = new List<Organisation>();
}