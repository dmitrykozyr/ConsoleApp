using MVVM.Commands;
using MVVM.Services;
using MVVM.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MVVM.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // В MakeReservationView.xaml есть строчка Text="{Binding Username}"
        // Когда меняем значение там - оно меняется и здесь
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private int _roomNumber;
        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        private int _floorNumber;
        public int FloorNumber
        {
            get { return _floorNumber; }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("StartDate must be smaller than EndDate", nameof(StartDate));
                }
            }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("EndDate must be bigger than StartDate", nameof(EndDate));
                }
            }
        }
        
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public MakeReservationViewModel(
            HotelStore hotelStore,
            NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(reservationViewNavigationService);

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
