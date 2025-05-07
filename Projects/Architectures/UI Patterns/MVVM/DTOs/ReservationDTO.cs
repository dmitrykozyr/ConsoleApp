using System;
using System.ComponentModel.DataAnnotations;

namespace MVVM.DTOs;

public class ReservationDTO
{
    [Key]
    public Guid Id { get; set; }

    public int FloorNumber { get; set; }

    public int RoomNumber { get; set; }

    public string? Username { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
