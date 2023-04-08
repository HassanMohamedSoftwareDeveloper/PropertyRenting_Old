using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class AccountSetupRepository : GenericRepository<AccountSetupEntity>, IAccountSetupRepository
{
    public AccountSetupRepository(DbContext context) : base(context)
    {
    }
}
