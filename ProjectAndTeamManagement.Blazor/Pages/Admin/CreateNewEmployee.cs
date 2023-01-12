using ManagementApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.Admin
{
    public partial class CreateNewEmployee
    {
        private UserForRegistrationDto _userForRegistration = new UserForRegistrationDto();
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public IRoleService RoleService { get; set; }

        public string RoleId { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Roles = (await RoleService.GetAllRoles()).ToList();
        }

        public async Task ExecuteRegister()
        {
            ShowAuthError = false;
            var result = await AuthenticationService.Register(_userForRegistration);
            if (!result.IsAuthSuccessful)
            {
                Error = result.Message;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
