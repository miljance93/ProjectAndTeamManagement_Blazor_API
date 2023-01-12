
using Shared;
using Shared.DTOs;

namespace ProjectAndTeamManagement.Blazor.Services.Interfaces
{
    public interface IEmployeeDataService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeDetails(string employeeId);
        Task<Result<bool>> UpdateEmployeeAsync(EmployeeDTO employeeDTO);
        Task<EmployeeDTO> GetCurrentEmployee();
        Task<EmployeeDTO> GetEmployeeByEmail(string email);
    }
}
