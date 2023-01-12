using Domain.IdentityAuth;
using Shared.DTOs;

namespace Persistence.Repo.Interfaces
{
    public interface IEmployeeRepository : IRepository<ApplicationUser>
    {
        Task<bool> UpdateEmployee(EmployeeDTO employee);
        Task<EmployeeDTO> GetByEmail(string email);
    }
}
