namespace FanFictionApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class NotificationsController : Controller
    {
        [HttpGet]
        public IActionResult UserNotifications(string nickname)
        {
            return this.View();
        }

        public IActionResult DeleteNotification(int id)
        {
            return RedirectToAction("UserNotifications");
        }

        public IActionResult DeleteAllNotifications(int id)
        {
            return RedirectToAction("UserNotifications");
        }
    }
}