namespace Estate.Models;

public abstract class BaseEntity : AuditibleSoftDeletable
{
    public int Id { get; set; }
}

public abstract class AuditibleSoftDeletable
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
