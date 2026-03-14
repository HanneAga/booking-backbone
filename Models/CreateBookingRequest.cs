namespace BookingApi.Models;

public class CreateBookingRequest
{
    public string Route { get; set; } = "";
    public int Passengers { get; set; }
    public bool Vehicle { get; set; }
}