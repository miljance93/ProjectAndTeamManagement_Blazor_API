using Blazored.LocalStorage;
using ManagementApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared;
using Shared.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProjectAndTeamManagement.Blazor.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public EmployeeDataService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticationHeaderValue> AuthorizedClient()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("authToken");
            return _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<IEnumerable<EmployeeDTO>>>
              (await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            
            return result.Value;
        }

        public async Task<EmployeeDTO> GetCurrentEmployee()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<EmployeeDTO>
                (await _httpClient.GetStreamAsync("api/employee/currentuser"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<EmployeeDTO> GetEmployeeDetails(string employeeId)
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<EmployeeDTO>>
                (await _httpClient.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return result.Value;
        }

        public async Task<EmployeeDTO> GetEmployeeByEmail(string email)
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<EmployeeDTO>>
                (await _httpClient.GetStreamAsync($"api/employee/email/{email}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return result.Value;
        }

        public async Task<Result<bool>> UpdateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var content = JsonSerializer.Serialize(employeeDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _httpClient.PutAsync("api/employee", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result =  JsonSerializer.Deserialize<Result<bool>>(authContent);

            if (authResult.IsSuccessStatusCode)
            {
                return new Result<bool> { IsSuccess = true, Value = result.Value };
            }

            return new Result<bool> { IsSuccess = false, Error = result.Error };
        }
    }
}
