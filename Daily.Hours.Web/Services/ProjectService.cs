using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Daily.Hours.Web.ViewModels;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Services
{
    public class ProjectService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal ProjectViewModel Create(ProjectViewModel projectViewModel, int ownerId)
        {
            var projectModel = new Project
            {
                CreatedOn = projectViewModel.CreatedOn == DateTime.MinValue? DateTime.Now : projectViewModel.CreatedOn,
                IsActive = true,
                Name = projectViewModel.Name,
                Owner = _context.Users.Single(u => u.UserId == ownerId)
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
            _context.Projects.Remove(_context.Projects.SingleOrDefaultAsync(u=>u.ProjectId == projectId).Result);
            _context.SaveChanges();
            return true;
        }

        internal ProjectViewModel Get(int projectId)
        {
            var project = _context.Projects.Single(u => u.ProjectId == projectId);
            return ProjectViewModel.From(project);
        }

        internal List<ProjectViewModel> Search(string term, int userId)
        {
            var projectsList = _context.Projects.Where(p =>(p.Users.Any(u => u.UserId == userId) || p.Owner.UserId == userId));
            if (!string.IsNullOrEmpty(term))
            {
                projectsList = projectsList.Where(p => p.Name.Contains(term));
            }
            return projectsList.ToList().Select(p => ProjectViewModel.From(p)).ToList();
        }

        internal List<ProjectViewModel> List(int userId)
        {
            var projectsList = _context.Projects.Where(p => p.Owner.UserId == userId).ToList();
            return projectsList.Select(p => ProjectViewModel.From(p)).ToList();
        }
    }
}