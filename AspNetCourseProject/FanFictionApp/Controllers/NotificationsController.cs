namespace FanFictionApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using FanFiction.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class NotificationsController : Controller
    {
        public NotificationsController(INotificationService notificationService)
        {
            this.NotificationService = notificationService;
        }

        protected INotificationService NotificationService { get; }

        public IActionResult DeleteNotification(int id)
        {
            this.NotificationService.DeleteNotification(id);

            return RedirectToInfohub();
        }

        public IActionResult DeleteAllNotifications(string username)
        {
            this.NotificationService.DeleteAllNotifications(username);
            return RedirectToInfohub();
        }

        public IActionResult MarkNotificationAsSeen(int id)
        {
            this.NotificationService.MarkNotificationAsSeen(id);

            return RedirectToInfohub();
        }

        private IActionResult RedirectToInfohub()
        {
            var username = this.User.Identity.Name;

            return RedirectToAction("Infohub", "Messages", new { username });
        }
    }
}