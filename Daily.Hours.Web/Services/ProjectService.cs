using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Daily.Hours.Web.ViewModels;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ProjectService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal ProjectViewModel Create(ProjectViewModel projectViewModel, int ownerId)
        {
            var projectModel = new ProjectModel
            {
                CreatedOn = projectViewModel.CreatedOn == DateTime.MinValue? DateTime.Now : projectViewModel.CreatedOn,
                IsActive = projectViewModel.IsActive,
                Name = projectViewModel.Name,
                Owner = _context.Users.Single(u => u.Id == ownerId)
            };

            _context.Projects.Add(projectModel);

            _context.SaveChanges();

            return ProjectViewModel.From(projectModel);
        }

        internal ProjectViewModel Update(ProjectViewModel project)
        {
            //var projectToUpdate = _context.Projects.SingleAsync(u => u.Id == project.Id).Result;
            //projectToUpdate.Name= projectToUpdate.Name;
            //return projectToUpdate;
            return null;
        }

        internal bool Delete(int projectId)
        {
            _context.Projects.Remove(_context.Projects.SingleOrDefaultAsync(u=>u.Id == projectId).Result);
            _context.SaveChanges();
            return true;
        }

        internal ProjectViewModel Get(int projectId)
        {
            var project = _context.Projects.Single(u => u.Id == projectId);
            return ProjectViewModel.From(project);
        }

        internal List<ProjectViewModel> List(int userId)
        {
            var projectsList = _context.Projects.Where(p => p.Owner.Id == userId).ToList();
            return projectsList.Select(p => ProjectViewModel.From(p)).ToList();
        }
    }
}