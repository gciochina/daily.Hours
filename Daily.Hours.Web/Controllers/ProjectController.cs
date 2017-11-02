using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Models;
using Daily.Hours.Web.Services;
using System.Collections.Generic;

namespace Daily.Hours.Web.Controllers
{
    public class ProjectController : ApiController
    {
        private ProjectService _projectService = new ProjectService();

        [HttpPut]
        public ProjectModel Create(ProjectModel project)
        {
            return _projectService.Create(project);
        }

        [HttpPost]
        public ProjectModel Update(ProjectModel project)
        {
            return _projectService.Update(project);
        }

        [HttpDelete]
        public bool Delete(int projectId)
        {
            return _projectService.Delete(projectId);
        }

        [HttpGet]
        public Task<ProjectModel> Get(int projectId)
        {
            return _projectService.Get(projectId);
        }

        [HttpGet]
        public List<ProjectModel> List(int userId)
        {
            return _projectService.List(userId);
        }
    }
}
