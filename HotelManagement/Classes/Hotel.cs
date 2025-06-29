
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.Classes;

public class Hotel : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Location { get; set; }
    [Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "The provided email is not valid")]
    public string Email { get; set; }
    public List<GuestHotel> GuestHotels { get; set; } = new List<GuestHotel>();
}
