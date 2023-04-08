using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class UnitRepository : GenericRepository<UnitEntity>, IUnitRepository
{
    public UnitRepository(DbContext context) : base(context)
    {
    }
}
