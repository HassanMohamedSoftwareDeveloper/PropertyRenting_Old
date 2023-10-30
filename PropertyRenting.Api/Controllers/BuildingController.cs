using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

public class BuildingController : BaseController
{

    #region CTORS :
    public BuildingController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper, cacheService)
    {

    }
    #endregion

    #region Actions :
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Buildings.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<BuildingDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookupAsync()
    {
        try
        {
            var data = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Building.Lookup,
              () => Context.Buildings
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

        var data = await Context.Buildings.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<BuildingDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Buildings.AsNoTracking().CountAsync());
        var result = new Pagination<BuildingDTO>
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
        var building = await Context.Buildings.AsNoTracking()
            .ProjectTo<BuildingDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (building == null) return NotFound();
        return Ok(building);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(BuildingDTO building)
    {
        if (building == null) return BadRequest();
        building.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<BuildingEntity>(building);
        await Context.Buildings.AddAsync(mappedEntity);

        var contributors = Mapper.Map<List<BuildingContributerEntity>>(building.Contributers);
        contributors.ForEach(contributor =>
        {
            contributor.Id = Guid.NewGuid();
            contributor.BuildingId = mappedEntity.Id;
        });

        Context.BuildingContributers.AddRange(contributors);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Building.Prefix);
        return Created($"~/byId/{building.Id}", building);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, BuildingDTO building)
    {
        if (building == null) return BadRequest();
        var currentBuilding = (await Context.Buildings.FirstOrDefaultAsync(x => x.Id == id));
        if (currentBuilding == null) return NotFound();

        currentBuilding = Mapper.Map(building, currentBuilding);
        currentBuilding.Contributers.Clear();
        Context.Buildings.Update(currentBuilding);

        var contributors = Mapper.Map<List<BuildingContributerEntity>>(building.Contributers);
        contributors.ForEach(contributor =>
        {
            contributor.Id = Guid.NewGuid();
            contributor.BuildingId = currentBuilding.Id;
        });

        Context.BuildingContributers.AddRange(contributors);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Building.Prefix);
        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentBuilding = (await Context.Buildings.FirstOrDefaultAsync(x => x.Id == id));
        if (currentBuilding == null) return NotFound();

        Context.Buildings.Remove(currentBuilding);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Building.Prefix);
        return Ok();
    }
    [HttpGet("count-by-construction-status")]
    public async Task<IActionResult> GetCountByConstructionStatus()
    {
        var result = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Building.CountByConstructionStatus,
                async () =>
                {
                    var groupedData = await Context.Buildings
                    .GroupBy(x => x.ConstructionStatusId)
                    .Select(x => new { ConstructionStatusId = x.Key, Count = x.Count() })
                    .ToListAsync();
                    var data = groupedData.Select(x => new BuildingCountDTO
                    {
                        Description = Resources.ConstructionStatus.ResourceManager.GetResourceValue(((ConstructionStatus)x.ConstructionStatusId).ToEnumString()),
                        Count = x.Count
                    });
                    return data;
                },
                60);


        return Ok(result);
    }
    [HttpGet("count-by-building-type")]
    public async Task<IActionResult> GetCountByBuildingType()
    {
        var result = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Building.CountByBuildingType,
                async () =>
                {
                    var groupedData = await Context.Buildings
                    .GroupBy(x => x.TypeId)
                    .Select(x => new { TypeId = x.Key, Count = x.Count() })
                    .ToListAsync();
                    var data = groupedData.Select(x => new BuildingCountDTO
                    {
                        Description = Resources.BuildingType.ResourceManager.GetResourceValue(((BuildingType)x.TypeId).ToEnumString()),
                        Count = x.Count
                    });
                    return data;
                },
                60);


        return Ok(result);
    }
    [HttpGet("count-by-city")]
    public async Task<IActionResult> GetCountByCity()
    {
        var result = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Building.CountByCity,
                 () =>
                {
                    return Context.Buildings
                     .GroupBy(x => Localizable.IsArabic ? x.District.City.NameAR : x.District.City.NameEN)
                     .Select(x => new BuildingCountDTO { Description = x.Key, Count = x.Count() })
                     .ToListAsync();
                },
                60);


        return Ok(result);
    }
}
#endregion
