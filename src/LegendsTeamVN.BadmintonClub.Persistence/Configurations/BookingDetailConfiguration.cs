using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class BookingDetailConfiguration : IEntityTypeConfiguration<BookingDetail>
{
    public void Configure(EntityTypeBuilder<BookingDetail> builder)
    {
        builder.ToTable("BookingDetails");
        builder.HasKey(bd => bd.Id);

        builder.Property(bd => bd.Price).HasColumnType("decimal(18,2)");

        builder.HasOne(bd => bd.Booking)
            .WithMany(b => b.BookingDetails)
            .HasForeignKey(bd => bd.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bd => bd.Court)
            .WithMany(c => c.BookingDetails)
            .HasForeignKey(bd => bd.CourtId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
