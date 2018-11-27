namespace FanFiction.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class FanFictionUserConfiguration : IEntityTypeConfiguration<FanFictionUser>
    {
        public void Configure(EntityTypeBuilder<FanFictionUser> builder)
        {
            builder.HasMany(x => x.BlockedUsers)
                .WithOne(x => x).HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Notifications)
                .WithOne(x => x.FanFictionUser)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}