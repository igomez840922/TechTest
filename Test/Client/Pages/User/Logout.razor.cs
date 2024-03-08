using Microsoft.AspNetCore.Components;
using Test.Client.Interfaces;

namespace Test.Client.Pages.User
{
    public partial class Logout
    {
        [Inject]
        private IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private bool ShowAuthError { get; set; }

        private string? Error { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowAuthError = false;

            var result = await AuthenticationService?.Logout();

            if (!result)
            {
                Error = "Something went wrong";
                ShowAuthError = true;
            }
            else
            {
                NavigationManager?.NavigateTo("/");
            }
        }
    }
}
