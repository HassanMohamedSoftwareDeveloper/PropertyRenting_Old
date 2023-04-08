using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class FinancialTransactionRepository : GenericRepository<RenterFinancialTransactionEntity>, IFinancialTransactionRepository
{
    public FinancialTransactionRepository(DbContext context) : base(context)
    {
    }
}
