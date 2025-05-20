using System.Net;

namespace FlowSphere.Models;

public interface IProjectRepository
{
    Task<ProjectModel> GetProjectById(string id);
    Task<List<ProjectModel>> GetProjectsByAssignedUser(string user);
    Task<ProjectModel> GetProjectByIdAsync(string projectId);
    
    public Task UpdateProjectAsync(ProjectModel project);
}