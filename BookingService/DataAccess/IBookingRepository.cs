using ExploreCalifornia.BookingService.Domain;

namespace ExploreCalifornia.BookingService.DataAccess
{
    public interface IBookingRepository
    {
        void Save(Booking booking);
    }
}