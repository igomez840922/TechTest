using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Data.ContextSoaint;
using Test.Shared.Dtos;
using Test.Shared.Entities;
using Test.Shared.Utilidades;

namespace Test.ServiceDependencies.Context
{
    public class UsuarioContext : IUsuarioContext
    {
        private readonly SoaintContext _soaintContext;
        private readonly IMainSettings _mainSettings;
        private readonly CryptoService _crypto;

        public UsuarioContext(SoaintContext soaintContext, IMainSettings mainSettings)
        {
            _mainSettings = mainSettings;
            _crypto = new CryptoService();
            _soaintContext = soaintContext;
        }

        public async Task<dynamic> Login(Login login)
        {
            try
            {
                login.Password = _crypto.EncryptText(login.Password, _mainSettings.KeyCripto);
                var user = _soaintContext.Acceso.Where(x => x.Email.Equals(login.Email) && x.Password.Equals(login.Password)).First();
                if (user == null)
                {
                    return new ResponseData()
                    {
                        Code = 201,
                        Mensaje = "No existe un usuario para estas credenciales.",
                        Error = string.Empty
                    };
                }
                else
                {
                    var token = await ObtenerToken(user);
                    await ActualizarFechaLogueo(user);
                    return token;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al loguearse.", ex.Message);
            }
        }

        private async Task ActualizarFechaLogueo(Acceso user)
        {
            try
            {
                user.UltimoAcceso = DateTime.Now;
                await _soaintContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al actualizar fecha de logueo.", ex.Message);
            }
        }

        private async Task<TokenData> ObtenerToken(Acceso user)
        {
            try
            {
                var token = new TokenData();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_mainSettings.KeyToken));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                token.FechaExpiracion = DateTime.Now.AddMinutes(60);
                var claims = new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("id", $"{user.IdUsuario}"),
                    new Claim("fechaExpiracion", $"{token.FechaExpiracion}"),
                    new Claim("version", "v1.0")
                };

                var tokenResult = new JwtSecurityToken(
                    issuer: _mainSettings.Domain,
                    audience: _mainSettings.Domain,
                    claims,
                    expires: token.FechaExpiracion,
                    signingCredentials: credentials
                    );
                var encodetoken = new JwtSecurityTokenHandler().WriteToken(tokenResult);
                token.Token = encodetoken;

                return await Task.FromResult(token);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al generar el token.", ex.Message);
            }
        }

        public async Task<ResponseData> NuevoUsuario(NuevoUsuario usuario)
        {
            try
            {
                usuario.Password = _crypto.EncryptText(usuario.Password, _mainSettings.KeyCripto);
                await _soaintContext.Database.ExecuteSqlAsync($"sp_NuevoUsuario @IdTipoDocumento={usuario.IdTipoDocumento}, @NroIdentificacion={usuario.NroIdentificacion}, @Nombres={usuario.Nombres}, @Apellidos={usuario.Apellidos}, @Email={usuario.Email}, @Password={usuario.Password}");
                return new ResponseData()
                {
                    Code = 200,
                    Mensaje = "Registro procesado",
                    Error = string.Empty
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al Registrar al nuevo usuario.", ex.Message);
            }
        }
    }
}
