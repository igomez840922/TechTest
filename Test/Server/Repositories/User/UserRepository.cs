using Microsoft.EntityFrameworkCore;
using BcryptNet = BCrypt.Net.BCrypt;
using Test.Shared.Contexts;
using Test.Shared.DTOS;
using Test.Shared.Extensions;
using Test.Shared.Models;

namespace Test.Server.Repositories
{
    /// <summary>
    /// Class to interact with the user data.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ProductRegistryContext _db;

        public UserRepository(ProductRegistryContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>Returns the user if found. Empty if not.</returns>
        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            User? existingUser = await _db.User.Where(u => u.Email == email).FirstOrDefaultAsync();
            return existingUser != null ? existingUser.UserAsDTO() : new UserDTO();
        }

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="email">The user ID.</param>
        /// <returns>Returns the user if found. Empty if not.</returns>
        public async Task<UserDTO> GetUserByIDAsync(long ID)
        {
            User? existingUser = await _db.User.Where(u => u.ID == ID).FirstOrDefaultAsync();
            return existingUser != null ? existingUser.UserAsDTO() : new UserDTO();
        }

        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>The user types.</returns>
        public async Task<List<Shared.Models.Type>> GetUserTypesAsync()
        {
            List<Shared.Models.Type> userTypes = await _db.Type.AsNoTracking().ToListAsync();
            return userTypes;
        }

        /// <summary>
        /// Registers a user in the database.
        /// </summary>
        /// <param name="registerUserDTO">The user data.</param>
        /// <returns>The created user.</returns>
        public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            User newUser = new User
            {
                Username = registerUserDTO.Username,
                Email = registerUserDTO.Email,
                Password = BcryptNet.HashPassword(registerUserDTO.Password),
                Type = registerUserDTO.Type.ID,
                CreatedAt = DateTime.Now,
            };

            await _db.User.AddAsync(newUser);
            await _db.SaveChangesAsync();
            return newUser.UserAsDTO();
        }
    }
}
