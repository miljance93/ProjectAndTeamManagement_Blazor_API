using ManagementApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public async Task ExecuteLogout()
        {
            await AuthenticationService.Logout();         
            NavigationManager.NavigateTo("/");            
        }
    }
}
