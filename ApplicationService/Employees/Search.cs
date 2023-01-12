using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Employees
{
    public class Search
    {
        public record Query(string Id) : IRequest<Result<EmployeeDTO>>;

        public class Handler : IRequestHandler<Query, Result<EmployeeDTO>>
        {
            private readonly IEmployeeRepository _employeeRepository;

            public Handler(IEmployeeRepository employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<Result<EmployeeDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var result =  await _employeeRepository.GetByIdAsync<EmployeeDTO>(x => x.Id == request.Id);
                if (result != null)
                {
                    return new Result<EmployeeDTO> { IsSuccess = true, Value = result };
                }

                return new Result<EmployeeDTO> { IsSuccess = false, Error = "Employee not found " };
            }
        }
    }
}
