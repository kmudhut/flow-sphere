using System.Windows.Controls;
using System.Windows.Input;
using FlowSphere.MVVM;
using FlowSphere.Services;
using FlowSphere.Views;

namespace FlowSphere.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    public MainViewModel()
    {
        _navigationService = new NavigationService(this);
        CurrentView = new ProjectsView(LoggedUserName, _navigationService);
        ShowProjectsViewCommand = new RelayCommand(ExecuteShowProjectsViewCommand);
        ShowSettingsViewCommand = new RelayCommand(ExecuteShowSettingsViewCommand);
    }

    private void ExecuteShowSettingsViewCommand(object obj)
    {
        CurrentView = new TextBlock { Text = "Ustawienia" }; // Tymczasowo
    }

    private void ExecuteShowProjectsViewCommand(object obj)
    { 
        CurrentView = new ProjectsView(LoggedUserName, _navigationService);
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