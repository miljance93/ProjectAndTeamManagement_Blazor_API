using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Employees
{
    public class SearchByEmail
    {
        public record Query(string Email) : IRequest<Result<EmployeeDTO>>;

        public class Handler : IRequestHandler<Query, Result<EmployeeDTO>>
        {
            private readonly IEmployeeRepository _employeeRepository;

            public Handler(IEmployeeRepository employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<Result<EmployeeDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _employeeRepository.GetByEmail(request.Email);

                if (result == null)
                    return new Result<EmployeeDTO> { IsSuccess = false, Error = "User not found" };

                return new Result<EmployeeDTO> { IsSuccess = true, Value = result };
            }
        }
    }
}
