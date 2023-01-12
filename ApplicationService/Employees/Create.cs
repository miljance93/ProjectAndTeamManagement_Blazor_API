using Domain.IdentityAuth;
using FluentValidation;
using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Employees
{
    public class Create
    {
        public record Command(EmployeeDTO Employee) : IRequest<Result<Task<bool>>>;

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor( x => x.Employee).SetValidator(new EmployeesValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Task<bool>>>
        {
            private readonly IEmployeeRepository _employeeRepository;

            public Handler(IEmployeeRepository employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<Result<Task<bool>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var applicationUser = new ApplicationUser
                {
                    FirstName = request.Employee.FirstName,
                    LastName = request.Employee.LastName,
                };

                var result = _employeeRepository.CreateAsync(applicationUser);

                return new Result<Task<bool>> { IsSuccess = true, Value = result };
            }
        }
    }
}
