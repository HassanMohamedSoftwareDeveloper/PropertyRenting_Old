using Microsoft.EntityFrameworkCore;
using PropertyRenting.UI.Models.Entities;

namespace PropertyRenting.UI.Models.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<OwnerEntity> Owners { get; set; }
    public DbSet<BuildingEntity> Buildings { get; set; }
    public DbSet<BuildingOwnerEntity> BuildingOwners { get; set; }
    public DbSet<ContactPersonEntity> ContactPersons { get; set; }
    public DbSet<DistrictEntity> Districts { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<RenterEntity> Renters { get; set; }
    public DbSet<UnitEntity> Units { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BuildingEntity>().Property(x => x.TotalArea).HasColumnType("decimal").HasPrecision(4);
        modelBuilder.Entity<BuildingEntity>().Property(x => x.RentableArea).HasColumnType("decimal").HasPrecision(4);
        modelBuilder.Entity<BuildingEntity>().Property(x => x.YearRentAmount).HasColumnType("decimal").HasPrecision(4);
        modelBuilder.Entity<BuildingEntity>().Property(x => x.YearReRentAmount).HasColumnType("decimal").HasPrecision(4);

        modelBuilder.Entity<UnitEntity>().Property(x => x.TotalArea).HasColumnType("decimal").HasPrecision(4);
        modelBuilder.Entity<UnitEntity>().Property(x => x.RentableArea).HasColumnType("decimal").HasPrecision(4);
        modelBuilder.Entity<UnitEntity>().Property(x => x.AnnualRentAmount).HasColumnType("decimal").HasPrecision(4);



        modelBuilder.Entity<BuildingOwnerEntity>().Property(x => x.Percentage).HasColumnType("decimal").HasPrecision(2);
    }
}
