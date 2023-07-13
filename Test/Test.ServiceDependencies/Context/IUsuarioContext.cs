using Test.Shared.Dtos;

namespace Test.ServiceDependencies.Context
{
    public interface IUsuarioContext
    {
        Task<dynamic> Login(Login login);
        Task<ResponseData> NuevoUsuario(NuevoUsuario usuario);
    }
}
