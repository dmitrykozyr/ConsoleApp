using BookingService.Domain;

namespace BookingService.ESB.Interfaces
{
    public interface IESBroxy
    {
        void NotifyBookingMade(Booking booking);
    }
}
