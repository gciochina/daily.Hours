﻿using System.Threading.Tasks;
using System.Web.Http;
using Daily.Hours.Web.Services;
using System.Collections.Generic;
using Daily.Hours.Web.ViewModels;
using System;

namespace Daily.Hours.Web.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private ProjectService _projectService = new ProjectService();

        [HttpGet]
        public ProjectViewModel Create(string name)
        {
            if (!AuthenticatedUserIsAdmin)
            {
                throw new UnauthorizedAccessException("No access. You are not an administrator");
            }

            return _projectService.Create(new ProjectViewModel { Name = name }, AuthenticatedUserId);
        }

        [HttpPost]
        public ProjectViewModel Update(ProjectViewModel project)
        {
            if (!AuthenticatedUserIsAdmin)
            {
                throw new UnauthorizedAccessException("No access. You are not an administrator");
            }

            return _projectService.Update(project);
        }

        [HttpGet]
        public bool Delete(int projectId)
        {
            if (!AuthenticatedUserIsAdmin)
            {
                throw new UnauthorizedAccessException("No access. You are not an administrator");
            }

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

        [HttpGet]
        public List<ProjectViewModel> Search(string term)
        {
            return _projectService.Search(term, AuthenticatedUserId);
        }
    }
}
