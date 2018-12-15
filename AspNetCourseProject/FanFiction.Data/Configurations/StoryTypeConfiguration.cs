namespace FanFiction.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class StoryTypeConfiguration : IEntityTypeConfiguration<StoryType>
    {
        public void Configure(EntityTypeBuilder<StoryType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.TitleLength);

            builder.HasMany(x => x.Stories)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.StoryTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}