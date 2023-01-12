using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.DepartmentLead.TeamManagement
{
    public partial class List
    {
        private bool createNewTeam = false;
        private string _employeeId = string.Empty;
        private IEnumerable<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        private IEnumerable<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        private IEnumerable<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
        [Inject]
        public ITeamService TeamService { get; set; }
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public IDepartmentLeadService DepartmentLeadService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = (await TeamService.GetAllTeams()).ToList();
            Employees = (await EmployeeDataService.GetAllEmployees()).Where(x => x.RoleId == "5" || x.RoleId == "4").ToList();
            Projects = (await DepartmentLeadService.GetProjectsAsync()).ToList(); 
        }

        public async Task AssignEmployeeToTeam(string employeeId)
        {
            _employeeId = employeeId;
            NavigationManager.NavigateTo($"DepartmentLead/TeamManagement/AssignToTeam/{_employeeId}");
        }

        public async Task RemoveFromTeam(string employeeId)
        {
            var employee = await EmployeeDataService.GetEmployeeDetails(employeeId);

            employee.TeamId = 1;

            await EmployeeDataService.UpdateEmployeeAsync(employee);
            NavigationManager.NavigateTo("/", true);
        }
    }
}
