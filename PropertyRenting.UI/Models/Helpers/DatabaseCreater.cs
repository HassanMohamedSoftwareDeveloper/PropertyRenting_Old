using PropertyRenting.UI.Models.Contexts;

namespace PropertyRenting.UI.Models.Helpers;

public static class DatabaseCreater
{
    public static void Create(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
