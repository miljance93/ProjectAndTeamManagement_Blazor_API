using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.TeamLead
{
    public partial class CurrentAssignments
    {
        public IEnumerable<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        public IEnumerable<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        public IEnumerable<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public ITeamService TeamService { get; set; }
        [Inject]
        public IDepartmentLeadService DepartmentLeadService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = await EmployeeDataService.GetCurrentEmployee();
            var teamLead = await EmployeeDataService.GetEmployeeByEmail(user.Email);

            Employees = (await EmployeeDataService.GetAllEmployees()).Where(x => x.TeamId == teamLead.TeamId).ToList();
            Teams = (await TeamService.GetAllTeams()).ToList();
            Projects = (await DepartmentLeadService.GetProjectsAsync()).ToList();
        }
    }
}
