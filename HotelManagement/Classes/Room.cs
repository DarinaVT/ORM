using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.Classes;

public enum RoomStatus
{
    Available,
    Unavailable
}

public class Room : BaseEntity
{
    [Required]
    public int RoomNumber { get; set; }

    [Required]
    public RoomStatus Status { get; set; } = RoomStatus.Available;

    public List<GuestHotel> GuestHotels { get; set; } = new List<GuestHotel>();

    public void UpdateStatus(bool isCheckedIn)
    {
        if (isCheckedIn)
        {
            Status = RoomStatus.Unavailable;
        }
        else
        {
            Status = RoomStatus.Available;
        }
    }
}
