namespace FanFictionApp.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using FanFiction.Services.Utilities;
    using FanFiction.Services.Interfaces;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;

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
            this.MessageService.DeleteMessage(id);

            return RedirectToAction("Infohub", "Messages", new { username = this.User.Identity.Name });
        }

        [HttpGet]
        public IActionResult DeleteAllMessages(string username)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.MessageService.DeleteAllMessages(userId);

            return RedirectToAction("Infohub", "Messages", new { username = this.User.Identity.Name });
        }

        public IActionResult MarkAllMessagesAsSeen(string username)
        {
            this.MessageService.AllMessagesSeen(username);

            return RedirectToAction("Infohub", "Messages", new { username = username });
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

        public IActionResult MessageSeen(int id)
        {
            this.MessageService.MessageSeen(id);

            return RedirectToAction("Infohub", "Messages", new { username = this.User.Identity.Name });
        }
    }
}