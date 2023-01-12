using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Requests
{
    public class Update
    {
        public record Command(RequestDTO Request) : IRequest<Result<bool>>;

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IRequestRepository _requestRepository;

            public Handler(IRequestRepository requestRepository)
            {
                _requestRepository = requestRepository;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _requestRepository.GetByIdAsync<RequestDTO>(x => x.RequestId == request.Request.RequestId);
                if (result != null)
                {
                    result.ProjectLeadId = request.Request.ProjectLeadId;
                    result.ProjectId = request.Request.ProjectId;
                    result.EmployeeId = request.Request.EmployeeId;
                    result.StartDate = request.Request.StartDate;
                    result.EndDate = request.Request.EndDate;
                    result.DepartmentLeadId = request.Request.DepartmentLeadId;
                    result.RequestStatusId = request.Request.RequestStatusId;
                    result.TeamLeadId = request.Request.TeamLeadId;
                    
                    var updatedRequest = await _requestRepository.UpdateAsync(result);

                    return new Result<bool> { IsSuccess = true, Value = updatedRequest };

                }

                return new Result<bool> { IsSuccess = false, Error = "Request is not updated!" };

            }
        }
    }
}
