using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class BuildingContributerConfig : IEntityTypeConfiguration<BuildingContributerEntity>
{
    public void Configure(EntityTypeBuilder<BuildingContributerEntity> builder)
    {
        builder.Property(x => x.Percentage).HasColumnType("decimal").HasPrecision(20, 4);
    }
}
