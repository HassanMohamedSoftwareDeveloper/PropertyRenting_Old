using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Repositories;

public sealed class EmployeeRepository : GenericRepository<EmployeeEntity>, IEmployeeRepository
{
    public EmployeeRepository(DbContext context) : base(context)
    {
    }
}
