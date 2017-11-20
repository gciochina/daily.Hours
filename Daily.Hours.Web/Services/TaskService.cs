using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Daily.Hours.Web.ViewModels;

namespace Daily.Hours.Web.Services
{
    public class TaskService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal TaskViewModel Create(TaskViewModel taskViewModel)
        {
            var taskModel = new Models.Task
            {
                Name = taskViewModel.Name,
                Project = _context.Projects.Single(p => p.ProjectId == taskViewModel.ProjectId)
            };

            _context.Tasks.Add(taskModel);

            _context.SaveChanges();

            return TaskViewModel.From(taskModel);
        }

        internal TaskViewModel Update(TaskViewModel task)
        {
            //var taskToUpdate = _context.Tasks.SingleAsync(u => u.Id == task.Id).Result;
            //taskToUpdate.Name = task.Name;
            //taskToUpdate.Project = task.Project;

            //_context.SaveChanges();

            //return taskToUpdate;
            return null;
        }

        internal List<TaskViewModel> Search(string term, int userId, int projectId)
        {
            var tasksList = _context.Tasks.Where(t => (t.Project.Users.Any(u => u.UserId == userId) || t.Project.Owner.UserId == userId) && t.Project.ProjectId == projectId);
            if (!string.IsNullOrEmpty(term))
            {
                tasksList = tasksList.Where(t => t.Name.Contains(term));
            }
            return tasksList.ToList().Select(t => TaskViewModel.From(t)).ToList();
        }

        internal bool Delete(int taskId)
        {
            _context.Tasks.Remove(_context.Tasks.SingleOrDefaultAsync(u=>u.TaskId == taskId).Result);
            _context.SaveChanges();
            return true;
        }

        internal List<TaskViewModel> List(int userId)
        {
            var tasksList = _context.Tasks.Where(t => t.Project.Users.Any(u => u.UserId == userId) || t.Project.Owner.UserId == userId).ToList();
            return tasksList.Select(t => TaskViewModel.From(t)).ToList();
        }

        internal TaskViewModel Get(int taskId)
        {
            var taskModel = _context.Tasks.Single(u => u.TaskId == taskId);
            return TaskViewModel.From(taskModel);
        }
    }
}