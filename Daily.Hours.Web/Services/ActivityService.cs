using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;
using System.Linq;
using System.Collections.Generic;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ActivityService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal ActivityModel Create(ActivityModel workLog)
        {
            _context.Activities.Add(workLog);

            _context.SaveChanges();

            return workLog;
        }

        internal ActivityModel Update(ActivityModel workLog)
        {
            var workLogToUpdate = _context.Activities.SingleAsync(u => u.Id == workLog.Id).Result;
            workLogToUpdate.Date = workLog.Date;
            workLogToUpdate.Hours = workLog.Hours;
            workLogToUpdate.Task = workLog.Task;
            workLogToUpdate.User = workLog.User;

            _context.SaveChanges();

            return workLogToUpdate;
        }

        internal bool Delete(int taskId)
        {
            _context.Tasks.Remove(_context.Tasks.SingleOrDefaultAsync(u=>u.Id == taskId).Result);
            _context.SaveChanges();
            return true;
        }

        internal List<ActivityModel> List(int userId, DateTime filterDate)
        {
            return _context.Activities.Where(a => a.User.Id == userId && a.Date > filterDate.Date && a.Date < filterDate.Date.AddDays(1)).ToList();
        }

        internal Task<ActivityModel> Get(int workLogId)
        {
            return _context.Activities.SingleOrDefaultAsync(u => u.Id == workLogId);
        }
    }
}