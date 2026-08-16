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

        // --- BỔ SUNG CÁC MỐI QUAN HỆ (RELATIONSHIPS) ---
        
        // 1. Quan hệ 1-N với VenueImage
        builder.HasMany(v => v.Images)
            .WithOne() // Thường trong class VenueImage sẽ có property Venue, nếu không có truyền rỗng
            .HasForeignKey("VenueId") // Dùng string nếu chưa biết chắc tên property trong Entity con
            .OnDelete(DeleteBehavior.Cascade);

        // 2. Quan hệ 1-N với Court (Sân con)
        builder.HasMany(v => v.Courts)
            .WithOne() 
            .HasForeignKey("VenueId")
            .OnDelete(DeleteBehavior.Cascade);

        // 3. Quan hệ 1-N với VenueSchedule (Lịch hoạt động)
        builder.HasMany(v => v.Schedules)
            .WithOne()
            .HasForeignKey("VenueId")
            .OnDelete(DeleteBehavior.Cascade);

        // 4. Quan hệ 1-N với VenueSportType (Các môn thể thao hỗ trợ)
        builder.HasMany(v => v.VenueSportTypes)
            .WithOne()
            .HasForeignKey("VenueId")
            .OnDelete(DeleteBehavior.Cascade);

        // 5. Quan hệ 1-N với RentalItem (Đồ cho thuê: Vợt, giày...)
        builder.HasMany(v => v.RentalItems)
            .WithOne()
            .HasForeignKey("VenueId")
            .OnDelete(DeleteBehavior.Cascade);

        // 6. Quan hệ 1-N với VenueStaff (Nhân viên của sân)
        builder.HasMany(v => v.Staffs)
            .WithOne()
            .HasForeignKey("VenueId")
            .OnDelete(DeleteBehavior.Cascade);

        // 7. Quan hệ 1-N với Review (Đánh giá của khách hàng)
        builder.HasMany(v => v.Reviews)
            .WithOne()
            .HasForeignKey("VenueId")
            .OnDelete(DeleteBehavior.Cascade);

        // 8. Quan hệ 1-N với Favourite (Danh sách yêu thích của khách hàng)
        builder.HasMany(v => v.Favourites)
            .WithOne()
            .HasForeignKey("VenueId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
