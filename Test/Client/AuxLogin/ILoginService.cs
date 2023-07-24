using Test.Shared.DTO;

namespace Test.Client.AuxLogin
{
    public interface ILoginservice
    {
        Task Login(TokenDTO tokenDTO);
        Task Logout();
        Task ManejarRenovacionToken();
    }
}
