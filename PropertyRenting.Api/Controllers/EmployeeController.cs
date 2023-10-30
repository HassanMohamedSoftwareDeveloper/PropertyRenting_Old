using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

public class EmployeeController : BaseController
{
    private readonly ICacheService _cacheService;

    public EmployeeController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper)
    {
        _cacheService = cacheService;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Employees.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<EmployeeDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookupAsync()
    {
        try
        {
            var data = await _cacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Employee.Lookup,
              () => Context.Employees
              .AsNoTracking()
              .OrderBy(x => x.CreatedOnUtc)
              .ProjectTo<LookupDTO>(Mapper.ConfigurationProvider)
              .ToListAsync(),
              60);

            return Ok(data);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Employees.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<EmployeeDTO>(Mapper.ConfigurationProvider)
            .Skip(skipped).Take(pageSize)
            .ToListAsync();
        int count = (await Context.Employees.AsNoTracking().CountAsync());
        var result = new Pagination<EmployeeDTO>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = count
        };

        return Ok(result);
    }
    [HttpGet("byId/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var employee = await Context.Employees.AsNoTracking()
            .ProjectTo<EmployeeDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(EmployeeDTO employee)
    {
        if (employee == null) return BadRequest();
        employee.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<EmployeeEntity>(employee);
        await Context.Employees.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{employee.Id}", employee);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, EmployeeDTO employee)
    {
        if (employee == null) return BadRequest();
        var currentEmployee = (await Context.Employees.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEmployee == null) return NotFound();
        currentEmployee = Mapper.Map(employee, currentEmployee);

        Context.Employees.Update(currentEmployee);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentEmployee = (await Context.Employees.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEmployee == null) return NotFound();

        Context.Employees.Remove(currentEmployee);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
