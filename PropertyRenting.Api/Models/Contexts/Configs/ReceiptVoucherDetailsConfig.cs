using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class ReceiptVoucherDetailsConfig : IEntityTypeConfiguration<ReceiptVoucherDetailsEntity>
{
    public void Configure(EntityTypeBuilder<ReceiptVoucherDetailsEntity> builder)
    {
        builder.Property(x => x.Amount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.Installment).HasColumnType("decimal").HasPrecision(20, 4);
    }
}
