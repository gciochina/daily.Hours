using System;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public static TaskViewModel From(Models.Task taskModel)
        {
            return new TaskViewModel
            {
                Id = taskModel.TaskId,
                Name = taskModel.Name,
                ProjectId = taskModel.Project.ProjectId,
                ProjectName = taskModel.Project.Name
            };
        }
    }
}