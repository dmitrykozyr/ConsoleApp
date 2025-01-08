using MVVM.Models;
using System.Threading.Tasks;

namespace MVVM.Services.ReservationCreators;

public interface IReservationCreator
{
    Task CreateReservation(Reservation reservation);
}
