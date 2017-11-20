using System;
using System.Collections.Generic;

namespace Daily.Hours.Web.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public Project()
        {
            Tasks = new HashSet<Task>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ICollection<User> Users { get; set; }
        
        public virtual User Owner { get; set; }
    }
}