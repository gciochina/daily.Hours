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
    public class TaskService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal TaskModel Create(TaskModel task)
        {
            task.Project = _context.Projects.Single(p => p.Id == task.Project.Id);

            _context.Tasks.Add(task);

            _context.SaveChanges();

            return task;
        }

        internal TaskModel Update(TaskModel task)
        {
            var taskToUpdate = _context.Tasks.SingleAsync(u => u.Id == task.Id).Result;
            taskToUpdate.Name = task.Name;
            taskToUpdate.Project = task.Project;

            _context.SaveChanges();

            return taskToUpdate;
        }

        internal bool Delete(int taskId)
        {
            _context.Tasks.Remove(_context.Tasks.SingleOrDefaultAsync(u=>u.Id == taskId).Result);
            _context.SaveChanges();
            return true;
        }

        internal List<TaskModel> List(int userId)
        {
            return _context.Tasks.Where(t => t.Project.Users.Any(u=>u.Id == userId)).ToList();
        }

        internal Task<TaskModel> Get(int taskId)
        {
            return _context.Tasks.SingleOrDefaultAsync(u => u.Id == taskId);
        }
    }
}