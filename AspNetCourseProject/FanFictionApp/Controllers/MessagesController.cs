namespace FanFictionApp.Controllers
{
    using FanFiction.Services.Interfaces;
    using FanFiction.Services.Utilities;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class MessagesController : Controller
    {
        public MessagesController(IMessageService messageService)
        {
            this.MessageService = messageService;
        }

        protected IMessageService MessageService { get; }

        [HttpGet]
        public IActionResult InfoHub(string username)
        {
            var model = this.MessageService.Infohub(username);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult DeleteMessage(int id)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult DeleteAllMessages(string userId)
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult SendMessage(MessageInputModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.Message))
            {
                this.TempData[GlobalConstants.Error] = GlobalConstants.EmptyMessage;
                return RedirectToAction("Profile", "Users", new { username = inputModel.ReceiverName });
            }

            this.MessageService.SendMessage(inputModel);

            return RedirectToAction("Profile", "Users", new { username = inputModel.ReceiverName });
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