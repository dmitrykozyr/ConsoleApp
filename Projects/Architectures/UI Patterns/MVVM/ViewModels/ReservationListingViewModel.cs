using MVVM.Commands;
using MVVM.Models;
using MVVM.Services;
using MVVM.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MVVM.ViewModels;

public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    private readonly HotelStore _hotelStore;
    public IEnumerable<ReservationViewModel> Reservations => _reservations;


    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));

            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    public bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public ICommand LoadReservationsCommand { get; }
    public ICommand MakeReservationCommand { get; }

    public ReservationListingViewModel(
                HotelStore hotelStore,
                NavigationService<MakeReservationViewModel> makeReservationNavigationService)
    {
        _hotelStore = hotelStore;
        _reservations = new ObservableCollection<ReservationViewModel>();

        LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);
        MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(makeReservationNavigationService);

        _hotelStore.ReservationMade += OnReservationMade;
    }

    public override void Dispose()
    {
        _hotelStore.ReservationMade -= OnReservationMade;
        base.Dispose();
    }

    private void OnReservationMade(Reservation reservation)
    {
        ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
        _reservations.Add(reservationViewModel);
    }

    public static ReservationListingViewModel LoadViewModel(
                    HotelStore hotelStore,
                    NavigationService<MakeReservationViewModel> makeReservationNavigationService)
    {
        ReservationListingViewModel viewModel = new ReservationListingViewModel(
                                                        hotelStore,
                                                        makeReservationNavigationService);
        viewModel.LoadReservationsCommand.Execute(null);
        return viewModel;
    }

    public void UpdateReservations(IEnumerable<Reservation> reservations)
    {
        _reservations.Clear();

        foreach (Reservation reservation in reservations)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }
    }
}
