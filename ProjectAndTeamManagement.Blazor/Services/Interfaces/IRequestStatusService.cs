using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Services.Interfaces
{
    public interface IRequestStatusService
    {
        Task<IEnumerable<RequestStatusDTO>> GetAllRequestStatuses();
    }
}
