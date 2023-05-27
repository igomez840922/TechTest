using Test.Shared.DTOS;
using Test.Shared.Models;
using Type = Test.Shared.Models.Type;

namespace Test.Server.Repositories
{
    public interface IUserRepository
    {

        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>The user types.</returns>
        Task<List<Type>> GetUserTypesAsync();

        /// <summary>
        /// Registers a user in the database.
        /// </summary>
        /// <param name="registerUserDTO">The user data.</param>
        /// <returns>The created user.</returns>
        Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDTO);

        /// <summary>
        /// Provides a user with a token that can be used to interact with the application.
        /// </summary>
        /// <param name="loginUserDTO">The user login information.</param>
        /// <returns>The loged user.</returns>
        Task<UserDTO> LoginUserAsync(LoginUserDTO loginUserDTO);

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>Returns the user if found. Empty if not.</returns>
        Task<UserDTO> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="email">The user ID.</param>
        /// <returns>Returns the user if found. Empty if not.</returns>
        Task<UserDTO> GetUserByIDAsync(long ID);
    }
}
