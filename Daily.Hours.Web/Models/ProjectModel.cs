using System;
using System.Collections.Generic;

namespace Daily.Hours.Web.Models
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual List<TaskModel> Tasks { get; set; }

        public virtual List<UserModel> Users { get; set; }
        
        public virtual UserModel Owner { get; set; }
    }
}