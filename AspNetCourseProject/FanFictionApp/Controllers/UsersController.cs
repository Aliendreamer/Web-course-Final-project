namespace FanFictionApp.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FanFiction.Services.Interfaces;
    using FanFiction.Services.Utilities;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

    [Authorize]
    public class UsersController : Controller
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginInputModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(loginModel);
            }

            var result = this.UserService.LogUser(loginModel);

            if (result != SignInResult.Success)
            {
                this.ViewData[GlobalConstants.ModelError] = GlobalConstants.LoginError;
                return this.View(loginModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterInputModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(registerModel);
            }
            var result = this.UserService.RegisterUser(registerModel).Result;
            if (result != SignInResult.Success)
            {
                this.ViewData[GlobalConstants.ModelError] = string.Format(GlobalConstants.NicknameUnique, registerModel.Nickname);
                return this.View(registerModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.UserService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        // [ResponseCache(Duration = 1200)]
        [Route(GlobalConstants.RouteConstants.UserProfileRoute)]
        public IActionResult Profile(string username)
        {
            bool fullAccess = this.User.Identity.Name == username || this.User.IsInRole(GlobalConstants.Admin);

            var user = this.UserService.GetUser(username);

            //temporarily only
            if (fullAccess)
            {
                return this.View("UserDetails", user);
            }

            return this.View(user);
        }

        [HttpGet]
        [Route(GlobalConstants.RouteConstants.UserBlockRoute)]
        public async Task<IActionResult> BlockUser(string username)
        {
            var currentUser = this.User.Identity.Name;

            await this.UserService.BlockUser(currentUser, username);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult BlockedUsers()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = this.UserService.BlockedUsers(userId);

            return this.View(model);
        }

        public IActionResult UnblockUser(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.UserService.UnblockUser(userId, id);

            return RedirectToAction("BlockedUsers", "Users");
        }
    }
}