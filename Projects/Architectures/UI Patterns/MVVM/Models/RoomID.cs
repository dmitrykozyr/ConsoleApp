using System;

namespace MVVM.Models;

public class RoomID
{
    public int FloorNumber { get; set; }
    public int RoomNumber { get; set; }

    public RoomID(int floorNumber, int roomNumber)
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
        return obj is RoomID roomId &&
            FloorNumber == roomId.FloorNumber &&
            RoomNumber == roomId.RoomNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FloorNumber, RoomNumber);
    }

    public static bool operator ==(RoomID roomID_1, RoomID roomID_2)
    {
        if (roomID_1 is null && roomID_2 is null)
            return true;

        return !(roomID_1 is null) && roomID_1.Equals(roomID_2);
    }

    public static bool operator !=(RoomID roomID_1, RoomID roomID_2)
    {
        return !(roomID_1 == roomID_2);
    }
}
