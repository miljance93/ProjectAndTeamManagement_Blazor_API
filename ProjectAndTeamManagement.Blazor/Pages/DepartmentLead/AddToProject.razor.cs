using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.DepartmentLead
{
    public partial class AddToProject
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
        private EmployeeDTO _employee = new EmployeeDTO();
        public IEnumerable<ProjectDTO> Projects = new List<ProjectDTO>();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IDepartmentLeadService DepartmentLeadService { get; set; }
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Projects = (await DepartmentLeadService.GetProjectsAsync()).Where(x => x.ProjectId != 46).ToList();   
        }

        public async Task AddEmployeeToProject()
        {
            var employee = await EmployeeDataService.GetEmployeeDetails(Id);
            employee.ProjectId = _employee.ProjectId;

            var result = await EmployeeDataService.UpdateEmployeeAsync(employee);
            NavigationManager.NavigateTo("departmentlead/projectassignment", true);
        }
    }
}
