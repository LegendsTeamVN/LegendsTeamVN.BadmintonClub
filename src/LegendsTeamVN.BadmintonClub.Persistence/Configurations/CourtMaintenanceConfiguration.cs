using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class CourtMaintenanceConfiguration : IEntityTypeConfiguration<CourtMaintenance>
{
    public void Configure(EntityTypeBuilder<CourtMaintenance> builder)
    {
        builder.ToTable("CourtMaintenances");
        builder.HasKey(cm => cm.Id);

        builder.Property(cm => cm.Reason).HasMaxLength(1000);

        builder.HasOne(cm => cm.Court)
            .WithMany(c => c.Maintenances)
            .HasForeignKey(cm => cm.CourtId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
