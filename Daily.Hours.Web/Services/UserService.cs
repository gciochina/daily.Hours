using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;
using System.Collections.Generic;
using System.Linq;

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
            return _context.Users.SingleOrDefaultAsync(u => u.UserName == userName && u.Password == password).Result;
        }
    }
}