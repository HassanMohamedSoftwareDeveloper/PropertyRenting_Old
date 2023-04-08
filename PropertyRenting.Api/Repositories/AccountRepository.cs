using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class AccountRepository : GenericRepository<AccountEntity>, IAccountRepository
{
    public AccountRepository(DbContext context) : base(context)
    {
    }
}
