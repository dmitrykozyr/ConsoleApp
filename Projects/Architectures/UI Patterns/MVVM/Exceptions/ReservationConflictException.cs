using MVVM.Models;
using System;
using System.Runtime.Serialization;

namespace MVVM.Exceptions;

public class ReservationConflictException : Exception
{
    public Reservation ExistingReservation { get; }
    public Reservation IncomingReservation { get; }

    public ReservationConflictException(
        Reservation existingReservation,
        Reservation incomingReservation)
    {
        ExistingReservation = existingReservation;
        IncomingReservation = incomingReservation;
    }

    public ReservationConflictException(string? message)
        : base(message)
    {
    }

    public ReservationConflictException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected ReservationConflictException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
