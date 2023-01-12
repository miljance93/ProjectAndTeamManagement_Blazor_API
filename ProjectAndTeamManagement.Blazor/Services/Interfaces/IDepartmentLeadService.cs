using Shared;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Services.Interfaces
{
    public interface IDepartmentLeadService
    {
        Task<IEnumerable<ProjectDTO>> GetProjectsAsync();
        Task<IEnumerable<ProjectStatusDTO>> GetProjectStatusesAsync();
        Task<Result<bool>> CreateProjectAsync(ProjectDTO projectDTO);
    }
}
