namespace FlowSphere.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UserModel
{
    public enum AccountsPlans
    {
        Standard,
        Premium
    }
    [BsonId]
    public ObjectId Id { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime AccountCreationDate { get; set; }
    public bool IsVerified { get; set; }
    public AccountsPlans UserType { get; set; }
    public string ProjectsIds { get; set; }
    
}