using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.TeamLead
{
    public partial class AssignmentRequests
    {
        public IEnumerable<RequestDTO> Requests { get; set; } = new List<RequestDTO>();
        public IEnumerable<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        public IEnumerable<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
        public IEnumerable<RequestStatusDTO> RequestStatuses { get; set; } = new List<RequestStatusDTO>();
        [Inject]
        public IRequestService RequestService { get; set; }
        [Inject]
        public IDepartmentLeadService DepartmentLeadService { get; set; }
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public IRequestStatusService RequestStatusService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var employee = await EmployeeDataService.GetCurrentEmployee();
            var teamLead = await EmployeeDataService.GetEmployeeByEmail(employee.Email);

            Requests = (await RequestService.GetAllRequestsAsync()).Where(x => x.TeamLeadId == teamLead.Id).ToList();
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            Projects = (await DepartmentLeadService.GetProjectsAsync()).ToList();
            RequestStatuses = (await RequestStatusService.GetAllRequestStatuses()).ToList();
        }

        public async Task ApproveRequest(int id)
        {
            var request = (await RequestService.GetAllRequestsAsync()).FirstOrDefault(x => x.RequestId == id);
            request.RequestStatusId = 2;
            var updateRequest = (await RequestService.UpdateRequest(request));

            var employee = (await EmployeeDataService.GetEmployeeDetails(request.EmployeeId));
            employee.ProjectId = request.ProjectId;
            var updateEmployee = (await EmployeeDataService.UpdateEmployeeAsync(employee));

            NavigationManager.NavigateTo("/", true);
        }

        public async Task DeclineRequest(int id)
        {
            var request = (await RequestService.GetAllRequestsAsync()).FirstOrDefault(x => x.RequestId == id);
            request.RequestStatusId = 3;
            var result = (await RequestService.UpdateRequest(request));

            NavigationManager.NavigateTo("/", true);
        }
    }
}
