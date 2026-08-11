using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.ToTable("Favourites");
        builder.HasKey(f => f.Id);

        builder.HasOne(f => f.Venue)
            .WithMany(v => v.Favourites)
            .HasForeignKey(f => f.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
