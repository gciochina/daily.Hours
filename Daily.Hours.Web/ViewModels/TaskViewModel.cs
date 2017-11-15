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

        public static TaskViewModel From(TaskModel taskModel)
        {
            return new TaskViewModel
            {
                Id = taskModel.Id,
                Name = taskModel.Name,
                ProjectId = taskModel.Project.Id,
                ProjectName = taskModel.Project.Name
            };
        }
    }
}