using Microsoft.AspNetCore.Components;
using Test.Client.Interfaces;
using Test.Shared.Entities.DTO;

namespace Test.Client.Pages.User
{
    public partial class Register
    {
        private UserRegisterRequest _registerModel = new UserRegisterRequest();

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool ShowRegistrationErros { get; set; }

        private IEnumerable<string> Errors { get; set; } = new List<string>();

        private bool success { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErros = false;

            var result = await AuthenticationService.RegisterUser(_registerModel);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                success = true;
                NavigationManager.NavigateTo("/");
            }
        }

    }
}
