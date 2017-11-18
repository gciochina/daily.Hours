using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;
using System.Collections.Generic;
using System.Security.Principal;
using System.Linq;
using System.Security.Claims;
using System;
using Microsoft.AspNet.Identity;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : ApiController
    {
        public int AuthenticatedUserId
        {
            get
            {
                return int.Parse(((ClaimsIdentity)User.Identity).FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }

        public bool AuthenticatedUserIsAdmin
        {
            get
            {
                return bool.Parse(((ClaimsIdentity)User.Identity).FindFirstValue(ClaimTypes.Role));
            }
        }
    }
}
