using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.JSInterop;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.DepartmentLead.TeamManagement
{
    public partial class CreateNewTeam
    {
        private TeamDTO _team = new TeamDTO();
        public IEnumerable<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public ITeamService TeamService { get; set; }
        public ILogger Logger { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Error { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).Where(x => x.RoleId == "5" || x.RoleId == "4").ToList();
        }

        public async Task CreateTeam()
        {
            var result = await TeamService.CreateTeam(_team);
            if (result.IsSuccess)
            {
                var employee = await EmployeeDataService.GetEmployeeDetails(_team.TeamLeadId);
                employee.RoleId = "3";

                await EmployeeDataService.UpdateEmployeeAsync(employee);
                NavigationManager.NavigateTo("/", true);
            }
            else
            {
                Error = result.Error;

            }
        }
    }
}
