using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PropertyRenting.Api.Enums;
using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Interceptors;

public class ActionsInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ActionsInterceptor(IHttpContextAccessor httpContextAccessor)
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
        var entries = dbContext.ChangeTracker.Entries();

        var userId = _httpContextAccessor.HttpContext?.User?.UserId();
        //var options = new JsonSerializerSettings
        //{
        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //};
        List<ActionLogEntity> actions = new();
        foreach (var entityEntry in entries)
        {
            //var data = JsonConvert.SerializeObject(entityEntry.Entity, options);

            ActionLogEntity logEntity = new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Data = entityEntry.DebugView.LongView,
                ActionDate = DateTime.UtcNow,
                Type = entityEntry.Entity.GetType().Name.Replace("Proxy", "")
            };

            if (entityEntry.State == EntityState.Added)
            {
                logEntity.Action = Actions.Added.ToString();
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                logEntity.Action = Actions.Updated.ToString();
            }
            else if (entityEntry.State == EntityState.Deleted)
            {
                logEntity.Action = Actions.Deleted.ToString();
            }

            actions.Add(logEntity);
        }
        dbContext.AddRange(actions);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
