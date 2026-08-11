using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class BookingCancellationConfiguration : IEntityTypeConfiguration<BookingCancellation>
{
    public void Configure(EntityTypeBuilder<BookingCancellation> builder)
    {
        builder.ToTable("BookingCancellations");
        builder.HasKey(bc => bc.Id);

        builder.Property(bc => bc.Reason).HasMaxLength(1000);
        builder.Property(bc => bc.RefundAmount).HasColumnType("decimal(18,2)");

        builder.HasOne(bc => bc.Booking)
            .WithOne(b => b.Cancellation)
            .HasForeignKey<BookingCancellation>(bc => bc.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
