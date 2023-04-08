using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class OwnerFinancialTransactionConfig : IEntityTypeConfiguration<OwnerFinancialTransactionEntity>
{
    public void Configure(EntityTypeBuilder<OwnerFinancialTransactionEntity> builder)
    {
        builder.Property(x => x.Amount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.PaidAmount).HasColumnType("decimal").HasPrecision(20, 4);
        builder.Property(x => x.Balance).HasColumnType("decimal").HasPrecision(20, 4);
    }
}
