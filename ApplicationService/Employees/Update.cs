using Domain.IdentityAuth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Employees
{
    public class Update
    {
        public record Command(EmployeeDTO Employee) : IRequest<Result<bool>>;

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(IEmployeeRepository employeeRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _employeeRepository = employeeRepository;
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _employeeRepository.UpdateEmployee(request.Employee);

                    var role = await _roleManager.FindByIdAsync(request.Employee.RoleId);
                    var roleName = await _roleManager.GetRoleNameAsync(role);

                    var applicationUser = new ApplicationUser
                    {
                        RoleId = request.Employee.RoleId,
                    };

                    await _userManager.AddToRoleAsync(applicationUser, roleName);
                    return new Result<bool> { IsSuccess = true, Value = result };
                }
                catch (Exception)
                {
                    return new Result<bool> { IsSuccess = false, Error = "User is not updated!" };
                }
            }
        }
    }
}
