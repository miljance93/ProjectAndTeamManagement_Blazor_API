using Shared;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDTO>> GetAllTeams();
        Task<Result<bool>> CreateTeam(TeamDTO team);
    }
}
