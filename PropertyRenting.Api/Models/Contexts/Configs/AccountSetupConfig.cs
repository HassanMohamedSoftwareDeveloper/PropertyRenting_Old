using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Contexts.Configs;

public class AccountSetupConfig : IEntityTypeConfiguration<AccountSetupEntity>
{
    public void Configure(EntityTypeBuilder<AccountSetupEntity> builder)
    {
        builder.HasOne(x => x.AccruedRevenueAccount).WithMany(x => x.AccruedRevenueAccounts).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.RevenueAccount).WithMany(x => x.RevenueAccounts).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.AccruedExpenseAccount).WithMany(x => x.AccruedExpenseAccounts).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ExpenseAccount).WithMany(x => x.ExpenseAccounts).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ContributerAccount).WithMany(x => x.ContributerAccounts).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.RenterAccount).WithMany(x => x.RenterAccounts).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.OwnerAccount).WithMany(x => x.OwnerAccounts).OnDelete(DeleteBehavior.Restrict);
    }
}
