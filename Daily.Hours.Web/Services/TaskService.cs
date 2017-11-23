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

        internal TaskViewModel Update(TaskViewModel task, int userId)
        {
            var taskToUpdate = _context.Tasks.SingleAsync(t => t.TaskId == task.Id).Result;

            if (!taskToUpdate.Project.Users.Any(u => u.UserId == userId) && taskToUpdate.Project.Owner.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not allowed to edit this task!");
            }

            taskToUpdate.Name = task.Name;

            _context.SaveChanges();

            return TaskViewModel.From(taskToUpdate);
        }

        internal List<TaskViewModel> Search(string term, int userId, int projectId)
        {
            var tasksList = _context.Tasks.Where(t => (t.Project.Users.Any(u => u.UserId == userId) || t.Project.Owner.UserId == userId) && t.Project.ProjectId == projectId);
            if (!string.IsNullOrEmpty(term))
            {
                tasksList = tasksList.Where(t => t.Name.StartsWith(term));
            }
            return tasksList.ToList().Select(t => TaskViewModel.From(t)).ToList();
        }

        internal bool Delete(int taskId, int userId)
        {
            var taskToRemove = _context.Tasks.Single(t => t.TaskId == taskId);
            if (!taskToRemove.Project.Users.Any(u=>u.UserId == userId) && taskToRemove.Project.Owner.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have access to this task");
            }

            if (_context.WorkLogs.Any(l=>l.Task.TaskId == taskId))
            {
                throw new InvalidOperationException("Work was already reported on this task");
            }

            _context.Tasks.Remove(taskToRemove);
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