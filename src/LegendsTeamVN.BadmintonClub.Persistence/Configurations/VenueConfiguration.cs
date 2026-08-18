using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("Venues");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name).HasMaxLength(200).IsRequired();
        builder.Property(v => v.Address).HasMaxLength(500).IsRequired();
        builder.Property(v => v.Latitude).HasColumnType("decimal(18,6)");
        builder.Property(v => v.Longitude).HasColumnType("decimal(18,6)");
        builder.Property(v => v.Description).HasMaxLength(2000);
        builder.Property(v => v.OpenTime).HasColumnType("time").IsRequired();
        builder.Property(v => v.CloseTime).HasColumnType("time").IsRequired();
        builder.Property(v => v.IsActive).HasDefaultValue(true);

        // LƯU Ý: Các mối quan hệ 1-N với VenueImage, Court, VenueSchedule,
        // VenueSportType, RentalItem, VenueStaff, Review, Favourite, CourtPricing
        // đã được khai báo trong file Configuration riêng của từng entity:
        //   - VenueImageConfiguration.cs
        //   - CourtConfiguration.cs
        //   - VenueScheduleConfiguration.cs
        //   - VenueSportTypeConfiguration.cs
        //   - RentalItemConfiguration.cs
        //   - VenueStaffConfiguration.cs
        //   - ReviewConfiguration.cs
        //   - FavouriteConfiguration.cs
        //   - CourtPricingConfiguration.cs
        // KHÔNG khai báo lại ở đây để tránh xung đột quan hệ (tạo shadow FK VenueId1).
    }
}
