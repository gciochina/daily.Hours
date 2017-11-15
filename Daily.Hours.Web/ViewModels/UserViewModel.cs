using System.Collections.Generic;

namespace Daily.Hours.Web.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int InviterId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActivated { get; set; }
        public string Password { get; set; }
    }
}