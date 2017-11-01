using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class TaskService
    {
        DailyHoursContext _context;

        internal TaskModel Create(TaskModel task)
        {
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

        internal Task<TaskModel> Get(int taskId)
        {
            return _context.Tasks.SingleOrDefaultAsync(u => u.Id == taskId);
        }
    }
}