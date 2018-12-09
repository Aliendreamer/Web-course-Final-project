namespace FanFiction.Services.Interfaces
{
    public interface INotificationService
    {
        void AddNotification(int storyId, string username);

        int NewNotifications(string username);
    }
}