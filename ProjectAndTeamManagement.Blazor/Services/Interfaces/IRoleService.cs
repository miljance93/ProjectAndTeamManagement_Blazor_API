using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRoles();
    }
}
