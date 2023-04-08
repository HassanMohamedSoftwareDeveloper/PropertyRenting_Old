using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class VoucherDetailsConfig : IEntityTypeConfiguration<VoucherDetailsEntity>
{
    public void Configure(EntityTypeBuilder<VoucherDetailsEntity> builder)
    {
        builder.Property(x => x.DebitAmount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.CreditAmount).HasColumnType("decimal").HasPrecision(20, 4);
    }
}
