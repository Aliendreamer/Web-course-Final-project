namespace FanFictionApp.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AdminsController : Controller
    {
        public IActionResult AllUsers()
        {
            return View();
        }

        public IActionResult AllStories()
        {
            return View();
        }
    }
}