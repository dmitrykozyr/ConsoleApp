using ExploreCalifornia.BookingService.Domain;

namespace ExploreCalifornia.BookingService.ExternalServices
{
    public interface IMailProxy
    {
        void SendMail(Booking booking);
    }
}