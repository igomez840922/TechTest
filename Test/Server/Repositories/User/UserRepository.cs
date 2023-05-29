using Microsoft.EntityFrameworkCore;
using BcryptNet = BCrypt.Net.BCrypt;
using Test.Shared.Contexts;
using Test.Shared.DTOS;
using Test.Shared.Extensions;
using Test.Shared.Models;
using Test.Server.Lib.JWT;

namespace Test.Server.Repositories
{
    /// <summary>
    /// Class to interact with the user data.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ProductRegistryContext _db;
        private readonly IJWT _jwt;

        public UserRepository(ProductRegistryContext db, IJWT jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            User? existingUser = await _db.User.Where(u => u.Email == email).FirstOrDefaultAsync();
            return existingUser != null ? existingUser.UserAsDTO() : new UserDTO();
        }

        public async Task<UserDTO> GetUserByIDAsync(long ID)
        {
            User? existingUser = await _db.User.Where(u => u.ID == ID).FirstOrDefaultAsync();
            return existingUser != null ? existingUser.UserAsDTO() : new UserDTO();
        }

        public async Task<List<Shared.Models.Type>> GetUserTypesAsync()
        {
            List<Shared.Models.Type> userTypes = await _db.Type.AsNoTracking().ToListAsync();
            return userTypes;
        }

        public async Task<UserDTO> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            User? existingUser = await _db.User.Where(u => u.Email == loginUserDTO.Email).FirstOrDefaultAsync();

            if(existingUser == null) { 
                return new UserDTO();
            }

            if(BcryptNet.Verify(loginUserDTO.Password, existingUser.Password))
            {
                return new UserDTO()
                {
                    Email = existingUser.Email,
                    ID = existingUser.ID,
                    Type = existingUser.Type,
                    Username = existingUser.Username,
                    Token = _jwt.GenerateToken(existingUser)
                };
            }

            return new UserDTO();
        }

        public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            User? existingUser = await _db.User.Where(u => u.Email == registerUserDTO.Email).FirstOrDefaultAsync();

            if(existingUser != null && existingUser.ID != 0)
            {
                return new UserDTO();
            }

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
