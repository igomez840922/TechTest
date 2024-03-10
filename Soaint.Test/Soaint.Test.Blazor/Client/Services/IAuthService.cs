using Soaint.Test.Blazor.Shared.Base;
using Soaint.Test.Blazor.Shared.DTOs;

namespace Soaint.Test.Blazor.Client.Services;

public interface IAuthService
{
    Task<RegisterResponse> Register(RegisterDto registerDto);
    Task<LoginResponse> Login(LoginDto loginDto);
    Task LogOut();
}