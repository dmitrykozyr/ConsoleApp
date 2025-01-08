using Microsoft.EntityFrameworkCore;
using MVVM.DbContexts;
using MVVM.DTOs;
using MVVM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVVM.Services.ReservationConflictValidators;

public class DatabaseReservationConflictValidator : IReservationConflictValidator
{
    private readonly ReservoomDbContextFactory _dbContextFactory;

    public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Reservation> GetConflictingReservation(Reservation reservation)
    {
        using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
        {
            ReservationDTO reservationDTO = await context.Reservations
                .Where(z => z.FloorNumber == reservation.RoomID.FloorNumber)
                .Where(z => z.RoomNumber == reservation.RoomID.RoomNumber)
                .Where(z => z.EndTime > reservation.StartTime)
                .Where(z => z.StartTime < reservation.EndTime)
                .FirstOrDefaultAsync();

            return reservation == null ? null : ToReservation(reservationDTO);
        }
    }

    private static Reservation ToReservation(ReservationDTO dto)
    {
        if (dto == null)
        {
            return null;
        }

        return new Reservation(
                    new RoomID(dto.FloorNumber, dto.RoomNumber),
                    dto.Username,
                    dto.StartTime,
                    dto.EndTime);
    }
}
