using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Services;
using System;
using System.Collections.Generic;
using Daily.Hours.Web.ViewModels;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class ActivityController : BaseController
    {
        private ActivityService _activityService = new ActivityService();

        [HttpPost]
        public ActivityViewModel Create(ActivityViewModel workLog)
        {
            workLog.UserId = AuthenticatedUserId;
            return _activityService.Create(workLog);
        }

        [HttpPost]
        public ActivityViewModel Update(ActivityViewModel workLog)
        {
            return _activityService.Update(workLog);
        }

        [HttpGet]
        public bool Delete(int workLogId)
        {
            return _activityService.Delete(workLogId, AuthenticatedUserId);
        }

        [HttpGet]
        public ActivityViewModel Get(int workLogId)
        {
            return _activityService.Get(workLogId);
        }

        [HttpGet]
        public List<ActivityViewModel> List(int userId, DateTime filterDate)
        {
            return _activityService.List(userId, filterDate);
        }

        [HttpGet]
        public List<ActivityViewModel> Report(DateTime startDate, DateTime endDate)
        {
            return _activityService.Report(startDate, endDate, AuthenticatedUserId);
        }
    }
}
