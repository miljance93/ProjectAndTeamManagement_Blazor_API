using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.Projects
{
    public class List
    {
        public record Query : IRequest<Result<IEnumerable<ProjectDTO>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<ProjectDTO>>>
        {
            private readonly IProjectRepository _projectRepository;

            public Handler(IProjectRepository projectRepository)
            {
                _projectRepository = projectRepository;
            }
            public async Task<Result<IEnumerable<ProjectDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await  _projectRepository.GetAllAsync<ProjectDTO>();

                if (result != null)
                {
                    return new Result<IEnumerable<ProjectDTO>> { IsSuccess = true, Value = result };
                }

                return new Result<IEnumerable<ProjectDTO>> { IsSuccess = false, Error = "No projects found" };
            }
        }
    }
}
