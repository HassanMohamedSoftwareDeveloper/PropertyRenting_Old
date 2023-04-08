using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class ExchangeVoucherDetailsConfig : IEntityTypeConfiguration<ExchangeVoucherDetailsEntity>
{
    public void Configure(EntityTypeBuilder<ExchangeVoucherDetailsEntity> builder)
    {
        builder.Property(x => x.Amount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.Installment).HasColumnType("decimal").HasPrecision(20, 4);
    }
}
