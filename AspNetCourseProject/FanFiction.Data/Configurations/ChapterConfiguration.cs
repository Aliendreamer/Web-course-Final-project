namespace FanFiction.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

	public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
	{
		public void Configure(EntityTypeBuilder<Chapter> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Title)
				.IsRequired(false)
				.HasMaxLength(ConfigurationConstants.TitleLength);

			builder.Property(x => x.Content)
				.IsRequired()
				.HasMaxLength(ConfigurationConstants.ChapterContentLength);

			builder.HasOne(x => x.FanFictionUser)
				.WithMany(x => x.Chapters)
				.HasForeignKey(x => x.AuthorId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.Property(x => x.CreatedOn)
				.IsRequired();

			builder.Ignore(x => x.Length);
		}
	}
}