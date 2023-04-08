using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Interceptors;

public class AutidableInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AutidableInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                          InterceptionResult<int> result,
                                                                          CancellationToken cancellationToken = default)
    {

        DbContext dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

        var userId = _httpContextAccessor.HttpContext?.User?.UserId();

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(x => x.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
                if (userId != null) entityEntry.Property(x => x.CreatedBy).CurrentValue = userId;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(x => x.UpdatedOnUtc).CurrentValue = DateTime.UtcNow;
                if (userId != null) entityEntry.Property(x => x.ModifiedBy).CurrentValue = userId;
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
