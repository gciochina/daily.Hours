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
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Daily.Hours.Web.Services
{
    public class UserService
    {
        DailyHoursContext _context = new DailyHoursContext();

        private const string EncryptionKey = "MAKV2SPBNI99212";

        private readonly byte[] SaltKey = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

        internal UserViewModel Create(UserViewModel user, int? inviterId)
        {
            if (_context.Users.AnyAsync(u => u.EmailAddress == user.EmailAddress).Result)
                throw new ArgumentException("This email address is already registered");

            var plainPassword = System.Web.Security.Membership.GeneratePassword(8, 1);

            var userModel = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = Encrypt(plainPassword),
                IsAdmin = user.IsAdmin,
                IsActivated = false
            };

            if (inviterId.HasValue)
            {
                userModel.Inviter = _context.Users.Single(u => u.UserId == inviterId);
                //userModel.Projects = _context.Projects.Where(p => p.Owner.UserId == inviterId).ToList();
                var linkedProjectIds = user.Projects.Select(project => project.Id).ToList();
                userModel.Projects = _context.Projects.Where(p => linkedProjectIds.Contains(p.ProjectId)).ToList();
            }

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
Pass: {plainPassword}<br />
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
                Base64ForUrlEncode(DateTime.Now.ToShortDateString() + "|" + user.EmailAddress);
        }

        ///<summary>
        /// Base 64 Encoding with URL and Filename Safe Alphabet using UTF-8 character set.
        ///</summary>
        ///<param name="str">The origianl string</param>
        ///<returns>The Base64 encoded string</returns>
        public static string Base64ForUrlEncode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return HttpServerUtility.UrlTokenEncode(encbuff);
        }
        ///<summary>
        /// Decode Base64 encoded string with URL and Filename Safe Alphabet using UTF-8.
        ///</summary>
        ///<param name="str">Base64 code</param>
        ///<returns>The decoded string.</returns>
        public static string Base64ForUrlDecode(string str)
        {
            byte[] decbuff = HttpServerUtility.UrlTokenDecode(str);
            return Encoding.UTF8.GetString(decbuff);
        }

        public UserViewModel Activate(string userActivationId)
        {
            var decodedActivationString = Base64ForUrlDecode(userActivationId);
            var emailAddress = decodedActivationString.Split('|')[1];

            var user = _context.Users.SingleOrDefault(u => u.EmailAddress == emailAddress);

            user.IsActivated = true;

            _context.SaveChanges();

            user.Password = Decrypt(user.Password);

            return UserViewModel.From(user);
        }

        internal UserViewModel UpdateProfile(UserViewModel user)
        {
            var userToUpdate = _context.Users.SingleAsync(u => u.UserId == user.Id).Result;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.EmailAddress = user.EmailAddress;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                userToUpdate.Password = Encrypt(user.Password);
            }

            _context.SaveChanges();

            return UserViewModel.From(userToUpdate);
        }

        internal List<UserViewModel> List(int inviterId)
        {
            var usersList = _context.Users.Where(u => u.Inviter.UserId == inviterId || u.UserId == inviterId).ToList();
            return usersList.Select(u=> UserViewModel.From(u)).ToList();
        }

        internal bool Delete(int userId)
        {
            _context.Users.Remove(_context.Users.SingleOrDefaultAsync(u=>u.UserId == userId).Result);
            _context.SaveChanges();
            return true;
        }

        internal UserViewModel Get(int userId)
        {
            var user = _context.Users.Single(u => u.UserId == userId);
            return UserViewModel.From(user);
        }

        internal UserViewModel Login(string emailAddress, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.EmailAddress == emailAddress && u.IsActivated);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Incorrect user or password");
            }

            if (Decrypt(user.Password) != password)
            {
                throw new UnauthorizedAccessException("Incorrect user or password");
            }

            return UserViewModel.From(user);
        }

        private string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, SaltKey);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, SaltKey);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch
            {
                //ToDo: encrypt all passwords!
                //sink exception for now, for users that haven't encrypted their passwords, the decryption is not going to work!
            }

            return cipherText;
        }
    }
}