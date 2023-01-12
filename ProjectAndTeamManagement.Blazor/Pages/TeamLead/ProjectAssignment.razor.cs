using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.TeamLead
{
    public partial class ProjectAssignment
    {
        bool addToProject = false;
        string empId = string.Empty;


        private IEnumerable<EmployeeDTO> Employees = new List<EmployeeDTO>();
        private IEnumerable<ProjectDTO> Projects = new List<ProjectDTO>();
        private IEnumerable<TeamDTO> Teams = new List<TeamDTO>();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public IDepartmentLeadService DepartmentLeadService { get; set; }
        [Inject]
        public ITeamService TeamService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = await EmployeeDataService.GetCurrentEmployee();
            var teamLead = await EmployeeDataService.GetEmployeeByEmail(user.Email);
            Employees = (await EmployeeDataService.GetAllEmployees()).Where(x => x.TeamId == teamLead.TeamId && x.RoleId == "5").ToList();
            Projects = (await DepartmentLeadService.GetProjectsAsync()).ToList();
            Teams = (await TeamService.GetAllTeams());
        }

        public void AddToProject(string employeeId)
        {
            addToProject = true;
            empId = employeeId;
        }


        public async void RemoveFromProject(string employeeId)
        {
            var employee = await EmployeeDataService.GetEmployeeDetails(employeeId);

            employee.ProjectId = 46;

            await EmployeeDataService.UpdateEmployeeAsync(employee);

            NavigationManager.NavigateTo("/teamlead/projectassignment", true);
        }
    }
}
