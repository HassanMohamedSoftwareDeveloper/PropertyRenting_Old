using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers
{
    public class ContributorController : BaseController
    {

        #region CTORS :
        public ContributorController(AppDbContext context, IMapper mapper, ICacheService cacheService) : base(context, mapper, cacheService)
        {
        }
        #endregion

        #region Actions :
        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await Context.Contributers.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
                .ProjectTo<ContributorDTO>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(data);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetLookupAsync()
        {

            try
            {
                var data = await CacheService.GetOrCreateAsync(Constants.Constants.CacheKeys.Contributor.Lookup,
                () => Context.Contributers
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

            var data = await Context.Contributers.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<ContributorDTO>(Mapper.ConfigurationProvider)
              .Skip(skipped).Take(pageSize)
              .ToListAsync();
            int count = (await Context.Contributers.AsNoTracking().CountAsync());
            var result = new Pagination<ContributorDTO>
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
            var owner = await Context.Contributers.AsNoTracking()
                .ProjectTo<ContributorDTO>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(ContributorDTO contributor)
        {
            if (contributor == null) return BadRequest();
            contributor.Id = Guid.NewGuid();
            var mappedEntity = Mapper.Map<ContributerEntity>(contributor);
            await Context.Contributers.AddAsync(mappedEntity);

            bool saved = (await Context.SaveChangesAsync()) > 0;
            if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
            await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Contributor.Prefix);

            return Created($"~/byId/{contributor.Id}", contributor);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, ContributorDTO contributor)
        {
            if (contributor == null) return BadRequest();
            var currentContributor = (await Context.Contributers.FirstOrDefaultAsync(x => x.Id == id));
            if (currentContributor == null) return NotFound();
            currentContributor = Mapper.Map(contributor, currentContributor);

            Context.Contributers.Update(currentContributor);

            bool saved = (await Context.SaveChangesAsync()) > 0;
            if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
            await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Contributor.Prefix);

            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var currentContributor = (await Context.Contributers.FirstOrDefaultAsync(x => x.Id == id));
            if (currentContributor == null) return NotFound();

            Context.Contributers.Remove(currentContributor);

            bool saved = (await Context.SaveChangesAsync()) > 0;
            if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
            await CacheService.RemoveByPrefixAsync(Constants.Constants.CacheKeys.Contributor.Prefix);

            return Ok();
        }
        #endregion
    }
}
