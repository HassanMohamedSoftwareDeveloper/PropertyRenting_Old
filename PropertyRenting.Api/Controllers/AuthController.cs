// Ignore Spelling: Auth Accessor

using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Services.Token;

namespace PropertyRenting.Api.Controllers;
[ApiController]
[Route("api/v1/[Controller]")]
public class AuthController : ControllerBase
{
    #region Fields :
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJWTTokenGenerator _tokenGenerator;
    private readonly DapperContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion

    #region CTORS :
    public AuthController(UserManager<IdentityUser> userManager,
                  SignInManager<IdentityUser> signInManager,
                  IJWTTokenGenerator tokenGenerator,
                  DapperContext context,
                  IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenGenerator = tokenGenerator;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    #region Actions :
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginVM model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        user ??= await _userManager.FindByEmailAsync(model.Username);

        if (user is null) return BadRequest();

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (result.Succeeded is false) return BadRequest();

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            Result = result,
            Username = user.UserName,
            Email = user.Email,
            Token = _tokenGenerator.GenerateToken(user, roles)
        });
    }

    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var query = "SELECT AspNetUsers.Id,AspNetUsers.UserName AS 'Username',AspNetUsers.Email,AspNetRoles.Id AS 'RoleId',AspNetRoles.Name AS 'Role' FROM AspNetUsers JOIN AspNetUserRoles ON AspNetUsers.Id = AspNetUserRoles.UserId JOIN AspNetRoles ON AspNetUserRoles.RoleId=AspNetRoles.Id";
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<UserDTO>(query);
        return Ok(result.ToList());
    }
    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterAsync(RegisterVM model)
    {
        var userToCreate = new IdentityUser
        {
            Email = model.Email,
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(userToCreate, model.Password);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(userToCreate.UserName);
            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok(result);
        }

        return BadRequest(result);
    }
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordVM request)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.UserId();
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return NotFound();

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (result.Succeeded) return Ok();
        return BadRequest(result.Errors.Select(x => x.Description).ToList());

    }
    #endregion
}
