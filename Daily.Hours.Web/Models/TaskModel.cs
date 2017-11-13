namespace Daily.Hours.Web.Models
{
    public class TaskModel : BaseModel
    {
        public ProjectModel Project { get; set; }
        public int Project_Id { get; set; }
        public string Name { get; set; }
    }
}