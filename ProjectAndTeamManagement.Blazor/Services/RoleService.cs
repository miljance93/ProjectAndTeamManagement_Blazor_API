using Blazored.LocalStorage;
using ManagementApp.Blazor.Services.Interfaces;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared;
using Shared.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectAndTeamManagement.Blazor.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public RoleService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticationHeaderValue> AuthorizedClient()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("authToken");
            return _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<IEnumerable<RoleDTO>>>
              (await _httpClient.GetStreamAsync($"api/role"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result.Value;
        }
    }
}
