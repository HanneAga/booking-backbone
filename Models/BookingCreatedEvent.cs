namespace BookingApi.Models;

public class BookingCreatedEvent
{
    public string EventType { get; set; } = "booking.created";
    public string BookingId { get; set; } = "";
    public string Route { get; set; } = "";
    public int Passengers { get; set; }
    public bool Vehicle { get; set; }
    public DateTime TimestampUtc { get; set; }
}