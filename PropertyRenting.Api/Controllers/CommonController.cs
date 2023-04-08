using Microsoft.AspNetCore.Mvc;
using PropertyRenting.Api.Enums;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class CommonController : ControllerBase
{
    [HttpGet("building-types")]
    public IActionResult GetBuildingTypes()
    {
        List<EnumVM> types = new();

        foreach (var item in Enum.GetValues<BuildingType>())
        {
            types.Add(new EnumVM { Id = (int)item, Description = item.ToString() });
        }
        return Ok(types);
    }

    [HttpGet("construction-statuses")]
    public IActionResult GetConstructionStatuses()
    {
        List<EnumVM> types = new();

        foreach (var item in Enum.GetValues<ConstructionStatus>())
        {
            types.Add(new EnumVM { Id = (int)item, Description = item.ToString() });
        }
        return Ok(types);
    }
}
