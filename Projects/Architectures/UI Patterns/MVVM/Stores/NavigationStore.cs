using MVVM.ViewModels;
using System;

namespace MVVM.Stores;

public class NavigationStore
{
    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            // Если ViewModel существует, освобождаем занимаемые ею ресурсы
            _currentViewModel?.Dispose();

            _currentViewModel = value;

            OnCurrentViewModelChanged();
        }
    }

    public event Action CurrentViewModelChanged;

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}
