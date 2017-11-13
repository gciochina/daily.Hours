using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Configuration;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal UserModel Create(UserModel user)
        {
            if (_context.Users.AnyAsync(u => u.UserName == user.UserName).Result)
                throw new ArgumentException("This username is already registered");

            if (_context.Users.AnyAsync(u => u.EmailAddress == user.EmailAddress).Result)
                throw new ArgumentException("This email address is already registered");

            _context.Users.Add(user);

#if RELEASE
            //send email
            var client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServerHost"]);
            var from = new MailAddress(ConfigurationManager.AppSettings["NoReplyAddress"]);
            var to = new MailAddress(user.EmailAddress);
            var activationLink = GenerateUserActivationString(user);

            var message = new MailMessage(from, to)
            {
                Subject = $"daily.Hours Account Confirmation",
                Body = @"
Hey there!

An account registration was started for your email address.
In order to prevent spammers, please confirm you email by clicking the link below:
<a href='{activationLink}'>Yes, that's me!</a>

Cheers,
daily.Hours"

            };
            client.Send(message);
#endif
            _context.SaveChanges();

            return user;
        }

        private string GenerateUserActivationString(UserModel user)
        {
            return
                ConfigurationManager.AppSettings["HostUrl"] + 
                @"\api\User\Activate\" +
                Base64Encode(user.UserName + "|" + user.EmailAddress);
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

        public UserModel Activate(string userActivationString)
        {
            var decodedActivationString = Base64Decode(userActivationString);
            var userName = decodedActivationString.Split('|')[0];
            var emailAddress = decodedActivationString.Split('|')[1];

            var user = _context.Users.SingleOrDefault(u => u.UserName == userName && u.EmailAddress == emailAddress);

            user.IsActivated = true;

            _context.SaveChanges();

            return user;
        }

        internal UserModel Update(UserModel user)
        {
            var userToUpdate = _context.Users.SingleAsync(u => u.Id == user.Id).Result;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.IsAdmin = user.IsAdmin;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Password = user.Password;
            userToUpdate.UserName = user.UserName;
            userToUpdate.EmailAddress = user.EmailAddress;
            _context.SaveChanges();
            return userToUpdate;
        }

        internal List<UserModel> List(int inviterId)
        {
            return _context.Users.Where(u => u.Inviter.Id == inviterId).ToList();
        }

        internal bool Delete(int userId)
        {
            _context.Users.Remove(_context.Users.SingleOrDefaultAsync(u=>u.Id == userId).Result);
            _context.SaveChanges();
            return true;
        }

        internal Task<UserModel> Get(int userId)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }

        internal UserModel Login(string userName, string password)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActivated).Result;
        }
    }
}