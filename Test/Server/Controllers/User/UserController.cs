using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Server.Repositories;
using Test.Shared.DTOS;
using Type = Test.Shared.Models.Type;

namespace Test.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet("usertypes")]
        public async Task<ActionResult<List<Type>>> GetUserTypesAsync()
        {
            List<Type> userTypes = new List<Type>();

            try
            {
                userTypes = await _userRepository.GetUserTypesAsync();
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status200OK, userTypes);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            try
            {
                UserDTO createdUser = await _userRepository.RegisterUserAsync(registerUserDTO);
                if(createdUser.ID == 0)
                {
                    return StatusCode(StatusCodes.Status409Conflict, new UserDTO());
                }

            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status201Created, await _userRepository.LoginUserAsync
                (
                    new LoginUserDTO 
                    { 
                        Email = registerUserDTO.Email, 
                        Password = registerUserDTO.Password
                    }
                )
            );
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            UserDTO user = new UserDTO();

            try
            {
                user = await _userRepository.LoginUserAsync(loginUserDTO);
                if(user.ID == 0)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, user);
                }
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status200OK, user);
        }
    }
}
