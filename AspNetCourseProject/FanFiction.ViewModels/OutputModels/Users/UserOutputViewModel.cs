namespace FanFiction.ViewModels.OutputModels.Users
{
    using System.Collections.Generic;
    using Stories;

    public class UserOutputViewModel
    {
        public UserOutputViewModel()
        {
            this.UserStories = new List<StoryOutputModel>();
            this.Notifications = new List<NotificationOutputModel>();
            this.Messages = new List<MessageOutputModel>();
            this.FollowedStories = new List<StoryOutputModel>();
        }

        public List<StoryOutputModel> FollowedStories { get; set; }

        public ICollection<MessageOutputModel> Messages { get; set; }

        public ICollection<StoryOutputModel> UserStories { get; set; }

        public ICollection<NotificationOutputModel> Notifications { get; set; }

        public string Id { get; set; }

        public string Username { get; set; }

        public string NickName { get; set; }

        public string Role { get; set; }

        public int Stories { get; set; }

        public int Comments { get; set; }

        public int BlockedUsers { get; set; }

        public int BlockedBy { get; set; }

        public int MessagesCount { get; set; }

        public string Email { get; set; }
    }
}