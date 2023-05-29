using Test.Shared.Models;

namespace Test.Server.Lib.JWT
{
    public interface IJWT
    {
        /// <summary>
        /// Generates a token for the current user.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>The token for the current user.</returns>
        string GenerateToken(User user);
        string GetUserIdFromToken(string token);
    }
}
