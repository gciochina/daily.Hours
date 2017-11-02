using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;
using System.Collections.Generic;

namespace Daily.Hours.Web.Controllers
{
    public class TaskController : ApiController
    {
        private TaskService _taskService = new TaskService();

        [HttpPut]
        public TaskModel Create(TaskModel task)
        {
            return _taskService.Create(task);
        }

        [HttpPost]
        public TaskModel Update(TaskModel task)
        {
            return _taskService.Update(task);
        }

        [HttpDelete]
        public bool Delete(int taskId)
        {
            return _taskService.Delete(taskId);
        }

        [HttpGet]
        public Task<TaskModel> Get(int taskId)
        {
            return _taskService.Get(taskId);
        }

        [HttpGet]
        public List<TaskModel> List(int userId)
        {
            return _taskService.List(userId);
        }
    }
}
