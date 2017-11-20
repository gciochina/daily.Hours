using System.Data.Entity;
using Daily.Hours.Web.Models;
using System;

namespace Daily.Hours.Web.Services
{
    public class DailyHoursContext : DbContext
    {
        public DailyHoursContext() : base("MyContext")
        {

        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WorkLog> WorkLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>();
            modelBuilder.Entity<Task>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<WorkLog>();

            modelBuilder.Entity<User>()
                .HasMany<Project>(x => x.Projects)
                .WithMany(x => x.Users)
                .Map(x => 
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("ProjectId");
                    x.ToTable("UserProjects");
                });
        }
    }
}