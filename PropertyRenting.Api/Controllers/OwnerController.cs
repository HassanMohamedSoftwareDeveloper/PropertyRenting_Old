namespace PropertyRenting.Api.Controllers;

public class OwnerController : BaseController
{
    public OwnerController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Owners.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<OwnerDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookupAsync()
    {

        try
        {
            var data = await Context.Owners
                .AsNoTracking()
                .OrderBy(x => x.CreatedOnUtc)
               .ProjectTo<LookupDTO>(Mapper.ConfigurationProvider)
               .ToListAsync();
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

        var data = await Context.Owners.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<OwnerDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Owners.AsNoTracking().CountAsync());
        var result = new Pagination<OwnerDTO>
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
        var owner = await Context.Owners.AsNoTracking()
            .ProjectTo<OwnerDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (owner == null) return NotFound();
        return Ok(owner);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(OwnerDTO owner)
    {
        if (owner == null) return BadRequest();
        owner.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<OwnerEntity>(owner);
        await Context.Owners.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{owner.Id}", owner);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, OwnerDTO owner)
    {
        if (owner == null) return BadRequest();
        var currentOwner = (await Context.Owners.FirstOrDefaultAsync(x => x.Id == id));
        if (currentOwner == null) return NotFound();
        currentOwner = Mapper.Map(owner, currentOwner);

        Context.Owners.Update(currentOwner);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentOwner = (await Context.Owners.FirstOrDefaultAsync(x => x.Id == id));
        if (currentOwner == null) return NotFound();

        Context.Owners.Remove(currentOwner);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }

}
