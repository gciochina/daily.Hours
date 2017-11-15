using System;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.ViewModels
{
    public class ActivityViewModel : BaseViewModel
    {
        public int UserId { get; set; }

        public string UserFullName { get; set; }

        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateTime Date { get; set; }

        public string Hours { get; set; }

        internal static ActivityViewModel From(ActivityModel activityModel)
        {
            return new ActivityViewModel
            {
                Id = activityModel.Id,
                Date = activityModel.Date,
                Hours = activityModel.Hours,
                TaskId = activityModel.Task.Id,
                TaskName = activityModel.Task.Name,
                UserId = activityModel.User.Id,
                UserFullName = activityModel.User.FullName,
                ProjectId = activityModel.Task.Project.Id,
                ProjectName = activityModel.Task.Project.Name
            };
        }
    }
}