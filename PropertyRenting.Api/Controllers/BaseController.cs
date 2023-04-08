using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyRenting.Api.Models.Contexts;

namespace PropertyRenting.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[Controller]")]
public class BaseController : ControllerBase
{

    public BaseController(AppDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    protected AppDbContext Context { get; }
    protected IMapper Mapper { get; }
}
