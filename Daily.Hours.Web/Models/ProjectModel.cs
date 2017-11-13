using System;
using System.Collections.Generic;

namespace Daily.Hours.Web.Models
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<UserModel> Users { get; set; }
        public UserModel Owner { get; set; }
        public int Owner_Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}