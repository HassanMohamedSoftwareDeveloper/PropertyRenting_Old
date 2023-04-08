using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class RenterRepository : GenericRepository<RenterEntity>, IRenterRepository
{
    public RenterRepository(DbContext context) : base(context)
    {
    }
}
