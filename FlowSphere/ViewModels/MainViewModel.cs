using System.Windows.Input;
using FlowSphere.MVVM;
using FlowSphere.Views;

namespace FlowSphere.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        CurrentView = new ProjectsView(LoggedUserName);
        ShowProjectsViewCommand = new RelayCommand(ExecuteShowProjectsViewCommand);
        ShowSettingsViewCommand = new RelayCommand(ExecuteShowSettingsViewCommand);
    }

    private void ExecuteShowSettingsViewCommand(object obj)
    {
        CurrentView = new LoginView(null);
    }

    private void ExecuteShowProjectsViewCommand(object obj)
    { 
        CurrentView = new ProjectsView(LoggedUserName);
    }

    private string? _loggedUserName = Thread.CurrentPrincipal.Identity.Name;
    private object _currentView;
    public ICommand ShowProjectsViewCommand { get; set; }
    public ICommand ShowSettingsViewCommand { get; set; }
    public string? LoggedUserName
    {
        get => _loggedUserName;
        set
        { 
            _loggedUserName = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
    public object CurrentView
    {
        get => _currentView;
        set
        { 
            _currentView = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }
}