using System.Collections.Generic;

namespace Daily.Hours.Web.Models
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}