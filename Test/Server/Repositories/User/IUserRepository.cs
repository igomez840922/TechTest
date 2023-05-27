using Test.Shared.DTOS;
using Test.Shared.Models;
using Type = Test.Shared.Models.Type;

namespace Test.Server.Repositories
{
    public interface IUserRepository
    {
        Task<List<Type>> GetUserTypesAsync();
        Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<UserDTO> GetUserByIDAsync(long ID);
    }
}
