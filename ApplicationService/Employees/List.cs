using Domain.IdentityAuth;
using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Employees
{
    public class List
    {
        public record Query : IRequest<Result<IEnumerable<EmployeeDTO>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<EmployeeDTO>>>
        {
            private readonly IEmployeeRepository _employeeRepository;

            public Handler(IEmployeeRepository employeeRepository)
            {
               _employeeRepository = employeeRepository;
            }
            public async Task<Result<IEnumerable<EmployeeDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var employees = await _employeeRepository.GetAllAsync<EmployeeDTO>();

                if (employees != null)
                {
                    return new Result<IEnumerable<EmployeeDTO>> { IsSuccess = true, Value = employees };
                }

                return new Result<IEnumerable<EmployeeDTO>> { IsSuccess = false, Error = "List of employees not found" };
            }
        }
    }
}
