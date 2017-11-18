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

            InitializeDb();
        }

        private void InitializeDb()
        {
            Database.SetInitializer<DailyHoursContext>(new DropCreateDatabaseAlways<DailyHoursContext>());

            DailyHoursContext _context = new DailyHoursContext();

            var defaultUser = new Models.UserModel
            {
                EmailAddress = "gciochina@gmail.com",
                FirstName = "Gabriel",
                LastName = "Ciochina",
                IsActivated = true,
                IsAdmin = true,
                Password = "tauceti"
            };
            _context.Users.Add(defaultUser);

            var defaultProject = new Models.ProjectModel
            {
                CreatedOn = DateTime.Now,
                Name = "MDC",
                IsActive = true,
                Owner = defaultUser
            };

            _context.Projects.Add(defaultProject);

            var defaultTask = new Models.TaskModel
            {
                Name = "MDC-43 MyiScan Xamarin Add Advertisement support",
                Project = defaultProject
            };

            _context.Tasks.Add(defaultTask);

            var defaultActivity = new Models.ActivityModel
            {
                Date = DateTime.Now,
                Hours = 5,
                Task = defaultTask,
                User = defaultUser
            };

            _context.Activities.Add(defaultActivity);

            _context.SaveChanges();
        }
    }
}
