﻿using MVVM.DbContexts;
using MVVM.DTOs;
using MVVM.Models;
using System.Threading.Tasks;

namespace MVVM.Services.ReservationCreators;

public class DatabaseReservationCreator : IReservationCreator
{
    private readonly ReservoomDbContextFactory _dbContextFactory;

    public DatabaseReservationCreator(ReservoomDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task CreateReservation(Reservation reservation)
    {
        using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
        {
            ReservationDTO reservationDTO = ToReservationDTO(reservation);

            context.Reservations.Add(reservationDTO);

            await context.SaveChangesAsync();
        }
    }

    private ReservationDTO ToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO()
        {
            FloorNumber = reservation.RoomId?.FloorNumber ?? 0,
            RoomNumber = reservation.RoomId?.RoomNumber ?? 0,
            Username = reservation.Username,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime
        };
    }
}
