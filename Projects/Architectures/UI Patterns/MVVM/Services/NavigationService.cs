using MVVM.Stores;
using MVVM.ViewModels;
using System;

namespace MVVM.Services
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            // Изменить ативную View с 'View Reservations' на 'Make Reservation'
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
