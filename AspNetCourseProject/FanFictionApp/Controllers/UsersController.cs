namespace FanFictionApp.Controllers
{
    using FanFiction.Services.Interfaces;
    using FanFiction.Services.Utilities;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

    public class UsersController : Controller
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
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
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
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
        public IActionResult Profile(string name)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult UserFriends(string name)
        {
            return this.View();
        }
    }
}