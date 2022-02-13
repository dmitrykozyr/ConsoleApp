using ExploreCalifornia.BookingService.Domain;

namespace ExploreCalifornia.BookingService.ExternalServices
{
    public interface ITravelAgentProxy
    {
        void NotifyTravelAgent(Booking booking);
    }
}