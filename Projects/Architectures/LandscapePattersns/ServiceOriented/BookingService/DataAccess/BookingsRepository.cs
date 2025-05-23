﻿using BookingService.DataAccess.Interfaces;
using BookingService.Domain;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BookingService.DataAccess;

public class BookingsRepository : IBookingsRepository
{
    private readonly string _connectionString;

    public BookingsRepository()
    {
        _connectionString = "Data Source=AppData/soa-bookings-database.db;";
    }

    public void Save(Booking booking)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Execute(
                "INSERT INTO Booking (TourId, Name, Email, Transport) VALUES (@TourId, @Name, @Email, @Transport)",
                new
                {
                    booking.TourId,
                    booking.Name,
                    booking.Email,
                    booking.Transport
                });
        }
    }
}
