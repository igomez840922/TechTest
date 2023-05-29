using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Server.Config;
using Test.Shared.Models;

namespace Test.Server.Lib.JWT
{
    public class JWT : IJWT
    {
        private const string ID = "ID";
        private readonly IConfiguration configuration;

        public JWT(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JWTConfig jwtConfig = configuration.GetSection(nameof(JWTConfig)).Get<JWTConfig>();
            byte[] tokenKey = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ID, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Type.ToString()),
                }),
                Expires = DateTime.Now.AddDays(jwtConfig.Duration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetUserIdFromToken(string token)
        {
            if (token == null)
                return string.Empty;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(configuration.GetSection(nameof(JWTConfig)).Get<JWTConfig>().Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                string userId = jwtToken.Claims.First(x => x.Type == ID).Value;

                return userId;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
