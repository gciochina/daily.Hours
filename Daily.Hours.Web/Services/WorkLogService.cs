using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class WorkLogService
    {
        DailyHoursContext _context;

        internal WorkLogModel Create(WorkLogModel workLog)
        {
            _context.WorkLogs.Add(workLog);

            _context.SaveChanges();

            return workLog;
        }

        internal WorkLogModel Update(WorkLogModel workLog)
        {
            var workLogToUpdate = _context.WorkLogs.SingleAsync(u => u.Id == workLog.Id).Result;
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

        internal Task<WorkLogModel> Get(int workLogId)
        {
            return _context.WorkLogs.SingleOrDefaultAsync(u => u.Id == workLogId);
        }
    }
}