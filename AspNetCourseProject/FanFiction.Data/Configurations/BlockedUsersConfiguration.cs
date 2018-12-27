namespace FanFiction.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

	public class BlockedUsersConfiguration : IEntityTypeConfiguration<BlockedUsers>
	{
		public void Configure(EntityTypeBuilder<BlockedUsers> builder)
		{
			builder.HasKey(x => new { x.FanfictionUserId, x.BlockedUserId });

			builder
				.HasOne(pt => pt.FanFictionUser)
				.WithMany(p => p.BlockedUsers)
				.HasForeignKey(pt => pt.FanfictionUserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(pt => pt.BlockedUser)
				.WithMany(t => t.BLockedBy)
				.HasForeignKey(pt => pt.BlockedUserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}