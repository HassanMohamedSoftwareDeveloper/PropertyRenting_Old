﻿using PropertyRenting.Api.Extensions;
using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

public class DistrictController : BaseController
{
    #region CTORS :
    public DistrictController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper, cacheService)
    {
    }
    #endregion

    #region Actions :
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Districts.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<DistrictDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup/{cityId}")]
    public async Task<IActionResult> GetLookupAsync(string cityId)
    {

        try
        {
            var validValue = Guid.TryParse(cityId, out Guid convertedCityId);
            var key = validValue
              ? string.Format(Constants.Constants.CacheKeys.District.LookupByCity, convertedCityId)
              : Constants.Constants.CacheKeys.District.Lookup;

            var data = await CacheService.GetOrCreateAsync(key,
                () => Context.Districts
                .AsNoTracking()
                .WhereIf(validValue, x => x.CityId == convertedCityId)
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

        var data = await Context.Districts.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<DistrictDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();

        int count = (await Context.Districts.AsNoTracking().CountAsync());

        var result = new Pagination<DistrictDTO>
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
        var city = await Context.Districts.AsNoTracking()
            .ProjectTo<DistrictDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return NotFound();
        return Ok(city);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(DistrictDTO district)
    {
        if (district == null) return BadRequest();
        district.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<DistrictEntity>(district);
        await Context.Districts.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.District.Prefix);

        return Created($"~/byId/{district.Id}", district);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, DistrictDTO district)
    {
        if (district == null) return BadRequest();
        var currentDistrict = (await Context.Districts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentDistrict == null) return NotFound();
        currentDistrict = Mapper.Map(district, currentDistrict);

        Context.Districts.Update(currentDistrict);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.District.Prefix);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentDistrict = (await Context.Districts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentDistrict == null) return NotFound();

        Context.Districts.Remove(currentDistrict);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.District.Prefix);

        return Ok();
    }
    #endregion
}
