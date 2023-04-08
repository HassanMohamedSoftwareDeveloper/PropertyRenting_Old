using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class ExchangeVoucherConfig : IEntityTypeConfiguration<ExchangeVoucherEntity>
{
    public void Configure(EntityTypeBuilder<ExchangeVoucherEntity> builder)
    {
        builder.Property(x => x.Amount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.AutoNumber).UseIdentityColumn(1, 1);
        builder.HasAlternateKey(x => x.AutoNumber);
    }
}
