using PropertyRenting.Api.Extensions;
using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

public class CityController : BaseController
{
    private readonly ICacheService _cacheService;

    public CityController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper)
    {
        _cacheService = cacheService;
    }
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Cities.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<CityDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup/{countryId}")]
    public async Task<IActionResult> GetLookupAsync(string countryId)
    {

        try
        {
            var validValue = Guid.TryParse(countryId, out Guid convertedCountryId);
            var key = validValue
                ? string.Format(Constants.Constants.CacheKeys.City.LookupByBuilding, convertedCountryId)
                : Constants.Constants.CacheKeys.City.Lookup;

            var data = await _cacheService.GetOrCreateAsync(key,
                () => Context.Cities
                .AsNoTracking()
                .WhereIf(validValue, x => x.CountryId == convertedCountryId)
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

        var data = await Context.Cities.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<CityDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Cities.AsNoTracking().CountAsync());
        var result = new Pagination<CityDTO>
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
        var city = await Context.Cities.AsNoTracking()
            .ProjectTo<CityDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return NotFound();
        return Ok(city);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CityDTO city)
    {
        if (city == null) return BadRequest();
        city.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<CityEntity>(city);
        await Context.Cities.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{city.Id}", city);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CityDTO city)
    {
        if (city == null) return BadRequest();
        var currentCity = (await Context.Cities.FirstOrDefaultAsync(x => x.Id == id));
        if (currentCity == null) return NotFound();
        currentCity = Mapper.Map(city, currentCity);

        Context.Cities.Update(currentCity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentCity = (await Context.Cities.FirstOrDefaultAsync(x => x.Id == id));
        if (currentCity == null) return NotFound();

        Context.Cities.Remove(currentCity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
