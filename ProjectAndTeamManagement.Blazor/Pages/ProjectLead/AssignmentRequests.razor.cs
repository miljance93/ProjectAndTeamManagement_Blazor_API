using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.ProjectLead
{
    public partial class AssignmentRequests
    {
        bool createNewRequest = false;
        public IEnumerable<RequestDTO> Requests { get; set; } = new List<RequestDTO>();
        public IEnumerable<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        public IEnumerable<RequestStatusDTO> RequestStatuses { get; set; } = new List<RequestStatusDTO>();  
        public ProjectDTO Project { get; set; } = new ProjectDTO();
        [Inject]
        public IRequestService RequestService { get; set; }
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; } 
        [Inject]
        public IDepartmentLeadService ProjectService { get; set; }
        [Inject]
        public IRequestStatusService RequestStatusService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var employee = await EmployeeDataService.GetCurrentEmployee();
            var projectLead = await EmployeeDataService.GetEmployeeByEmail(employee.Email);

            Requests = (await RequestService.GetAllRequestsAsync()).Where(x => x.ProjectLeadId == employee.Id).ToList();
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            Project = (await ProjectService.GetProjectsAsync()).FirstOrDefault(x => x.ProjectId == projectLead.ProjectId);
            RequestStatuses = (await RequestStatusService.GetAllRequestStatuses()).ToList();
        }
    }
}
