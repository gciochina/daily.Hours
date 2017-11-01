using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ProjectService
    {
        DailyHoursContext _context;

        internal ProjectModel Create(ProjectModel project)
        {
            _context.Projects.Add(project);

            _context.SaveChanges();

            return project;
        }

        internal ProjectModel Update(ProjectModel project)
        {
            var projectToUpdate = _context.Projects.SingleAsync(u => u.Id == project.Id).Result;
            projectToUpdate.Name= projectToUpdate.Name;
            return projectToUpdate;
        }

        internal bool Delete(int projectId)
        {
            _context.Projects.Remove(_context.Projects.SingleOrDefaultAsync(u=>u.Id == projectId).Result);
            _context.SaveChanges();
            return true;
        }

        internal Task<ProjectModel> Get(int projectId)
        {
            return _context.Projects.SingleOrDefaultAsync(u => u.Id == projectId);
        }
    }
}