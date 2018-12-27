namespace FanFiction.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

	public class StoryRatingConfiguration : IEntityTypeConfiguration<StoryRating>
	{
		public void Configure(EntityTypeBuilder<StoryRating> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Rating).IsRequired();

			builder.HasOne(x => x.FanFictionUser)
				.WithMany(x => x.StoryRatings)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}