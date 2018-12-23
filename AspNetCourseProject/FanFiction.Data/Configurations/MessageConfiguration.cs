﻿namespace FanFiction.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

	public class MessageConfiguration : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Sender)
				.WithMany(x => x.SendMessages)
				.HasForeignKey(x => x.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.Receiver)
				.WithMany(x => x.ReceivedMessages)
				.HasForeignKey(x => x.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Property(x => x.Text).IsRequired()
				.HasMaxLength(ConfigurationConstants.MessageContentLength);
		}
	}
}