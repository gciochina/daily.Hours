using System.Data.Entity;
using Daily.Hours.Web.Models;
using System;

namespace Daily.Hours.Web.Services
{
    public class DailyHoursContext : DbContext
    {
        public DailyHoursContext() :base("MyContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectModel>();
            modelBuilder.Entity<TaskModel>();
            modelBuilder.Entity<UserModel>();
            modelBuilder.Entity<ActivityModel>();

            InitializeDb();
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        #region initializer
        private void InitializeDb()
        {
            Database.SetInitializer<DailyHoursContext>(null);

            return;
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
                Name = "DCX",
                IsActive = true,
                Owner = defaultUser
            };

            _context.Projects.Add(defaultProject);

            var defaultTask1 = new Models.TaskModel
            {
                Name = "DCX-43 SCAN Xamarin Advertisement support",
                Project = defaultProject
            };

            var defaultTask2 = new Models.TaskModel
            {
                Name = "DCX-3 Reconstruct in-memory session whenever a call arrives in BUS after a reset",
                Project = defaultProject
            };

            _context.Tasks.Add(defaultTask1);
            _context.Tasks.Add(defaultTask2);

            var defaultActivity = new Models.ActivityModel
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

            _context.Activities.Add(defaultActivity);

            _context.SaveChanges();
        }
        #endregion
    }
}