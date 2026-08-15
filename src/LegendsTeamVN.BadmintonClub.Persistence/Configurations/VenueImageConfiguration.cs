using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class VenueImageConfiguration : IEntityTypeConfiguration<VenueImage>
{
    public void Configure(EntityTypeBuilder<VenueImage> builder)
    {
        builder.ToTable("VenueImages");
        builder.HasKey(vi => vi.Id);

        builder.Property(vi => vi.ImageUrl).HasMaxLength(1000).IsRequired();

        builder.HasOne(vi => vi.Venue)
            .WithMany(v => v.Images)
            .HasForeignKey(vi => vi.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
