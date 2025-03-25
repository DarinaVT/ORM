
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.CompanyManagement;

public class User : BaseEntity
{
    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "The provided email is not valid")]
    public string Email { get; set; }

    public string? Address { get; set; }

    public IList<AssignedTask> AssignedTasks { get; set; } = new List<AssignedTask>();
}
