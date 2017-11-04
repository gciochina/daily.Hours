using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;
using System;
using System.Collections.Generic;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        private ActivityService _activityService = new ActivityService();

        [HttpPut]
        public ActivityModel Create(ActivityModel workLog)
        {
            return _activityService.Create(workLog);
        }

        [HttpPost]
        public ActivityModel Update(ActivityModel workLog)
        {
            return _activityService.Update(workLog);
        }

        [HttpDelete]
        public bool Delete(int workLogId)
        {
            return _activityService.Delete(workLogId);
        }

        [HttpGet]
        public Task<ActivityModel> Get(int workLogId)
        {
            return _activityService.Get(workLogId);
        }

        [HttpGet]
        public List<ActivityModel> List(int userId, DateTime filterDate)
        {
            return _activityService.List(userId, filterDate);
        }
    }
}
