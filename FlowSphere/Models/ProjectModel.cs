using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlowSphere.Models
{
    public class ProjectModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ProjectCreationDate { get; set; }

        public string[] AssignedUsers { get; set; }

        public TaskModel[] ToDoTasks { get; set; } = [];

        public TaskModel[] InProgressTasks { get; set; } = [];

        public TaskModel[] DoneTasks { get; set; } = [];
    }
}