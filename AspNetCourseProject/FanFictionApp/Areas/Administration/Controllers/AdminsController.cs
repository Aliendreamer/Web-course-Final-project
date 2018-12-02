namespace FanFictionApp.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using FanFiction.Services.Utilities;
    using FanFiction.Services.Interfaces;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using FanFiction.ViewModels.OutputModels.Users;

    [Area(GlobalConstants.RouteConstants.Administration)]
    [Authorize(Roles = "admin,moderator")]
    public class AdminsController : Controller
    {
        protected IAdminService AdminService { get; }

        public AdminsController(IAdminService adminService)
        {
            this.AdminService = adminService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult AllUsers()
        {
            var model = this.AdminService.AllUsers().Result;

            return View(model);
        }

        [HttpGet]
        public IActionResult AllStories()
        {
            var model = this.AdminService.CurrentStories();

            return View(model);
        }

        [HttpGet]
        public IActionResult AllAnnouncements()
        {
            var model = this.AdminService.AllAnnouncements();

            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteAnnouncement(int Id)
        {
            this.AdminService.DeleteAnnouncement(Id);
            return RedirectToAction("AllAnnouncements");
        }

        [HttpGet]
        public IActionResult DeleteAllAnnouncements()
        {
            this.AdminService.DeleteAllAnnouncements();
            return RedirectToAction("AllAnnouncements");
        }

        [HttpPost]
        public IActionResult AllAnnouncements(AnnouncementInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                this.AdminService.AddAnnouncement(inputModel);
                return RedirectToAction("AllAnnouncements");
            }
            //TODO: if i have enough time i may fix the validation problem here but is it worth it?
            this.ViewData[GlobalConstants.Error] = GlobalConstants.TooShortAnnouncement;
            return RedirectToAction("AllAnnouncements");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult DeleteUSer(string id)
        {
            this.AdminService.DeleteUser(id);

            return RedirectToAction("AllUsers");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult EditRole(string id)
        {
            var model = this.AdminService.AdminModifyRole(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult EditRole(ChangingRoleModel inputModel)
        {
            var result = this.AdminService.ChangeRole(inputModel).Result;

            if (result == IdentityResult.Success)
            {
                var username = this.User.Identity.Name;
                return RedirectToAction("Details", new { username });
            }

            this.ViewData[GlobalConstants.Error] = GlobalConstants.RoleChangeError;
            return this.EditRole(inputModel.Id);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Details(string username)
        {
            return View();
        }
    }
}