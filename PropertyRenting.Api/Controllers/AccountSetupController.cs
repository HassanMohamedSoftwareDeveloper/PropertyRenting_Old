namespace PropertyRenting.Api.Controllers;

public class AccountSetupController : BaseController
{
    public AccountSetupController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var entity = await Context.AccountSetups.AsNoTracking().ProjectTo<AccountSetupDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return Ok(entity);
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(AccountSetupDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<AccountSetupEntity>(entityDTO);
        await Context.AccountSetups.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{entityDTO.Id}", entityDTO);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, AccountSetupDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.AccountSetups.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        currentEntity = Mapper.Map(entityDTO, currentEntity);

        Context.AccountSetups.Update(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
