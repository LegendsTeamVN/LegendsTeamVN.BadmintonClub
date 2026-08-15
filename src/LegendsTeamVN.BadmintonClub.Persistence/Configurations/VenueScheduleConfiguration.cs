using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class VenueScheduleConfiguration : IEntityTypeConfiguration<VenueSchedule>
{
    public void Configure(EntityTypeBuilder<VenueSchedule> builder)
    {
        builder.ToTable("VenueSchedules");
        builder.HasKey(vs => vs.Id);

        builder.HasOne(vs => vs.Venue)
            .WithMany(v => v.Schedules)
            .HasForeignKey(vs => vs.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
