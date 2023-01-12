using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Requests
{
    public class List
    {
        public record Query : IRequest<Result<IEnumerable<RequestDTO>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<RequestDTO>>>
        {
            private readonly IRequestRepository _requestRepository;

            public Handler(IRequestRepository requestRepository)
            {
                _requestRepository = requestRepository;
            }
            public async Task<Result<IEnumerable<RequestDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _requestRepository.GetAllAsync<RequestDTO>();
                if (result != null)
                {
                    return new Result<IEnumerable<RequestDTO>> { IsSuccess = true, Value = result };
                }

                return new Result<IEnumerable<RequestDTO>> { IsSuccess = false, Error = "Requests not found!" };
            }
        }
    }
}
