using Daily.Hours.Web.Services;
using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Daily.Hours.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //InitializeDb();
        }

        #region initializer
        private void InitializeDb()
        {
            Database.SetInitializer<DailyHoursContext>(new DropCreateDatabaseAlways<DailyHoursContext>());

            DailyHoursContext _context = new DailyHoursContext();

            var defaultUser = new Models.User
            {
                EmailAddress = "gciochina@gmail.com",
                FirstName = "Gabriel",
                LastName = "Ciochina",
                IsActivated = true,
                IsAdmin = true,
                Password = "tauceti"
            };
            _context.Users.Add(defaultUser);

            var defaultProject = new Models.Project
            {
                CreatedOn = DateTime.Now,
                Name = "DCX",
                IsActive = true,
                Owner = defaultUser
            };

            _context.Projects.Add(defaultProject);

            var defaultTask1 = new Models.Task
            {
                Name = "DCX-43 SCAN Xamarin Advertisement support",
                Project = defaultProject
            };

            var defaultTask2 = new Models.Task
            {
                Name = "DCX-3 Reconstruct in-memory session whenever a call arrives in BUS after a reset",
                Project = defaultProject
            };

            _context.Tasks.Add(defaultTask1);
            _context.Tasks.Add(defaultTask2);

            var defaultActivity = new Models.WorkLog
            {
                Date = DateTime.Now,
                Hours = 5,
                Task = defaultTask1,
                User = defaultUser,
                Description = @"
- added repository
- integrate security
- testing scenario#1
- updated PBC
"
            };

            _context.WorkLogs.Add(defaultActivity);

            _context.SaveChanges();
        }
        #endregion
    }
}
