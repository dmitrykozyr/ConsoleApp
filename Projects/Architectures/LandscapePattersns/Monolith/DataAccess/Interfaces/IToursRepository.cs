using Monolith.Domain;

namespace Monolith.DataAccess.Interfaces
{
    public interface IToursRepository
    {
        IList<Tour> GetTours();
        Tour GetTour(int id);
    }
}
