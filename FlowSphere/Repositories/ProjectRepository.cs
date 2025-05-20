using System.Net;
using FlowSphere.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlowSphere.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly IMongoCollection<ProjectModel> _projects;
    private readonly string _connectionUri = App.Configuration["MongoDB:ConnectionString"]!;

    public ProjectRepository()
    {
         var settings = MongoClientSettings.FromConnectionString(_connectionUri);
         settings.ServerApi = new ServerApi(ServerApiVersion.V1);
         var client = new MongoClient(settings);
         var database = client.GetDatabase(App.Configuration["MongoDB:DatabaseName"]);
        _projects = database.GetCollection<ProjectModel>("projects");
    }
    
    public Task<ProjectModel> GetProjectById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectModel> GetProjectByIdAsync(string projectId)
    {
        var filter = Builders<ProjectModel>.Filter.Eq(p => p.Id, ObjectId.Parse(projectId));
        return await _projects.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<ProjectModel>> GetProjectsByAssignedUser(string user)
    {
        var filter = Builders<ProjectModel>.Filter.AnyEq(p => p.AssignedUsers, user);
        return await _projects.Find(filter).ToListAsync();
    }
    
    public async Task UpdateProjectAsync(ProjectModel project)
    {
        var filter = Builders<ProjectModel>.Filter.Eq(p => p.Id, project.Id);
        await _projects.ReplaceOneAsync(filter, project);
    }

}