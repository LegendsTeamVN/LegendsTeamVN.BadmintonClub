using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class CourtPricingConfiguration : IEntityTypeConfiguration<CourtPricing>
{
    public void Configure(EntityTypeBuilder<CourtPricing> builder)
    {
        builder.ToTable("CourtPricings");
        builder.HasKey(cp => cp.Id);

        builder.Property(cp => cp.PricePerHour).HasColumnType("decimal(18,2)");

        builder.HasOne(cp => cp.Venue)
            .WithMany(v => v.Pricings)
            .HasForeignKey(cp => cp.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
