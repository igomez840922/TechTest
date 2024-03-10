using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Soaint.Test.Blazor.Shared.Base;
using Soaint.Test.Blazor.Shared.DTOs;

namespace Soaint.Test.Blazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginController(IConfiguration configuration, SignInManager<IdentityUser> signInManager)
    {
        _configuration = configuration;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email!, request.Password!, false, false);
        if (!result.Succeeded)
            return BadRequest(new LoginResponse { Succesful = false, Error = "Username and password invalid" });

        var claim = new[]
        {
            new Claim(ClaimTypes.Name, request.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtExpiryInMinutes"]));

        var token = new JwtSecurityToken(
            _configuration["JwtIssuer"],
            _configuration["JwtAudience"],
            claim,
            expires: expiry,
            signingCredentials: creds);

        return Ok(new LoginResponse { Succesful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}