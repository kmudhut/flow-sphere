using System.Windows.Controls;
using FlowSphere.Services;
using FlowSphere.ViewModels;

namespace FlowSphere.Views;

public partial class ProjectsView : UserControl
{
    public ProjectsView(string username, INavigationService navigationService)
    {
        ProjectsViewModel vm = new ProjectsViewModel(username, navigationService);
        DataContext = vm;
        InitializeComponent();
    }
}