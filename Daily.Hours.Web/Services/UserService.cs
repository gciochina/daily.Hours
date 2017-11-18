using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Configuration;
using Daily.Hours.Web.ViewModels;
using System.Net;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal UserViewModel Create(UserViewModel user, int? inviterId)
        {
            if (_context.Users.AnyAsync(u => u.EmailAddress == user.EmailAddress).Result)
                throw new ArgumentException("This email address is already registered");

            var userModel = new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = System.Web.Security.Membership.GeneratePassword(8, 1),
                IsAdmin = false,
                Inviter = _context.Users.Single(u => u.Id == inviterId),
                IsActivated = false
            };
            userModel.Projects = _context.Projects.Where(p => p.Owner.Id == inviterId).ToList();

            _context.Users.Add(userModel);

            var activationLink = GenerateUserActivationString(user);

            var message = new MailMessage(
                new MailAddress(ConfigurationManager.AppSettings["NoReplyAddress"], ConfigurationManager.AppSettings["NoReplyName"]),
                new MailAddress(user.EmailAddress, user.FullName))
            {
                Subject = $"daily.Hours Account Confirmation",
                IsBodyHtml = true,
                Body = $@"
Hey {userModel.FullName },<br/>
<br />
An account registration was started for your email address.<br />
<br />
Here are your login details:<br />
User: {userModel.EmailAddress}<br />
Pass: {userModel.Password}<br />
In order to prevent spammers, please confirm you email by clicking the link below:<br />
<a href='{activationLink}'>Yes, that's me!</a><br />
<br />
Cheers,<br />
daily.Hours"

            };

            //send email
            using (var client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServerHost"], Convert.ToInt32(ConfigurationManager.AppSettings["SmtpServerPort"])))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NoReplyAddress"], ConfigurationManager.AppSettings["SmtpPassword"]);

                client.Send(message);
            };

            _context.SaveChanges();

            return user;
        }

        private string GenerateUserActivationString(UserViewModel user)
        {
            return
                ConfigurationManager.AppSettings["HostUrl"] + 
                @"\api\User\Activate\" +
                Base64Encode(DateTime.Now.ToShortDateString() + "|" + user.EmailAddress);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public UserViewModel Activate(string userActivationId)
        {
            var decodedActivationString = Base64Decode(userActivationId);
            var emailAddress = decodedActivationString.Split('|')[1];

            var user = _context.Users.SingleOrDefault(u => u.EmailAddress == emailAddress);

            user.IsActivated = true;

            _context.SaveChanges();

            return UserViewModel.From(user);
        }

        internal UserViewModel Update(UserViewModel user)
        {
            var userToUpdate = _context.Users.SingleAsync(u => u.Id == user.Id).Result;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.IsAdmin = user.IsAdmin;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Password = user.Password;
            userToUpdate.EmailAddress = user.EmailAddress;

            _context.SaveChanges();

            return UserViewModel.From(userToUpdate);
        }

        internal List<UserViewModel> List(int inviterId)
        {
            var usersList = _context.Users.Where(u => u.Inviter.Id == inviterId || u.Id == inviterId).ToList();
            return usersList.Select(u=> UserViewModel.From(u)).ToList();
        }

        internal bool Delete(int userId)
        {
            _context.Users.Remove(_context.Users.SingleOrDefaultAsync(u=>u.Id == userId).Result);
            _context.SaveChanges();
            return true;
        }

        internal UserViewModel Get(int userId)
        {
            var user = _context.Users.Single(u => u.Id == userId);
            return UserViewModel.From(user);
        }

        internal UserViewModel Login(string emailAddress, string password)
        {
            var user = _context.Users.Single(u => u.EmailAddress == emailAddress && u.Password == password && u.IsActivated);
            return UserViewModel.From(user);
        }
    }
}