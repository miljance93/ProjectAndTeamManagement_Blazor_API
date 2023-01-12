using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.ProjectLead
{
    public partial class CreateNewRequest
    {
        private RequestDTO _request = new RequestDTO();
        private EmployeeDTO ProjectLead = new EmployeeDTO();
        public IEnumerable<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        public IEnumerable<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
        public IEnumerable<RequestStatusDTO> RequestStatuses { get; set; } = new List<RequestStatusDTO>();  
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public IDepartmentLeadService ProjectService { get; set; }
        [Inject]
        public IRequestService RequestService { get; set; }
        [Inject]
        public IRequestStatusService RequestStatusService { get; set; } 
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var currentEmployee = (await EmployeeDataService.GetCurrentEmployee());
            ProjectLead = (await EmployeeDataService.GetEmployeeByEmail(currentEmployee.Email));
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList(); 
            Projects = (await ProjectService.GetProjectsAsync()).Where(x => x.ProjectLeadId == ProjectLead.Id).ToList();
            RequestStatuses = (await RequestStatusService.GetAllRequestStatuses()).ToList();
        }

        public async Task CreateRequest()
        {
            var leadId = await EmployeeDataService.GetEmployeeDetails(_request.LeadId);
            if(leadId.RoleId == "3") 
                _request.TeamLeadId = leadId.Id;
            
            if(leadId.RoleId == "2")
                _request.DepartmentLeadId = leadId.Id;

            _request.ProjectLeadId = ProjectLead.Id;
            var result = await RequestService.CreateRequest(_request);
            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/", true);
            }
            else
            {
                NavigationManager.NavigateTo("/projectlead/createnewrequest", true);
            }
        }
    }
}
