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

        [HttpPut]
        public TaskViewModel Create(TaskViewModel task)
        {
            return _taskService.Create(task);
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
        public List<TaskViewModel> List(int userId)
        {
            return _taskService.List(userId);
        }
    }
}
