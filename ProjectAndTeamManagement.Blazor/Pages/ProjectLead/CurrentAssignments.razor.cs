using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.ProjectLead
{
    public partial class CurrentAssignments
    {
        private EmployeeDTO ProjectLead = new EmployeeDTO();
        public IEnumerable<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        public IEnumerable<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
        public IEnumerable<TeamDTO> Teams { get; set; } = new List<TeamDTO>();  
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public IDepartmentLeadService ProjectService { get; set; }
        [Inject]
        public ITeamService TeamService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var currentEmployee = (await EmployeeDataService.GetCurrentEmployee());
            ProjectLead = (await EmployeeDataService.GetEmployeeByEmail(currentEmployee.Email));
            Employees = (await EmployeeDataService.GetAllEmployees()).Where(x => x.ProjectId == ProjectLead.ProjectId).ToList();
            Projects = (await ProjectService.GetProjectsAsync()).ToList();
            Teams = (await TeamService.GetAllTeams()).ToList();
        }
    }
}
