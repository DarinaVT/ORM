using System.ComponentModel.DataAnnotations;

namespace Models;

public class Base
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
