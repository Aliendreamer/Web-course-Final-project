namespace FanFictionApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : Controller
    {
        [HttpGet]
        public IActionResult UserMessages(string userNickname)
        {
            return this.View();
        }
    }
}