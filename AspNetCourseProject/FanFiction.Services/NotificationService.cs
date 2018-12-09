namespace FanFiction.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Utilities;

    public class NotificationService : BaseService, INotificationService
    {
        public NotificationService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context, IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

        public void AddNotification(int storyId, string username)
        {
            var notificationRange = new List<Notification>();

            var users = this.Context.Users
                .Where(x => x.FollowedStories.Any(xx => xx.FanFictionStoryId == storyId && x.UserName != username)).ToArray();

            foreach (var u in users)
            {
                var notice = new Notification
                {
                    FanFictionUserId = u.Id,
                    Message = string.Format(GlobalConstants.NotificationMessage, storyId),
                    Seen = false
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
    }
}