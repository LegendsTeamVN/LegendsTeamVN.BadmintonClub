using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Vouchers");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Code).HasMaxLength(50).IsRequired();
        builder.HasIndex(v => v.Code).IsUnique();

        builder.Property(v => v.DiscountAmount).HasColumnType("decimal(18,2)");
        builder.Property(v => v.MaxDiscount).HasColumnType("decimal(18,2)");
        builder.Property(v => v.MinOrderValue).HasColumnType("decimal(18,2)");
    }
}
