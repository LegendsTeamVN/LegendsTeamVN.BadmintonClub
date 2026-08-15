using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class VenueSportTypeConfiguration : IEntityTypeConfiguration<VenueSportType>
{
    public void Configure(EntityTypeBuilder<VenueSportType> builder)
    {
        builder.ToTable("VenueSportTypes");
        builder.HasKey(vst => vst.Id);

        builder.HasOne(vst => vst.Venue)
            .WithMany(v => v.VenueSportTypes)
            .HasForeignKey(vst => vst.VenueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(vst => vst.SportType)
            .WithMany(st => st.VenueSportTypes)
            .HasForeignKey(vst => vst.SportTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
