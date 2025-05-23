﻿using Dapper;
using Microsoft.Data.Sqlite;
using Monolith.DataAccess.Interfaces;
using Monolith.Domain;

namespace Monolith.DataAccess;

public class BookingsRepository : IBookingsRepository
{
    private readonly string _connectionString;

    public BookingsRepository()
    {
        _connectionString = "Data Source=AppData/monolith-database.db;";
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
