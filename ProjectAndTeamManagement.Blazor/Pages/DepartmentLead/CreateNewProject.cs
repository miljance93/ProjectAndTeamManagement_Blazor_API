using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.DepartmentLead
{
    public partial class CreateNewProject
    {
        private ProjectDTO _project = new();

        public IEnumerable<EmployeeDTO> Employees = new List<EmployeeDTO>();
        public IEnumerable<ProjectStatusDTO> ProjectStatuses = new List<ProjectStatusDTO>();
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public IDepartmentLeadService DepartmentLeadService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Error { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).Where(x => x.RoleId == "5" || x.RoleId == "4" && x.ProjectId == 51).ToList();
            ProjectStatuses = (await DepartmentLeadService.GetProjectStatusesAsync()).ToList();
        }

        public async Task CreateProject()
        {           

           var result = await DepartmentLeadService.CreateProjectAsync(_project);

            var employee = await EmployeeDataService.GetEmployeeDetails(_project.ProjectLeadId);
            employee.RoleId = "4";

            await EmployeeDataService.UpdateEmployeeAsync(employee);

            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/departmentlead/projectmanagement", true);
            }
            else
            {
                Error = result.Error;
                Console.WriteLine(result.Error);
            }
        }
    }
}
