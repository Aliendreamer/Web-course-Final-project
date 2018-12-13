namespace FanFiction.Services.Interfaces
{
    using ViewModels.InputModels;
    using ViewModels.OutputModels.InfoHub;

    public interface IMessageService
    {
        void SendMessage(MessageInputModel inputModel);

        bool CanSendMessage(string senderName, string receiverName);

        int NewMessages(string userId);

        InfoHubViewModel Infohub(string username);

        void AllMessagesSeen(string username);

        void DeleteAllMessages(string userId);

        void MessageSeen(int id);

        void DeleteMessage(int id);
    }
}