
using System.Collections.ObjectModel;
using FlowSphere.Models;
using FlowSphere.MVVM;
using FlowSphere.Repositories; 
using MongoDB.Bson;

namespace FlowSphere.ViewModels;

public class ProjectDetailsViewModel : ViewModelBase
{
    private readonly IProjectRepository _projectRepository = new ProjectRepository();
    private ProjectModel _project;

    public ObservableCollection<TaskModel> ToDoTasks { get; set; } = new();
    public ObservableCollection<TaskModel> InProgressTasks { get; set; } = new();
    public ObservableCollection<TaskModel> DoneTasks { get; set; } = new();

    public string ProjectName => _project?.Name ?? string.Empty;
    public string ProjectDescription => _project?.Description ?? string.Empty;
    private string _projectId;
    public ProjectDetailsViewModel(string projectId)
    {
        _projectId = projectId;
        _ = LoadProjectAsync(projectId);
    }

    private async Task LoadProjectAsync(string projectId)
    {
        if (!ObjectId.TryParse(projectId, out var objectId))
            return;

        _project = await _projectRepository.GetProjectByIdAsync(projectId);

        if (_project == null) return;

        ToDoTasks = new ObservableCollection<TaskModel>(_project.ToDoTasks ?? []);
        InProgressTasks = new ObservableCollection<TaskModel>(_project.InProgressTasks ?? []);
        DoneTasks = new ObservableCollection<TaskModel>(_project.DoneTasks ?? []);

        // Zgłoszenie zmian dla widoku (jeśli używasz bindingów)
        OnPropertyChanged(nameof(ToDoTasks));
        OnPropertyChanged(nameof(InProgressTasks));
        OnPropertyChanged(nameof(DoneTasks));
        OnPropertyChanged(nameof(ProjectName));
        OnPropertyChanged(nameof(ProjectDescription));
    }

    public void MoveTask(TaskModel task, string fromList, string toList)
    {
        if (fromList == toList) return;

        // Usunięcie zadania z listy źródłowej
        var fromCollection = GetListByName(fromList);
        fromCollection?.Remove(task);

        // Dodanie zadania do listy docelowej
        var toCollection = GetListByName(toList);
        toCollection?.Add(task);

        // Zaktualizowanie projektu w bazie danych
        UpdateProjectInDatabase();
    }

    private async Task UpdateProjectInDatabase()
    {
        var project = await _projectRepository.GetProjectByIdAsync(_projectId);
        if (project == null) return;

        // Zaktualizuj zadania w projekcie
        project.ToDoTasks = ToDoTasks.ToArray();
        project.InProgressTasks = InProgressTasks.ToArray();
        project.DoneTasks = DoneTasks.ToArray();

        // Zapisz zmiany w bazie danych
         _projectRepository.UpdateProjectAsync(project);
    }

    private ObservableCollection<TaskModel> GetListByName(string name) => name switch
    {
        "ToDo" => ToDoTasks,
        "InProgress" => InProgressTasks,
        "Done" => DoneTasks,
        _ => null
    };
}