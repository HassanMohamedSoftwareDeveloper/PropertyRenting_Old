using Microsoft.AspNetCore.Authorization;

namespace PropertyRenting.Api.Controllers;

public class AccountController : BaseController
{

    public AccountController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {

        try
        {
            var data = await Context.Accounts//.AsNoTracking()
                                             //.Include(x => x.ParentAccount)
                                             //.Include(x => x.AccountChildren)
                .OrderBy(x => x.CreatedOnUtc)
               //.ProjectTo<AccountDTO>(Mapper.ConfigurationProvider)
               .ToListAsync();
            return Ok(Mapper.Map<List<AccountDTO>>(data));
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [AllowAnonymous]
    [HttpGet("list-grid")]
    public async Task<IActionResult> GetAllGridAsync()
    {

        try
        {
            var data = await Context
                .Accounts
                .Where(x => x.ParentId == null)
                .OrderBy(x => x.CreatedOnUtc)
                .ToListAsync();
            List<FlatAccountDto> final = new();
            foreach (var item in data)
            {
                final.Add(Mapper.Map<FlatAccountDto>(item));
                final.AddRange(GetChildrenAccounts(item));

            }
            return Ok(final);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    private List<FlatAccountDto> GetChildrenAccounts(AccountEntity account)
    {
        List<FlatAccountDto> final = new();
        bool hasChildren = account.AccountChildren != null && account.AccountChildren.Any();
        if (hasChildren)
        {
            foreach (var item in account.AccountChildren)
            {
                final.Add(Mapper.Map<FlatAccountDto>(item));
                var c = GetChildrenAccounts(item);
                final.AddRange(c);
            }
        }
        return final;
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Accounts
            .AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<AccountDTO>(Mapper.ConfigurationProvider)
            .Skip(skipped).Take(pageSize)
            .ToListAsync();

        int count = (await Context.Accounts.AsNoTracking().CountAsync());

        var result = new Pagination<AccountDTO>
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
        var entity = await Context.Accounts.AsNoTracking()
            .ProjectTo<AccountDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(AccountDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<AccountEntity>(entityDTO);
        if (mappedEntity.ParentId != null)
        {
            var parent = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == mappedEntity.ParentId);
            mappedEntity.Level = parent.Level + 1;
        }
        else
        {
            mappedEntity.Level = 1;
        }
        await Context.Accounts.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{entityDTO.Id}", entityDTO);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, AccountDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.Accounts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        currentEntity = Mapper.Map(entityDTO, currentEntity);
        if (currentEntity.ParentId != null)
        {
            var parent = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == currentEntity.ParentId);
            currentEntity.Level = parent.Level + 1;
        }
        else
        {
            currentEntity.Level = 1;
        }
        Context.Accounts.Update(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentEntity = (await Context.Accounts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        Context.Accounts.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }

}
