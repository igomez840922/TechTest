using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Shared.DTO;

namespace Test.Server.Controllers
{
    [ApiController]
    [Route("api/UserLogs")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDTO>> CreateUser(UserRegisterInfo user)
        {
            var userCreate = new IdentityUser { UserName = user.Email, Email = user.Email };
            var Result = await userManager.CreateAsync(userCreate, user.Password);

            if (Result.Succeeded)
            {
                return await TokenBuilder(user);
            }
            else
            {
                return BadRequest(Result.Errors.First());
            }



        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDTO>> Login([FromBody] UserRegisterInfo user)
        {
            var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await TokenBuilder(user);
            }
            else
            {
                return BadRequest("Credenciales invalidas");
            }
        }

        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("renovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TokenDTO>> Renovar()
        {
            var userInfo = new UserRegisterInfo()
            {
                Email = HttpContext.User.Identity!.Name!
            };

            return await TokenBuilder(userInfo);
        }

        private async Task<TokenDTO> TokenBuilder(UserRegisterInfo userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Email),

                new Claim("miValor", "Lo que yo quiera")
            };

            var usuario = await userManager.FindByEmailAsync(userInfo.Email);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(1);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                tokenExpiredDate = expiration
            };
        }
    }
}
