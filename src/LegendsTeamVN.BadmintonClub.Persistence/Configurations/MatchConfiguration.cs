using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("Matches");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.PricePerPlayer).HasColumnType("decimal(18,2)");
        builder.Property(m => m.Description).HasMaxLength(1000);

        builder.HasOne(m => m.BookingDetail)
            .WithOne(bd => bd.Match)
            .HasForeignKey<Match>(m => m.BookingDetailId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
