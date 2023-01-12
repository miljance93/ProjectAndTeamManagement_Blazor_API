using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.DepartmentLead.TeamManagement
{
    public partial class AssignToTeam
    {
        [Parameter]
        public string EmployeeId { get; set; }
        private TeamDTO _team = new TeamDTO();
        public IEnumerable<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        [Inject]
        public ITeamService TeamService { get; set; }
        [Inject] IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = (await TeamService.GetAllTeams()).Where(x => x.TeamId != 1).ToList();
        }

        public async Task AssignEmployeeToTeam()
        {
            var employee = (await EmployeeDataService.GetEmployeeDetails(EmployeeId));

            employee.TeamId = _team.TeamId;

            await EmployeeDataService.UpdateEmployeeAsync(employee);
            NavigationManager.NavigateTo("/", true);
        }
    }
}
