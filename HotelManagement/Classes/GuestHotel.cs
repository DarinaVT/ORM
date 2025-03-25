using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.Classes;

public class GuestHotel : BaseEntity
{
    [Required]
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; }

    [Required]
    public int GuestId { get; set; }
    public Guest Guest { get; set; }

    [Required, DataType(DataType.DateTime)]
    public DateTime CheckedIn { get; set; }

    [Required, DataType(DataType.DateTime)]
    public DateTime CheckedOut { get; set; }

    [Required]
    public int RoomId { get; set; }
    public Room Room { get; set; }

    public void CheckIn()
    {
        if (Room.Status == RoomStatus.Unavailable)
            throw new InvalidOperationException("Room is not available");

        Room.UpdateStatus(true);
        CheckedIn = DateTime.Now;
    }

    public void CheckOut()
    {
        Room.UpdateStatus(false);
        CheckedOut = DateTime.Now;
    }
}
