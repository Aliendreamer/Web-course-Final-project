namespace FanFictionApp.Areas.Administration.Controllers
{
    using FanFiction.Services.Interfaces;
    using FanFiction.Services.Utilities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(GlobalConstants.RouteConstants.Administration)]
    [Authorize(Roles = "admin,moderator")]
    public class AdminsController : Controller
    {
        protected IAdminService AdminService { get; }

        public AdminsController(IAdminService adminService)
        {
            this.AdminService = adminService;
        }

        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult AllUsers()
        {
            var model = this.AdminService.AllUsers().Result;

            return View(model);
        }

        public IActionResult AllStories()
        {
            return View();
        }

        public IActionResult AllNotifications()
        {
            return View();
        }
    }
}