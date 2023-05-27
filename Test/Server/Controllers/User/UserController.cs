using Microsoft.AspNetCore.Mvc;
using Test.Server.Repositories;
using Test.Shared.DTOS;

namespace Test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("usertypes")]
        public async Task<ActionResult<List<Shared.Models.Type>>> GetUserTypesAsync()
        {
            List<Shared.Models.Type> userTypes = new List<Shared.Models.Type>();

            try
            {
                userTypes = await _userRepository.GetUserTypesAsync();
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status200OK, userTypes);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            UserDTO existingUser = await _userRepository.GetUserByEmailAsync(registerUserDTO.Email);
            
            if(existingUser.ID != 0)
            {
                return StatusCode(StatusCodes.Status409Conflict, new UserDTO());
            }

            UserDTO createdUser = await _userRepository.RegisterUserAsync(registerUserDTO);
            return StatusCode(StatusCodes.Status201Created, createdUser);
        }
    }
}
