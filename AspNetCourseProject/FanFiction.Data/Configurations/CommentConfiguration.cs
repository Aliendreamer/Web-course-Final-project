namespace FanFiction.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Message)
				.IsRequired(true)
				.HasMaxLength(ConfigurationConstants.CommentContentLength);

			builder.HasOne(x => x.FanFictionUser)
				.WithMany(x => x.Comments)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}