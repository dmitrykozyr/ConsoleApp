namespace BookingService.ESB.Contracts;

public class BookingMade
{
    public int TourId { get; init; }

    public string? Email { get; init; }

    public string? Name { get; init; }

    public bool Transport { get; init; }
}
