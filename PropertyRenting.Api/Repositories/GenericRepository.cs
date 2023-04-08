using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;
using System.Linq.Expressions;

namespace PropertyRenting.Api.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> EntitySet;

    public GenericRepository(DbContext context)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));

        this.Context = context;
        this.EntitySet = this.Context.Set<TEntity>();
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) => await this.EntitySet.AsNoTracking().ToListAsync(cancellationToken);
    public async Task<List<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        => await this.EntitySet.AsNoTracking().Where(filter).ToListAsync(cancellationToken);
    public async Task<List<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        return await EntitySet.AsNoTracking().OrderBy(x => x.CreatedOnUtc).Skip(skipped).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default) => await this.EntitySet.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default) => await this.EntitySet.AsNoTracking().CountAsync(cancellationToken);



    public void Create(TEntity entity) => this.EntitySet.Add(entity);
    public void CreateList(List<TEntity> entities) => this.EntitySet.AddRange(entities);
    public void Update(TEntity entity) => this.EntitySet.Update(entity);
    public void Delete(TEntity entity) => this.EntitySet.Remove(entity);
    public void DeleteList(List<TEntity> entities) => this.EntitySet.RemoveRange(entities);
}
