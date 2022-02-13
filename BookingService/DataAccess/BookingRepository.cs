using System.Data;
using ExploreCalifornia.BookingService.Domain;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ExploreCalifornia.BookingService.DataAccess
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository()
        {
            _connectionString = "Data Source=/data/BookingService/microservices-bookings-database.db;";
        }

        public void Save(Booking booking)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Execute(
                    "INSERT INTO Booking (TourId, Name, Email, Transport) VALUES (@TourId, @Name, @Email, @Transport)",
                    new { TourId = booking.TourId, Name = booking.Name, Email = booking.Email, Transport = booking.Transport });
            }
        }
    }
}
