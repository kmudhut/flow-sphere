using FlowSphere.ViewModels;
using FlowSphere.Views;

namespace FlowSphere.Services;

public class NavigationService : INavigationService
{
    private readonly MainViewModel _mainViewModel;

    public NavigationService(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public void NavigateTo(object view)
    {
        if (view is ProjectDetailsViewModel pdvm)
        {
            _mainViewModel.CurrentView = new ProjectDetailsView { DataContext = pdvm };
        }
    }
}
