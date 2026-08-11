using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("Venues");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name).HasMaxLength(200).IsRequired();
        builder.Property(v => v.Address).HasMaxLength(500).IsRequired();
        builder.Property(v => v.Latitude).HasColumnType("decimal(18,6)");
        builder.Property(v => v.Longitude).HasColumnType("decimal(18,6)");
        builder.Property(v => v.Description).HasMaxLength(2000);
    }
}
