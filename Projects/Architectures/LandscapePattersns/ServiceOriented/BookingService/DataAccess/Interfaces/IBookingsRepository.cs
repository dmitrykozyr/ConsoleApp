using BookingService.Domain;

namespace BookingService.DataAccess.Interfaces
{
    public interface IBookingsRepository
    {
        void Save(Booking booking);
    }
}
