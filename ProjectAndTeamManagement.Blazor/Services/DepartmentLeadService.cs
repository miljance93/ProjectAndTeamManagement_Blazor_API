using Blazored.LocalStorage;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared;
using Shared.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProjectAndTeamManagement.Blazor.Services
{
    public class DepartmentLeadService : IDepartmentLeadService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public DepartmentLeadService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticationHeaderValue> AuthorizedClient()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("authToken");
            
            return _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        }

        public async Task<Result<bool>> CreateProjectAsync(ProjectDTO projectDTO)
        {
            await AuthorizedClient();

            var content = JsonSerializer.Serialize(projectDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _httpClient.PostAsync("api/departmentlead/createproject", bodyContent);

            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Result<bool>>(authContent);

            if (authResult.IsSuccessStatusCode)
            {
                return new Result<bool> { IsSuccess = true, Value = result.Value};
            }

            return new Result<bool> { IsSuccess = false, Error = result.Error };
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsAsync()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<IEnumerable<ProjectDTO>>>
              (await _httpClient.GetStreamAsync($"api/departmentlead/getallprojects"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result.Value;
        }

        public async Task<IEnumerable<ProjectStatusDTO>> GetProjectStatusesAsync()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<IEnumerable<ProjectStatusDTO>>>
              (await _httpClient.GetStreamAsync($"api/departmentlead/getallprojectstatuses"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result.Value;
        }
    }
}
