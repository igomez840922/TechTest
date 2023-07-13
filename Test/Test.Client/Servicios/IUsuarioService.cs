using Test.Shared.Dtos;

namespace Test.Client.Servicios
{
    public interface IUsuarioService
    {
        Task<dynamic> Login(Login login);
        Task<ResponseData> NuevoUsuario(NuevoUsuario usuario);
    }
}
