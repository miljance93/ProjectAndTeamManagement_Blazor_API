using Microsoft.AspNetCore.Components;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Pages.Admin
{
    public partial class EmployeeOverview
    {
        public IEnumerable<EmployeeDTO> Employees { get; set; }
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        protected async override Task OnInitializedAsync()
        {

            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }
    }
}
