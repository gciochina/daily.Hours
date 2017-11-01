using System.Data.Entity;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

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
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
    }
}