using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;
using Owin;

namespace Daily.Hours.Web
{
    public class Startup
    {
        private static bool IsAjaxRequest(IOwinRequest request)
        {
            var query = request.Query;
            if ((query != null) 
                && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            var headers = request.Headers;
            return ((headers != null) 
                && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var cookieAuthOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/api/User/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = context =>
                    {
                        if (IsAjaxRequest(context.Request))
                        {
                            context.Response.Redirect(context.RedirectUri);
                        }
                    }
                }
            };

            

            app.UseCookieAuthentication(cookieAuthOptions);
        }
    }
}
