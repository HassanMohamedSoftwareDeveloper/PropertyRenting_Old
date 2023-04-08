using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class CityRepository : GenericRepository<CityEntity>, ICityRepository
{
    public CityRepository(DbContext context) : base(context)
    {
    }
}
