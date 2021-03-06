﻿using System;
using System.Threading.Tasks;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        internal static ProjectViewModel From(Project projectModel)
        {
            return new ProjectViewModel
            {
                Id = projectModel.ProjectId,
                CreatedOn = projectModel.CreatedOn,
                IsActive = projectModel.IsActive,
                Name = projectModel.Name,
                OwnerId = projectModel.Owner.UserId
            };
        }
    }
}