using Microsoft.AspNetCore.Components;
using Test.Client.Interfaces;
using Test.Shared.Entities.DTO;

namespace Test.Client.Pages.User
{
    public partial class Login
    {
        private UserAuthenticationRequest _loginModel = new UserAuthenticationRequest();

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool ShowAuthError { get; set; }

        private string? Error { get; set; }

        private async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(_loginModel);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
