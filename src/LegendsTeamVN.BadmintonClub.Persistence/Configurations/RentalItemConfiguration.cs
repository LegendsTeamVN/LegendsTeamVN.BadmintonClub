using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class RentalItemConfiguration : IEntityTypeConfiguration<RentalItem>
{
    public void Configure(EntityTypeBuilder<RentalItem> builder)
    {
        builder.ToTable("RentalItems");
        builder.HasKey(ri => ri.Id);

        builder.Property(ri => ri.Name).HasMaxLength(200).IsRequired();
        builder.Property(ri => ri.Price).HasColumnType("decimal(18,2)");

        builder.HasOne(ri => ri.Venue)
            .WithMany(v => v.RentalItems)
            .HasForeignKey(ri => ri.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
