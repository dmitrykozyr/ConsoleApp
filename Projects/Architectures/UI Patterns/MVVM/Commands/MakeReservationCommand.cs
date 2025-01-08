using MVVM.Exceptions;
using MVVM.Models;
using MVVM.Services;
using MVVM.Stores;
using MVVM.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Commands;

public class MakeReservationCommand : AsyncCommandBase
{
    private readonly MakeReservationViewModel _makeReservationViewModel;
    private readonly HotelStore _hotelStore;
    private readonly NavigationService<ReservationListingViewModel> _reservationViewNavigationService;

    public MakeReservationCommand(
                MakeReservationViewModel makeReservationViewModel,
                HotelStore hotelStore,
                NavigationService<ReservationListingViewModel> reservationViewNavigationService)
    {
        _makeReservationViewModel = makeReservationViewModel;
        _hotelStore = hotelStore;
        _reservationViewNavigationService = reservationViewNavigationService;
        _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    // Отключаем кнопку Submit, если поле Username не заполнено
    public override bool CanExecute(object? parameter)
    {
        if (!string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
            _makeReservationViewModel.FloorNumber > 0 &&
            _makeReservationViewModel.RoomNumber > 0 &&
            base.CanExecute(parameter))
        {
            return true;
        }

        return false;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        Reservation reservation = new Reservation(
            new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
            _makeReservationViewModel.Username,
            _makeReservationViewModel.StartDate,
            _makeReservationViewModel.EndDate);

        try
        {
            await _hotelStore.MakeReservation(reservation);

            MessageBox.Show("Successfully reserved room", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _reservationViewNavigationService.Navigate();
        }
        catch (ReservationConflictException)
        {
            MessageBox.Show("This room is already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to make reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Отслеживаем, было-ли изменено поле Username
        if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
            e.PropertyName == nameof(MakeReservationViewModel.FloorNumber) ||
            e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
        {
            OnCanExecuteChanged();
        }
    }
}
