namespace FanFiction.Services.Interfaces
{
    using ViewModels.InputModels;

    public interface IMessageService
    {
        void SendMessage(MessageInputModel inputModel);

        bool CanSendMessage(string senderName, string receiverName);

        int NewMessages(string userId);
    }
}