using System;

namespace MVVM.Models;

public class Reservation
{
    public RoomID RoomID { get; }

    public string Username { get; }

    public DateTime StartTime { get; }

    public DateTime EndTime { get; }

    public TimeSpan Length => EndTime.Subtract(StartTime);

    public Reservation(RoomID roomID, string userName, DateTime startTime, DateTime endTime)
    {
        RoomID = roomID;
        Username = userName;
        StartTime = startTime;
        EndTime = endTime;
    }
}
