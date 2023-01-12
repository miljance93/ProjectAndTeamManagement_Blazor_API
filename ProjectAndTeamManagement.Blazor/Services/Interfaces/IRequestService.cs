using Shared;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Services.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestDTO>> GetAllRequestsAsync();
        Task<Result<bool>> CreateRequest(RequestDTO request);
        Task<Result<bool>> UpdateRequest(RequestDTO request);
    }
}
