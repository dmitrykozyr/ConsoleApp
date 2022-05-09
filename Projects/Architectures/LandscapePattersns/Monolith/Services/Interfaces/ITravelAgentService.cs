using Monolith.Domain;

namespace Monolith.Services.Interfaces
{
    public interface ITravelAgentService
    {
        void NotifyTravelAgentOfBooking(Booking booking);
    }
}
