using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories
{
    public sealed class BuildingRepository : GenericRepository<BuildingEntity>, IBuildingRepository
    {
        public BuildingRepository(DbContext context) : base(context)
        {
        }
    }
}
