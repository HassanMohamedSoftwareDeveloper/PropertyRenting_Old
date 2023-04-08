using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class ContractRepository : GenericRepository<OwnerContractEntity>, IContractRepository
{
    public ContractRepository(DbContext context) : base(context)
    {
    }
}
