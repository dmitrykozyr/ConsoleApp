using Monolith.Domain;
using Monolith.Services.Interfaces;

namespace Monolith.Services;

public class TravelAgentService : ITravelAgentService
{
    public void NotifyTravelAgentOfBooking(Booking booking)
    {
        File.AppendAllText("AppData\\travelAgentAPI.txt",
            $"{DateTime.Now.ToString("O")} " +
            $"Sent booking by {booking.Email} " +
            $"({booking.Name}) to travel agent " +
            $"for tour {booking.TourId}." + Environment.NewLine);
    }
}
