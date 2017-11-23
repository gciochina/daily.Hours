using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Daily.Hours.Web.ViewModels;
using Daily.Hours.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Daily.Hours.Web.Services
{
    public class ActivityService
    {
        DailyHoursContext _context = new DailyHoursContext();

        internal ActivityViewModel Create(ActivityViewModel activityViewModel)
        {
            if (activityViewModel.TaskId == 0)
                throw new ValidationException("Please select a task!");

            var startDate = activityViewModel.Date.Date;
            var endDate = activityViewModel.Date.Date.AddDays(1);

            var workLog = _context.WorkLogs.SingleOrDefault(l => l.Task.TaskId == activityViewModel.TaskId
            && l.User.UserId == activityViewModel.UserId
            && l.Date > startDate && l.Date < endDate);

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

        internal ActivityViewModel Update(ActivityViewModel workLog, int userId)
        {
            var workLogToUpdate = _context.WorkLogs.Single(u => u.WorkLogId == workLog.Id);

            if (workLogToUpdate.User.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not allowed to edit this worklog!");
            }

            workLogToUpdate.Hours = workLog.Hours;
            workLogToUpdate.Description = workLog.Description;
            _context.SaveChanges();

            return ActivityViewModel.From(workLogToUpdate);
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

        internal List<ActivityViewModel> Report(DateTime startDate, DateTime endDate, int userId)
        {
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1);

            var workLogs = _context.WorkLogs.Where(l => l.Date > startDate && l.Date < endDate
                && (l.Task.Project.Users.Any(u => u.UserId == userId) || l.Task.Project.Owner.UserId == userId))
                .GroupBy(a => a.Task.TaskId)
                .ToList()
                .Select(calc => new ActivityViewModel
                {
                    Id = calc.FirstOrDefault().WorkLogId,
                    ProjectId = calc.FirstOrDefault().Task.Project.ProjectId,
                    ProjectName = calc.FirstOrDefault().Task.Project.Name,
                    TaskId = calc.FirstOrDefault().Task.TaskId,
                    TaskName = calc.FirstOrDefault().Task.Name,
                    FirstName = string.Join(", ", calc.Select(a => a.User.FullName).Distinct()),
                    Hours = calc.Sum(a => a.Hours),
                    Description = string.Join(System.Environment.NewLine, calc.Select(a => a.Description))
                });

            return workLogs.ToList();
        }

        internal ActivityViewModel Get(int workLogId)
        {
            var activityModel = _context.WorkLogs.Single(u => u.WorkLogId == workLogId);
            return ActivityViewModel.From(activityModel);
        }
    }
}