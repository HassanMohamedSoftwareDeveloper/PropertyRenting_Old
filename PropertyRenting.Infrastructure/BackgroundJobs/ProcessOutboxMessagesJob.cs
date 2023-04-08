using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PropertyRenting.Domain.Primitives;
using PropertyRenting.Infrastructure.Persistence.Outbox;
using Quartz;

namespace PropertyRenting.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly DbContext _dbContext;//ApplicationDbContext
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(DbContext dbContext, IPublisher publisher)
    {
        this._dbContext = dbContext;
        this._publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext.Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(outboxMessage.Content);

            if (domainEvent is null) continue;

            await _publisher.Publish(domainEvent, context.CancellationToken);
            outboxMessage.OccuredOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
