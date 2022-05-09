using MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Services.ReservationProviders
{
    public interface IReservationProvider
    {
        Task<IEnumerable<Reservation>> GetAllReservations();
    }
}
