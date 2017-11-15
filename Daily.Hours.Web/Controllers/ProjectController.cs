using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Services;
using System.Collections.Generic;
using Daily.Hours.Web.ViewModels;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private ProjectService _projectService = new ProjectService();

        [HttpPut]
        public ProjectViewModel Create(ProjectViewModel project)
        {
            return _projectService.Create(project, AuthenticatedUserId);
        }

        [HttpPost]
        public ProjectViewModel Update(ProjectViewModel project)
        {
            return _projectService.Update(project);
        }

        [HttpGet]
        public bool Delete(int projectId)
        {
            return _projectService.Delete(projectId);
        }

        [HttpGet]
        public ProjectViewModel Get(int projectId)
        {
            return _projectService.Get(projectId);
        }

        [HttpGet]
        public List<ProjectViewModel> List()
        {
            return _projectService.List(AuthenticatedUserId);
        }
    }
}
