using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

public class ContractAdditionsController : BaseController
{

    #region CTORS :
    public ContractAdditionsController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper, cacheService)
    {
    }
    #endregion

    #region Actions :
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.ContractAdditions.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<ContractAdditionsDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookupAsync()
    {
        try
        {
            var data = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.ContractAdditions.Lookup,
                () => Context.ContractAdditions
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

        var data = await Context.ContractAdditions.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<ContractAdditionsDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();

        int count = (await Context.ContractAdditions.AsNoTracking().CountAsync());

        var result = new Pagination<ContractAdditionsDTO>
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
        var entity = await Context.ContractAdditions.AsNoTracking()
            .ProjectTo<ContractAdditionsDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(ContractAdditionsDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<ContractAdditionsEntity>(entityDTO);
        await Context.ContractAdditions.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.ContractAdditions.Prefix);

        return Created($"~/byId/{entityDTO.Id}", entityDTO);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, ContractAdditionsDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.ContractAdditions.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        currentEntity = Mapper.Map(entityDTO, currentEntity);

        Context.ContractAdditions.Update(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.ContractAdditions.Prefix);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentEntity = (await Context.ContractAdditions.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        Context.ContractAdditions.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.ContractAdditions.Prefix);

        return Ok();
    }
    #endregion
}
