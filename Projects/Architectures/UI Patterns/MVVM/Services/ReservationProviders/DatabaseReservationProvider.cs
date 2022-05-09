using Microsoft.EntityFrameworkCore;
using MVVM.DbContexts;
using MVVM.DTOs;
using MVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVVM.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

                await Task.Delay(2000);

                return reservationDTOs.Select(z => ToReservation(z));
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(
                            new RoomID(dto.FloorNumber, dto.RoomNumber),
                            dto.Username,
                            dto.StartTime,
                            dto.EndTime);
        }
    }
}
