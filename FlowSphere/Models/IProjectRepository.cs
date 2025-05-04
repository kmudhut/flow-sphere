using System.Net;

namespace FlowSphere.Models;

public interface IProjectRepository
{
    Task<ProjectModel> GetProjectById(string id);
    Task<List<ProjectModel>> GetProjectsByAssignedUser(string user);
}