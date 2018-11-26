namespace FanFictionApp.Controllers
{
    using System.Diagnostics;
    using Extensions;
    using FanFiction.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [ServiceFilter(typeof(LogExceptionActionFilter))]
    public class HomeController : Controller
    {
        public HomeController(IUserService userService)
        {
            this.UserService = userService;
        }

        protected IUserService UserService { get; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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