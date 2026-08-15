using LegendsTeamVN.BadmintonClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsTeamVN.BadmintonClub.Persistence.Configurations;

public class SportTypeConfiguration : IEntityTypeConfiguration<SportType>
{
    public void Configure(EntityTypeBuilder<SportType> builder)
    {
        builder.ToTable("SportTypes");
        builder.HasKey(st => st.Id);

        builder.Property(st => st.Name).HasMaxLength(100).IsRequired();
    }
}
