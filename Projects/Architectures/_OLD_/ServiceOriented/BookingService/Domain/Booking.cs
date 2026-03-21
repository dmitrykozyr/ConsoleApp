namespace BookingService.Domain;

public class Booking
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Email { get; init; }

    public bool Transport { get; init; }

    public int TourId { get; init; }
}
