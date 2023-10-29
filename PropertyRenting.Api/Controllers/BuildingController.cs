namespace PropertyRenting.Api.Controllers;

public class BuildingController : BaseController
{
    public BuildingController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

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
            var data = await Context.Buildings
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

        var contributers = Mapper.Map<List<BuildingContributerEntity>>(building.Contributers);
        contributers.ForEach(contributer =>
        {
            contributer.Id = Guid.NewGuid();
            contributer.BuildingId = mappedEntity.Id;
        });

        Context.BuildingContributers.AddRange(contributers);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

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

        var contributers = Mapper.Map<List<BuildingContributerEntity>>(building.Contributers);
        contributers.ForEach(contributer =>
        {
            contributer.Id = Guid.NewGuid();
            contributer.BuildingId = currentBuilding.Id;
        });

        Context.BuildingContributers.AddRange(contributers);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentBuilding = (await Context.Buildings.FirstOrDefaultAsync(x => x.Id == id));
        if (currentBuilding == null) return NotFound();

        Context.Buildings.Remove(currentBuilding);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
