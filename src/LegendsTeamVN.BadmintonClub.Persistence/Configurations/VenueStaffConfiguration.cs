using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class VenueStaffConfiguration : IEntityTypeConfiguration<VenueStaff>
{
    public void Configure(EntityTypeBuilder<VenueStaff> builder)
    {
        builder.ToTable("VenueStaffs");
        builder.HasKey(vs => vs.Id);

        builder.HasOne(vs => vs.Venue)
            .WithMany(v => v.Staffs)
            .HasForeignKey(vs => vs.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
