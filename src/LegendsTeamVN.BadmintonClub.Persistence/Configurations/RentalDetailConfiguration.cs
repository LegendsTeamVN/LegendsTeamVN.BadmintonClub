using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class RentalDetailConfiguration : IEntityTypeConfiguration<RentalDetail>
{
    public void Configure(EntityTypeBuilder<RentalDetail> builder)
    {
        builder.ToTable("RentalDetails");
        builder.HasKey(rd => rd.Id);

        builder.Property(rd => rd.Price).HasColumnType("decimal(18,2)");

        builder.HasOne(rd => rd.Booking)
            .WithMany(b => b.RentalDetails)
            .HasForeignKey(rd => rd.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rd => rd.RentalItem)
            .WithMany(ri => ri.RentalDetails)
            .HasForeignKey(rd => rd.RentalItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
