using Monolith.Domain;

namespace Monolith.DataAccess.Interfaces;

public interface IBookingsRepository
{
    void Save(Booking booking);
}
