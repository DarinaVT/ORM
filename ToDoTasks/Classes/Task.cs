using System;
using System.ComponentModel.DataAnnotations;

namespace Company.CompanyManagement;

public class TaskList : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public IList<AssignedTask> AssignedUsers { get; set; } = new List<AssignedTask>();
}
