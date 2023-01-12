using Shared.DTOs;
using System.Net.Http.Headers;

namespace ManagementApp.Blazor.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDto> Register(UserForRegistrationDto user);
        Task<AuthResponseDto> Login(UserForAuthenticationDto user);
        Task Logout();
    }
}
