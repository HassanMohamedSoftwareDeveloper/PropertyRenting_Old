using Microsoft.AspNetCore.Identity;

namespace PropertyRenting.Api.Services.Token;

public interface IJWTTokenGenerator
{
    string GenerateToken(IdentityUser user, IList<string> roles);
}
