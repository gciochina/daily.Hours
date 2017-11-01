using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;

namespace Daily.Hours.Web.Controllers
{
    public class WorkLogController : ApiController
    {
        private WorkLogService _workLogService = new WorkLogService();

        [HttpPut]
        public WorkLogModel Create(WorkLogModel workLog)
        {
            return _workLogService.Create(workLog);
        }

        [HttpPost]
        public WorkLogModel Update(WorkLogModel workLog)
        {
            return _workLogService.Update(workLog);
        }

        [HttpDelete]
        public bool Delete(int workLogId)
        {
            return _workLogService.Delete(workLogId);
        }

        [HttpGet]
        public Task<WorkLogModel> Get(int workLogId)
        {
            return _workLogService.Get(workLogId);
        }
    }
}
