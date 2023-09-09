namespace Projects.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Projects.Models;
    using Projects.Services.Data;
    using Projects.Services.Mapping;
    using Projects.Services.Models;
    using Projects.Web.Models;

    public class HomeController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IDataInitializationService dataInitializationService;

        public HomeController(IUsersService usersService
            , IDataInitializationService dataInitializationService)
        {
            this.usersService = usersService;
            this.dataInitializationService = dataInitializationService;
        }

        public async Task<IActionResult> Index([FromQuery]UsersPageQueryModel usersPageQuery)
        {
            PageQueryModel pageQueryModel = usersPageQuery.To<PageQueryModel>();

            var usersPageViewModel = (await this.usersService.GetUsers<UserViewModel>(pageQueryModel))
                .To<UsersPageViewModel>();

            return this.View(usersPageViewModel);
        }


        public async Task<IActionResult> InitializaDatabase()
        {
            await this.dataInitializationService.InitializeNewDataAsync();

            return this.RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
