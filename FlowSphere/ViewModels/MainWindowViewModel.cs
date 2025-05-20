using FlowSphere.MVVM;
using FlowSphere.Views;

namespace FlowSphere.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            if (_currentView != value)
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
    }

    public MainWindowViewModel()
    {
        CurrentView = new LoginView(this);
    }

    public void SwitchToMainView()
    {
        CurrentView = new MainView();
    }
}