using MVVM.Models;

namespace MVVM.ViewModels;

public class ReservationViewModel : ViewModelBase
{
    private Reservation _reservation;

    public string RoomId => _reservation.RoomId.ToString();

    public string Username => _reservation.Username;

    public string StartDate => _reservation.StartTime.ToString("d");

    public string EndDate => _reservation.EndTime.ToString("d");


    public ReservationViewModel(Reservation reservation)
    {
        _reservation = reservation;
    }
}
