using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class OwnerRepository : GenericRepository<OwnerEntity>, IOwnerRepository
{
    public OwnerRepository(DbContext context) : base(context)
    {
    }
}
