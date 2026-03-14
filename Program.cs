using BookingApi.Models;
using BookingApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BookingStore>();
builder.Services.AddSingleton<EventStore>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok(new { service = "booking-api", status = "running" }));

app.MapGet("/bookings", (BookingStore store) =>
{
    return Results.Ok(store.GetAll());
});

app.MapPost("/bookings", async (
    CreateBookingRequest request,
    BookingStore bookingStore,
    EventStore eventStore) =>
{
    if (string.IsNullOrWhiteSpace(request.Route))
    {
        return Results.BadRequest(new { error = "Route is required." });
    }

    if (request.Passengers <= 0)
    {
        return Results.BadRequest(new { error = "Passengers must be greater than 0." });
    }

    var booking = bookingStore.Add(request);

    var bookingCreatedEvent = new BookingCreatedEvent
    {
        BookingId = booking.Id,
        Route = booking.Route,
        Passengers = booking.Passengers,
        Vehicle = booking.Vehicle,
        TimestampUtc = DateTime.UtcNow
    };

    await eventStore.AppendAsync(bookingCreatedEvent);

    return Results.Ok(new
    {
        message = "Booking created",
        booking,
        eventFile = eventStore.GetPath()
    });
});

app.Run();