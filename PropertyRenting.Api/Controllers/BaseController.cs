using Microsoft.AspNetCore.Authorization;
using PropertyRenting.Api.Services;

namespace PropertyRenting.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[Controller]")]
public class BaseController : ControllerBase
{

    #region CTORS :
    public BaseController(AppDbContext context, IMapper mapper, ICacheService cacheService = default)
    {
        Context = context;
        Mapper = mapper;
        CacheService = cacheService;
    }
    #endregion

    #region PROPS :
    protected AppDbContext Context { get; }
    protected IMapper Mapper { get; }
    public ICacheService CacheService { get; }
    #endregion

}
