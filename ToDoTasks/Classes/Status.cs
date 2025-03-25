using Company.CompanyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManaging.Classes;
public class Status : BaseEntity
{
    public int TaskId { get; set; }
    public Task Task { get; set; } = null!;

    [Required]
    public string TaskStatus { get; set; }

    public int AssignedTaskId { get; set; }
    public AssignedTask AssignedTask { get; set; } = null!;
}

