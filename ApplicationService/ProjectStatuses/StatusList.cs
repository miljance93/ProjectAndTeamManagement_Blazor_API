using MediatR;
using Persistence.Repo.Interfaces;
using Shared;
using Shared.DTOs;

namespace ApplicationService.ProjectStatuses
{
    public class StatusList
    {
        public record Query : IRequest<Result<IEnumerable<ProjectStatusDTO>>>;

        public class Handler : IRequestHandler<Query, Result<IEnumerable<ProjectStatusDTO>>>
        {
            private readonly IProjectStatusRepository _projectStatusRepository;

            public Handler(IProjectStatusRepository projectStatusRepository)
            {
                _projectStatusRepository = projectStatusRepository;
            }
            public async Task<Result<IEnumerable<ProjectStatusDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projectStatuses = await _projectStatusRepository.GetAllAsync<ProjectStatusDTO>();

                if (projectStatuses != null)
                {
                    return new Result<IEnumerable<ProjectStatusDTO>> { IsSuccess = true, Value = projectStatuses };
                }

                return new Result<IEnumerable<ProjectStatusDTO>> { IsSuccess = false, Error = "Project Statuses not found" };
            }
        }
    }
}
