using Blazored.LocalStorage;
using ManagementApp.Blazor.Services.Interfaces;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared;
using Shared.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectAndTeamManagement.Blazor.Services
{
    public class RequestStatusService : IRequestStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public RequestStatusService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticationHeaderValue> AuthorizedClient()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("authToken");
            return _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        }

        public async Task<IEnumerable<RequestStatusDTO>> GetAllRequestStatuses()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<IEnumerable<RequestStatusDTO>>>
             (await _httpClient.GetStreamAsync($"api/request/getrequeststatuses"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result.Value;
        }
    }
}
