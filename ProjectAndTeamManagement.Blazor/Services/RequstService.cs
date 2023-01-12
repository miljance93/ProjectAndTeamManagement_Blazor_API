using Blazored.LocalStorage;
using Shared;
using Shared.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProjectAndTeamManagement.Blazor.Services.Interfaces
{
    public class RequstService : IRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public RequstService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticationHeaderValue> AuthorizedClient()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("authToken");
            return _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        }

        public async Task<IEnumerable<RequestDTO>> GetAllRequestsAsync()
        {
            await AuthorizedClient();

            var result = await JsonSerializer.DeserializeAsync<Result<IEnumerable<RequestDTO>>>
              (await _httpClient.GetStreamAsync($"api/request"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result.Value;
        }
        public async Task<Result<bool>> UpdateRequest(RequestDTO request)
        {
            await AuthorizedClient();

            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _httpClient.PutAsync("api/request", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Result<bool>>(authContent);

            if (authResult.IsSuccessStatusCode)
            {
                return new Result<bool> { IsSuccess = true, Value = result.Value };
            }

            return new Result<bool> { IsSuccess = false, Error = result.Error };
        }

        public async Task<Result<bool>> CreateRequest(RequestDTO request)
        {
            await AuthorizedClient();

            request.RequestStatusId = 1;

            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _httpClient.PostAsync("api/request", bodyContent);
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
