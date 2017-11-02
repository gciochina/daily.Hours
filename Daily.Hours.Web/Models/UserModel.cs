using System.Collections.Generic;

namespace Daily.Hours.Web.Models
{
    public class UserModel : BaseModel
    {
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserModel Inviter { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public List<ProjectModel> Projects { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}