using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Requests
{
    public class Create
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
                var result = await _requestRepository.CreateAsync(request.Request);

                if (result)
                {
                    return new Result<bool> { IsSuccess = true, Value = result };
                }

                return new Result<bool> { IsSuccess = false, Error = "Request not created!" };
            }
        }
    }
}
