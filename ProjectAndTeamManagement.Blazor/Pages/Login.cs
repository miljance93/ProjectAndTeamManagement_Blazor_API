using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using ManagementApp.Blazor.Services.Interfaces;

namespace ProjectAndTeamManagement.Blazor.Pages
{
    public partial class Login
    {
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }
        public async Task ExecuteLogin()
        {
            ShowAuthError = false;
            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.Message;
                ShowAuthError = true;
            }
            else
            {

                NavigationManager.NavigateTo("/", true);
            }
        }

        private void GoogleSignIn()
        {
            NavigationManager.NavigateTo("https://localhost:7147/api/account/googlesignin", true);
        }
    }
}
