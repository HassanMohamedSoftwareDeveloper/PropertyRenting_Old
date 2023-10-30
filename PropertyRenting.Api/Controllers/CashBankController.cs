using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

public class CashBankController : BaseController
{

    #region CTORS :
    public CashBankController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper, cacheService)
    {
    }
    #endregion

    #region Actions :
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.CashBanks
            .AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<CashBankDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookupAsync()
    {

        try
        {
            var data = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.CashBank.Lookup,
            () => Context.CashBanks
                .AsNoTracking()
                .OrderBy(x => x.CreatedOnUtc)
               .ProjectTo<LookupDTO>(Mapper.ConfigurationProvider)
               .ToListAsync(), 60);

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

        var data = await Context.CashBanks.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<CashBankDTO>(Mapper.ConfigurationProvider)
            .Skip(skipped)
            .Take(pageSize)
            .ToListAsync();

        int count = (await Context.CashBanks.AsNoTracking().CountAsync());

        var result = new Pagination<CashBankDTO>
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
        var entity = await Context.CashBanks.AsNoTracking()
            .ProjectTo<CashBankDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CashBankDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<CashBankEntity>(entityDTO);
        await Context.CashBanks.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.CashBank.Prefix);
        return Created($"~/byId/{entityDTO.Id}", entityDTO);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CashBankDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.CashBanks.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        currentEntity = Mapper.Map(entityDTO, currentEntity);

        Context.CashBanks.Update(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.CashBank.Prefix);
        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentEntity = (await Context.CashBanks.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        Context.CashBanks.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.CashBank.Prefix);
        return Ok();
    }
    #endregion
}
