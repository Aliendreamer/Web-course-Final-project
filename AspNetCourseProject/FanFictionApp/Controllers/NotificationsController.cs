namespace FanFictionApp.Controllers
{
    using FanFiction.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class NotificationsController : Controller
    {
        public NotificationsController(INotificationService notificationService)
        {
            this.NotificationService = notificationService;
        }

        protected INotificationService NotificationService { get; }

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