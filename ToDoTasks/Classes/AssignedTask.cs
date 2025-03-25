using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using TaskManaging.Classes;

namespace Company.CompanyManagement;

public class AssignedTask : BaseEntity
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int TaskId { get; set; }

    public User User { get; set; } = null!;
    public TaskList Task { get; set; } = null!;

    public IList<Status> Statuses { get; set; } = new List<Status>();
}
