using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class BuildingConfig : IEntityTypeConfiguration<BuildingEntity>
{
    public void Configure(EntityTypeBuilder<BuildingEntity> builder)
    {
        builder.Property(x => x.TotalArea).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.RentableArea).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.YearRentAmount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.YearReRentAmount).HasColumnType("decimal").HasPrecision(20, 4);
    }
}
