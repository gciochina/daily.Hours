using System;
using System.Collections.Generic;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? InviterId { get; set; }

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


        internal static UserViewModel From(UserModel user)
        {
            return new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                Id = user.Id,

                IsActivated = user.IsActivated,
                IsAdmin = user.IsAdmin,
                InviterId = user.Inviter?.Id
            };
        }
    }
}