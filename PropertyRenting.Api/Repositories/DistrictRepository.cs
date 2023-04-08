using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class DistrictRepository : GenericRepository<DistrictEntity>, IDistrictRepository
{
    public DistrictRepository(DbContext context) : base(context)
    {
    }
}
