using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Constants;
using PropertyRenting.Api.Enums;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Models.Helpers;

public static class DatabaseCreater
{
    public static async Task Create(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
            bool addData = false;
            Guid additionId = Guid.Parse(SharedId.Rent_Type_Id);
            if (context.ContractAdditions.Any(x => x.Id == additionId) is false)
            {
                addData = true;
                context.ContractAdditions.Add(new Entities.ContractAdditionsEntity
                {
                    Id = additionId,
                    NameEN = "Rent",
                    NameAR = "إيجار",
                    AccountId = null
                });
            }

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            IdentityUser adminUser = new() { Id = Guid.NewGuid().ToString(), UserName = "admin", Email = "admin@admin.com" };
            IdentityUser userUser = new() { Id = Guid.NewGuid().ToString(), UserName = "user", Email = "user@user.com" };
            string password = "P@ssw0rd";
            if (await userManager.FindByNameAsync(adminUser.UserName) == null)
            {
                await userManager.CreateAsync(adminUser, password);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            if (await userManager.FindByNameAsync(userUser.UserName) == null)
            {
                await userManager.CreateAsync(userUser, password);
                await userManager.AddToRoleAsync(userUser, "User");
            }

            if (context.Accounts.Any() is false)
            {
                addData = true;
                context.Accounts.AddRange(new List<AccountEntity>()
                {
                    new AccountEntity
                    {
                        Code="1",
                        NameAR="الأصول",
                        NameEN="Assets",
                        ParentId=null,
                        CreatedBy=adminUser.Id,
                        AccountTypeId=(int) AccountType.Total,
                        Level=1

                    },
                    new AccountEntity
                    {
                        Code="2",
                        NameAR="الخصوم",
                        NameEN="Liabilities",
                        ParentId=null,
                        CreatedBy=adminUser.Id,
                        AccountTypeId=(int) AccountType.Total,
                        Level=1
                    },
                    new AccountEntity
                    {
                        Code="3",
                        NameAR="المصروفات",
                        NameEN="Expenses",
                        ParentId=null,
                        CreatedBy=adminUser.Id,
                        AccountTypeId=(int) AccountType.Total,
                        Level=1
                    },
                    new AccountEntity
                    {
                        Code="4",
                        NameAR="الإيرادات",
                        NameEN="Revenues",
                        ParentId=null,
                        CreatedBy=adminUser.Id,
                        AccountTypeId=(int) AccountType.Total,
                        Level=1
                    },
                });
            }
            if (addData)
            {
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
