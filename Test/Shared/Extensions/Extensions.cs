using Test.Shared.DTOS;
using Test.Shared.Models;

namespace Test.Shared.Extensions
{
    public static class Extensions
    {
        public static UserDTO UserAsDTO(this User user)
        {
            return new UserDTO
            {
                ID = user.ID,
                Email = user.Email,
                Username = user.Username,
                Type = user.Type
            };
        }
    }
}
