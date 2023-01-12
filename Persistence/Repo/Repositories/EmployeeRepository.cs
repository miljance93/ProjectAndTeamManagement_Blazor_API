using AutoMapper;
using Domain.IdentityAuth;
using Microsoft.EntityFrameworkCore;
using Persistence.Repo.Interfaces;
using Shared.DTOs;

namespace Persistence.Repo.Repositories
{
    public class EmployeeRepository : Repository<ApplicationUser>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public async Task<EmployeeDTO> GetByEmail(string email)
        {
            var user =   _context.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());

            var employeeDTO = new EmployeeDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id,
                ProjectId = user.ProjectId,
                TeamId = user.TeamId,
                RoleId = user.RoleId,
            };

            return employeeDTO;

        }

        public async Task<bool> UpdateEmployee(EmployeeDTO employee)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == employee.Id);

            user.FirstName = employee.FirstName;
            user.LastName = employee.LastName;
            user.Email = employee.Email;
            user.RoleId = employee.RoleId;
            user.Id = employee.Id;
            user.ProjectId = employee.ProjectId;
            user.TeamId = employee.TeamId;

             _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
