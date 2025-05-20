public class TaskModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } 
    public string[] AssignedUsers { get; set; } 
    public string AssignedUsersString { get; set; } // tylko do formularza
}