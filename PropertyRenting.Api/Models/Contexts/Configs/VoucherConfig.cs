using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class VoucherConfig : IEntityTypeConfiguration<VoucherEntity>
{
    public void Configure(EntityTypeBuilder<VoucherEntity> builder)
    {
        builder.Property(x => x.AutoNumber).UseIdentityColumn(1, 1);
        builder.HasAlternateKey(x => x.AutoNumber);
    }
}
