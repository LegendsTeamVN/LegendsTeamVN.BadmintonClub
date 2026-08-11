using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Content).HasMaxLength(2000);
        builder.Property(r => r.ImageUrl).HasMaxLength(1000);

        builder.HasOne(r => r.Venue)
            .WithMany(v => v.Reviews)
            .HasForeignKey(r => r.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
