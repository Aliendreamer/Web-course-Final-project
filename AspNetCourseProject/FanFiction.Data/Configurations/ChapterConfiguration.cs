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

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(3000);

            builder.HasOne(x => x.FanFictionUser)
                .WithMany(x => x.Chapters)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Ignore(x => x.Length);
        }
    }
}