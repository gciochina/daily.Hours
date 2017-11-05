using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ProjectService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal ProjectModel Create(ProjectModel project, int ownerId)
        {
            project.Owner = _context.Users.Single(u => u.Id == ownerId);

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

        internal List<ProjectModel> List(int userId)
        {
            return _context.Projects.Where(p => p.Owner.Id == userId).ToList();
        }
    }
}