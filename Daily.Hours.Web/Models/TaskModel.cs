namespace Daily.Hours.Web.Models
{
    public class TaskModel : BaseModel
    {
        public ProjectModel Project { get; set; }
        public string Name { get; set; }
    }
}