﻿namespace FanFiction.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

	internal class FanFictionStoryConfiguration : IEntityTypeConfiguration<FanFictionStory>
	{
		public void Configure(EntityTypeBuilder<FanFictionStory> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedOn).IsRequired();

			builder.Property(x => x.LastEditedOn).IsRequired();

			builder.Property(x => x.ImageUrl).IsRequired(false);

			//builder.HasMany<Chapter>()
			//    .WithOne(x => x.FanFictionStory)
			//    .HasForeignKey(x => x.FanFictionStoryId)
			//    .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(x => x.Type)
				.WithMany(x => x.Stories)
				.HasForeignKey(x => x.StoryTypeId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(x => x.Comments)
				.WithOne(x => x.FanFictionStory)
				.OnDelete(DeleteBehavior.SetNull);

			builder.Property(x => x.Summary)
				.IsRequired(false)
				.HasMaxLength(ConfigurationConstants.FictionStorySymmary);

			builder.HasOne(x => x.Author)
				.WithMany(x => x.FanFictionStories)
				.HasForeignKey(x => x.AuthorId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.Property(x => x.Title).IsRequired()
				.HasMaxLength(ConfigurationConstants.TitleLength);

			builder.Ignore(x => x.Rating);

			builder.Ignore(x => x.Length);
		}
	}
}