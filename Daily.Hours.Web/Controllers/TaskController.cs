using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Services;
using System.Collections.Generic;
using Daily.Hours.Web.ViewModels;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private TaskService _taskService = new TaskService();

        [HttpGet]
        public TaskViewModel Create(int projectId, string name)
        {
            return _taskService.Create(new TaskViewModel
            {
                Name = name,
                ProjectId = projectId
            });
        }

        [HttpPost]
        public TaskViewModel Update(TaskViewModel task)
        {
            return _taskService.Update(task);
        }

        [HttpGet]
        public bool Delete(int taskId)
        {
            return _taskService.Delete(taskId);
        }

        [HttpGet]
        public TaskViewModel Get(int taskId)
        {
            return _taskService.Get(taskId);
        }

        [HttpGet]
        public List<TaskViewModel> List(int? userId = null)
        {
            return _taskService.List(userId ?? AuthenticatedUserId);
        }

        [HttpGet]
        public List<TaskViewModel> Search(string term, int projectId)
        {
            return _taskService.Search(term, AuthenticatedUserId, projectId);
        }
    }
}
