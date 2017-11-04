using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Security.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System;
using System.Security.Principal;
using System.Net;

namespace WebApp
{
    //public class BasicAuthMessageHandler : DelegatingHandler
    //{
    //    private const string BasicAuthResponseHeader = "WWW-Authenticate";
    //    private const string BasicAuthResponseHeaderValue = "Basic";

    //    public adminPrincipalProvider PrincipalProvider = new adminPrincipalProvider();

    //    protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
    //        HttpRequestMessage request,
    //        CancellationToken cancellationToken)
    //    {
    //        AuthenticationHeaderValue authValue = request.Headers.Authorization;
    //        if (authValue != null && authValue.Parameter != "undefined" &&
    //            !String.IsNullOrWhiteSpace(authValue.Parameter))
    //        {
    //            string email = authValue.Parameter;
    //            if (HttpContext.Current.Session == null ||
    //                HttpContext.Current.Session["userToken"] == null ||
    //                string.IsNullOrEmpty(HttpContext.Current.Session["userToken"].ToString()))
    //            {
    //                HttpContext.Current.Session["userToken"] = email;
    //            }
    //            else
    //            {
    //                email = HttpContext.Current.Session["userToken"].ToString();
    //            }

    //            if (!string.IsNullOrEmpty(email))
    //            {
    //                IPrincipal principalObj = PrincipalProvider.createPrincipal(email, "Admin");
    //                Thread.CurrentPrincipal = principalObj;
    //                HttpContext.Current.User = principalObj;
    //            }
    //        }
    //        return base.SendAsync(request, cancellationToken)
    //           .ContinueWith(task =>
    //           {
    //               var response = task.Result;
    //               if (response.StatusCode == HttpStatusCode.Unauthorized
    //                   && !response.Headers.Contains(BasicAuthResponseHeader))
    //               {
    //                   response.Headers.Add(BasicAuthResponseHeader
    //                       , BasicAuthResponseHeaderValue);
    //               }
    //               return response;
    //           });
    //    }
    //}

    //public class AddCustomHeaderFilter : ActionFilterAttribute
    //{
    //    private const string AuthorizationRequest = "/api/User/Login";

    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        if (!actionContext.ControllerContext.RequestContext.Principal.Identity.IsAuthenticated && !actionContext.Request.RequestUri.ToString().EndsWith(AuthorizationRequest))
    //        {
    //            throw new AuthenticationException("Unauthenticated user");
    //        }

    //        base.OnActionExecuting(actionContext);
    //    }

    //    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        actionExecutedContext.Response.Headers.Add("backendVersion", typeof(Daily.Hours.Web.Controllers.HomeController).Assembly.GetName().Version.ToString());
    //    }
    //}

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    //public class ApplicationUserManager : UserManager<ApplicationUser>
    //{
    //    public ApplicationUserManager(IUserStore<ApplicationUser> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //        // Configure validation logic for usernames
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };
    //        // Configure validation logic for passwords
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}
}
