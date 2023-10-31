using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

public class UnitController : BaseController
{
    #region CTORS :
    public UnitController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper, cacheService)
    {
    }
    #endregion

    #region Actions :
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Units.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<UnitDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookupAsync()
    {

        try
        {
            var data = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Unit.Lookup,
          () => Context.Units
          .AsNoTracking()
          .OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<UnitLookupDTO>(Mapper.ConfigurationProvider)
          .ToListAsync(),
          60);


            return Ok(data);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpGet("list/available/{buildingId}")]
    public async Task<IActionResult> GetAvailableAsync(Guid buildingId)
    {
        var data = await Context.Units.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .Where(x => x.BuildingId == buildingId
            && (!x.RenterContracts.Any()
            || x.RenterContracts.All(c => c.ContractState != (int)ContractState.Activated
            || (c.ContractState == (int)ContractState.Activated && c.ContractEndDate.Date < DateTime.UtcNow.Date))))
            .ProjectTo<LookupDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();



        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Units.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<UnitDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Units.AsNoTracking().CountAsync());
        var result = new Pagination<UnitDTO>
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
        var unit = await Context.Units.AsNoTracking()
            .ProjectTo<UnitDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (unit == null) return NotFound();
        return Ok(unit);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(UnitDTO unit)
    {
        if (unit == null) return BadRequest();
        unit.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<UnitEntity>(unit);
        await Context.Units.AddAsync(mappedEntity);
        var building = await Context.Buildings.FirstAsync(x => x.Id == unit.BuildingId);
        building.UnitsNo += 1;
        Context.Buildings.Update(building);
        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Unit.Prefix);

        return Created($"~/byId/{unit.Id}", unit);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UnitDTO unit)
    {
        if (unit == null) return BadRequest();
        var currentUnit = (await Context.Units.FirstOrDefaultAsync(x => x.Id == id));
        if (currentUnit == null) return NotFound();
        currentUnit = Mapper.Map(unit, currentUnit);

        Context.Units.Update(currentUnit);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Unit.Prefix);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentUnit = (await Context.Units.FirstOrDefaultAsync(x => x.Id == id));
        if (currentUnit == null) return NotFound();

        Context.Units.Remove(currentUnit);
        var building = currentUnit.Building;
        building.UnitsNo -= 1;
        Context.Buildings.Update(building);
        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Unit.Prefix);

        return Ok();
    }
    [HttpGet("count-by-city")]
    public async Task<IActionResult> GetCountByCity()
    {
        var result = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Unit.CountByCity,
                 () =>
                 {
                     return Context.Units
                      .GroupBy(x => Localizable.IsArabic ? x.District.City.NameAR : x.District.City.NameEN)
                      .Select(x => new UnitCountDTO { Description = x.Key, Count = x.Count() })
                      .ToListAsync();
                 },
                60);


        return Ok(result);
    }
    [HttpGet("count-by-district")]
    public async Task<IActionResult> GetCountByDistrict()
    {
        var result = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Unit.CountByDistrict,
                 () =>
                 {
                     return Context.Units
                      .GroupBy(x => Localizable.IsArabic ? x.District.NameAR : x.District.NameEN)
                      .Select(x => new UnitCountDTO { Description = x.Key, Count = x.Count() })
                      .ToListAsync();
                 },
                60);


        return Ok(result);
    }
    #endregion
}

