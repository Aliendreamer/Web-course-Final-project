namespace FanFiction.Services.Interfaces
{
    public interface INotificationService
    {
        void AddNotification(int storyId, string username, string storyTitle);

        int NewNotifications(string username);

        void DeleteNotification(int id);

        void DeleteAllNotifications(string username);

        void MarkNotificationAsSeen(int id);

        bool StoryExists(int id);
    }
}