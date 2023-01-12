using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.DepartmentLead
{
    public partial class ProjectManagement
    {
        
        public IEnumerable<ProjectDTO> Projects = new List<ProjectDTO>();

        public IEnumerable<EmployeeDTO> Employees = new List<EmployeeDTO>();

        public IEnumerable<ProjectStatusDTO> ProjectStatuses = new List<ProjectStatusDTO>();

        [Inject]
        public IDepartmentLeadService DepartmentLeadService { get; set; }        
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Projects = (await DepartmentLeadService.GetProjectsAsync()).Where(x => x.ProjectId != 51).ToList();
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            ProjectStatuses = (await DepartmentLeadService.GetProjectStatusesAsync()).ToList(); 
        }
    }
}
