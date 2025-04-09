using System.Net;
using FlowSphere.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FlowSphere.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _users;
    private readonly string _connectionUri = App.Configuration["MongoDB:ConnectionString"]!;

    public UserRepository()
    {
        var settings = MongoClientSettings.FromConnectionString(_connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase(App.Configuration["MongoDB:DatabaseName"]);
        _users = database.GetCollection<UserModel>("users");
    }

    public int AuthenticateUser(NetworkCredential credentials)
    {
        var user = _users.Find(u => u.Email == credentials.UserName).FirstOrDefault();

        if (user == null)
        {
            return 1; // nie odnaleziono maila
        }
        return user.Password != credentials.Password ? 2 : 0; // 2 - hasło błędne, 0 - wszystko OK
    }

    public void CreateUser(UserModel user)
    {
    }

    public void RemoveUser(string id)
    {
    }

    public UserModel GetUserById(string id)
    {
        return null;
    }

    public UserModel GetUserByEmail(string email)
    {
        return null;
    }

    public IEnumerable<UserModel> GetAllUsers()
    {
        return null;
    }
}