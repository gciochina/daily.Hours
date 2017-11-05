using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;
using System.Collections.Generic;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Net;
using System;
using System.Web;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private UserService _userService = new UserService();
        
        [HttpPut]
        public UserModel Create(UserModel user)
        {
            return _userService.Create(user);
        }

        [HttpPost]
        public UserModel Update(UserModel user)
        {
            return _userService.Update(user);
        }

        [HttpDelete]
        public bool Delete(int userId)
        {
            return _userService.Delete(userId);
        }

        [HttpGet]
        public Task<UserModel>Get(int userId)
        {
            return _userService.Get(userId);
        }

        [HttpGet]
        public List<UserModel> List(int inviterId)
        {
            return _userService.List(inviterId);
        }

        [AllowAnonymous]
        [HttpPost]
        public UserModel Login(UserModel userModel, bool rememberMe)
        {
            UserModel authenticatedUser = null;
            authenticatedUser = _userService.Login(userModel.UserName, userModel.Password);

            if (authenticatedUser == null)
                throw new UnauthorizedAccessException("Username or password incorrect");

            IdentitySignin(authenticatedUser, rememberMe);

            return authenticatedUser;
        }

        [HttpPost]
        public void Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                          DefaultAuthenticationTypes.ExternalCookie);
        }

        [AllowAnonymous]
        [HttpPost]
        public UserModel Register(UserModel user)
        {
            return _userService.Create(user);
        }

        private void IdentitySignin(UserModel userModel, bool isPersistent = false) //string userId, string name, string providerKey = null, bool isPersistent = false
        {
            var claims = new List<Claim>();

            // create *required* claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, userModel.FullName));
            // create *optional* claims (the ones we need for storing user related data)
            claims.Add(new Claim(ClaimTypes.Email, userModel.EmailAddress ?? string.Empty));
            claims.Add(new Claim(ClaimTypes.Role, userModel.IsAdmin.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            }, identity);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
    }
}
