using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Services;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using Daily.Hours.Web.ViewModels;
using System.Net.Http;
using System.Net;
using System.Configuration;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        #region members
        private UserService _userService = new UserService();
        #endregion

        #region actions
        [HttpPut]
        public UserViewModel Create(UserViewModel user)
        {
            if (!AuthenticatedUserIsAdmin)
            {
                throw new UnauthorizedAccessException("No access. You are not an administrator");
            }

            return _userService.Create(user, AuthenticatedUserId);
        }

        [HttpPost]
        public UserViewModel Update(UserViewModel user)
        {
            if (AuthenticatedUserIsAdmin || AuthenticatedUserId == user.Id )
            {
                return _userService.Update(user);
            }
            else
            {
                throw new UnauthorizedAccessException("No access. You are not an administrator and you're attempting to update someone else");
            }

            
        }

        [HttpGet]
        public bool Delete(int userId)
        {
            if (!AuthenticatedUserIsAdmin)
            {
                throw new UnauthorizedAccessException("No access. You are not an administrator");
            }

            return _userService.Delete(userId);
        }

        [HttpGet]
        public UserViewModel Get(int userId)
        {
            return _userService.Get(userId);
        }

        [HttpGet]
        public List<UserViewModel> List(int inviterId)
        {
            return _userService.List(inviterId);
        }

        [AllowAnonymous]
        [HttpPost]
        public UserViewModel Login(UserViewModel user, bool rememberMe)
        {
            UserViewModel authenticatedUser = null;
            authenticatedUser = _userService.Login(user.EmailAddress, user.Password);

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
        public UserViewModel Register(UserViewModel user)
        {
            return _userService.Create(user, null);
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Activate(string id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var activatedUserModel = _userService.Activate(id);

            if (activatedUserModel != null)
            {
                Login(activatedUserModel, true);

                response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri(ConfigurationManager.AppSettings["HostUrl"]);
            }
            return response;
        }

        /// <summary>
        /// get the current user (if any is logged in)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public UserViewModel WhoAmI()
        {
            var currentUserIdentity = User.Identity;
            if (currentUserIdentity.IsAuthenticated)
            {
                try
                {
                    return
                         new UserViewModel
                         {
                             Id = Convert.ToInt32(((ClaimsIdentity)currentUserIdentity).FindFirstValue(ClaimTypes.NameIdentifier)),
                             FirstName = ((ClaimsIdentity)currentUserIdentity).FindFirstValue(ClaimTypes.Name),
                             LastName = ((ClaimsIdentity)currentUserIdentity).FindFirstValue(ClaimTypes.Surname),
                             EmailAddress = ((ClaimsIdentity)currentUserIdentity).FindFirstValue(ClaimTypes.Email),
                             IsAdmin = Convert.ToBoolean(((ClaimsIdentity)currentUserIdentity).FindFirstValue(ClaimTypes.Role))
                         };
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        #endregion

        #region privates

        private void IdentitySignin(UserViewModel user, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            // create *required* claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            // create *optional* claims (the ones we need for storing user related data)
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress ?? string.Empty));
            claims.Add(new Claim(ClaimTypes.Role, user.IsAdmin.ToString()));

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
        #endregion
    }
}
