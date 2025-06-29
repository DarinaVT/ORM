namespace Models;

public abstract class BaseEntity : AdditionalInfo
{
    public int Id { get; set; }
}
public abstract class AdditionalInfo
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
