using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;
using System.Collections.Generic;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private ProjectService _projectService = new ProjectService();

        [HttpPut]
        public ProjectModel Create(ProjectModel project)
        {
            return _projectService.Create(project, AuthenticatedUserId);
        }

        [HttpPost]
        public ProjectModel Update(ProjectModel project)
        {
            return _projectService.Update(project);
        }

        [HttpGet]
        public bool Delete([FromBody]int projectId)
        {
            return _projectService.Delete(projectId);
        }

        [HttpGet]
        public Task<ProjectModel> Get(int projectId)
        {
            return _projectService.Get(projectId);
        }

        [HttpGet]
        public List<ProjectModel> List()
        {
            return _projectService.List(AuthenticatedUserId);
        }
    }
}
