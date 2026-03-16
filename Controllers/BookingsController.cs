using BookingApi.Models;
using BookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingStore _bookingStore;
    private readonly EventStore _eventStore;

    public BookingsController(BookingStore bookingStore, EventStore eventStore)
    {
        _bookingStore = bookingStore;
        _eventStore = eventStore;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bookingStore.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Route))
        {
            return BadRequest(new { error = "Route is required." });
        }

        if (request.Passengers <= 0)
        {
            return BadRequest(new { error = "Passengers must be greater than 0." });
        }

        var booking = _bookingStore.Add(request);

        var bookingCreatedEvent = new BookingCreatedEvent
        {
            BookingId = booking.Id,
            Route = booking.Route,
            Passengers = booking.Passengers,
            Vehicle = booking.Vehicle,
            TimestampUtc = DateTime.UtcNow
        };

        await _eventStore.AppendAsync(bookingCreatedEvent);

        return Ok(new
        {
            message = "Booking created",
            booking,
            eventType = bookingCreatedEvent.EventType,
            eventFile = _eventStore.GetPath()
        });
    }
}