namespace FanFiction.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class FanFictionUser : IdentityUser
    {
        public FanFictionUser()
        {
            this.Notifications = new HashSet<Notification>();
            this.BlockedUsers = new HashSet<FanFictionUser>();
            this.Comments = new HashSet<Comment>();
            this.SendMessages = new HashSet<Message>();
            this.FollowedStories = new HashSet<UserStory>();
            this.FanFictionStories = new HashSet<FanFictionStory>();
            this.Chapters = new HashSet<Chapter>();
            this.StoryRatings = new HashSet<StoryRating>();
            this.Announcements = new HashSet<Announcement>();
            this.ReceivedMessages = new HashSet<Message>();
        }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"[A-Za-z]+")]
        public string Nickname { get; set; }

        [Required]
        public string Fullname { get; set; }

        public bool? IsDeleted { get; set; }

        public ICollection<StoryRating> StoryRatings { get; set; }

        public ICollection<FanFictionStory> FanFictionStories { get; set; }

        public ICollection<Chapter> Chapters { get; set; }

        public virtual ICollection<FanFictionUser> BlockedUsers { get; set; }

        public ICollection<Message> ReceivedMessages { get; set; }

        public ICollection<Message> SendMessages { get; set; }

        public ICollection<UserStory> FollowedStories { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<Announcement> Announcements { get; set; }
    }
}