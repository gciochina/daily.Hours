using System.Collections.Generic;

namespace Daily.Hours.Web.Models
{
    public class User
    {
        public int UserId { get; set; }

        public User()
        {
            Projects = new HashSet<Project>();
        }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public bool IsAdmin { get; set; }

        public bool IsActivated { get; set; }

        public string Password { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public virtual User Inviter { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}