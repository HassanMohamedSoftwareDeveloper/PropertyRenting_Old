using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class NationalityRepository : GenericRepository<NationalityEntity>, INationalityRepository
{
    public NationalityRepository(DbContext context) : base(context)
    {
    }
}
