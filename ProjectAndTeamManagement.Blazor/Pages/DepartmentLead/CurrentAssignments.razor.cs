using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.DepartmentLead
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
            Employees = (await EmployeeDataService.GetAllEmployees()).Where(x => x.RoleId != "1" && x.RoleId != "2").ToList();
            Teams = (await TeamService.GetAllTeams()).ToList();
            Projects = (await DepartmentLeadService.GetProjectsAsync()).ToList();   
        }
    }
}
