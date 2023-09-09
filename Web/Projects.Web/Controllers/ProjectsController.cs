namespace Projects.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Projects.Services.Data;
    using Projects.Web.Models;

    public class ProjectsController : Controller
    {
        private readonly IProjectsService projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        public JsonResult GetTop10Projects(DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom is null)
            {
                dateFrom = DateTime.MinValue;
            }

            if (dateTo is null)
            {
                dateTo = DateTime.UtcNow;
            }

            var projects = this.projectsService.GetTop10ProjectsAsync<TopProjectsViewModel>(dateFrom, dateTo);

            return Json(projects);
        }
    }
}
