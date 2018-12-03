namespace FanFictionApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class MessagesController : Controller
    {
        [HttpGet]
        public IActionResult InfoHub(string userNickname)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult DeleteMessage(int id)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult DeleteNotification()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult DeleteAllNotification()
        {
            return this.View();
        }
    }
}