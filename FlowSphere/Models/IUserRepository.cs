using System.Net;

namespace FlowSphere.Models;

public interface IUserRepository
{
    int AuthenticateUser(NetworkCredential credentials);
    void CreateUser(UserModel user);
    void RemoveUser(string id);
    UserModel GetUserById(string id);
    UserModel GetUserByEmail(string email);
    IEnumerable<UserModel> GetAllUsers();
}