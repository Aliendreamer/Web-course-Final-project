namespace FanFiction.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class FanFictionRatingConfiguration : IEntityTypeConfiguration<FanFictionRating>
    {
        public void Configure(EntityTypeBuilder<FanFictionRating> builder)
        {
            builder.HasKey(x => new
            {
                x.FanFictionId,
                x.RatingId
            });

            builder.HasOne(x => x.FanFictionStory)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.FanFictionId);

            builder.HasOne(x => x.StoryRating)
                .WithMany(x => x.FanFictionRatings)
                .HasForeignKey(x => x.RatingId);
        }
    }
}