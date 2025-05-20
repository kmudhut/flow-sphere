using System.Collections.ObjectModel;
using System.Windows.Input;
using FlowSphere.Models;
using FlowSphere.MVVM;
using FlowSphere.Repositories;
using FlowSphere.Services;

namespace FlowSphere.ViewModels;

public class ProjectsViewModel : ViewModelBase
{
    private readonly IProjectRepository _projectRepository;
    private readonly INavigationService _navigationService;

    public ObservableCollection<ProjectModel> Projects { get; set; } = new();
    public ICommand OpenProjectCommand { get; }

    public ProjectsViewModel(string username, INavigationService navigationService)
    {
        _projectRepository = new ProjectRepository();
        _navigationService = navigationService;
        OpenProjectCommand = new RelayCommand(ExecuteOpenProjectCommand);
        _ = InitializeAsync(username);
    }

    private async Task InitializeAsync(string username)
    {
        var projects = await _projectRepository.GetProjectsByAssignedUser(username);
        foreach (var project in projects)
        {
            Projects.Add(project);
        }
    }

    private void ExecuteOpenProjectCommand(object arg)
    {
        if (arg is not ProjectModel project)
            return;
        
        var projectDetailsVM = new ProjectDetailsViewModel(project.Id.ToString());
        Console.WriteLine(project.Id);
        _navigationService.NavigateTo(projectDetailsVM);
    }
}