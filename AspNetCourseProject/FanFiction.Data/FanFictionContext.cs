namespace FanFiction.Data
{
    using Configurations;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FanFictionContext : IdentityDbContext<FanFictionUser>
    {
        public FanFictionContext(DbContextOptions<FanFictionContext> options)
            : base(options)
        {
        }

        public DbSet<FanFictionStory> FictionStories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<BlockedUsers> BlockedUsers { get; set; }

        public DbSet<DbLog> Logs { get; set; }

        public DbSet<StoryRating> StoryRatings { get; set; }

        public DbSet<FanFictionRating> FictionRatings { get; set; }

        public DbSet<UserStory> UsersStories { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<StoryType> StoryTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ChapterConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new UserStoryConfiguration());
            builder.ApplyConfiguration(new AnnouncementConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new StoryTypeConfiguration());
            builder.ApplyConfiguration(new StoryRatingConfiguration());
            builder.ApplyConfiguration(new FanFictionRatingConfiguration());
            builder.ApplyConfiguration(new FanFictionStoryConfiguration());
            builder.ApplyConfiguration(new BlockedUsersConfiguration());
            builder.ApplyConfiguration(new NotificationConfiguration());

            base.OnModelCreating(builder);
        }
    }
}