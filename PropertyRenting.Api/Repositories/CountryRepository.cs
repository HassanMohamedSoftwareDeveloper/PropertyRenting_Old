using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class CountryRepository : GenericRepository<CountryEntity>, ICountryRepository
{
    public CountryRepository(DbContext context) : base(context)
    {
    }
}
