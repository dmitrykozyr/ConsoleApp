using MVVM.Exceptions;
using MVVM.Services.ReservationConflictValidators;
using MVVM.Services.ReservationCreators;
using MVVM.Services.ReservationProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(
            IReservationProvider reservationProvider,
            IReservationCreator reservationCreator,
            IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation =
                await _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictingReservation is not null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }

            await _reservationCreator.CreateReservation(reservation);
        }
    }
}
