namespace FanFiction.Services
{
    using Data;
    using Models;
    using Interfaces;
    using AutoMapper;
    using System.Linq;
    using ViewModels.InputModels;
    using Microsoft.AspNetCore.Identity;
    using AutoMapper.QueryableExtensions;
    using ViewModels.OutputModels.InfoHub;
    using ViewModels.OutputModels.Stories;

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

            if (!notBlocked)
            {
                return true;
            }

            return false;
        }

        public int NewMessages(string userId)
        {
            var newMessages = this.Context.Messages.Count(x => x.ReceiverId == userId && x.IsReaden == false);

            return newMessages;
        }

        public InfoHubViewModel Infohub(string username)
        {
            var allMessages = this.Context.Messages.Where(x => x.Receiver.UserName == username).ProjectTo<MessageOutputModel>().ToList();

            var newMessages = allMessages.Where(x => x.IsReaden == false).ToArray();

            var oldMessages = allMessages.Where(x => newMessages.All(z => z.Id != x.Id)).ToArray();

            var comments = this.Context.Comments
                .Where(x => x.FanFictionUser.UserName == username)
                .ProjectTo<CommentOutputModel>().ToArray();

            var notifications = this.Context.Notifications.Where(x => x.FanFictionUser.UserName == username).ProjectTo<NotificationOutputModel>().ToList();

            var model = new InfoHubViewModel
            {
                NewMessages = newMessages,
                OldMessages = oldMessages,
                Notifications = notifications,
                UserComments = comments
            };

            return model;
        }

        public void AllMessagesSeen(string username)
        {
            var messages = this.Context.Messages
                .Where(x => x.Receiver.UserName == username && x.IsReaden == false)
                .ToList();

            messages.ForEach(x => x.IsReaden = true);

            this.Context.UpdateRange(messages);
            this.Context.SaveChanges();
        }

        public void DeleteAllMessages(string userId)
        {
            var messages = this.Context.Messages.Where(x => x.Receiver.Id == userId).ToArray();

            this.Context.Messages.RemoveRange(messages);

            this.Context.SaveChanges();
        }

        public void MessageSeen(int id)
        {
            var message = this.Context.Messages.Find(id);
            message.IsReaden = true;

            this.Context.Messages.Update(message);

            this.Context.SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            var message = this.Context.Messages.Find(id);

            this.Context.Messages.Remove(message);

            this.Context.SaveChanges();
        }
    }
}