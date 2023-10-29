using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PropertyRenting.Api.Models.Contexts.Configs;

namespace PropertyRenting.Api.Models.Contexts;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<OwnerEntity> Owners { get; set; }
    public DbSet<BuildingEntity> Buildings { get; set; }
    public DbSet<BuildingContributerEntity> BuildingContributers { get; set; }
    public DbSet<ContactPersonEntity> ContactPersons { get; set; }
    public DbSet<DistrictEntity> Districts { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<RenterEntity> Renters { get; set; }
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<NationalityEntity> Nationalities { get; set; }
    public DbSet<ContributerEntity> Contributers { get; set; }
    public DbSet<OwnerContractEntity> OwnerContracts { get; set; }
    public DbSet<OwnerFinancialTransactionEntity> OwnerFinancialTransactions { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<AccountSetupEntity> AccountSetups { get; set; }
    public DbSet<RenterContractEntity> RenterContracts { get; set; }
    public DbSet<RenterFinancialTransactionEntity> RenterFinancialTransactions { get; set; }
    public DbSet<ContractAdditionsEntity> ContractAdditions { get; set; }
    public DbSet<ReceiptVoucherEntity> ReceiptVouchers { get; set; }
    public DbSet<ReceiptVoucherDetailsEntity> ReceiptVoucherDetails { get; set; }
    public DbSet<ExchangeVoucherEntity> ExchangeVouchers { get; set; }
    public DbSet<ExchangeVoucherDetailsEntity> ExchangeVoucherDetails { get; set; }
    public DbSet<VoucherEntity> Vouchers { get; set; }
    public DbSet<VoucherDetailsEntity> VoucherDetails { get; set; }
    public DbSet<ExpenseEntity> Expenses { get; set; }
    public DbSet<CashBankEntity> CashBanks { get; set; }

    public DbSet<ActionLogEntity> ActionLogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountConfig());
        modelBuilder.ApplyConfiguration(new AccountSetupConfig());

        modelBuilder.ApplyConfiguration(new BuildingConfig());
        modelBuilder.ApplyConfiguration(new BuildingContributerConfig());

        modelBuilder.ApplyConfiguration(new UnitConfig());

        modelBuilder.ApplyConfiguration(new OwnerContractConfig());
        modelBuilder.ApplyConfiguration(new OwnerFinancialTransactionConfig());

        modelBuilder.ApplyConfiguration(new RenterContractConfig());
        modelBuilder.ApplyConfiguration(new RenterFinancialTransactionConfig());

        modelBuilder.ApplyConfiguration(new ReceiptVoucherConfig());
        modelBuilder.ApplyConfiguration(new ReceiptVoucherDetailsConfig());

        modelBuilder.ApplyConfiguration(new ExchangeVoucherConfig());
        modelBuilder.ApplyConfiguration(new ExchangeVoucherDetailsConfig());

        modelBuilder.ApplyConfiguration(new VoucherConfig());
        modelBuilder.ApplyConfiguration(new VoucherDetailsConfig());
    }
}
