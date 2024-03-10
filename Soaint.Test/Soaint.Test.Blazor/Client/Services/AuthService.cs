using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Soaint.Test.Blazor.Client.Helper;
using Soaint.Test.Blazor.Shared.Base;
using Soaint.Test.Blazor.Shared.DTOs;

namespace Soaint.Test.Blazor.Client.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _stateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient client,
        AuthenticationStateProvider stateProvider,
        ILocalStorageService localStorage)
    {
        _client = client;
        _stateProvider = stateProvider;
        _localStorage = localStorage;
    }

    public async Task<RegisterResponse> Register(RegisterDto registerDto)
    {
        var result = await _client.PostAsJsonAsync("api/Account/CreateAccount", registerDto);

        return !result.IsSuccessStatusCode
            ? new RegisterResponse { Succesful = false, Errors = new List<string> { "Error" } }
            : new RegisterResponse { Succesful = true, Errors = new List<string> { "Account has ben created" } };
    }

    public async Task<LoginResponse> Login(LoginDto loginDto)
    {
        var login = JsonSerializer.Serialize(loginDto);
        var response =
            await _client.PostAsync("api/login", new StringContent(login, Encoding.UTF8, "application/json"));

        var result = JsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (!response.IsSuccessStatusCode)
            return result;

        await _localStorage.SetItemAsync("doToken", result!.Token);
        ((SoaintAuthStateProvider)_stateProvider).SetUserAsAuthenticated(loginDto.Email!);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

        return result;
    }

    public async Task LogOut()
    {
        await _localStorage.RemoveItemAsync("doToken");
        ((SoaintAuthStateProvider)_stateProvider).SetUserAsLoggedOut();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}