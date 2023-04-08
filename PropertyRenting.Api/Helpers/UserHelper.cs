using System.Security.Claims;

namespace PropertyRenting.Api.Helpers;

public static class UserHelper
{
    public static string UserId(this ClaimsPrincipal @this)
    {
        return @this?.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
    }
}
