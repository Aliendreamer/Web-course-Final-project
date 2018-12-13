namespace FanFiction.Services
{
    using Data;
    using Models;
    using Utilities;
    using Interfaces;
    using AutoMapper;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class NotificationService : BaseService, INotificationService
    {
        public NotificationService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context, IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

        public void AddNotification(int storyId, string username, string storyTitle)
        {
            var notificationRange = new List<Notification>();

            var users = this.Context.Users
                .Where(x => x.FollowedStories.Any(xx => xx.FanFictionStoryId == storyId && x.UserName != username)).ToArray();

            foreach (var u in users)
            {
                var notice = new Notification
                {
                    FanFictionUserId = u.Id,
                    Message = string.Format(GlobalConstants.NotificationMessage, storyTitle),
                    Seen = false,
                    UpdatedStoryId = storyId
                };

                notificationRange.Add(notice);
                u.Notifications.Add(notice);
            }

            this.Context.Notifications.AddRange(notificationRange);
            this.Context.Users.UpdateRange(users);
            this.Context.SaveChanges();
        }

        public int NewNotifications(string username)
        {
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            var newNotices = this.Context.Notifications.Include(x => x.FanFictionUser)
                .Where(x => x.FanFictionUserId == user.Id && x.Seen == false).ToList().Count;

            return newNotices;
        }

        public void DeleteNotification(int id)
        {
            var notification = this.Context.Notifications.Find(id);

            this.Context.Notifications.Remove(notification);

            this.Context.SaveChanges();
        }

        public void DeleteAllNotifications(string username)
        {
            var notifications = this.Context.Notifications.Where(x => x.FanFictionUser.UserName == username).ToArray();

            this.Context.Notifications.RemoveRange(notifications);

            this.Context.SaveChanges();
        }

        public void MarkNotificationAsSeen(int id)
        {
            var notice = this.Context.Notifications.Find(id);
            notice.Seen = true;

            this.Context.Notifications.Update(notice);

            this.Context.SaveChanges();
        }

        public bool StoryExists(int id)
        {
            bool exists = this.Context.FictionStories.Any(x => x.Id == id);

            return exists;
        }
    }
}