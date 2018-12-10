namespace FanFiction.Services
{
    using Data;
    using Models;
    using Interfaces;
    using AutoMapper;
    using System.Linq;
    using ViewModels.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Utilities;

    public class MessageService : BaseService, IMessageService
    {
        public MessageService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context,
            IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

        public void SendMessage(MessageInputModel inputModel)
        {
            var sender = this.UserManager.FindByNameAsync(inputModel.SenderName).GetAwaiter().GetResult();
            var receiver = this.UserManager.FindByNameAsync(inputModel.ReceiverName).GetAwaiter().GetResult();

            var message = new Message
            {
                IsReaden = false,
                SendOn = inputModel.SendDate,
                Text = inputModel.Message,
                ReceiverId = receiver.Id,
                SenderId = sender.Id
            };

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();
        }

        public bool CanSendMessage(string senderName, string receiverName)
        {
            var sender = this.UserManager.FindByNameAsync(senderName).GetAwaiter().GetResult();
            var receiver = this.UserManager.FindByNameAsync(receiverName).GetAwaiter().GetResult();

            var notBlocked =
                this.Context.BlockedUsers.Any(x => x.BlockedUserId == receiver.Id && x.FanfictionUserId == sender.Id);

            if (notBlocked)
            {
                return true;
            }

            return false;
        }

        public int NewMessages(string userId)
        {
            var newMessages = this.Context.Messages.Count(x => x.ReceiverId == userId);

            return newMessages;
        }
    }
}