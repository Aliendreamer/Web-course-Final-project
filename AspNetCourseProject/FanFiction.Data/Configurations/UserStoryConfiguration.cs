namespace FanFiction.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserStoryConfiguration : IEntityTypeConfiguration<UserStory>
    {
        public void Configure(EntityTypeBuilder<UserStory> builder)
        {
            builder.HasKey(x => new { x.FanfictionUserId, x.FanFictionStoryId });
        }
    }
}