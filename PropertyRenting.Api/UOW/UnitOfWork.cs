using Microsoft.EntityFrameworkCore;

namespace PropertyRenting.Api.UOW;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));
        _context = context;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        => (await _context.SaveChangesAsync(cancellationToken)) > 0;
}
