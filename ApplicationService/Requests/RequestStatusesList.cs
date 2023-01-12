using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Requests
{
    public class RequestStatusesList
    {
        public record Query : IRequest<Result<IEnumerable<RequestStatusDTO>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<RequestStatusDTO>>>
        {
            private readonly IRequestStatusRepository _requestStatusRepository;

            public Handler(IRequestStatusRepository requestStatusRepository)
            {
                _requestStatusRepository = requestStatusRepository;
            }
            public async Task<Result<IEnumerable<RequestStatusDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _requestStatusRepository.GetAllAsync<RequestStatusDTO>();

                if (result != null)
                {
                    return new Result<IEnumerable<RequestStatusDTO>> { IsSuccess = true, Value = result };
                }
                return new Result<IEnumerable<RequestStatusDTO>> { IsSuccess = false, Error = "Status not found" };
            }
        }
    }
}
