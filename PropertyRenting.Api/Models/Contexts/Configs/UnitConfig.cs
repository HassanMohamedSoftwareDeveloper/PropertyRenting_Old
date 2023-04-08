using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class UnitConfig : IEntityTypeConfiguration<UnitEntity>
{
    public void Configure(EntityTypeBuilder<UnitEntity> builder)
    {
        builder.HasOne(x => x.District).WithMany(x => x.Units).OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.TotalArea).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.RentableArea).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.AnnualRentAmount).HasColumnType("decimal").HasPrecision(20, 4);
    }
}
