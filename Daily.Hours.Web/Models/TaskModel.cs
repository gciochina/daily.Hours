namespace Daily.Hours.Web.Models
{
    public class TaskModel : BaseModel
    {
        public string Name { get; set; }

        public virtual ProjectModel Project { get; set; }
    }
}