using MVVM.Stores;

namespace MVVM.ViewModels;

//! ViewModel для MainWindow.xaml
public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainViewModel(NavigationStore navigationStore)
    {
        // В MainWindow.xaml делаем binding, чтобы при запуске запускалась View
        // CurrentViewModel, а здесь этой переменной присваиваем значение
        // ReservationListingViewModel, то есть она и будет запущена
        _navigationStore = navigationStore;

        // Изменить ативную View с 'View Reservations' на 'Make Reservation'
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }
    
    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
