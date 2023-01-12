using Blazored.LocalStorage;
using ManagementApp.Blazor.Services.Interfaces;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared;
using Shared.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProjectAndTeamManagement.Blazor.Services
{
    public class TeamService : ITeamService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public TeamService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticationHeaderValue> AuthorizedClient()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("authToken");
            return _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        }

        public async Task<IEnumerable<TeamDTO>> GetAllTeams()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<IEnumerable<TeamDTO>>>
              (await _httpClient.GetStreamAsync($"api/team"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result.Value;
        }

        public async Task<Result<bool>> CreateTeam(TeamDTO team)
        {
            await AuthorizedClient();

            var content = JsonSerializer.Serialize(team);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _httpClient.PostAsync("api/team", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Result<bool>>(authContent);

            if (authResult.IsSuccessStatusCode)
            {
                return new Result<bool> { IsSuccess = true, Value = result.Value };
            }

            return new Result<bool> { IsSuccess = false, Error = result.Error };
        }
    }
}
