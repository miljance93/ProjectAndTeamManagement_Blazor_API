using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Teams
{
    public class Create
    {
        public record Command(TeamDTO Team) : IRequest<Result<bool>>;

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ITeamRepository _teamRepository;

            public Handler(ITeamRepository teamRepository)
            {
                _teamRepository = teamRepository;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var allTeams = await _teamRepository.GetAllAsync<TeamDTO>();
                var ifExists =  allTeams.Where(x => x.TeamName.ToLower() == request.Team.TeamName.ToLower()).ToList();

                if (ifExists.Count == 0)
                {
                    var result = await _teamRepository.CreateAsync(request.Team);
                    return new Result<bool> { IsSuccess = true, Value = result };
                }

                return new Result<bool> { IsSuccess = false, Error = "Team is not created!" };
            }
        }
    }
}
