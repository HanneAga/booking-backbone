namespace BookingApi.Models;

public class Booking
{
    public string Id { get; set; } = "";
    public string Route { get; set; } = "";
    public int Passengers { get; set; }
    public bool Vehicle { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}