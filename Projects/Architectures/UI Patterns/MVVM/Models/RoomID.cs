using System;

namespace MVVM.Models;

public class RoomId
{
    public int FloorNumber { get; set; }

    public int RoomNumber { get; set; }

    public RoomId(int floorNumber, int roomNumber)
    {
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
    }

    public override string ToString()
    {
        return $"{FloorNumber}{RoomNumber}";
    }

    public override bool Equals(object obj)
    {
        return obj is RoomId roomId
            && FloorNumber == roomId.FloorNumber
            && RoomNumber == roomId.RoomNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FloorNumber, RoomNumber);
    }

    public static bool operator ==(RoomId roomId_1, RoomId roomId_2)
    {
        if (roomId_1 is null && roomId_2 is null)
        {
            return true;
        }

        return !(roomId_1 is null) && roomId_1.Equals(roomId_2);
    }

    public static bool operator !=(RoomId roomId_1, RoomId roomId_2)
    {
        return !(roomId_1 == roomId_2);
    }
}
