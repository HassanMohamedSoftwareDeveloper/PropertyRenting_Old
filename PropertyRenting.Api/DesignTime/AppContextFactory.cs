using Microsoft.EntityFrameworkCore.Design;

namespace PropertyRenting.Api.DesignTime;


public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{

    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=SQL5104.site4now.net;Database=db_a8eb58_propertyrentingdb;User Id=db_a8eb58_propertyrentingdb_admin;Password=Sk87654321;");

        return new AppDbContext(optionsBuilder.Options);
    }
}