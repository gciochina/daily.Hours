using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Daily.Hours.Web.ViewModels;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Services
{
    public class ActivityService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal ActivityViewModel Create(ActivityViewModel activityViewModel)
        {
            var startDate = activityViewModel.Date.Date;
            var enddDate = activityViewModel.Date.Date.AddDays(1);

            var workLog = _context.WorkLogs.SingleOrDefault(l => l.Task.TaskId == activityViewModel.TaskId
            && l.User.UserId == activityViewModel.UserId
            && l.Date > startDate && l.Date < enddDate);

            if (workLog == null)
            {
                workLog = new WorkLog
                {
                    Date = activityViewModel.Date,
                    Hours = activityViewModel.Hours,
                    Description = activityViewModel.Description,
                    Task = _context.Tasks.Single(t => t.TaskId == activityViewModel.TaskId),
                    User = _context.Users.Single(u => u.UserId == activityViewModel.UserId),
                };

                _context.WorkLogs.Add(workLog);
            }
            else
            {
                workLog.Hours += activityViewModel.Hours;
            }

            _context.SaveChanges();

            return ActivityViewModel.From(workLog);
        }

        internal ActivityViewModel Update(ActivityViewModel workLog)
        {
            //var workLogToUpdate = _context.Activities.SingleAsync(u => u.Id == workLog.Id).Result;
            //workLogToUpdate.Date = workLog.Date;
            //workLogToUpdate.Hours = workLog.Hours;
            //workLogToUpdate.Task = workLog.Task;
            //workLogToUpdate.User = workLog.User;

            //_context.SaveChanges();

            //return workLogToUpdate;
            return null;
        }

        internal bool Delete(int workLogId, int userId)
        {
            var workLogToRemove = _context.WorkLogs.Single(u => u.WorkLogId == workLogId);

            if (workLogToRemove.User.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not allowed to remove this worklog");
            }

            _context.WorkLogs.Remove(workLogToRemove);
            _context.SaveChanges();
            return true;
        }

        internal List<ActivityViewModel> List(int userId, DateTime filterDate)
        {
            var startDate = filterDate.Date;
            var enddDate = filterDate.Date.AddDays(1);
            var activitiesList = _context.WorkLogs
                .Where(a => a.User.UserId == userId && a.Date >= startDate && a.Date <= enddDate)
                .GroupBy(a => a.Task.TaskId)
                .ToList()
                .Select(calc => new ActivityViewModel
                {
                    Id = calc.FirstOrDefault().WorkLogId,
                    ProjectId = calc.FirstOrDefault().Task.Project.ProjectId,
                    ProjectName = calc.FirstOrDefault().Task.Project.Name,
                    TaskId = calc.FirstOrDefault().Task.TaskId,
                    TaskName = calc.FirstOrDefault().Task.Name,
                    FirstName = calc.FirstOrDefault().User.FirstName,
                    LastName = calc.FirstOrDefault().User.LastName,
                    UserId = userId,
                    Date = filterDate,
                    Hours = calc.Sum(a => a.Hours),
                    Description = string.Join(System.Environment.NewLine, calc.Select(a => a.Description))
                });
            return activitiesList.ToList();
        }

        internal ActivityViewModel Get(int workLogId)
        {
            var activityModel = _context.WorkLogs.Single(u => u.WorkLogId == workLogId);
            return ActivityViewModel.From(activityModel);
        }
    }
}