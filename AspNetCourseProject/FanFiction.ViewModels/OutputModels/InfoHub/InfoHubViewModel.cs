namespace FanFiction.ViewModels.OutputModels.InfoHub
{
    using System.Collections.Generic;
    using Stories;

    public class InfoHubViewModel
    {
        public InfoHubViewModel()
        {
            this.NewMessages = new HashSet<MessageOutputModel>();
            this.OldMessages = new HashSet<MessageOutputModel>();
            this.Notifications = new HashSet<NotificationOutputModel>();
            this.UserComments = new HashSet<CommentOutputModel>();
        }

        public ICollection<MessageOutputModel> NewMessages { get; set; }

        public ICollection<MessageOutputModel> OldMessages { get; set; }

        public ICollection<NotificationOutputModel> Notifications { get; set; }

        public ICollection<CommentOutputModel> UserComments { get; set; }
    }
}