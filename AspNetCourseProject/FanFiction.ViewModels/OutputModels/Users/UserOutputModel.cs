namespace FanFiction.ViewModels.OutputModels.Users
{
    using System.Collections.Generic;

    public class UserOutputModel
    {
        public UserOutputModel()
        {
            this.Messages = new HashSet<MessageOutputModel>();
            this.Notifications = new HashSet<NotificationOutputModel>();
            this.Messages = new List<MessageOutputModel>();
        }

        public string UserName { get; set; }

        public string Nickname { get; set; }

        public string Id { get; set; }

        public ICollection<MessageOutputModel> Messages { get; set; }

        public ICollection<StoryOutputModel> UserStories { get; set; }

        public ICollection<NotificationOutputModel> Notifications { get; set; }
    }
}