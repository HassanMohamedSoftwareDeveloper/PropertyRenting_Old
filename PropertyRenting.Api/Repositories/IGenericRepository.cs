using PropertyRenting.Api.Models.Entities;
using System.Linq.Expressions;

namespace PropertyRenting.Api.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(CancellationToken cancellationToken = default);

    void Create(TEntity entity);
    void CreateList(List<TEntity> entities);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteList(List<TEntity> entities);
}
