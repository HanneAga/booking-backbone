using BookingApi.Models;

namespace BookingApi.Services;

public class BookingStore
{
    private readonly List<Booking> _bookings = [];

    public Booking Add(CreateBookingRequest request)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid().ToString("N")[..8],
            Route = request.Route,
            Passengers = request.Passengers,
            Vehicle = request.Vehicle,
            CreatedAtUtc = DateTime.UtcNow
        };

        _bookings.Add(booking);
        return booking;
    }

    public IReadOnlyList<Booking> GetAll() => _bookings;
}