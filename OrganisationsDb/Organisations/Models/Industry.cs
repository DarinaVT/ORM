namespace Models;

public class Industry : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Organisation> Organisations { get; set; } = new List<Organisation>();
}