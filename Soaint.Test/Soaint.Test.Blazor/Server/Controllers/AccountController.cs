using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soaint.Test.Blazor.Shared.Base;
using Soaint.Test.Blazor.Shared.DTOs;

namespace Soaint.Test.Blazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> CreateAccount([FromBody] RegisterDto request)
    {
        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email
        };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(s => s.Description);
            return Ok(new RegisterResponse { Succesful = false, Errors = errors });
        }

        return Ok(new RegisterResponse { Succesful = true });
    }
}