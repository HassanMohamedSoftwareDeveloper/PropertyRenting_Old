using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class RenterContractConfig : IEntityTypeConfiguration<RenterContractEntity>
{
    public void Configure(EntityTypeBuilder<RenterContractEntity> builder)
    {
        builder.HasOne(x => x.Unit).WithMany(x => x.RenterContracts).OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.ContractAmount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.IncreasingValue).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.AutoNumber).UseIdentityColumn(1, 1);
        builder.HasAlternateKey(x => x.AutoNumber);
    }
}
