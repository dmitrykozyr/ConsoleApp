using System;

namespace MVVM.Models;

public class Reservation
{
    public RoomId RoomId { get; }

    public string Username { get; }

    public DateTime StartTime { get; }

    public DateTime EndTime { get; }

    public TimeSpan Length => EndTime.Subtract(StartTime);

    public Reservation(RoomId roomId, string userName, DateTime startTime, DateTime endTime)
    {
        RoomId = roomId;
        Username = userName;
        StartTime = startTime;
        EndTime = endTime;
    }
}
