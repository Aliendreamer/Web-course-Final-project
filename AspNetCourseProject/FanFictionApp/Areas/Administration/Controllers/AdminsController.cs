namespace FanFictionApp.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
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
        public AdminsController(IAdminService adminService, IStoryService storyService)
        {
            this.AdminService = adminService;
            this.StoryService = storyService;
        }

        protected IAdminService AdminService { get; }
        protected IStoryService StoryService { get; }

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
            var model = this.StoryService.CurrentStories(null);

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

            this.ViewData[GlobalConstants.Error] = GlobalConstants.TooShortAnnouncement;
            return RedirectToAction("AllAnnouncements");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public async Task<IActionResult> DeleteUSer(string id)
        {
            await this.AdminService.DeleteUser(id);

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
                return RedirectToAction("Profile", "Users", new { username });
            }

            this.ViewData[GlobalConstants.Error] = GlobalConstants.RoleChangeError;
            return this.EditRole(inputModel.Id);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStory(int Id)
        {
            if (!this.User.IsInRole(GlobalConstants.Admin) && !this.User.IsInRole(GlobalConstants.Moderator))
            {
                return RedirectToAction("Error", "Home", "");
            }

            string username = this.User.Identity.Name;

            await this.StoryService.DeleteStory(Id, username);

            return RedirectToAction("AllStories");
        }

        [HttpGet]
        public IActionResult CurrentGenres()
        {
            var model = this.StoryService.Genres();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult CurrentGenres(string name)
        {
            var genres = this.StoryService.Genres();

            if (string.IsNullOrEmpty(name))
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.NullName;
                return this.View(genres);
            }

            var result = this.AdminService.AddGenre(name);

            if (result != GlobalConstants.Success)
            {
                this.ViewData[GlobalConstants.Error] = string.Join(GlobalConstants.AlreadyExistsInDb, name);
                return this.View(genres);
            }

            return RedirectToAction("CurrentGenres");
        }
    }
}