using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.Classes;

public class Guest : BaseEntity
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required, MaxLength(50), DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid phone number")]
    public string PhoneNumber {  get; set; }

    [DataType(DataType.EmailAddress, ErrorMessage = "The provided email is not valid")]
    public string? Email { get; set; }

    public List<GuestHotel> GuestHotels { get; set; } = new List<GuestHotel>();
}
