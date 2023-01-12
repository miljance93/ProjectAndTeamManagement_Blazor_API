using Domain;
using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Teams
{
    public class List
    {
        public record Query : IRequest<Result<IEnumerable<TeamDTO>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<TeamDTO>>>
        {
            private readonly ITeamRepository _teamRepository;

            public Handler(ITeamRepository teamRepository)
            {
                _teamRepository = teamRepository;
            }

            public async Task<Result<IEnumerable<TeamDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result =  await _teamRepository.GetAllAsync<TeamDTO>();

                if (result != null)
                {
                    return new Result<IEnumerable<TeamDTO>> { IsSuccess = true, Value = result };
                }

                return new Result<IEnumerable<TeamDTO>> { IsSuccess = false, Error = "No teams found" };
            }
        }
    }
}
