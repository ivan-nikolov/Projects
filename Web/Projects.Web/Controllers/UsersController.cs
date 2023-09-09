namespace Projects.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Projects.Services.Data;
    using Projects.Services.Mapping;
    using Projects.Services.Models;
    using Projects.Web.Models;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IProjectsService projectsService;

        public UsersController(IUsersService usersService, IProjectsService projectsService)
        {
            this.usersService = usersService;
            this.projectsService = projectsService;
        }

        public async Task<JsonResult> GetAllUsers(UsersPageQueryModel usersPageQuery)
        {
            PageQueryModel pageQueryModel = usersPageQuery.To<PageQueryModel>();

            var usersPageViewModel = (await this.usersService.GetUsers<UserViewModel>(pageQueryModel))
                .To<UsersPageViewModel>();

            return this.Json(usersPageViewModel);
        }

        public async Task<JsonResult> GetTop10Users(DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom is null)
            {
                dateFrom = DateTime.MinValue;
            }

            if (dateTo is null)
            {
                dateTo = DateTime.UtcNow;
            }

            var result = await this.usersService.GetTop10Users<TopUsersViewModel>(dateFrom, dateTo);

            return this.Json(result);
        }

        public JsonResult GetTotalHoursForUser(int userId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom is null)
            {
                dateFrom = DateTime.MinValue;
            }

            if (dateTo is null)
            {
                dateTo = DateTime.UtcNow;
            }

            var result = this.projectsService.GetTotalHoursByUser<TopUsersViewModel>(userId, dateFrom, dateTo);

            return this.Json(result);
        }

        public JsonResult GetTopProjectHoursByUserId(int userId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom is null)
            {
                dateFrom = DateTime.MinValue;
            }

            if (dateTo is null)
            {
                dateTo = DateTime.UtcNow;
            }

            var result = this.projectsService.GetTopProjectHoursByUserId<TopProjectsViewModel>(userId, dateFrom, dateTo);

            return this.Json(result);
        }
    }
}
